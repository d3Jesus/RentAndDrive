using RentAndDrive.Models.Pessoas.DAL;
using RentAndDrive.Models.Users.DAL;
using RentAndDrive.Models.Users.Mdl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentAndDrive.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            var data = UsersDal.GetAllUsers();
            return View(data);
        }

        #region User Roles
        //[Authorize(Roles = "ALLOW_USER_MANAGER, ALLOW_USER_MANAGE_ACCESS")]
        public ActionResult GerirAcessos(string id)
        {
            if (id == null || id == "")
            {
                TempData["falha"] = "Selecione um utilizador para gerir os acessos!";
                return RedirectToAction("Index");
            }

            //VwSystemUser user = UsersManager.GetUserByLogin(username);
            //TempData["vwSystemUser"] = user;

            string username = DalFuncionarios.GetFuncionarioResumo(id).nome;
            string userId = DalPessoa.CarregaIdUsuarioPeloIdDaEntidade(id).idUsuario;
            TempData["userId"] = userId;
            TempData["Id"] = id;
            TempData["username"] = username;

            ICollection<VwRoleDescription> notuserRoles = UsersDal.CarregarTodasAcessosQueOUsuarioNaoTem(id);
            ICollection<VwRoleDescription> userInRoles = UsersDal.CarregarTodasAcessosQueOUsuarioTem(id);
            List<VwUserRoles> userRoles = UsersDal.GetAllVwUserRoles(userId);
            ViewData["notuserRoles"] = notuserRoles;
            ViewData["userInRoles"] = userInRoles;

            return View(userRoles);
        }

        [HttpGet]
        public ActionResult AtribuirAcessos(string id)
        {
            TempData["Id"] = id;
            string userId = DalPessoa.CarregaIdUsuarioPeloIdDaEntidade(id).idUsuario;
            TempData["UserId"] = userId;
            var data = UsersDal.CarregarTodasAcessosQueOUsuarioNaoTem(userId);
            return View(data);
        }

        [HttpPost]
        public ActionResult AtribuirAcessos(string userId, string[] selectedRoles)
        {
            string id = TempData["Id"] as string;
            try
            {
                if (selectedRoles != null)
                {
                    UsersDal.AtribuirAcessos(userId, selectedRoles);

                    TempData["sucesso"] = " " + selectedRoles.Length + " Acesso(s) adicionado(s) com sucesso.";
                }
                else
                {
                    TempData["falha"] = " Nenhum acesso foi selecionado!";
                }
            }
            catch (Exception e)
            {
                TempData["falha"] = "Ocorreu uma falha ao adicionar acessos. Tente novamente ou contacte o administrador do sistema!";
            }

            return RedirectToAction(nameof(GerirAcessos), new { id });
        }

        [HttpGet]
        public ActionResult RemoverAcessos(string id)
        {
            TempData["Id"] = id;
            string userId = DalPessoa.CarregaIdUsuarioPeloIdDaEntidade(id).idUsuario;
            TempData["UserId"] = userId;
            var data = UsersDal.CarregarTodasAcessosQueOUsuarioTem(userId);
            return View(data);
        }

        [HttpPost]
        public ActionResult RemoverAcessos(string userId, string[] selectedRoles)
        {
            string id = TempData["Id"] as string;
            try
            {
                if (selectedRoles != null)
                {
                    UsersDal.RemoverAcessos(userId, selectedRoles);

                    TempData["sucesso"] = " " + selectedRoles.Length + " Acesso(s) removido(s) com sucesso.";
                }
                else
                {
                    TempData["falha"] = " Nenhum acesso foi selecionado!";
                }

            }
            catch (Exception e)
            {
                TempData["falha"] = "Ocorreu uma falha ao remover os acessos. Tente novamente ou contacte o administrador do sistema!";
            }

            return RedirectToAction(nameof(GerirAcessos), new { id });
        }

        #endregion
    }
}