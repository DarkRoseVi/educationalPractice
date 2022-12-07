using Microsoft.Win32;
using Practice.Componens;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace Practice.MyPages
{
    /// <summary>
    /// Логика взаимодействия для EditingPage.xaml
    /// </summary>
    public partial class EditingPage : Page
    {
        public Product product { get; set; }
        public List<UnitMeasurement> MesreUnits { get; set; }
        public EditingPage(Product _product)
        {
            BdConect.db.UnitMeasurement.Load();
            MesreUnits = BdConect.db.UnitMeasurement.Local.ToList();
            product = _product;

            InitializeComponent();

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

        private void UnitMeasurementCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (UnitMeasurementCb.SelectedItem == null)
                return;
            product.UnitMeasurement = UnitMeasurementCb.SelectedItem as UnitMeasurement;
        }
    }
}
