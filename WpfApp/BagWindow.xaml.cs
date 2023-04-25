using Newtonsoft.Json;
using ShopApi.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp.Model;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for BagWindow.xaml
    /// </summary>
    public partial class BagWindow : Window
    {
        public UserModel User { get; set; }
        public Setups Setup { get; set; }
        public BagWindow(UserModel user, Setups setup)
        {
            InitializeComponent();
            User = user;
            Setup = setup;
            lblBagCounter1.Content = Setup.BagCounter;
        }

        private void menuCpu_Click(object sender, RoutedEventArgs e)
        {
            var page = new CpuWindow(User, Setup);

            page.Show();

            this.Close();
        }

        private void menuMother_Click(object sender, RoutedEventArgs e)
        {
            var page = new MotherWindow(User, Setup);

            page.Show();

            this.Close();
        }

        private void menuGpu_Click(object sender, RoutedEventArgs e)
        {
            var page = new GpuWindow(User, Setup);

            page.Show();

            this.Close();
        }

        private void menuFans_Click(object sender, RoutedEventArgs e)
        {
            var page = new FansWindow(User, Setup);

            page.Show();

            this.Close();
        }

        private void btnTaip_Click_1(object sender, RoutedEventArgs e)
        {
            var newPage = new MainWindow(User, Setup);

            newPage.Show();

            this.Close();
        }

        private void dtgBag_Loaded(object sender, RoutedEventArgs e)
        {
            dtgBag.ItemsSource = Setup.Bag;

            var column = dtgBag.Columns.FirstOrDefault(c => c.Header.ToString() == "ImageData");
            if (column != null)
            {
                column.Visibility = Visibility.Collapsed;
            }
        }

        private void btnBag_Click(object sender, RoutedEventArgs e)
        {
            var page = new BagWindow(User, Setup);

            page.Show();

            this.Close();
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var item = dtgBag.SelectedItem as ProductModel;
            if (item != null)
            {
                Setup.Bag.Remove(item);
                Setup.BagCounter--;
                lblBagCounter1.Content = Setup.BagCounter;
                var page = new BagWindow(User, Setup);

                page.Show();

                this.Close();          
            }
            else
            {
                lblErorDelete.Content = "Norint istrinti preke is jusu krepselio, reikia issirinkti ji";
            }
            
        }

        private void btnPirkti_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Setup.Logging == "unUser")
                {
                    MessageBox.Show("Norint pirkti, reikia uzsiregistuotis");
                }
                else if (Setup.Bag.Count == 0)
                {
                    MessageBox.Show("Norint pradeti pirkimo processa, reikia ideti produktus i krepseli");
                }
                else
                {
                    var page = new OrderWindow(User, Setup);

                    page.Show();

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Reikia buti prisijingusiam!");
            }
        }

        private void btnViewAllOrders_Click(object sender, RoutedEventArgs e)
        {
            if (Setup.Logging != "unUser")
            {
                var page = new AllOrdersWindow(User, Setup);

                page.Show();

                this.Close();
            }
            else
            {
                MessageBox.Show("Norint paziureti uzsakytus produktus, reikia uzeiti i paskyra arba uzsiregistruotis");
            }
        }
    }
}
