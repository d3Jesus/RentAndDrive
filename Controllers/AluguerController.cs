using RentAndDrive.Models.Alugueres.DAL;
using RentAndDrive.Models.Alugueres.Mdl;
using RentAndDrive.Models.Pessoas.DAL;
using RentAndDrive.Models.Viaturas.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentAndDrive.Controllers
{
    [Authorize]
    public class AluguerController : Controller
    {
        #region CRUD
        // GET: Aluguer
        public ActionResult Index()
        {
            return View(DalAluguer.CarregarAluguers());
        }

        [HttpGet]
        public ActionResult Registar()
        {
            // Marcas das viaturas
            ViewData["Marcas"] = DalViaturas.GetViaturaHelpersPeloTipo("Marca");
            return View(new Aluguer());
        }

        [HttpPost]
        public ActionResult Registar(Aluguer aluguer)
        {
            aluguer.IdFuncionario = Session["idFuncionario"] as string;
            aluguer.valor = DalViaturas.GetViaturaPelaMatricula(aluguer.IdViatura).valorAluguer;
            if (DalAluguer.Registar(aluguer))
            {
                if(DalViaturas.AlterarEstado(aluguer.IdViatura, "Ocupado"))
                    TempData["sucesso"] = "Aluguer registada com sucesso!";
            }
            else
                TempData["falha"] = "Ocorreu um erro ao registar o aluguer. Tente novamente mais tarde!";
            return RedirectToAction(nameof(Index));
        }
        
        [HttpGet]
        public ActionResult AlguerActivo()
        {
            return View(DalAluguer.CarregarAluguers().Where(a => a.estado == "Activo").ToList());
        }

        [HttpPost]
        public ActionResult CancelarAluguer(string al)
        {
            if(al == null || al == "" || al == "rowId")
            {
                TempData["falha"] = "Selecione o aluguer que deseja cancelar!";
                return RedirectToAction(nameof(Index));
            }
            if (DalAluguer.Cancelar(al))
            {
                var viatura = DalAluguer.CarregarAluguerPeloId(al).IdViatura;
                if (DalViaturas.AlterarEstado(viatura, "Disponivel"))
                    TempData["sucesso"] = "Aluguer cancelada com sucesso!";
            }
            else
                TempData["falha"] = "Ocorreu um erro ao cancelar o aluguer. Tente novamente mais tarde!";
            return RedirectToAction(nameof(Index));
        }
        
        [HttpGet]
        public ActionResult Terminar(string al)
        {
            if(al == null || al == "" || al == "rowId")
            {
                TempData["falha"] = "Selecione o aluguer que deseja terminar!";
                return RedirectToAction(nameof(AlguerActivo));
            }
            if (DalAluguer.Devolver(al))
                TempData["sucesso"] = "Aluguer terminado com sucesso!";
            else
                TempData["falha"] = "Ocorreu um erro ao terminar o aluguer. Tente novamente mais tarde!";
            return RedirectToAction(nameof(AlguerActivo));
        }
        #endregion

        #region Fetch
        [HttpPost]
        public ActionResult PesquisarCliente(string nome)
        {
            var clients = DalPessoa.GetPessoaPeloTipoENome(nome, "Cliente");
            SelectList list = new SelectList(clients, "id", "nome");
            return Json(list);
        }
        [HttpPost]
        public ActionResult CarregarMotoristasViatura(string id)
        {
            var clients = DalPessoa.CarregarMotoristasViatura(id);
            SelectList list = new SelectList(clients, "id", "nome");
            return Json(list);
        }
        #endregion
    }
}