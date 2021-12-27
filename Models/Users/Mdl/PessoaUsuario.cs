using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentAndDrive.Models.Users.Mdl
{
    [Table("PESSOA_USUARIO")]
    public class PessoaUsuario
    {
        [Key, Column("ID")]
        public int id { get; set; }
        [Column("IdUsuario")]
        public string idUsuario { get; set; }
        [Column("IdPessoa")]
        public string idPessoa { get; set; }
        [Column("Status")]
        public string estado { get; set; }
        [Column("DataRegisto")]
        public DateTime dataRegisto { get; set; }
    }
}