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
        [Authorize(Roles = "CONSULTAR_ALUGUERES")]
        public ActionResult Index()
        {
            return View(DalAluguer.CarregarAluguers().Where(a => a.tipo.ToUpper() == "ALUGUER"));
        }

        [HttpGet]
        [Authorize(Roles = "REGISTAR_ALUGUER")]
        public ActionResult Registar()
        {
            // Marcas das viaturas
            ViewData["Marcas"] = DalViaturas.GetViaturaHelpersPeloTipo("Marca");
            return View(new VwAluguerReserva());
        }

        [HttpPost]
        public ActionResult Registar(VwAluguerReserva aluguer)
        {
            aluguer.idFuncionario = Session["idFuncionario"] as string;
            aluguer.valor = DalViaturas.GetViaturaPelaMatricula(aluguer.idViatura).valorAluguer;
            if (DalAluguer.Registar(aluguer, "ALUGUER", DateTime.Now.Date))
            {
                if(DalViaturas.AlterarEstado(aluguer.idViatura, "Ocupado"))
                    TempData["sucesso"] = "Aluguer registada com sucesso!";
            }
            else
                TempData["falha"] = "Ocorreu um erro ao registar o aluguer. Tente novamente mais tarde!";
            return RedirectToAction(nameof(Index));
        }
        
        //[HttpGet]
        //[Authorize(Roles = "CONSULTAR_ALUGUER_ACTIVO")]
        //public ActionResult AlguerActivo()
        //{
        //    return View(DalAluguer.CarregarAluguers().Where(a => a.estado == "Activo").ToList());
        //}

        [HttpGet]
        [Authorize(Roles = "CANCELAR_ALUGUER")]
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
        [Authorize(Roles = "TERMINAR_O_SERVICO_DE_ALUGUER")]
        public ActionResult Terminar(string al)
        {
            if(al == null || al == "" || al == "rowId")
            {
                TempData["falha"] = "Selecione o aluguer que deseja terminar!";
                return RedirectToAction(nameof(Index));
            }
            if (DalAluguer.Devolver(al))
                TempData["sucesso"] = "Aluguer terminado com sucesso!";
            else
                TempData["falha"] = "Ocorreu um erro ao terminar o aluguer. Tente novamente mais tarde!";
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region RESERVA
        public ActionResult Reserva()
        {
            return View(DalAluguer.CarregarAluguers().Where(a => a.tipo.ToUpper() == "RESERVA"));
        }

        public ActionResult RegistarReserva()
        {
            // Marcas das viaturas
            ViewData["Marcas"] = DalViaturas.GetViaturaHelpersPeloTipo("Marca");
            return View(new VwAluguerReserva());
        }

        [HttpPost]
        public ActionResult RegistarReserva(VwAluguerReserva aluguer)
        {
            aluguer.idFuncionario = Session["idFuncionario"] as string;
            aluguer.valor = DalViaturas.GetViaturaPelaMatricula(aluguer.idViatura).valorAluguer;
            if (DalAluguer.Registar(aluguer, "RESERVA", aluguer.dataAluguer))
                TempData["sucesso"] = "Reserva registada com sucesso!";
            else
                TempData["falha"] = "Ocorreu um erro ao registar a reserva. Tente novamente mais tarde!";
            return RedirectToAction(nameof(Reserva));
        }

        public ActionResult RegistrarEntrega(string al)
        {
            if (al == null || al == "" || al == "rowId")
            {
                TempData["falha"] = "Selecione a reserva que deseja registar a entrega!";
                return RedirectToAction(nameof(Reserva));
            }
            if (DalAluguer.Devolver(al))
                TempData["sucesso"] = "Viatura entregue !";
            else
                TempData["falha"] = "Ocorreu um erro ao registar a entrega da viatura. Tente novamente mais tarde!";
            return RedirectToAction(nameof(Reserva));
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