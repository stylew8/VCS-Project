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
    /// Interaction logic for OrderForUnUserWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        public UserModel User { get; set; }
        public Setups Setup { get; set; }
        public OrderWindow(UserModel user, Setups setup)
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

        private void btnBag_Click(object sender, RoutedEventArgs e)
        {
            var page = new BagWindow(User, Setup);

            page.Show();

            this.Close();
        }

        private async void btnUzsakyti_Click(object sender, RoutedEventArgs e)
        {
            if (!(bool)rbAtsis.IsChecked || !(bool)rbPris.IsChecked)
            {
                lblError.Content = "Nebuvo pazymetas pristatymas arba apmokejimas";
            }
            else
            {
                if (Setup.Bag != null)
                {


                    var order = new OrderModel()
                    {
                        UserId = User.Id,
                        IsPaid = false
                    };

                    var httpClient = new HttpClient();

                    var content = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync("http://stylew8-001-site1.ctempurl.com/api/Order", content);
                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("RESPONSE");
                    }


                    foreach (var item in Setup.Bag)
                    {
                        var orderLine = new OrderLineModel()
                        {
                            OrderId =  User.Id,
                            ProductId = item.Id
                        };

                        var contentOrderLine = new StringContent(JsonConvert.SerializeObject(orderLine), Encoding.UTF8, "application/json");

                        var response1 = await httpClient.PostAsync("http://stylew8-001-site1.ctempurl.com/api/OrderLine", contentOrderLine);

                        if (!response1.IsSuccessStatusCode)
                        {
                            MessageBox.Show("RESPONSE1");
                        }
                    }

                    MessageBox.Show("Jusu uzsakymas buvo pridetas i apdorojima, tolimesi informacija bus issiusta el.pastu");

                    Setup.Bag = null;
                    Setup.BagCounter = 0;

                    var page = new MainWindow(User, Setup);

                    page.Show();

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Jusu krepselis tuscias");
                }
            }
        }
    }
}
