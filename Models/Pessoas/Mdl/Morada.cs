using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentAndDrive.Models.Pessoas.Mdl
{
    [Table("MORADA")]
    public class Morada
    {
        [Key, Column("IdMorada")]
        public int id { get; set; }
        [Column("rua")]
        public string rua { get; set; }
        [Column("avenida")]
        public string avenida { get; set; }
        [Column("quarteirao")]
        public int quarteirao { get; set; }
    }
}