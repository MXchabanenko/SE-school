using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using WebApplication2.___Repository___;
using WebApplication2.IServices;

namespace WebApplication2.Services
{
    public class SubscriptionService : ISubsriptionService
    {
        private MyRepository myRepository { get; set; }
        private CourseService courseService { get; set; }

        public SubscriptionService()
        {
            courseService = new CourseService();
            myRepository = new MyRepository();
        }
        public void Subscribe(string email)
        {
            if (!new EmailAddressAttribute().IsValid(email))
                throw new Exception("Invalid email");

            var list = myRepository.GetAllEmails();

            if (!list.Contains(email))
                myRepository.AddEmail(email);
            else
                throw new Exception("Email already exists");
        }
        public void SendEmails()
        {
            var emailList = myRepository.GetAllEmails();
            Email(emailList, courseService.GetBitcoinToUAH().ToString());
        }
        private static void Email(List<string> to, string text)
        {

            MailMessage msg = new MailMessage();

            foreach (var email in to)
                msg.To.Add(email);

            MailAddress address = new MailAddress("batmen237@gmail.com");
            msg.From = address;
            msg.Subject = "Bitcoin course";
            msg.Body = text;

            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = false;
            client.Host = "relay-hosting.secureserver.net";
            client.Port = 25;

            NetworkCredential credentials = new NetworkCredential("batmen237@gmail.com", "********");
            client.UseDefaultCredentials = true;
            client.Credentials = credentials;

            client.Send(msg);

            //Ця команда відправляє емейли(працює ніби лише з gmail поштами та листи приходять з запізненням),
            //але я б не став надавати свій пароль від скриньки у коді (тому зараз метод не працює), тому замість справжнього пароля, який я використовував при тестуванні — зірочки)



        }
    }
}
