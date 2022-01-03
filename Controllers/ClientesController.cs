using RentAndDrive.Models.Pessoas.DAL;
using RentAndDrive.Models.Pessoas.Mdl.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentAndDrive.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        #region CRUD
        // GET: Cliente
        [Authorize(Roles = "CONSULTAR_CLIENTES")]
        public ActionResult Index()
        {
            return View(DalCliente.CarregarListaDeClientes());
        }

        [HttpGet]
        [Authorize(Roles = "REGISTAR_CLIENTES")]
        public ActionResult RegistarCliente()
        {
            return View(new VwCliente());
        }

        [HttpPost]
        public ActionResult RegistarCliente(VwCliente model)
        {
            if (DalCliente.Registar(model))
                TempData["sucesso"] = "Cliente registado com sucesso!";
            else
                TempData["falha"] = "Ocorreu um erro durante o registo do Cliente. Tente novamente mais tarde!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "EDITAR_CLIENTES")]
        public ActionResult EditarCliente(string id)
        {
            if (id == null || id == "rowId")
            {
                TempData["falha"] = "Selecione o Cliente que deseja editar.";
                return RedirectToAction(nameof(Index));
            }
            var pessoa = DalPessoa.GetPessoaPeloId(id);
            var morada = DalPessoa.GetMoradaPessoa(pessoa.morada);
            var dadosBancarios = DalPessoa.GetDadosBancariosPessoa(pessoa.dadosBancarios.GetValueOrDefault());
            VwCliente data = new VwCliente()
            {
                id = pessoa.id,
                nome = pessoa.nome,
                morada = pessoa.morada,
                sexo = pessoa.sexo,
                estadoCivil = pessoa.estadoCivil,
                tipoDeIdentificacao = pessoa.tipoDeIdentificacao,
                numeroIdentificacao = pessoa.numeroIdentificacao,
                numeroCarta = pessoa.numeroCarta,
                codigoCarta = pessoa.codigoCarta,
                validadeCarta = pessoa.validadeCarta,
                rua = morada.rua,
                avenida = morada.avenida,
                quarteirao = Convert.ToInt32(morada.quarteirao)
            };

            return View(data);
        }

        [HttpPost]
        public ActionResult EditarCliente(VwCliente model)
        {
            if (DalCliente.Editar(model))
                TempData["sucesso"] = "Cliente actualizado com sucesso!";
            else
                TempData["falha"] = "Ocorreu um erro durante a actualização do Cliente. Tente novamente mais tarde!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "ELIMINAR_CLIENTES")]
        public ActionResult EliminarCliente(string id)
        {
            if (id == null || id == "rowId")
            {
                TempData["falha"] = "Selecione o Cliente que deseja eliminar.";
                return RedirectToAction(nameof(Index));
            }
            var data = DalCliente.CarregarClientePeloId(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult EliminarCliente(VwCliente model)
        {
            if (DalCliente.Apagar(model.id))
                TempData["sucesso"] = "Cliente eliminado com sucesso!";
            else
                TempData["falha"] = "Ocorreu um erro durante a eliminação do Cliente. Tente novamente mais tarde!";
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}