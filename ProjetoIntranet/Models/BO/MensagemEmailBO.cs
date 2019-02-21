using ProjetoIntranet.Models.Entity;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;


namespace ProjetoIntranet.Models.BO
{
    public class MensagemEmailBO
    {

        public void EnviarMsg(MensagemEmail obj)
        {
            
            try
            {
                MailMessage msg = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                MailAddress from = new MailAddress(obj.Email.ToString());
                StringBuilder sb = new StringBuilder();
                msg.To.Add("gabriel.knight.srs@gmail.com");
                msg.From = from;
                msg.Subject = "Teste de Envio";
                msg.IsBodyHtml = false;
                smtp.Host = "smtp.gmail.com";
                smtp.Port =465 ;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential("gabriel.knight.srs@gmail.com","gabrielgts250nv");
                sb.Append("Nome: " + obj.Nome);
                sb.Append(Environment.NewLine);
                sb.Append("Fone: " + obj.Fone);
                sb.Append(Environment.NewLine);
                sb.Append("Email: " + obj.Email);
                sb.Append(Environment.NewLine);
                sb.Append("Mensagem: " + obj.Msg);

                msg.Body = sb.ToString();

                
                var tEmail = new Thread(() => smtp.Send(msg));
                tEmail.Start();

                msg.Dispose();
               
               
            }
            catch 
            {

                throw ;
            }


        }

    }
}