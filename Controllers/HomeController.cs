using RentAndDrive.Models.Alugueres.DAL;
using RentAndDrive.Models.Pessoas.DAL;
using RentAndDrive.Models.Users.DAL;
using RentAndDrive.Models.Viaturas.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentAndDrive.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // recepcionist dashboard
        [Authorize]
        public ActionResult Dashboard()
        {
            ViewBag.ViaturasDispon = DalViaturas.GetViaturasDisponiveis().Count;
            ViewBag.Motoristas = DalFuncionarios.CarregarMotoristas().Count;
            ViewBag.TotalAluguer = DalAluguer.CarregarAluguers().Count;
            ViewBag.TotalReserva = DalAluguer.CarregarReservas().Count;
            return View();
        }

        // manager dashboard
        [Authorize]
        public ActionResult Dashboard2()
        {
            string func = Session["idFuncionario"] as string;
            ViewBag.ViaturasDispon = DalViaturas.GetViaturasDisponiveis().Count;
            ViewBag.Motoristas = DalFuncionarios.CarregarMotoristas().Count;
            ViewBag.TotalAluguer = DalAluguer.CarregarAluguers().Count;
            ViewBag.TotalReserva = DalAluguer.CarregarReservas().Count;

            ViewBag.TotalViaturasAlugadas = DalAluguer.CarregarAluguers().Count;
            ViewBag.TotalFunc = DalFuncionarios.CarregarFuncionarios().Count;
            ViewBag.TotalGanho = DalAluguer.CarregarTotalGanhoAnual();
            // aluguers cancelados
            ViewBag.TotalAluguerCancelados = DalAluguer.CarregarAluguersPeloEstadoETipo("Cancelado", "ALUGUER").Count;
            // reservas canceladas
            ViewBag.TotalReservasCanceladas = DalAluguer.CarregarAluguersPeloEstadoETipo("Cancelado", "RESERVA").Count;

            ViewBag.TotalClientes = DalCliente.CarregarListaDeClientes().Count;
            ViewBag.TotalUsers = UsersDal.GetAllUsers().Count;

            // anos para popular o grafico
            ViewBag.Ano1 = DateTime.Now.Year - 1;
            ViewBag.Ano2 = DateTime.Now.Year;

            return View();
        }

        public JsonResult FillBarChart(string ano)
        {
            var list = DalAluguer.FillBarChart(ano);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        // admin dashboard
        [Authorize]
        public ActionResult Dashboard3()
        {
            ViewBag.TotalRent = DalAluguer.CarregarAluguers().Count;
            ViewBag.PendingRent = DalAluguer.CarregarAluguersPeloEstado("Activo").Count;
            return View();
        }
    }
}