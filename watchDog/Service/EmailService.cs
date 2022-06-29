using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace WatchDog.Service
{

    class EmailService
    {
        public bool emailForWebsiteDownIsSent;

        const string from = "smc.developer@outlook.com";
        const string to = "rg00749943@edu.mon.bg";

        const string username = "smc.developer@outlook.com";
        const string password = "***********";

        const string host = "smtp.office365.com";
        const int port = 587;

        public void SendEmailForDownServer()
        {
            var EmailClient = new SmtpClient(host, port)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true
            };

            EmailClient.Send(from, to, "Test", "Test");
        }

        public void SendEmailForUpServer()
        {
            var EmailClient = new SmtpClient(host, port)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true
            };

            EmailClient.Send(from, to, "Test", "Test");
        }

    }
}
