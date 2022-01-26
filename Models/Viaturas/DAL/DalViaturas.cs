using RentAndDrive.Models.Db;
using RentAndDrive.Models.Viaturas.Mdl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentAndDrive.Models.Viaturas.DAL
{
    public class DalViaturas
    {
        public static List<VwViatura> GetViaturas()
        {
            using (DataDbContext ctx = new DataDbContext())
                return ctx.vwViaturas.ToList();
        }
        public static decimal GetValorUnitarioViatura(string matricula)
        {
            using (DataDbContext ctx = new DataDbContext())
                return ctx.vwViaturas.Where(v => v.matricula == matricula).FirstOrDefault().valorAluguer;
        }
        public static List<VwViatura> GetViaturasDisponiveis()
        {
            using (DataDbContext ctx = new DataDbContext())
                return ctx.vwViaturas.Where(d => d.estado == "Disponivel").ToList();
        }
        public static List<VwViatura> GetViaturasEmUso()
        {
            using (DataDbContext ctx = new DataDbContext())
                return ctx.vwViaturas.Where(d => d.estado == "Ocupado").ToList();
        }
        public static List<VwViatura> GetViaturasForaDeUso()
        {
            using (DataDbContext ctx = new DataDbContext())
                return ctx.vwViaturas.Where(d => d.estado == "Indisponivel").ToList();
        }

        public static Viatura GetViaturaPelaMatricula(string matricula)
        {
            using (DataDbContext ctx = new DataDbContext())
                return ctx.viaturas.Where(v => v.matricula == matricula).FirstOrDefault();
        }

        public static List<Viatura> GetViaturaPeloModelo(string modelo)
        {
            using (DataDbContext ctx = new DataDbContext())
                return ctx.viaturas.Where(v => v.modelo == modelo && v.estado == "Disponivel").ToList();
        }

        public static bool Registar(Viatura viatura)
        {
            using(DataDbContext ctx = new DataDbContext())
            {
                try
                {
                    ctx.viaturas.Add(viatura);
                    ctx.SaveChanges();
                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
        }

        public static bool Editar(Viatura viatura)
        {
            using(DataDbContext ctx = new DataDbContext())
            {
                try
                {
                    var v = ctx.viaturas.Where(vt => vt.matricula == viatura.matricula).FirstOrDefault();
                    v.cor = viatura.cor;
                    v.marca = viatura.marca;
                    v.modelo = viatura.modelo;
                    v.valorAluguer = viatura.valorAluguer;
                    v.estado = viatura.estado;
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static bool Apagar(Viatura v)
        {
            using (DataDbContext ctx = new DataDbContext())
            {
                try
                {
                    ctx.viaturas.Attach(v);
                    ctx.viaturas.Remove(v);
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static bool Disponibilidade(string matricula)
        {
            using (DataDbContext ctx = new DataDbContext())
            {
                var v = ctx.viaturas.Where(vt => vt.matricula == matricula).FirstOrDefault();
                return v.estado == "Disponivel";
            }
        }

        public static string Estado(string matricula)
        {
            using (DataDbContext ctx = new DataDbContext())
            {
                return ctx.viaturas.Where(vt => vt.matricula == matricula).FirstOrDefault().estado;
            }
        }

        public static bool AlterarEstado(string matricula, string estado)
        {
            using (DataDbContext ctx = new DataDbContext())
            {
                var v = ctx.viaturas.Where(vt => vt.matricula == matricula).FirstOrDefault();
                v.estado = estado;
                ctx.SaveChanges();
                return true;
            }
        }

        public static List<ViaturaHelper> GetViaturaHelpersPeloId(string id)
        {
            using (DataDbContext ctx = new DataDbContext())
                return ctx.viaturaHelpers.Where(h => h.id == id).ToList();
        }
        public static List<ViaturaHelper> GetViaturaHelpersPeloIdMarca(string marcaId)
        {
            using (DataDbContext ctx = new DataDbContext())
                return ctx.viaturaHelpers.Where(h => h.marcaId == marcaId).ToList();
        }
        public static List<ViaturaHelper> GetViaturaHelpersPeloTipo(string tipo)
        {
            using (DataDbContext ctx = new DataDbContext())
                return ctx.viaturaHelpers.Where(h => h.tipo == tipo).ToList();
        }
    }
}