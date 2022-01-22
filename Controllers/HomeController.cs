using RentAndDrive.Models.Alugueres.DAL;
using RentAndDrive.Models.Users.DAL;
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

        [Authorize]
        public ActionResult Dashboard()
        {
            ViewBag.TotalUsers = UsersDal.GetAllUsers().Count;
            ViewBag.TotalRent = DalAluguer.CarregarAluguers().Count;
            ViewBag.PendingRent = DalAluguer.CarregarAluguersPeloEstado("Activo").Count;
            ViewBag.Earnings = DalAluguer.CarregarTotalGanhoAnual();
            return View();
        }
    }
}