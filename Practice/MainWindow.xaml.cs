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
using Practice.Componens;
using Practice.MyPages;

namespace Practice
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Navigation.main = this;
            Navigation.NextPage(new Nav("Авторизация", new AauthorizationPage()));
            
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.IsUser = false;
            Navigation.navs.Clear();
            Navigation.NextPage(new Nav("Авторизация", new AauthorizationPage()));
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.BackPage();
        }
    }
}
