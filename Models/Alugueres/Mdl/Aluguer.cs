using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentAndDrive.Models.Alugueres.Mdl
{
    [Table("ALUGUER")]
    public class Aluguer
    {
        [Key, Column("Id")]
        public string id { get; set; }
        [Required, Column("IdCliente")]
        public string idCliente { get; set; }
        [Required, Column("IdFuncionario")]
        public string IdFuncionario { get; set; }
        [Required, Column("IdViatura")]
        public string IdViatura { get; set; }
        [Column("IdMotorista")]
        public string IdMotorista { get; set; }
        [Required, Column("PeriodoAluguer")]
        public int PeriodoAluguer { get; set; }
        [Required, Column("UnidadePeriodo")]
        public string UnidadePeriodo { get; set; }
        [Column("DataAluguer")]
        public DateTime dataAluguer { get; set; }
        [Column("Estado")]
        public string estado { get; set; }
        [Column("DataDevolucao")]
        public DateTime dataDevolucao { get; set; }
        [Column("TIPO")]
        public string tipo { get; set; }
        [NotMapped]
        public string marca { get; set; }
        [NotMapped]
        public string modelo { get; set; }
        [NotMapped]
        public decimal valor { get; set; }
    }
}