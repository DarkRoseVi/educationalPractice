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
    /// Логика взаимодействия для ListproductPage.xaml
    /// </summary>
    public partial class ListproductPage : Page
    {
        int actualPage = 0;

        public ObservableCollection<Product> products
        {
            get { return (ObservableCollection<Product>)GetValue(productsProperty); }
            set { SetValue(productsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for products.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty productsProperty =
            DependencyProperty.Register("products", typeof(ObservableCollection<Product>), typeof(ListproductPage));



        public ListproductPage()
        {
            BdConect.db.Product.Load();
            products = BdConect.db.Product.Local;
            InitializeComponent();
        }
        public void Rechres()
        {
            ObservableCollection<Product> pr = new ObservableCollection<Product>();
            switch ((SortCb.SelectedItem as ComboBoxItem).Tag)
            {
                case "1":
                    pr = new ObservableCollection<Product>(products.OrderBy(x=>x.Name));
                    break;
                case "2":
                    pr = new ObservableCollection<Product>(products.OrderByDescending(x => x.Name));
                    break;
                case "3":
                    pr = BdConect.db.Product.Local;
                    break;
                default:
                    break;
            }

         
            if (QuantityCb.SelectedIndex>0 && pr.Count>0) 
            {
                int selCount = Convert.ToInt32((QuantityCb.SelectedItem as ComboBoxItem).Content);
                pr = new ObservableCollection<Product>( pr.Skip(selCount* actualPage).Take(selCount));
                if (pr.Count() == 0) 
                {
                    actualPage--;
                    Rechres();
                }
            }

            if (PoiskTb.Text.Length>0)
            {
                pr=new ObservableCollection<Product>(products.Where(x => x.Name.ToLower().StartsWith(PoiskTb.Text.ToLower())));
            }
            products = pr;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var Prod = (sender as Button).DataContext as Product;   
            Navigation.NextPage(new Nav("Редкатирование", new EditingPage(Prod)));
        }

        private void SortCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Rechres();
        }



        private void RightBtn_Click(object sender, RoutedEventArgs e)
        {
            actualPage++;
            Rechres();
        }

        private void LeftBtn_Click(object sender, RoutedEventArgs e)
        {
            actualPage--;
            if (actualPage < 0)
                actualPage = 0;
            Rechres();
        }

        private void PoiskTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Rechres();
        }
    }
}
