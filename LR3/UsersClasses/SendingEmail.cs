using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Net.Http;
using System.Xml.Linq;

namespace LR3.UsersClasses
{
    public class SendingEmail
    {
        private InfoEmailSending InfoEmailSending { get; set; }

        public SendingEmail(InfoEmailSending infoEmailSending)
        {
            InfoEmailSending = infoEmailSending
            ?? throw new ArgumentNullException(nameof(infoEmailSending));
        }

        public void Send()
        {
            // Добавляем обработку исключений
            try
            {
                // Вносим адрес ЅМТР сервера
                SmtpClient mySmtpClient =
                new SmtpClient(InfoEmailSending.SmtpClientAdress);
                // Задаем учётные данные пользователя 
                mySmtpClient.UseDefaultCredentials = false;
                // Включаем ипользование портокола SSl
                mySmtpClient.EnableSsl = true;
                // Задаём учётные данные пользователя 
                NetworkCredential basicAuthenticationInfo = new
                NetworkCredential(
                InfoEmailSending.EmailAdressFrom.EmailAdress,
                InfoEmailSending.EmailPassword);
                mySmtpClient.Credentials = basicAuthenticationInfo;
                // Добавляем адрес откуда отправлнено сообщение 
                MailAddress from = new MailAddress(
                InfoEmailSending.EmailAdressFrom.EmailAdress,
                InfoEmailSending.EmailAdressFrom.Name);
                // Добавляем адрес куда будет отправлнено сообщение
                MailAddress to = new MailAddress(
                InfoEmailSending.EmailAdressTo.EmailAdress,
                InfoEmailSending.EmailAdressTo.Name);
                MailMessage myMail = new MailMessage(from, to);
                // Добавляем наш адрес в список адресов для ответа
                MailAddress replyTo =
                new MailAddress(InfoEmailSending.EmailAdressFrom.EmailAdress);
                myMail.ReplyToList.Add(replyTo);
                // Выбираем кодировку символов в письме
                //В нашем случае UTF8
                Encoding encoding = Encoding.UTF8;
                // Задаем значение Заголовка и его кодировку
                myMail.Subject = InfoEmailSending.Subject;
                myMail.SubjectEncoding = encoding;
                // Задаем значение Сообщения и его кодировку 
                myMail.Body = InfoEmailSending.Body;
                myMail.BodyEncoding = encoding;
                // Отправляем письмо
                mySmtpClient.Send(myMail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
