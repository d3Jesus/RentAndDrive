using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentAndDrive.Models.Users.Mdl
{
    [Table("AspNetUserRoles")]
    public class UserInRoles
    {
        [Key, Column(Order = 0)]
        public virtual string userId { get; set; }
        [ForeignKey("userId")]
        public virtual User users { get; set; }


        [Key, Column(Order = 1)]
        public virtual string roleId { get; set; }
        [ForeignKey("roleId")]
        public virtual Roles roles { get; set; }

    }
}