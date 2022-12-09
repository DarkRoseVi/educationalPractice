using Practice.Componens;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Practice.MyPages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }
      

        private void RegBt_Click(object sender, RoutedEventArgs e)
        {

            string login = LoginTb.Text.Trim();
            string password = PasswordTb.Password.Trim();
            string lastname = LastNameTb.Text.Trim();
            string fistname = FirstNameTb.Text.Trim();
            string patronymic = PatronymicTb.Text.Trim();
            string password2 = Password2Tb.Password.Trim();

            var check = BdConect.db.User.Where(x => x.Login == login).FirstOrDefault();
            if (check==null)
            {
                if (login.Length > 0 && password.Length > 0 && password2.Length > 0)
                {
                   
                    if (BdConect.db.User.Local.Any(x => x.Login == login))
                    {
                        MessageBox.Show("Такой пользователль уже существует ");
                        return;
                    }
                    else
                    { 
                        BdConect.db.User.Add(new User
                        {
                            Login = login,
                            Password = password,
                            LastName = lastname,
                            FirstName = fistname,
                            Patronymic = patronymic
                        });
                        //string[] datalogin = LoginTb.Text.Split('@');
                        //if (datalogin.Length == 2)
                        //{
                        //    string[] datalogin2 = datalogin[1].Split('.');
                        //    if (datalogin2.Length == 2)
                        //    {
                        //        MessageBox.Show("Правильный логин");
                        //    }
                        //    else MessageBox.Show("Укажите логин в форме n@n.n");
                        //}
                        //else MessageBox.Show("Укажите логин в форме n@n.n");

                        //проверка пороля 

                        if (password.Length >= 6)
                        {
                            bool IsallUpper = false;
                            bool symbo = false;
                            bool number = true;
                            for (int i = 0; i < password.Length; i++)
                            {
                                if (password[i] >= '0' && password[i] <= '9')
                                    number = true;
                                if (password[i] == '!' || password[i] == '@' || password[i] == '#' || password[i] == '$' || password[i] == '%' || password[i] == '^')
                                    symbo = true;
                                if (Char.IsUpper(password[i]))
                                    IsallUpper = true;
                            }
                            if (symbo != true)
                                MessageBox.Show("Добавьте один из символов !  @ % # $ ^");
                            if (number != true)
                                MessageBox.Show("Добавьте хотя бы 1 цифру ");
                            if (IsallUpper != true)
                                MessageBox.Show("Добавьте хотя бы 1 прописную букву ");
                        }
                        else
                        {
                            MessageBox.Show("Пароль должен быть больше 6 символов ");
                        }
                        if (password == password2)
                        {
                            MessageBox.Show("Пароли совпадают");
                        }
                        else
                        {
                            MessageBox.Show("Скорее всего пароли не совпадают, проверьте");
                        }


                        MessageBox.Show("Заполните поля");
                        BdConect.db.SaveChanges();


                    }

                    MessageBox.Show("Сохранено");


                }

            }
            else
            {
                MessageBox.Show("Такой логмин уже существует");
            }
            
         
        }

        private void DeletBt_Click(object sender, RoutedEventArgs e)
        {
             LoginTb.Text= " ";
            PasswordTb.Password = "";
           LastNameTb.Text = " ";
             FirstNameTb.Text = " ";
          PatronymicTb.Text=" ";
          Password2Tb.Password="";
        }
    }
}

