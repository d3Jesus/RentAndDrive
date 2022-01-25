using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentAndDrive.Models.Alugueres.Mdl
{
    [Table("VW_ALUGUER")]
    public class VwAluguer
    {
        [Key, Column("Id")]
        public string id { get; set; }
        [Column("NomeCliente")]
        public string nomeCliente { get; set; }
        [Column("IDFuncionario")]
        public string idFuncionario { get; set; }
        [Column("NomeFuncionario")]
        public string nomeFuncionario { get; set; }
        [Column("Viatura")]
        public string viatura { get; set; }
        [Column("Matricula")]
        public string matricula { get; set; }
        [Column("NomeMotorista")]
        public string nomeMotorista { get; set; }
        [Column("DataAluguer")]
        public DateTime dataAluguer { get; set; }
        [Column("Periodo")]
        public string periodo { get; set; }
        [Column("Estado")]
        public string estado { get; set; }
        [Column("DataDevolucao")]
        public DateTime dataDevolucao { get; set; }
        [Column("ValorPago")]
        public decimal valorPago { get; set; }
        [Column("TIPO")]
        public string tipo { get; set; }
    }
}