using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentAndDrive.Models.Pagamentos.Mdl
{
    [Table("PAGAMENTO")]
    public class Pagamento
    {
        [Key, Column("Id")]
        public int id { get; set; }
        [Column("DataPagamento")]
        public DateTime data { get; set; }
        [Column("ValorPago")]
        public decimal valorPago { get; set; }
        [Column("IdAluguer")]
        public string aluguer { get; set; }
        [Column("IdFuncionario")]
        public string funcionario { get; set; }

    }
}