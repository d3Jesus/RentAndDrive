using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentAndDrive.Models.Users.Mdl
{
    [Table("VW_USERS")]
    public class VwUsers
    {
        [Key, Column("Id")]
        public string id { get; set; }
        [Column("UserId")]
        public string userId { get; set; }
        [Column("Nome")]
        public string nome { get; set; }
        [Column("Email")]
        public string email { get; set; }
        [Column("DataRegisto")]
        public DateTime dataRegisto { get; set; }
        [Column("Status")]
        public string estado { get; set; }
    }
}