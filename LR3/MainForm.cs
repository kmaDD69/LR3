using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LR3.UsersClasses;
using static LR3.UsersClasses.InfoEmailSending;

namespace LR3
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            textBoxEmail.Text = "task_code_development@list.ru";
            textBoxName.Text = "Антон Паокин";
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            //проверка TextBox на наличие значений
            if (string.IsNullOrWhiteSpace(textBoxEmail.Text) ||
            string.IsNullOrWhiteSpace(textBoxName.Text) ||
            string.IsNullOrWhiteSpace(textBoxSubject.Text) ||
            string.IsNullOrWhiteSpace(textBoxBody.Text))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            // Ввод данных с формы в объекты ранее созданных классов 
            string smtp = "smtp.mail.ru";
            // Необходимо ввести свой mail.ru адрес!!! И своё ФИО 
            StringPair fromInfo = new StringPair("katenasovamail364@mail.ru", "Катя Катерина");
            // Необходимо ввести свой пароль который вывел mail.ru !!!
            string password = "pvEpDMiNbLwfwa9Z7heP";

            StringPair toInfo = new StringPair(textBoxEmail.Text, textBoxName.Text);
            string subject = textBoxSubject.Text;
            string body = $"{DateTime.Now} \n" +
            $"{Dns.GetHostName()} \n" +
            $"{Dns.GetHostAddresses(Dns.GetHostName()).First()} \n" +
            $"{textBoxBody.Text}";

            InfoEmailSending info =
            new InfoEmailSending(smtp, fromInfo, password, toInfo, subject, body);
            //Отправка данных в виде электронного письма
            SendingEmail sendingEmail = new SendingEmail(info);
            sendingEmail.Send();

            //Уведомления для пользователя и очистка всех TextBox 
            MessageBox.Show("Письмо отправлено!");
            foreach (TextBox textBox in Controls.OfType<TextBox>())
                textBox.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
