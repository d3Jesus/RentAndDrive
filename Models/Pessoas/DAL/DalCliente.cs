using RentAndDrive.Models.Db;
using RentAndDrive.Models.Pessoas.Mdl;
using RentAndDrive.Models.Pessoas.Mdl.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentAndDrive.Models.Pessoas.DAL
{
    public class DalCliente
    {
        public static List<VwCliente> CarregarListaDeClientes()
        {
            using (DataDbContext ctx = new DataDbContext())
                return ctx.vwClientes.ToList();
        }

        public static VwCliente CarregarClientePeloId(string id)
        {
            using (DataDbContext ctx = new DataDbContext())
                return ctx.vwClientes.Where(p => p.id == id).FirstOrDefault();
        }

        public static bool Registar(VwCliente model)
        {
            using (DataDbContext ctx = new DataDbContext())
            using (var tran = ctx.Database.BeginTransaction())
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

                    #region Pessoa
                    Pessoa p = new Pessoa()
                    {
                        id = DalPessoa.GetId("Cliente"),
                        nome = model.nome,
                        morada = m.id,
                        tipo = "Cliente",
                        sexo = model.sexo,
                        estadoCivil = model.estadoCivil,
                        funcao = "",
                        tipoDeIdentificacao = model.tipoDeIdentificacao,
                        numeroIdentificacao = model.numeroIdentificacao,
                        numeroCarta = model.numeroCarta,
                        codigoCarta = model.codigoCarta,
                        validadeCarta = model.validadeCarta.GetValueOrDefault()
                    };
                    ctx.pessoas.Add(p);
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
        public static bool Editar(VwCliente model)
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

                    #region Pessoa
                    var pessoa = ctx.pessoas.Where(p => p.id == model.id).FirstOrDefault();
                    pessoa.nome = model.nome;
                    pessoa.sexo = model.sexo;
                    pessoa.estadoCivil = model.estadoCivil;
                    pessoa.tipoDeIdentificacao = model.tipoDeIdentificacao;
                    pessoa.numeroIdentificacao = model.numeroIdentificacao;
                    pessoa.numeroCarta = model.numeroCarta;
                    pessoa.codigoCarta = model.codigoCarta;
                    pessoa.validadeCarta = model.validadeCarta.GetValueOrDefault();
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
        public static bool Apagar(string idCliente)
        {
            using (DataDbContext ctx = new DataDbContext())
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    var pessoa = ctx.pessoas.Where(p => p.id == idCliente).FirstOrDefault();
                    var morada = ctx.moradas.Where(m => m.id == pessoa.morada).FirstOrDefault();

                    if (pessoa != null)
                    {
                        ctx.pessoas.Attach(pessoa);
                        ctx.pessoas.Remove(pessoa);
                        ctx.SaveChanges();
                    }

                    if (morada != null)
                    {
                        ctx.moradas.Attach(morada);
                        ctx.moradas.Remove(morada);
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
    }
}