using System;
using System.Net;
using System.Net.Mail;
using Facware.Library.Utility.GlobalMessageHandling;

namespace Facware.Services.Email.Services
{
    public class MailManager
    {
        private string _server;
        private int _port;
        private string _user;
        private string _password;

        public MailManager(string server, int port, string user, string password)
        {
            _server = server;
            _port = port;
            _user = user;
            _password = password;
        }

        public GlobalMessage Send(string body) 
        {
            try
            {

                using (var mailMessage = new MailMessage())
                using (var client = new SmtpClient(_server, _port))
                {
                    // configure the client and send the message
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_user, _password);
                    client.EnableSsl = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;

                    // configure the mail message
                    mailMessage.From = new MailAddress($"contact@facware.com");
                    mailMessage.To.Insert(0, new MailAddress($"respinozabarboza@gmail.com"));
                    mailMessage.Bcc.Insert(0, new MailAddress($"respinozabarboza@gmail.com"));
                    mailMessage.Subject = $"Test";
                    mailMessage.Body = $"{body}";
                    mailMessage.IsBodyHtml = true;

                    client.Send(mailMessage);
                }
                var result = GlobalMessage.SuccessResult($"Email{true}");
                return result;
                // return true;
            }
            catch (Exception ex)
            {
                return GlobalMessage.FailResult($"fails {false}",ex);
            }
            
        }
    }
}
