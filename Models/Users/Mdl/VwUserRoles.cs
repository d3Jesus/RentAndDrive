using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentAndDrive.Models.Users.Mdl
{
    public class VwUserRoles
    {
        [Key]
        [Column("UserId")]
        public string userId { get; set; }
        [Column("RoleId")]
        public string roleId { get; set; }
        [Column("RoleName")]
        public string roleName { get; set; }
    }
}