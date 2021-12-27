using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentAndDrive.Models.Users.Mdl
{
    [Table("VW_USER_DATA")]
    public class VwUserData
    {
        [Key, Column("Id")]
        public string id { get; set; }
        [Required(ErrorMessage = "* campo obrigatório."), Column("Nome")]
        public string nome { get; set; }
        [Required(ErrorMessage = "* campo obrigatório."), Column("Morada")]
        public int morada { get; set; }
        [Required(ErrorMessage = "* campo obrigatório."), Column("Tipo")]
        public string tipo { get; set; }
        [Required(ErrorMessage = "* campo obrigatório."), Column("Sexo")]
        public string sexo { get; set; }
        [Required(ErrorMessage = "* campo obrigatório."), Column("EstadoCivil")]
        public string estadoCivil { get; set; }
        [Required(ErrorMessage = "* campo obrigatório."), Column("Funcao")]
        public string funcao { get; set; }
        [Required(ErrorMessage = "* campo obrigatório."), Column("TipoDeIdentificacao")]
        public string tipoDeIdentificacao { get; set; }
        [Required(ErrorMessage = "* campo obrigatório."), Column("NumeroIdentificacao")]
        public string numeroIdentificacao { get; set; }
        [Column("NumeroCarta")]
        public string numeroCarta { get; set; }
        [Column("CodigoCarta")]
        public string codigoCarta { get; set; }
        [Column("ValidadeCarta")]
        public DateTime? validadeCarta { get; set; }
        [Required(ErrorMessage = "* campo obrigatório."), Column("SalarioBase")]
        public decimal salario { get; set; }
        [Column("DadosBancarios")]
        public int dadosBancarios { get; set; }
        #region Morada
        [Required(ErrorMessage = "* campo obrigatório."), Column("rua")]
        public string rua { get; set; }
        [Required(ErrorMessage = "* campo obrigatório."), Column("avenida")]
        public string avenida { get; set; }
        [Required(ErrorMessage = "* campo obrigatório."), Column("quarteirao")]
        public int quarteirao { get; set; }
        #endregion
        #region Dados bancarios
        [Required(ErrorMessage = "* campo obrigatório."), Column("Banco")]
        public string banco { get; set; }
        [Required(ErrorMessage = "* campo obrigatório."), Column("NumeroConta")]
        public string numeroConta { get; set; }
        [Required(ErrorMessage = "* campo obrigatório."), Column("NIB")]
        public string nib { get; set; }
        [Required(ErrorMessage = "* campo obrigatório."), Column("Titutal")]
        public string titular { get; set; }
        #endregion
    }
}