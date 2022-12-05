using Practice.Componens;
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

namespace Practice.MyPages
{
    /// <summary>
    /// Логика взаимодействия для EditingPage.xaml
    /// </summary>
    public partial class EditingPage : Page
    {
        Componens.Product product; 
        public EditingPage(Componens.Product _product)
        {
            InitializeComponent();
            product = _product;
            DataContext = product;

        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            BdConect.db.Product.Add(product);
            BdConect.db.SaveChanges();
            MessageBox.Show("yes");
        }
        public void Update() 
        {
        }
    }
}
