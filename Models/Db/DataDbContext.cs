using RentAndDrive.Models.Alugueres.Mdl;
using RentAndDrive.Models.Chart.Mdl;
using RentAndDrive.Models.Pagamentos.Mdl;
using RentAndDrive.Models.Pessoas.Mdl;
using RentAndDrive.Models.Pessoas.Mdl.Cliente;
using RentAndDrive.Models.Pessoas.Mdl.Funcionario;
using RentAndDrive.Models.Users.Mdl;
using RentAndDrive.Models.Viaturas.Mdl;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RentAndDrive.Models.Db
{
    public class DataDbContext : DbContext
    {
        public DataDbContext() : base("DefaultConnection")
        {
            Database.SetInitializer<DataDbContext>(null);
        }

        public DbSet<UserInRoles> userInRoles { get; set; }
        public DbSet<VwUsers> vwUsers { get; set; }
        public DbSet<Pessoa> pessoas { get; set; }
        public DbSet<Morada> moradas { get; set; }
        public DbSet<DadosBancarios> dadosBancarios { get; set; }
        public DbSet<ViaturaHelper> viaturaHelpers { get; set; }
        public DbSet<Viatura> viaturas { get; set; }
        public DbSet<VwViatura> vwViaturas { get; set; }
        public DbSet<MotoristaViatura> motoristaViaturas { get; set; }
        public DbSet<Aluguer> aluguers { get; set; }
        public DbSet<VwAluguer> vwAluguers { get; set; }
        public DbSet<PessoaUsuario> pessoaUsuarios { get; set; }
        public DbSet<VwFuncionario> vwFuncionarios { get; set; }
        public DbSet<VwFuncionarioResumo> vwFuncionarioResumos { get; set; }
        public DbSet<VwCliente> vwClientes { get; set; }
        public DbSet<Pagamento> pagamentos { get; set; }
        public DbSet<ComparisonBarChart> ComparisonBarCharts { get; set; }
    }
}