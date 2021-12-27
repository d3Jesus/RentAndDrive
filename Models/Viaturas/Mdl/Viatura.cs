using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentAndDrive.Models.Viaturas.Mdl
{
    [Table("VIATURA")]
    public class Viatura
    {
        [Key, Column("Matricula")]
        public string matricula { get; set; }
        [Column("Cor")]
        public string cor { get; set; }
        [Column("Marca")]
        public string marca { get; set; }
        [Column("Modelo")]
        public string modelo { get; set; }
        [Column("ValorAluguer")]
        public decimal valorAluguer { get; set; }
        [Column("Estado")]
        public string estado { get; set; }
    }
}