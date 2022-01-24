using RentAndDrive.Models.Alugueres.Mdl;
using RentAndDrive.Models.Db;
using RentAndDrive.Models.Pagamentos.Mdl;
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

        public static List<VwAluguer> CarregarAluguers()
        {
            using (DataDbContext ctx = new DataDbContext())
                return ctx.vwAluguers.ToList();
        }
        public static VwAluguer CarregarReserva(string reserva)
        {
            using (DataDbContext ctx = new DataDbContext())
                return ctx.vwAluguers.Where(r => r.id == reserva).FirstOrDefault();
        }

        public static List<VwAluguer> CarregarAluguersPeloEstado(string estado)
        {
            using (DataDbContext ctx = new DataDbContext())
                return ctx.vwAluguers.Where(s => s.estado == estado).ToList();
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

        public static bool Registar(Aluguer aluguer)
        {
            using(DataDbContext ctx = new DataDbContext())
                using(var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    aluguer.id = gerarId();
                    aluguer.dataAluguer = DateTime.Now.Date;
                    aluguer.dataDevolucao = Convert.ToDateTime("1960-01-01");
                    aluguer.estado = "Activo";

                    ctx.aluguers.Add(aluguer);
                    ctx.SaveChanges();

                    decimal total = 0;
                    if (aluguer.PeriodoAluguer > 0)
                        if (aluguer.UnidadePeriodo != "0")
                        {
                            if (aluguer.UnidadePeriodo == "Dia")
                                total = aluguer.PeriodoAluguer * aluguer.valor;
                            if (aluguer.UnidadePeriodo == "Semana")
                                total = aluguer.PeriodoAluguer * 7 * aluguer.valor;
                            if (aluguer.UnidadePeriodo == "Mes")
                                total = aluguer.PeriodoAluguer * 30 * aluguer.valor;
                        }

                    Pagamento pagamento = new Pagamento()
                    {
                        data = DateTime.Now,
                        valorPago = total,
                        funcionario = aluguer.IdFuncionario,
                        aluguer = aluguer.id
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
                ctx.SaveChanges();
                return true;
            }
        }

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