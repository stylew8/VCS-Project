using Newtonsoft.Json;
using ShopApi.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
    /// Interaction logic for MotherWindow.xaml
    /// </summary>
    public partial class MotherWindow : Window
    {
        public UserModel User { get; set; }
        public Setups Setup { get; set; }
        public MotherWindow(UserModel user, Setups setup)
        {
            InitializeComponent();
            User = user;
            Setup = setup;
            lblBagCounter1.Content = Setup.BagCounter;

            if (User == null)
            {
                btnAtsijungti.Visibility = System.Windows.Visibility.Hidden;
            }
        }
        private void btnAtsijungti_Click(object sender, RoutedEventArgs e)
        {
            var page = new MainWindow();

            page.Show();

            this.Close();
        }

        private void btnBag_Click(object sender, RoutedEventArgs e)
        {
            var page = new BagWindow(User, Setup);

            page.Show();

            this.Close();
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

        private async void dtgProducts_Loaded(object sender, RoutedEventArgs e)
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync("http://foreshop-001-site1.atempurl.com/api/Product/category/category/Motherboard");

            var responseContent = await response.Content.ReadAsStringAsync();

            var productList = JsonConvert.DeserializeObject<List<ProductModel>>(responseContent);

            dtgProducts.ItemsSource = productList;

            var column = dtgProducts.Columns.FirstOrDefault(c => c.Header.ToString() == "ImageData");
            if (column != null)
            {
                column.Visibility = Visibility.Collapsed;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var item = dtgProducts.SelectedItem as ProductModel;

            if (item != null)
            {
                if (Setup.Bag == null)
                {
                    Setup.Bag = new List<ProductModel>();
                }

                lblEror.Content = "";
                Setup.Bag.Add(item);
                Setup.BagCounter++;
                lblBagCounter1.Content = Setup.BagCounter;
            }
            else
            {
                lblEror.Content = "Noredami prideti reikia issirinkti produkta";
            }

        }

        private void dtgProducts_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 1)
            {
                var row = sender as DataGridRow;
                if (row != null)
                {
                    if (row.IsSelected)
                    {
                        row.IsSelected = false;
                        e.Handled = true;
                    }
                }
            }
        }

        private void btnPaskyra_Click(object sender, RoutedEventArgs e)
        {
            if (Setup == null)
            {
                Setup.Logging = "unUser";
                Setup.BagCounter = 0;
            }

            if (Setup.Logging == "unUser")
            {
                var pageLog = new LoggInWindow(User, Setup);

                pageLog.Show();

                this.Close();
            }
            else if (User.Premissions == "user")
            {
                var pagePaskyra = new PaskyraLoggedWindow(User, Setup);

                pagePaskyra.Show();

                this.Close();
            }
            else if (User.Premissions == "admin")
            {
                var pageAdmin = new PaskyraAdminWindow(User, Setup);
                pageAdmin.Show();
                this.Close();
            }
        }

        private void Image_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Setup == null)
            {
                Setup.Logging = "unUser";
                Setup.BagCounter = 0;
            }

            if (Setup.Logging == "unUser")
            {
                var pageLog = new LoggInWindow(User, Setup);

                pageLog.Show();

                this.Close();
            }
            else if (User.Premissions == "user")
            {
                var pagePaskyra = new PaskyraLoggedWindow(User, Setup);

                pagePaskyra.Show();

                this.Close();
            }
            else if (User.Premissions == "admin")
            {
                var pageAdmin = new PaskyraAdminWindow(User, Setup);
                pageAdmin.Show();
                this.Close();
            }
        }
    }
}
