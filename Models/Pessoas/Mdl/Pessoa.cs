using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentAndDrive.Models.Pessoas.Mdl
{
    [Table("PESSOA")]
    public class Pessoa
    {
        [Key, Column("Id")]
        public string id { get; set; }
        [Column("Nome")]
        public string nome { get; set; }
        [Column("Morada")]
        public int morada { get; set; }
        [Column("Tipo")]
        public string tipo { get; set; }
        [Column("Sexo")]
        public string sexo { get; set; }
        [Column("EstadoCivil")]
        public string estadoCivil { get; set; }
        [Column("Funcao")]
        public string funcao { get; set; }
        [Column("TipoDeIdentificacao")]
        public string tipoDeIdentificacao { get; set; }
        [Column("NumeroIdentificacao")]
        public string numeroIdentificacao { get; set; }
        [Column("NumeroCarta")]
        public string numeroCarta { get; set; }
        [Column("CodigoCarta")]
        public string codigoCarta { get; set; }
        [Column("ValidadeCarta")]
        public DateTime validadeCarta { get; set; }
        [Column("SalarioBase")]
        public decimal? salario { get; set; }
        [Column("DadosBancarios")]
        public int? dadosBancarios { get; set; }

    }
}