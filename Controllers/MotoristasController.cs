using RentAndDrive.Models.Pessoas.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentAndDrive.Controllers
{
    [Authorize]
    public class MotoristasController : Controller
    {
        // GET: Motoristas
        [Authorize(Roles = "CONSULTAR_MOTORISTAS")]
        public ActionResult Index()
        {
            return View(DalFuncionarios.CarregarFuncionariosPelaFuncao("Motorista"));
        }

        [Authorize(Roles = "CONSULTAR_VIATURAS_ATRIBUIDAS_AOS_MOTORISTAS")]
        public ActionResult ViaturasAtribuidas(string id)
        {
            if (id == null || id == "rowId")
            {
                TempData["falha"] = "Selecione o motorista para proceder.";
                return RedirectToAction(nameof(Index));
            }
            var data = DalFuncionarios.GetFuncionarioResumo(id);
            TempData["Id"] = data.id;
            TempData["Motorista"] = data.nome;
            return View(DalFuncionarios.ListaDeViaturasAtribuidasAoMotorista(id));
        }

        [HttpGet]
        [Authorize(Roles = "ATRIBUIR_VIATURAS_AOS_MOTORISTAS")]
        public ActionResult AtribuirViaturas(string id)
        {
            string nome = TempData["Motorista"] as string;
            TempData["Motorista"] = nome;
            TempData["IdMotorista"] = id;
            var data = DalFuncionarios.ListaDeViaturasNaoAtribuidasAoMotorista(id);
            return View(data);
        }
        [HttpPost]
        public ActionResult AtribuirViaturas(string id, string[] selectedVehicles)
        {
            if (selectedVehicles != null)
            {
                DalFuncionarios.AtribuirViaturasAoMotorista(id, selectedVehicles);

                TempData["sucesso"] = " " + selectedVehicles.Length + " Viatura(s) adicionada(s) com sucesso.";
            }
            else
            {
                TempData["falha"] = " Nenhuma viatura foi selecionada!";
            }
            return RedirectToAction(nameof(ViaturasAtribuidas), new { id = id });
        }

        [HttpGet]
        [Authorize(Roles = "REMOVER_VIATURAS_ATRIBUIDAS_AOS_MOTORISTAS")]
        public ActionResult RemoverViaturas(string id)
        {
            string nome = TempData["Motorista"] as string;
            TempData["Motorista"] = nome;
            TempData["IdMotorista"] = id;
            var data = DalFuncionarios.ListaDeViaturasAtribuidasAoMotorista(id);
            return View(data);
        }
        [HttpPost]
        public ActionResult RemoverViaturas(string id, string[] selectedVehicles)
        {
            if (selectedVehicles != null)
            {
                DalFuncionarios.RemoverViaturasAoMotorista(id, selectedVehicles);

                TempData["sucesso"] = " " + selectedVehicles.Length + " Viatura(s) removida(s) com sucesso.";
            }
            else
            {
                TempData["falha"] = " Nenhuma viatura foi selecionada!";
            }
            return RedirectToAction(nameof(ViaturasAtribuidas), new { id = id });
        }
    }
}