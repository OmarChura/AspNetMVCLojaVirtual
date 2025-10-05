using LojaVirtual.Models;
using System.Net;
using System.Net.Mail;

namespace LojaVirtual.Libraries.Email
{
    public class GerenciarEmail
    {
        
        //SMTP -> servidor q vai enviar a mensagem
        private SmtpClient _smtp;
        //config para obter email do site a partir do appsettings.json
        private IConfiguration _configuration;
        public GerenciarEmail(SmtpClient smtp, IConfiguration configuration)
        {
            _smtp = smtp;
            _configuration = configuration;
        }
        public void EnviarContatoPorEmail(Contato contato)
        {
            string corpoMsg = string.Format("<h2>Contato - Loja Virtual</h2>" +
                "<b>Nome:</b> {0} <br/>" +
                "<b>E-mail:</b> {1} <br/>" +
                "<b>Texto:</b> {2} <br/>" +
                "<br> Email enviado automaticamente do site LojaVirtual" ,
                contato.Nome,
                contato.Email,
                contato.Texto
                );

            /*
             MailMessage -> construir mensagem a ser enviada
             */
            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress(_configuration.GetValue<string>("Email:UserName"));
            mensagem.To.Add("propiomar@gmail.com");  
            mensagem.Subject = "Contato - LojaVirtual - E-mail: " + contato.Email;
            mensagem.Body = "";
            mensagem.IsBodyHtml = true;

            //Enviar mensagem via SMTP
            _smtp.Send(mensagem);
        }

        public void EnviarSenhaParaColaboradorPorEmail(Colaborador colaborador)
        {
            string corpoMsg = string.Format("<h2>Colaborador - Loja Virtual</h2>" +
                "<b>Sua senha é:</b> <br/>" +
                "<h3>{0}</h3> <br/>" +
                "<br> Email enviado automaticamente do site LojaVirtual",
                colaborador.Senha
                );

            /*
             MailMessage -> construir mensagem a ser enviada
             */
            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress(_configuration.GetValue<string>("Email:UserName"));
            mensagem.To.Add(colaborador.Email);
            mensagem.Subject = "colaborador - LojaVirtual - Senha do colaborador: " + colaborador.Nome;
            mensagem.Body = "";
            mensagem.IsBodyHtml = true;

            //Enviar mensagem via SMTP
            _smtp.Send(mensagem);
        }
    }
}
