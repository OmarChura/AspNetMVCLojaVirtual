using LojaVirtual.Database;
using LojaVirtual.Libraries.Email;
using LojaVirtual.Libraries.Filtro;
using LojaVirtual.Libraries.Login;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LojaVirtual.Controllers
{
    public class HomeController : Controller
    {
        private IClienteRepository _repositoryCliente;
        private INewsletterRepository _newsletterRepository;
        private LoginCliente _loginCliente;
        private GerenciarEmail _gerenciarEmail;

        public HomeController(IClienteRepository repository, INewsletterRepository newsletterRepository, LoginCliente loginCliente, GerenciarEmail gerenciarEmail)
        {
            _repositoryCliente = repository;
            _newsletterRepository = newsletterRepository;
            _loginCliente = loginCliente;
            _gerenciarEmail = gerenciarEmail;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index([FromForm]NewletterEmail newletterEmail)
        {
            //TODO - validações
            if (ModelState.IsValid) 
            {
                //TODO - Adição no banco de dados
                _newsletterRepository.Cadastrar(newletterEmail);
                /*
                _banco.NewletterEmails.Add(newletterEmail);
                _banco.SaveChanges();
                */
                TempData["MSG_S"] = "E-mail cadastrado! ";
                
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }

        }

        public IActionResult Contato()
        {
            return View();
        }

        public IActionResult ContatoAcao()
        {
            try 
            {
                Contato contato = new Contato();
                contato.Nome = HttpContext.Request.Form["nome"];
                contato.Email = HttpContext.Request.Form["email"];
                contato.Texto = HttpContext.Request.Form["texto"];

                var listaMensagens = new List<ValidationResult>();
                var contexto = new ValidationContext(contato);
                bool isValid = Validator.TryValidateObject(contato, contexto, listaMensagens, true);

                if (isValid)
                {
                    _gerenciarEmail.EnviarContatoPorEmail(contato);
                    /*
                    return new ContentResult() { Content = string.Format("Dados recebidos <br/> Nome: {0} <br/> Email: {1} <br/> Texto: {2}",contato.Nome, contato.Email,contato.Texto), ContentType = "text/html"
                    };*/
                    ViewData["MSG_S"] = "Mesagem de contato enviado com sucesso";
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var texto in listaMensagens)
                    {
                        sb.Append(texto.ErrorMessage + "<br/>");
                    }
                    ViewData["MSG_E"] = sb.ToString();
                    ViewData["CONTATO"] = contato;
                }
            }
            catch(Exception ex)  
            {
                ViewData["MSG_E"] = "Opps! tivemos um erro, tente novamente mais tartde!";

                //TODO - Implementar Log
            }
            

            return View("Contato");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login([FromForm]Cliente cliente)
        {
            Cliente clienteDB = _repositoryCliente.Login(cliente.Email, cliente.Senha);
            if(clienteDB != null)
            {
                _loginCliente.Login(clienteDB);

                return new RedirectResult(Url.Action(nameof(Painel)));
            }
            else
            {
                ViewData["MSG_E"] = "Usuário não encontrado, e-mail ou senha incorreta";
                return View();
            }
            /*if(cliente.Email == "1234@1234" && cliente.Senha == "1234")
            {
                //fazer consulta banco de dados email,senha
                //armazenar essa sesao, na sessao(cliente)
                HttpContext.Session.Set("ID", new byte[] { 51 });
                HttpContext.Session.SetString("email", cliente.Email);
                HttpContext.Session.SetInt32("CPF", 12345498);
                return new ContentResult() { Content="logado"};
                //return RedirectToAction(nameof(Index));
            }
            else
            {
                return new ContentResult() { Content = "nao logado" };
            }*/
        }

        [HttpGet]
        [ClienteAutorizacao]
        public IActionResult Painel()
        {
            return new ContentResult() {Content = "este é o painel do cliente"};


            /*
            byte[] UsuarioId;
            if(HttpContext.Session.TryGetValue("ID", out UsuarioId))
            {
                return new ContentResult() { Content = "acesso concedido" + UsuarioId[0] + ". email" + HttpContext.Session.GetString("email") };
            }
            else
            {
                return new ContentResult() { Content = "acesso negado" };
            }*/

        }

        [HttpGet]
        public IActionResult CadastroCliente()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CadastroCliente([FromForm]Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                //TODO - Grava no banco
                _repositoryCliente.Cadastrar(cliente);

                TempData["MSG_S"] = "cadastro realizado com sucesso!";

                //TODO - implementar redirecionamentos diferentes (painel, carrinho de compras, etc)
                return RedirectToAction(nameof(CadastroCliente));
            }

            return View();
        }

        public IActionResult CarrinhoCompras()
        {
            return View();
        }
    }
}
