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
            if (Navigation.AutoUser.RolyId == 1)
                AddProdBt.Visibility = Visibility.Collapsed;
          else if ( Navigation.AutoUser.RolyId == 2 && Navigation.AutoUser.RolyId == 3 )
                OrderBt.Visibility = Visibility.Collapsed;

                //EditingBt.Visibility = Visibility.Collapsed;
            //else if (Navigation.AutoUser.RolyId == 1)



        }
        public void Rechres()
        {
            ObservableCollection<Product> pr = products;
            if (SortCb.SelectedItem !=null)
            {
                switch ((SortCb.SelectedItem as ComboBoxItem).Tag)
                {
                    case "1":
                        pr = new ObservableCollection<Product>(products.OrderBy(x => x.Name));
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
            }

            if (FiltrCb.SelectedItem != null)
            {
                switch ((FiltrCb.SelectedItem as ComboBoxItem).Tag)
                {
                    case "1":
                        pr = BdConect.db.Product.Local;
                        break;
                    case "2":
                        pr = new ObservableCollection<Product>(products.Where(x => x.UnitMeasurementId == 1));
                        break;
                    case "3":
                        pr = new ObservableCollection<Product>(products.Where(x => x.UnitMeasurementId == 2));
                        break;
                    default:
                        break;
                }
            }
  

            ProductListViu.ItemsSource = pr.ToArray();
            if (QuantityCb.SelectedIndex > -1 && pr.Count > 0)
            {
                int selCount;
                if ((QuantityCb.SelectedItem as ComboBoxItem).Content.ToString()=="Все")
                {
                    products = BdConect.db.Product.Local;
                }
                else
                {
                    selCount = Convert.ToInt32((QuantityCb.SelectedItem as ComboBoxItem).Content);

                    products = new ObservableCollection<Product>(products.Skip(selCount * actualPage).Take(selCount));
                    if (products.Count() == 0)
                    {
                        actualPage--;
                        Rechres();
                    }
                }
            }
       

            if (PoiskTb.Text.Length>0)
            {
                //ProductListViu.ItemsSource = BdConect.db.Product.Where(x => x.Name.Contains(PoiskTb.Text)).ToList();
                pr = new ObservableCollection<Product>(products.Where(x => x.Name.Contains(PoiskTb.Text) || x.Description.Contains(PoiskTb.Text)));
             
            }
            else
            {
                pr = BdConect.db.Product.Local;
            }
            products = pr;


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

        private void FiltrCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Rechres();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           // Navigation.NextPage(new Nav("Создание заказа", new OrderSavepage()));
        }

        private void AddProdBt_Click(object sender, RoutedEventArgs e)
        {
           
            Navigation.NextPage(new Nav("",new EditingPage(new Product())));
        }

        private void EditingBt_Click(object sender, RoutedEventArgs e)
        {
            var Prod = (sender as Button).DataContext as Product;
          Navigation.NextPage(new Nav("Редкатирование", new EditingPage(Prod)));
        }

        private void OrderBt_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("Заказы", new OrderPage()));
        }

        private void QuantityCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             Rechres();
        }
    }
}
