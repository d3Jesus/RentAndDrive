using RentAndDrive.Models.Pessoas.DAL;
using RentAndDrive.Models.Pessoas.Mdl;
using RentAndDrive.Models.Pessoas.Mdl.Funcionario;
using RentAndDrive.Models.Users.Mdl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentAndDrive.Controllers
{
    [Authorize]
    public class FuncionariosController : Controller
    {

        #region CRUD
        // GET: Funcionarios
        public ActionResult Index()
        {
            var data = DalFuncionarios.CarregarFuncionarios();
            return View(data);
        }

        [HttpGet]
        public ActionResult RegistarFuncionario()
        {
            return View(new VwUserData());
        }

        [HttpPost]
        public ActionResult RegistarFuncionario(VwUserData model)
        {
            if (DalFuncionarios.Registar(model))
                TempData["sucesso"] = "Funcionário registado com sucesso!";
            else
                TempData["falha"] = "Ocorreu um erro durante o registo do funcionário. Tente novamente mais tarde!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult EditarFuncionario(string id)
        {
            if (id == null || id == "rowId")
            {
                TempData["falha"] = "Selecione o funcionário que deseja editar.";
                return RedirectToAction(nameof(Index));
            }
            var pessoa = DalPessoa.GetPessoaPeloId(id);
            var morada = DalPessoa.GetMoradaPessoa(pessoa.morada);
            var dadosBancarios = DalPessoa.GetDadosBancariosPessoa(pessoa.dadosBancarios.GetValueOrDefault());
            VwUserData data = new VwUserData()
            {
                id = pessoa.id,
                nome = pessoa.nome,
                morada = pessoa.morada,
                sexo = pessoa.sexo,
                estadoCivil = pessoa.estadoCivil,
                funcao = pessoa.funcao,
                tipoDeIdentificacao = pessoa.tipoDeIdentificacao,
                numeroIdentificacao = pessoa.numeroIdentificacao,
                numeroCarta = pessoa.numeroCarta,
                codigoCarta = pessoa.codigoCarta,
                validadeCarta = pessoa.validadeCarta,
                salario = pessoa.salario.GetValueOrDefault(),
                dadosBancarios = pessoa.dadosBancarios.GetValueOrDefault(),
                rua = morada.rua,
                avenida = morada.avenida,
                quarteirao = Convert.ToInt32(morada.quarteirao),
                banco = dadosBancarios.banco,
                numeroConta = dadosBancarios.numeroConta,
                nib = dadosBancarios.nib,
                titular = dadosBancarios.titular
            };

            return View(data);
        }

        [HttpPost]
        public ActionResult EditarFuncionario(VwUserData model)
        {
            if (DalFuncionarios.Editar(model))
                TempData["sucesso"] = "Funcionário actualizado com sucesso!";
            else
                TempData["falha"] = "Ocorreu um erro durante a actualização do funcionário. Tente novamente mais tarde!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult EliminarFuncionario(string id)
        {
            if (id == null || id == "rowId")
            {
                TempData["falha"] = "Selecione o funcionário que deseja eliminar.";
                return RedirectToAction(nameof(Index));
            }
            var data = DalFuncionarios.GetFuncionarioResumo(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult EliminarFuncionario(Pessoa model)
        {
            if (DalFuncionarios.Apagar(model.id))
                TempData["sucesso"] = "Funcionário eliminado com sucesso!";
            else
                TempData["falha"] = "Ocorreu um erro durante a eliminação do funcionário. Tente novamente mais tarde!";
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Credendiais
        public ActionResult Credenciais(string id)
        {
            if (id == null || id == "rowId")
            {
                TempData["falha"] = "Selecione o funcionario para que possa avançar.";
                return RedirectToAction(nameof(Index));
            }
            var data = DalFuncionarios.GetFuncionarioResumo(id);
            if (data.funcao.ToUpper().Equals("MOTORISTA"))
            {
                TempData["falha"] = "Um motorista não tem acesso ao sistema.";
                return RedirectToAction(nameof(Index));
            }
            return View(data);
        }
        [HttpGet]
        public ActionResult ActivarFuncionario(string id)
        {
            if (DalFuncionarios.ActivarDesactivarCredenciais(id, "Activo"))
                TempData["sucesso"] = "Acesso activado com sucesso!";
            else
                TempData["falha"] = "Ocorreu um erro ao activar o acesso do funcionário. Tente novamente mais tarde!";
            return RedirectToAction(nameof(Credenciais), routeValues: new { id = id });
        }
        [HttpGet]
        public ActionResult DesactivarFuncionario(string id)
        {
            if (DalFuncionarios.ActivarDesactivarCredenciais(id, "Inactivo"))
                TempData["sucesso"] = "Acesso desactivado com sucesso!";
            else
                TempData["falha"] = "Ocorreu um erro ao desactivar o acesso do funcionário. Tente novamente mais tarde!";
            return RedirectToAction(nameof(Credenciais), routeValues: new { id = id });
        }
        #endregion
    }
}