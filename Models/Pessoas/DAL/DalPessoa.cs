using RentAndDrive.Models.Db;
using RentAndDrive.Models.Pessoas.Mdl;
using RentAndDrive.Models.Users.Mdl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentAndDrive.Models.Pessoas.DAL
{
    public class DalPessoa
    {
        #region ID
        private static string gerarId(string serie)
        {
            DataDbContext ctx = new DataDbContext();
            string id = ctx.pessoas.Where(x => x.id.Contains(serie)).Max(x => x.id);
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

        public static string GetId(string pessoa)
        {
            string s = string.Empty;
            switch (pessoa.ToUpper())
            {
                case "CLIENTE": s = "RTCL" + DateTime.Now.Year; break;
                case "FUNCIONARIO": s = "RTUS"; break;
            }
            return gerarId(s);
        }
        #endregion

        public static Pessoa GetPessoaPeloId(string id)
        {
            using (DataDbContext ctx = new DataDbContext())
                return ctx.pessoas.Where(m => m.id == id).FirstOrDefault();
        }
        public static List<Pessoa> GetPessoaPeloTipoENome(string nome, string tipo)
        {
            using (DataDbContext ctx = new DataDbContext())
                return ctx.pessoas.Where(m => m.tipo == tipo && m.nome.Contains(nome)).ToList();
        }
        public static List<Pessoa> GetPessoasPelaFuncao(string funcao)
        {
            using (DataDbContext ctx = new DataDbContext())
                return ctx.pessoas.Where(m => m.funcao == funcao).ToList();
        }
        public static List<Pessoa> GetPessoasPeloTipo(string tipo)
        {
            using (DataDbContext ctx = new DataDbContext())
                return ctx.pessoas.Where(m => m.tipo == tipo).ToList();
        }

        public static Morada GetMoradaPessoa(int id)
        {
            using (DataDbContext ctx = new DataDbContext())
                return ctx.moradas.Where(m => m.id == id).FirstOrDefault();
        }
        public static DadosBancarios GetDadosBancariosPessoa(int id)
        {
            using (DataDbContext ctx = new DataDbContext())
                return ctx.dadosBancarios.Where(m => m.id == id).FirstOrDefault();
        }

        public static PessoaUsuario CarregaIdUsuarioPeloIdDaEntidade(string id)
        {
            using (var ctx = new DataDbContext())
                return ctx.pessoaUsuarios.Where(p => p.idPessoa == id).FirstOrDefault();
        }

        public static List<LstMotoristas> CarregarMotoristasViatura(string viatura)
        {
            using (DataDbContext ctx = new DataDbContext())
            {
                try
                {
                    string query = "SELECT Id, Nome FROM PESSOA WHERE Id IN (SELECT IdMotorista FROM MOTORISTA_VIATURA WHERE IdViatura = '" + viatura + "')";
                    return ctx.Database.SqlQuery<LstMotoristas>(query).ToList();
                }
                catch(Exception ex)
                {
                    return null;
                }
            }
        }
    }
}