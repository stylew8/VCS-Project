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
    /// Interaction logic for CpuWindow.xaml
    /// </summary>
    public partial class CpuWindow : Window
    {
        public UserModel User { get; set; }
        public Setups Setup { get; set; }

        public CpuWindow(UserModel user, Setups setup)
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

        private async void dtgCpu_Loaded(object sender, RoutedEventArgs e)
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync("http://foreshop-001-site1.atempurl.com/api/Product/category/category/CPU");

            var responseContent = await response.Content.ReadAsStringAsync();

            var productList = JsonConvert.DeserializeObject<List<ProductModel>>(responseContent);

            //var Converter = new ConvertProductModel();

            //var convertedList = new List<ProductModelConverted>();

            //foreach (var item in productList)
            //{
            //    convertedList.Add(Converter.Convert(item));
            //}

            dtgCpu.ItemsSource = productList;

            var column = dtgCpu.Columns.FirstOrDefault(c => c.Header.ToString() == "ImageData");
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
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var item = dtgCpu.SelectedItem as ProductModel;

            if (item != null)
            {
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
    }
}
