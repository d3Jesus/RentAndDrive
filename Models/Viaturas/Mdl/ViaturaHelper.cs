using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentAndDrive.Models.Viaturas.Mdl
{
    [Table("VIATURA_HELPER")]
    public class ViaturaHelper
    {
        [Key, Column("IdHelper")]
        public string id { get; set; }
        [Column("MarcaId")]
        public string marcaId { get; set; }
        [Column("Tipo")]
        public string tipo { get; set; }
        [Column("Descricao")]
        public string descricao { get; set; }
    }
}