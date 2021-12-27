using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentAndDrive.Models.Users.Mdl
{
    [Table("AspNetUsers")]
    public class User
    {
        [Key]
        [Column("Id")]
        public string Id { get; set; }

        [Column("Email"), DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Column("EmailConfirmed")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public bool EmailConfirmed { get; set; }

        [Column("PasswordHash")]
        public string PasswordHash { get; set; }

        [Column("SecurityStamp")]
        public string SecurityStamp { get; set; }

        [Column("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Column("PhoneNumberConfirmed")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public bool PhoneNumberConfirmed { get; set; }

        [Column("TwoFactorEnabled")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public bool TwoFactorEnabled { get; set; }

        [Column("LockoutEndDateUtc")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? LockoutEndDateUtc { get; set; }

        [Column("LockoutEnabled")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public bool LockoutEnabled { get; set; }

        [Column("AccessFailedCount")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int AccessFailedCount { get; set; }

        [Column("UserName")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string UserName { get; set; }
    }
}