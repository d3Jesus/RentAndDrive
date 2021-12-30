using RentAndDrive.Models.Db;
using RentAndDrive.Models.Users.Mdl;
using System.Collections.Generic;
using System.Linq;

namespace RentAndDrive.Models.Users.DAL
{
    public class UsersDal
    {
        public static bool IsActive(string id)
        {
            using (var ctx = new DataDbContext())
            {
                var status = ctx.pessoaUsuarios.Where(u => u.idUsuario == id).FirstOrDefault().estado;
                return status == "Activo";
            }
        }

        public static List<VwUsers> GetAllUsers()
        {
            using (DataDbContext ctx = new DataDbContext())
                return ctx.vwUsers.ToList();
        }

        public static VwUsers GetUserById(string id)
        {
            using (DataDbContext ctx = new DataDbContext())
                return ctx.vwUsers.Where(w => w.id == id).FirstOrDefault();
        }

        public static VwUsers GetUserByEmail(string email)
        {
            using (DataDbContext ctx = new DataDbContext())
                return ctx.vwUsers.Where(w => w.email == email).FirstOrDefault();
        }

        public static ICollection<VwRoleDescription> CarregarTodasAcessosQueOUsuarioNaoTem(string userId)
        {
            string query = "SELECT * FROM DESCRICAO_ACESSOS WHERE IdAcesso NOT IN (SELECT RoleId FROM VW_USER_ROLES WHERE UserId ='" + userId + "')";
            using (DataDbContext ctx = new DataDbContext())
            {
                return ctx.Database.SqlQuery<VwRoleDescription>(query).ToList();
            }
        }

        public static ICollection<VwRoleDescription> CarregarTodasAcessosQueOUsuarioTem(string userId)
        {
            string query = "SELECT * FROM DESCRICAO_ACESSOS WHERE IdAcesso IN (SELECT RoleId FROM VW_USER_ROLES WHERE UserId ='" + userId + "')";
            using (DataDbContext ctx = new DataDbContext())
            {
                return ctx.Database.SqlQuery<VwRoleDescription>(query).ToList();
            }
        }

        public static List<VwUserRoles> GetAllVwUserRoles(string userId)
        {
            string query = "SELECT * FROM VW_USER_ROLES where UserId='" + userId + "'";
            using (DataDbContext ctx = new DataDbContext())
            {
                return ctx.Database.SqlQuery<VwUserRoles>(query).ToList();
            }
        }

        public static List<UserInRoles> GetAllUserRolesByUserId(string userId)
        {
            using (DataDbContext context = new DataDbContext())
            {
                return context.userInRoles.Where(x => x.userId == userId).ToList();
            }
        }

        public static bool AtribuirAcessos(string userId, string[] selectedRoles)
        {
            using (DataDbContext ctx = new DataDbContext())
            {
                List<UserInRoles> roleList = GetAllUserRolesByUserId(userId);
                if (roleList.Count == 0)
                    roleList = new List<UserInRoles>();

                for (int i = 0; i < selectedRoles.Length; i++)
                {
                    UserInRoles uInRoles = roleList.Where(x => x.roleId == selectedRoles[i]).FirstOrDefault();
                    if (uInRoles == null)
                    {
                        uInRoles = new UserInRoles { userId = userId, roleId = selectedRoles[i] };
                        roleList.Add(uInRoles);
                    }

                    ctx.userInRoles.Add(uInRoles);
                }

                ctx.SaveChanges();

                return true;
            }
        }
        public static bool RemoverAcessos(string userId, string[] selectedRoles)
        {
            using (DataDbContext ctx = new DataDbContext())
            {
                List<UserInRoles> roleList = GetAllUserRolesByUserId(userId);
                if (roleList == null)
                    roleList = new List<UserInRoles>();

                if (selectedRoles != null)
                    for (int i = 0; i < selectedRoles.Length; i++)
                    {
                        UserInRoles uInRoles = roleList
                            .Where(x => x.roleId == selectedRoles[i])
                            .FirstOrDefault();
                        ctx.userInRoles.Attach(uInRoles);
                        ctx.userInRoles.Remove(uInRoles);
                    }

                ctx.SaveChanges();

                return true;
            }
        }
    }
}