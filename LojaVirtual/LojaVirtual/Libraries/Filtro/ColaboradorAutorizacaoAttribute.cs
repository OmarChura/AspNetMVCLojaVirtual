using LojaVirtual.Libraries.Login;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LojaVirtual.Libraries.Filtro
{
    public class ColaboradorAutorizacaoAttribute : Attribute, IAuthorizationFilter
    {
        LoginColaborador _loginColaborador;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _loginColaborador = (LoginColaborador)context.HttpContext.RequestServices.GetService(typeof(LoginColaborador));
            Colaborador colaborador = _loginColaborador.GetColaborador();
            if (colaborador == null)
            {
                //context.Result = new ContentResult() { Content = "acesso negado" };
                context.Result = new RedirectToActionResult("Login","Home",null);
            }
        }
    }
}
