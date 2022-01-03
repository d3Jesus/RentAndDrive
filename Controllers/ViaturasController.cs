using RentAndDrive.Models.Viaturas.DAL;
using RentAndDrive.Models.Viaturas.Mdl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentAndDrive.Controllers
{
    [Authorize]
    public class ViaturasController : Controller
    {
        #region CRUD
        // GET: Veiculos
        [Authorize(Roles = "CONSULTAR_VIATURAS")]
        public ActionResult Index()
        {
            var data = DalViaturas.GetViaturas();
            return View(data);
        }
       
        [HttpGet]
        [Authorize(Roles = "REGISTAR_VIATURAS")]
        public ActionResult RegistarViatura()
        {
            ViewData["Marcas"] = DalViaturas.GetViaturaHelpersPeloTipo("Marca");
            return View(new Viatura());
        }

        [HttpPost]
        public ActionResult RegistarViatura(Viatura viatura)
        {
            if (DalViaturas.Registar(viatura))
                TempData["sucesso"] = "Viatura registada com sucesso!";
            else
                TempData["falha"] = "Ocorreu um erro ao registar a viatura. Tente novamente mais tarde!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "EDITAR_VIATURAS")]
        public ActionResult EditarViatura(string matricula)
        {
            if (matricula == null || matricula == "rowId")
            {
                TempData["falha"] = "Selecione a viatura que deseja editar.";
                return RedirectToAction(nameof(Index));
            }
            var data = DalViaturas.GetViaturaPelaMatricula(matricula);
            ViewData["Marcas"] = DalViaturas.GetViaturaHelpersPeloTipo("Marca");
            ViewData["Modelos"] = DalViaturas.GetViaturaHelpersPeloIdMarca(data.marca);
            return View(data);
        }

        [HttpPost]
        public ActionResult EditarViatura(Viatura viatura)
        {
            if (DalViaturas.Editar(viatura))
                TempData["sucesso"] = "Viatura actualizada com sucesso!";
            else
                TempData["falha"] = "Ocorreu um erro ao actualizar a viatura. Tente novamente mais tarde!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "ELIMINAR_VIATURAS")]
        public ActionResult EliminarViatura(string matricula)
        {
            if (matricula == null || matricula == "rowId")
            {
                TempData["falha"] = "Selecione o viatura que deseja eliminar.";
                return RedirectToAction(nameof(Index));
            }
            var data = DalViaturas.GetViaturaPelaMatricula(matricula);
            return View(data);
        }

        [HttpPost]
        public ActionResult EliminarViatura(Viatura viatura)
        {
            if (DalViaturas.Apagar(viatura))
                TempData["sucesso"] = "Viatura eliminada com sucesso!";
            else
                TempData["falha"] = "Ocorreu um erro ao eliminar a viatura. Tente novamente mais tarde!";
            return RedirectToAction(nameof(Index));
        }
        #endregion
        [Authorize(Roles = "DISPONIBILIZAR_VIATURAS")]
        public ActionResult DisponibilizarViatura(string m)
        {
            if (m == null || m == "" || m == "rowId")
            {
                TempData["falha"] = "Selecione a viatura que deseja disponibilizar!";
                return RedirectToAction(nameof(Index));
            }
            string estado = DalViaturas.Estado(m);
            if(estado.ToUpper().Equals("DISPONIVEL"))
                TempData["falha"] = "A viatura já se encontra disponível para uso.";
            else
                if (DalViaturas.AlterarEstado(m, "Disponivel"))
                    TempData["sucesso"] = "Viatura " + m + " disponível para uso!";

            return RedirectToAction(nameof(Index));
        }
        
        [Authorize(Roles = "INDISPONIBILIZAR_VIATURAS")]
        public ActionResult IndisponibilizarViatura(string m)
        {
            if (m == null || m == "" || m == "rowId")
            {
                TempData["falha"] = "Selecione a viatura que deseja indisponibilizar!";
                return RedirectToAction(nameof(Index));
            }
            string estado = DalViaturas.Estado(m);
            if (estado.ToUpper().Equals("INDISPONIVEL"))
                TempData["falha"] = "A viatura já se encontra indisponível para uso.";
            else
                if (DalViaturas.AlterarEstado(m, "Indisponivel"))
                    TempData["sucesso"] = "Viatura " + m + " indisponível para uso!";

            return RedirectToAction(nameof(Index));
        }

        #region FETCH DATA
        [HttpPost]
        public ActionResult CarregarModelosPelaMarca(string mrc)
        {
            List<ViaturaHelper> modelos = DalViaturas.GetViaturaHelpersPeloIdMarca(mrc);
            SelectList list = new SelectList(modelos, "id", "descricao", 0);
            return Json(list);
        }
        [HttpPost]
        public ActionResult CarregarViaturasPeloModelo(string modelo)
        {
            var viaturas = DalViaturas.GetViaturaPeloModelo(modelo);
            SelectList list = new SelectList(viaturas, "matricula", "matricula", 0);
            return Json(list);
        }
        [HttpPost]
        public ActionResult CarregarValor(string id)
        {
            var valor = DalViaturas.GetViaturaPelaMatricula(id).valorAluguer;
            return Json(valor);
        }
        #endregion

    }
}