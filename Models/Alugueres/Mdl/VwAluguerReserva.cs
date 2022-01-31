using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentAndDrive.Models.Alugueres.Mdl
{
    [Table("VW_RESERVA")]
    public class VwAluguerReserva
    {
        [Key, Column("IdPessoa", Order = 0)]
        public string idPessoa { get; set; }
        [Column("Nome")]
        public string nome { get; set; }
        [Column("Tipo")]
        public string tipo { get; set; }
        [Column("Sexo")]
        public string sexo { get; set; }
        [Column("EstadoCivil")]
        public string estadoCivil { get; set; }
        [Column("TipoDeIdentificacao")]
        public string tipoIdent { get; set; }
        [Column("NumeroIdentificacao")]
        public string numeroIdent { get; set; }
        [Column("NumeroCarta")]
        public string numeroCarta { get; set; }
        [Column("CodigoCarta")]
        public string codigoCarta { get; set; }
        [Column("ValidadeCarta")]
        public DateTime? validadeCarta { get; set; }
        [Key, Column("IdAluguer", Order = 1)]
        public string idAluguer { get; set; }
        [Column("IdFuncionario")]
        public string idFuncionario { get; set; }
        [Column("IdViatura")]
        public string idViatura { get; set; }
        [Column("Viatura")]
        public string viatura { get; set; }
        [Column("IdMotorista")]
        public string idMotorista { get; set; }
        [Column("PeriodoAluguer")]
        public int periodo { get; set; }
        [Column("UnidadePeriodo")]
        public string unidade { get; set; }
        [Column("Estado")]
        public string estado { get; set; }
        [Column("DataAluguer")]
        public DateTime dataAluguer { get; set; }
        [Column("DataDevolucao")]
        public DateTime dataDevolucao { get; set; }

        #region NOT MAPPED
        #region Cliente
        [NotMapped]
        public string idCliente { get; set; }
        #endregion

        [NotMapped]
        public string marca { get; set; }

        [NotMapped]
        public string modelo { get; set; }

        [NotMapped]
        public string matricula { get; set; }

        [NotMapped]
        public decimal valor { get; set; }

        #region Morada
        [NotMapped]
        public string avenida { get; set; }

        [NotMapped]
        public string rua { get; set; }

        [NotMapped]
        public int quarteirao { get; set; }
        #endregion
        #endregion
    }
}