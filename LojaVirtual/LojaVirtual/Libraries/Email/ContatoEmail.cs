using LojaVirtual.Models;
using System.Net;
using System.Net.Mail;

namespace LojaVirtual.Libraries.Email
{
    public class ContatoEmail
    { 
        public static void EnviarContatoPorEmail(Contato contato)
        {
            /*
             SMTP -> servidor q vai enviar a mensagem
             */
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.UseDefaultCredentials = false;

            //conta e senha gmail
            smtp.Credentials = new NetworkCredential("", "");
            //conexao segura
            smtp.EnableSsl = true;

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
            mensagem.From = new MailAddress("propiomar@gmail.com");
            mensagem.To.Add("propiomar@gmail.com");  
            mensagem.Subject = "Contato - LojaVirtual - E-mail: " + contato.Email;
            mensagem.Body = "";
            mensagem.IsBodyHtml = true;

            //Enviar mensagem via SMTP
            smtp.Send(mensagem);
        }
    }
}
