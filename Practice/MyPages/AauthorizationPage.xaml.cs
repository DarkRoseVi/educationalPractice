using System;
using System.Collections.Generic;
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
using System.Windows.Threading;
using Practice.Componens;
using Practice.MyPages;
using Practice.Properties;

namespace Practice.MyPages
{
    /// <summary>
    /// Логика взаимодействия для AauthorizationPage.xaml
    /// </summary>
    public partial class AauthorizationPage : Page
    {
        DispatcherTimer timer = new DispatcherTimer();
        public AauthorizationPage()
        {
            InitializeComponent();
            if (Properties.Settings.Default.Login !=null)
                LoginTb.Text = Properties.Settings.Default.Login;
            if (Properties.Settings.Default.Password != null)
            PasswordTb.Text = Properties.Settings.Default.Password;
          

        }

        private void EntranceBtn_Click(object sender, RoutedEventArgs e)
        {
            int CountAuto = Properties.Settings.Default.CountAuth;
            string login = LoginTb.Text.Trim();
            string password = PasswordTb.Text.Trim();
            if (CountAuto < 3)
            {
                if (login.Length == 0 && password.Length == 0)
                {
                    MessageBox.Show("Заполните поля!");
                }
                else
                {
                    Navigation.AutoUser = BdConect.db.User.ToList().Find(x => x.Login == login && x.Password == password);
                    if (Navigation.AutoUser == null)
                    {
                        MessageBox.Show("Такого пользователя нет ");
                        CountAuto += 1;
                        Properties.Settings.Default.CountAuth = CountAuto;

                    }
                    else
                    {
                        if (RememberCh.IsChecked == true)
                        {
                            Properties.Settings.Default.Login = LoginTb.Text;
                            Properties.Settings.Default.Password = PasswordTb.Text;
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            Properties.Settings.Default.Login = null;
                            Properties.Settings.Default.Password = null;
                            Properties.Settings.Default.Save();
                        }
                        CountAuto = 0;
                        Navigation.IsUser = true;
                        Navigation.NextPage(new Nav("Продукты", new ListproductPage()));
                    
                    }
                   
                   
                }

            }
            else 
            {
                MessageBox.Show("Вы ввели 3 раза неправильный пароль\nВход заблокировани на 1 минуту", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                CountAuto = 0;
                EntranceBtn.IsEnabled = false;
                RegisterBtn.IsEnabled = false;
                timer.Interval = new TimeSpan(0, 0, 30);
                timer.Tick += new EventHandler(isVisibleBTN);
                timer.Start();
            }
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("Регистрация",new  RegistrationPage()));
        }
        private void isVisibleBTN(object sender, EventArgs e)
        {
            EntranceBtn.IsEnabled = true;
            RegisterBtn.IsEnabled = true;
            timer.Stop();
        }
    }
}
