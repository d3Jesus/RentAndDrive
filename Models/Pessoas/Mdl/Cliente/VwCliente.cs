using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentAndDrive.Models.Pessoas.Mdl.Cliente
{
    [Table("VW_CLIENT_DATA")]
    public class VwCliente
    {
        [Key, Column("Id")]
        public string id { get; set; }
        [Required(ErrorMessage = "* campo obrigatório."), Column("Nome")]
        public string nome { get; set; }
        [Required(ErrorMessage = "* campo obrigatório."), Column("Morada")]
        public int morada { get; set; }
        [Required(ErrorMessage = "* campo obrigatório."), Column("Sexo")]
        public string sexo { get; set; }
        [Required(ErrorMessage = "* campo obrigatório."), Column("EstadoCivil")]
        public string estadoCivil { get; set; }
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
        #region Morada
        [Required(ErrorMessage = "* campo obrigatório."), Column("rua")]
        public string rua { get; set; }
        [Required(ErrorMessage = "* campo obrigatório."), Column("avenida")]
        public string avenida { get; set; }
        [Required(ErrorMessage = "* campo obrigatório."), Column("quarteirao")]
        public int quarteirao { get; set; }
        #endregion
    }
}