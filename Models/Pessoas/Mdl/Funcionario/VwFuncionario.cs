using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentAndDrive.Models.Pessoas.Mdl.Funcionario
{
    [Table("VW_FUNCIONARIO")]
    public class VwFuncionario
    {
        [Key, Column("Id")]
        public string id { get; set; }
        [Column("Nome")]
        public string nome { get; set; }
        [Column("EstadoCivil")]
        public string estadoCivil { get; set; }
        [Column("Funcao")]
        public string funcao { get; set; }
        [Column("Endereco")]
        public string endereco { get; set; }
    }
}