using Practice.Componens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для OrderSavepage.xaml
    /// </summary>
    public partial class OrderSavepage : Page
    {
        Componens.Order order;
        public OrderSavepage()
        {
          
            InitializeComponent();
            //order = _order;
            ProductListViu.ItemsSource = BdConect.db.Product.ToList();
        }
        public static void refrashe()
        {
        
        }

        private void ViborBt_Click(object sender, RoutedEventArgs e)
        {
            if (ProductListViu.SelectedItem == null)
                return;
            Name.Text = (ProductListViu.SelectedItem as Product).Name;
        }
    }
}
