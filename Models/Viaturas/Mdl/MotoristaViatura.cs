using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentAndDrive.Models.Viaturas.Mdl
{
    [Table("MOTORISTA_VIATURA")]
    public class MotoristaViatura
    {
        [Key, Column("IdMotorista", Order = 0)]
        public string motorista { get; set; }
        [Key, Column("IdViatura", Order = 1)]
        public string viatura { get; set; }
    }
}