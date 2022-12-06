using Microsoft.Win32;
using Practice.Componens;
using System;
using System.Collections.Generic;
using System.IO;
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
            var ProdId = BdConect.db.Product.Where(x => x.Name == NameTb.Text.Trim()).FirstOrDefault();
            if (ProdId == null) 
            { 
                BdConect.db.Product.Add(product);
                MessageBox.Show("yes");
            }
            BdConect.db.SaveChanges();
            Navigation.NextPage(new Nav("Продукты", new ListproductPage()));
        }
        public void Update() 
        {
        }

        private void AddBt_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFil = new OpenFileDialog() 
            {
            Filter = "**.png|*.png|*.jpeg|*.jpeg|*.jpg|*.jpg",
            };
            if (openFil.ShowDialog().GetValueOrDefault())
            {
                product.Photo = File.ReadAllBytes(openFil.FileName);
                ProductPhoto.Source = new BitmapImage(new Uri(openFil.FileName));
            }
        }
    }
}
