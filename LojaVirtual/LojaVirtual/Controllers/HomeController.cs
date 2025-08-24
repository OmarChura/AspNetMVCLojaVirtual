using LojaVirtual.Database;
using LojaVirtual.Libraries.Email;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LojaVirtual.Controllers
{
    public class HomeController : Controller
    {
        private LojaVirtualContext _banco;

        public HomeController(LojaVirtualContext banco)
        {
            _banco = banco;
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
                _banco.NewletterEmails.Add(newletterEmail);
                _banco.SaveChanges();

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
                    ContatoEmail.EnviarContatoPorEmail(contato);
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
                ViewData["MSG_E"] = "OPps! tivemos um erro, tente novamente mais tartde!";

                //TODO - Implementar Log
            }
            

            return View("Contato");
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult CadastroCliente()
        {
            return View();
        }

        public IActionResult CarrinhoCompras()
        {
            return View();
        }
    }
}
