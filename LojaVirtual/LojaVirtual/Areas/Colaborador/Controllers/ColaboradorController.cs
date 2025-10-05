using LojaVirtual.Libraries.Email;
using LojaVirtual.Libraries.Filtro;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Libraries.Texto;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using X.PagedList;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    [ColaboradorAutorizacao("G")]
    public class ColaboradorController : Controller
    {
        private IColaboradorRepository _colaboradorRepository;
        private GerenciarEmail _gerenciarEmail;
        public ColaboradorController(IColaboradorRepository colaboradorRepository, GerenciarEmail gerenciarEmail) 
        {
            _colaboradorRepository = colaboradorRepository;
            _gerenciarEmail = gerenciarEmail;
        }
        public IActionResult Index(int? pagina)
        {
            IPagedList<Models.Colaborador> colaboradores = _colaboradorRepository.ObterTodosColaboradores(pagina);

            return View(colaboradores);
        }
        [httpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastrar([FromForm]Models.Colaborador colaborador)
        {
            ModelState.Remove("Senha");
            ModelState.Remove("ConfirmacaoSenha");
            ModelState.Remove("Tipo");
            //ModelState.Remove("Senha");
            if (ModelState.IsValid)
            {
                //TODO - Gerar senha aleatorio, salvar senha , enviar por email

                colaborador.Tipo = "C";
                colaborador.Senha = KeyGenerator.GetUniqueKey(8);

                _colaboradorRepository.Cadastrar(colaborador);
                //TODO -- enviar email
                //descomentar o enviar email quando prencher os dados do email e senha no appsettings.json
                //_gerenciarEmail.EnviarSenhaParaColaboradorPorEmail(colaborador);

                TempData["MSG_S"] = Mensagem.MSG_S001;

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [httpGet]
        public IActionResult GerarSenha(int id)
        {

            //TODO - Gerar senha aleatorio, salvar senha , enviar por email
            Models.Colaborador colaborador = _colaboradorRepository.ObterColaborador(id);
            colaborador.Senha = KeyGenerator.GetUniqueKey(8);
            _colaboradorRepository.AtualizarSenha(colaborador);
            //TODO  - enviar o email
            //descomentar o enviar email quando prencher os dados do email e senha no appsettings.json
            //_gerenciarEmail.EnviarSenhaParaColaboradorPorEmail(colaborador);

            TempData["MSG_S"] = Mensagem.MSG_S003;

            return RedirectToAction(nameof(Index));

        }
        [httpGet]
        public IActionResult Atualizar(int id)
        {
            Models.Colaborador colaborador = _colaboradorRepository.ObterColaborador(id);
            return View(colaborador);
        }
        [HttpPost]
        public IActionResult Atualizar([FromForm] Models.Colaborador colaborador, int id)
        {
            ModelState.Remove("Senha");
            ModelState.Remove("ConfirmacaoSenha");
            if (ModelState.IsValid)
            {
                // puxar a senha do colaborador para atualizar diminui a performance do aplicativo pois vai ter fazer duas requisições ao banco
                //o mlhor maneira é criar 2 novos metod um para atualizar senha e outro para atualizar email e nome do usuario

                _colaboradorRepository.Atualizar(colaborador);

                TempData["MSG_S"] = Mensagem.MSG_S001;

                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [httpGet]
        public IActionResult Excluir(int id)
        {
            _colaboradorRepository.Excluir(id);

            TempData["MSG_S"] = Mensagem.MSG_S001;

            return RedirectToAction(nameof(Index));

        }
    }
}
