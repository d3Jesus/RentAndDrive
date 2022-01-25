using RentAndDrive.Models.Alugueres.Mdl;
using RentAndDrive.Models.Chart.Mdl;
using RentAndDrive.Models.Db;
using RentAndDrive.Models.Pagamentos.Mdl;
using RentAndDrive.Models.Pessoas.DAL;
using RentAndDrive.Models.Pessoas.Mdl;
using RentAndDrive.Models.Viaturas.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentAndDrive.Models.Alugueres.DAL
{
    public class DalAluguer
    {
        #region ID
        private static string gerarId()
        {
            string serie = "ALRD" + DateTime.Now.Year;
            DataDbContext ctx = new DataDbContext();
            string id = ctx.aluguers.Where(x => x.id.Contains(serie)).Max(x => x.id);
            if (id == null)
            {
                id = serie + "0000001";
            }
            else
            {
                int lengthSerie = serie.Length;
                string tempStr = "1" + id.Remove(0, lengthSerie).ToString();
                decimal tempDec = (Convert.ToDecimal(tempStr) + 1);
                id = serie + tempDec.ToString().Remove(0, 1);
            }
            return id;
        }
        #endregion

        #region ALUGUER
        public static List<VwAluguer> CarregarAluguers()
        {
            using (DataDbContext ctx = new DataDbContext())
                return ctx.vwAluguers.ToList();
        }

        public static List<VwAluguer> CarregarAluguersPeloEstado(string estado)
        {
            using (DataDbContext ctx = new DataDbContext())
                return ctx.vwAluguers.Where(s => s.estado == estado).ToList();
        }

        public static List<VwAluguer> CarregarAluguersPeloEstadoETipo(string estado, string tipo)
        {
            using (DataDbContext ctx = new DataDbContext())
                return ctx.vwAluguers.Where(s => s.estado == estado && s.tipo == tipo).ToList();
        }

        public static decimal CarregarTotalGanhoAnual()
        {
            using (DataDbContext ctx = new DataDbContext())
                return ctx.vwAluguers
                    .Where(s => s.estado != "Cancelado" && s.dataAluguer.Year == DateTime.Now.Year)
                    .ToList().Sum(s => s.valorPago);
        }
        
        public static Aluguer CarregarAluguerPeloId(string al)
        {
            using (DataDbContext ctx = new DataDbContext())
                return ctx.aluguers.Where(v => v.id == al).FirstOrDefault();
        }

        public static bool Registar(VwAluguerReserva aluguer, string tipo, DateTime data)
        {
            using(DataDbContext ctx = new DataDbContext())
                using(var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    #region CLIENTE
                    if(aluguer.idCliente == "" || aluguer.idCliente == null)
                    {
                        #region Morada
                        Morada m = new Morada()
                        {
                            rua = aluguer.rua,
                            avenida = aluguer.avenida,
                            quarteirao = aluguer.quarteirao
                        };
                        ctx.moradas.Add(m);
                        ctx.SaveChanges();
                        #endregion

                        #region Pessoa
                        Pessoa p = new Pessoa()
                        {
                            id = DalPessoa.GetId("Cliente"),
                            nome = aluguer.nome,
                            morada = m.id,
                            tipo = "Cliente",
                            sexo = aluguer.sexo,
                            estadoCivil = aluguer.estadoCivil,
                            funcao = "",
                            tipoDeIdentificacao = aluguer.tipoIdent,
                            numeroIdentificacao = aluguer.numeroIdent,
                            numeroCarta = aluguer.numeroCarta,
                            codigoCarta = aluguer.codigoCarta,
                            validadeCarta = aluguer.validadeCarta.GetValueOrDefault()
                        };
                        ctx.pessoas.Add(p);
                        ctx.SaveChanges();
                        aluguer.idCliente = p.id;
                        #endregion
                    }
                    #endregion

                    #region Aluguer
                    var al = new Aluguer();
                    al.id = gerarId();
                    al.idCliente = aluguer.idCliente;
                    al.IdFuncionario = aluguer.idFuncionario;
                    al.IdViatura = aluguer.idViatura;
                    al.IdMotorista = aluguer.idMotorista;
                    al.PeriodoAluguer = aluguer.periodo;
                    al.UnidadePeriodo = aluguer.unidade;
                    al.dataAluguer = data;
                    al.dataDevolucao = Convert.ToDateTime("1960-01-01");
                    al.estado = tipo.ToUpper() == "ALUGUER" ? "Activo" : "Reservado";
                    al.tipo = tipo;

                    ctx.aluguers.Add(al);
                    ctx.SaveChanges();
                    aluguer.idAluguer = al.id;
                    #endregion

                    decimal total = 0;
                    if (aluguer.periodo > 0)
                        if (aluguer.unidade != "0")
                        {
                            if (aluguer.unidade == "Dia")
                                total = aluguer.periodo * aluguer.valor;
                            if (aluguer.unidade == "Semana")
                                total = aluguer.periodo * 7 * aluguer.valor;
                            if (aluguer.unidade == "Mes")
                                total = aluguer.periodo * 30 * aluguer.valor;
                        }

                    Pagamento pagamento = new Pagamento()
                    {
                        data = DateTime.Now,
                        valorPago = total,
                        funcionario = aluguer.idFuncionario,
                        aluguer = aluguer.idAluguer
                    };
                    ctx.pagamentos.Add(pagamento);
                    ctx.SaveChanges();
                    tran.Commit();
                    return true;
                }
                catch(Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }

        public static bool Cancelar(string aluguer)
        {
            using(DataDbContext ctx = new DataDbContext())
            {
                var al = ctx.aluguers.Where(a => a.id == aluguer).FirstOrDefault();
                al.estado = "Cancelado";
                ctx.SaveChanges();
                return true;
            }
        }

        public static bool Devolver(string aluguer)
        {
            using(DataDbContext ctx = new DataDbContext())
            {
                var al = ctx.aluguers.Where(a => a.id == aluguer).FirstOrDefault();
                al.estado = "Terminado";
                al.dataDevolucao = DateTime.Now;
                DalViaturas.AlterarEstado(al.IdViatura, "Devolvido");
                ctx.SaveChanges();
                return true;
            }
        }

        #endregion
        #region RESERVAS
        public static List<VwAluguer> CarregarReservas()
        {
            using (DataDbContext ctx = new DataDbContext())
                return ctx.vwAluguers.Where(r => r.tipo == "RESERVA" && r.estado != "Cancelado").ToList();
        }
        public static VwAluguer CarregarReserva(string reserva)
        {
            using (DataDbContext ctx = new DataDbContext())
                return ctx.vwAluguers.Where(r => r.id == reserva).FirstOrDefault();
        }
        #endregion
        #region OUTROS
        public static List<ComparisonBarChart> FillBarChart(string ano)
        {
            string query = " SELECT " +
                "[JAN] = (SELECT COUNT(*) FROM ALUGUER WHERE CONVERT(CHAR(4), DataAluguer, 23) = '" + ano + "' AND MONTH(DataAluguer) = '01'), " +
                "[FEV] = (SELECT COUNT(*) FROM ALUGUER WHERE CONVERT(CHAR(4), DataAluguer, 23) = '" + ano + "' AND MONTH(DataAluguer) = '02'), " +
                "[MAR] = (SELECT COUNT(*) FROM ALUGUER WHERE CONVERT(CHAR(4), DataAluguer, 23) = '" + ano + "' AND MONTH(DataAluguer) = '03'), " +
                "[ABR] = (SELECT COUNT(*) FROM ALUGUER WHERE CONVERT(CHAR(4), DataAluguer, 23) = '" + ano + "' AND MONTH(DataAluguer) = '04'), " +
                "[MAI] = (SELECT COUNT(*) FROM ALUGUER WHERE CONVERT(CHAR(4), DataAluguer, 23) = '" + ano + "' AND MONTH(DataAluguer) = '05'), " +
                "[JUN] = (SELECT COUNT(*) FROM ALUGUER WHERE CONVERT(CHAR(4), DataAluguer, 23) = '" + ano + "' AND MONTH(DataAluguer) = '06'), " +
                "[JUL] = (SELECT COUNT(*) FROM ALUGUER WHERE CONVERT(CHAR(4), DataAluguer, 23) = '" + ano + "' AND MONTH(DataAluguer) = '07'), " +
                "[AGO] = (SELECT COUNT(*) FROM ALUGUER WHERE CONVERT(CHAR(4), DataAluguer, 23) = '" + ano + "' AND MONTH(DataAluguer) = '08'), " +
                "[SET] = (SELECT COUNT(*) FROM ALUGUER WHERE CONVERT(CHAR(4), DataAluguer, 23) = '" + ano + "' AND MONTH(DataAluguer) = '09'), " +
                "[OUT] = (SELECT COUNT(*) FROM ALUGUER WHERE CONVERT(CHAR(4), DataAluguer, 23) = '" + ano + "' AND MONTH(DataAluguer) = '10'), " +
                "[NOV] = (SELECT COUNT(*) FROM ALUGUER WHERE CONVERT(CHAR(4), DataAluguer, 23) = '" + ano + "' AND MONTH(DataAluguer) = '11'), " +
                "[DEZ] = (SELECT COUNT(*) FROM ALUGUER WHERE CONVERT(CHAR(4), DataAluguer, 23) = '" + ano + "' AND MONTH(DataAluguer) = '12')";
            using (DataDbContext ctx = new DataDbContext())
            {
                return ctx.Database.SqlQuery<ComparisonBarChart>(query).ToList();
            }
        }
        #endregion
        public static bool Pagamento(Pagamento pag)
        {
            using(DataDbContext ctx = new DataDbContext())
            {
                pag.data = DateTime.Now;
                ctx.pagamentos.Add(pag);
                ctx.SaveChanges();
                return true;
            }
        }
    }
}