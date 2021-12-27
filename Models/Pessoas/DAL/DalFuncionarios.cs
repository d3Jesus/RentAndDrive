using RentAndDrive.Models.Db;
using RentAndDrive.Models.Pessoas.Mdl;
using RentAndDrive.Models.Pessoas.Mdl.Funcionario;
using RentAndDrive.Models.Users.Mdl;
using RentAndDrive.Models.Viaturas.Mdl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentAndDrive.Models.Pessoas.DAL
{
    public class DalFuncionarios
    {
        public static List<VwFuncionario> CarregarFuncionarios()
        {
            using (DataDbContext ctx = new DataDbContext())
                return ctx.vwFuncionarios.ToList();
        }
        public static List<VwFuncionario> CarregarFuncionariosPelaFuncao(string funcao)
        {
            using (DataDbContext ctx = new DataDbContext())
                return ctx.vwFuncionarios.Where(f => f.funcao == funcao).ToList();
        }

        public static VwFuncionarioResumo GetFuncionarioResumo(string id)
        {
            using (var ctx = new DataDbContext())
                return ctx.vwFuncionarioResumos.Where(r => r.id == id).FirstOrDefault();
        }

        public static bool Registar(VwUserData model)
        {
            using(DataDbContext ctx = new DataDbContext())
            using(var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    #region Morada
                    Morada m = new Morada()
                    {
                        rua = model.rua,
                        avenida = model.avenida,
                        quarteirao = model.quarteirao
                    };
                    ctx.moradas.Add(m);
                    ctx.SaveChanges();
                    #endregion

                    #region Dados Bancarios
                    DadosBancarios d = new DadosBancarios()
                    {
                        banco = model.banco,
                        numeroConta = model.numeroConta,
                        nib = model.nib,
                        titular = model.titular
                    };
                    ctx.dadosBancarios.Add(d);
                    ctx.SaveChanges();
                    #endregion

                    #region Pessoa
                    Pessoa p = new Pessoa()
                    {
                        id = DalPessoa.GetId("Funcionario"),
                        nome = model.nome,
                        morada = m.id,
                        tipo = "Funcionario",
                        sexo = model.sexo,
                        estadoCivil = model.estadoCivil,
                        funcao = model.funcao,
                        tipoDeIdentificacao = model.tipoDeIdentificacao,
                        numeroIdentificacao = model.numeroIdentificacao,
                        numeroCarta = model.numeroCarta,
                        codigoCarta = model.codigoCarta,
                        validadeCarta = model.validadeCarta.GetValueOrDefault(),
                        salario = model.salario,
                        dadosBancarios = d.id
                    };
                    ctx.pessoas.Add(p);
                    ctx.SaveChanges();
                    #endregion

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
        public static bool Editar(VwUserData model)
        {
            using (DataDbContext ctx = new DataDbContext())
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    #region Morada
                    var morada = ctx.moradas.Where(m => m.id == model.morada).FirstOrDefault();
                    morada.rua = model.rua;
                    morada.avenida = model.avenida;
                    morada.quarteirao = model.quarteirao;
                    ctx.SaveChanges();
                    #endregion

                    #region Dados Bancarios
                    var dados = ctx.dadosBancarios.Where(d => d.id == model.dadosBancarios).FirstOrDefault();
                    dados.banco = model.banco;
                    dados.numeroConta = model.numeroConta;
                    dados.nib = model.nib;
                    dados.titular = model.titular;
                    ctx.SaveChanges();
                    #endregion

                    #region Pessoa
                    var pessoa = ctx.pessoas.Where(p => p.id == model.id).FirstOrDefault();
                    pessoa.nome = model.nome;
                    pessoa.sexo = model.sexo;
                    pessoa.estadoCivil = model.estadoCivil;
                    pessoa.funcao = model.funcao;
                    pessoa.tipoDeIdentificacao = model.tipoDeIdentificacao;
                    pessoa.numeroIdentificacao = model.numeroIdentificacao;
                    pessoa.numeroCarta = model.numeroCarta;
                    pessoa.codigoCarta = model.codigoCarta;
                    pessoa.validadeCarta = model.validadeCarta.GetValueOrDefault();
                    pessoa.salario = model.salario;
                    ctx.SaveChanges();
                    #endregion

                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }
        public static bool Apagar(string idFuncionario)
        {
            using (DataDbContext ctx = new DataDbContext())
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    var pessoa = ctx.pessoas.Where(p => p.id == idFuncionario).FirstOrDefault();
                    var morada = ctx.moradas.Where(m => m.id == pessoa.morada).FirstOrDefault();
                    var dados = ctx.dadosBancarios.Where(d => d.id == pessoa.dadosBancarios).FirstOrDefault();
                    var pessoaU = ctx.pessoaUsuarios.Where(pss => pss.idPessoa == pessoa.id).FirstOrDefault();

                    if(pessoaU != null)
                    {
                        ctx.pessoaUsuarios.Attach(pessoaU);
                        ctx.pessoaUsuarios.Remove(pessoaU);
                        ctx.SaveChanges();
                    }

                    if(pessoa != null)
                    {
                        ctx.pessoas.Attach(pessoa);
                        ctx.pessoas.Remove(pessoa);
                        ctx.SaveChanges();
                    }

                    if(morada != null)
                    {
                        ctx.moradas.Attach(morada);
                        ctx.moradas.Remove(morada);
                        ctx.SaveChanges();
                    }

                    if(dados != null)
                    {
                        ctx.dadosBancarios.Attach(dados);
                        ctx.dadosBancarios.Remove(dados);
                        ctx.SaveChanges();
                    }
                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }

        public static bool ActivarDesactivarCredenciais(string idFuncionario, string estado)
        {
            try
            {
                using(DataDbContext ctx = new DataDbContext())
                {
                    PessoaUsuario p = ctx.pessoaUsuarios.Where(ps => ps.idPessoa == idFuncionario).FirstOrDefault();
                    p.estado = estado;
                    ctx.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        #region Motorista_Viatura
        public static List<MotoristaViatura> CarregaAsViaturasDoMotorista(string motorista)
        {
            using (DataDbContext context = new DataDbContext())
            {
                return context.motoristaViaturas.Where(x => x.motorista == motorista).ToList();
            }
        }
        public static List<VwViatura> ListaDeViaturasAtribuidasAoMotorista(string id)
        {
            using (DataDbContext ctx = new DataDbContext())
            {
                string query = "SELECT * FROM VW_VIATURAS WHERE MATRICULA IN (SELECT IdViatura FROM MOTORISTA_VIATURA WHERE IdMotorista = '"+ id +"')";
                return ctx.Database.SqlQuery<VwViatura>(query).ToList();
            }
        }
        public static List<VwViatura> ListaDeViaturasNaoAtribuidasAoMotorista(string id)
        {
            using (DataDbContext ctx = new DataDbContext())
            {
                string query = "SELECT * FROM VW_VIATURAS WHERE MATRICULA NOT IN (SELECT IdViatura FROM MOTORISTA_VIATURA WHERE IdMotorista = '" + id + "')";
                return ctx.Database.SqlQuery<VwViatura>(query).ToList();
            }
        }

        public static bool AtribuirViaturasAoMotorista(string motorista, string[] selectedVehicles)
        {
            using (DataDbContext ctx = new DataDbContext())
            {
                List<MotoristaViatura> viaturas = CarregaAsViaturasDoMotorista(motorista);
                if (viaturas == null)
                    viaturas = new List<MotoristaViatura>();

                for (int i = 0; i < selectedVehicles.Length; i++)
                {
                    MotoristaViatura mv = viaturas.Where(x => x.viatura == selectedVehicles[i]).FirstOrDefault();
                    if (mv == null)
                    {
                        mv = new MotoristaViatura { motorista = motorista, viatura = selectedVehicles[i] };
                        viaturas.Add(mv);
                    }
                    ctx.motoristaViaturas.Add(mv);
                }
                ctx.SaveChanges();
                return true;
            }
        }
        public static bool RemoverViaturasAoMotorista(string motorista, string[] selectedVehicles)
        {
            using (DataDbContext ctx = new DataDbContext())
            {
                List<MotoristaViatura> viaturas = CarregaAsViaturasDoMotorista(motorista);
                if (viaturas == null)
                    viaturas = new List<MotoristaViatura>();

                for (int i = 0; i < selectedVehicles.Length; i++)
                {
                    MotoristaViatura mv = viaturas
                            .Where(x => x.viatura == selectedVehicles[i])
                            .FirstOrDefault();
                    ctx.motoristaViaturas.Attach(mv);
                    ctx.motoristaViaturas.Remove(mv);
                }
                ctx.SaveChanges();
                return true;
            }
        }
        #endregion
    }
}