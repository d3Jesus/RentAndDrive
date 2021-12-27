using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentAndDrive.Models.Users.Mdl
{
    [Table("AspNetRoles")]
    public class Roles
    {
        [Key]
        [Column("Id")]
        public virtual string RoleId { get; set; }
        [Column("Name")]
        public virtual string RoleName { get; set; }
    }
}