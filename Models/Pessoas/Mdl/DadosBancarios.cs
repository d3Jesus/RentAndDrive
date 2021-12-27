using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentAndDrive.Models.Pessoas.Mdl
{
    [Table("DADOS_BANCARIOS")]
    public class DadosBancarios
    {
        [Key, Column("Id")]
        public int id { get; set; }
        [Column("Banco")]
        public string banco { get; set; }
        [Column("NumeroConta")]
        public string numeroConta { get; set; }
        [Column("NIB")]
        public string nib { get; set; }
        [Column("Titutal")]
        public string titular { get; set; }
    }
}