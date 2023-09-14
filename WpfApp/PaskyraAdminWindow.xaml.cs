using Microsoft.Win32;
using Newtonsoft.Json;
using ShopApi.Models.User;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Linq;
using WpfApp.Model;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for PaskyraAdminWindow.xaml
    /// </summary>
    public partial class PaskyraAdminWindow : Window
    {
        public UserModel User { get; set; }
        public Setups Setup { get; set; }
        public byte[] Bitmap { get; set; }
        public PaskyraAdminWindow(UserModel user, Setups setup)
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
        private void btnBag_Click(object sender, RoutedEventArgs e)
        {
            var page = new BagWindow(User, Setup);

            page.Show();

            this.Close();
        }
        private void btnAtsijungti_Click(object sender, RoutedEventArgs e)
        {
            var page = new MainWindow();

            page.Show();

            this.Close();
        }


        private void btnChoosePhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files(*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";

                BitmapImage bitmap = null;
                if (openFileDialog.ShowDialog() == true)
                {

                    string filePath = openFileDialog.FileName;

                   
                    bitmap = new BitmapImage(new Uri(filePath));
                }
                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));

                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    Bitmap = stream.ToArray();
                }

                lblFoto.Content = "✔️";
            }
            catch (Exception ex)
            {
                lblFoto.Content = "❌";
                using (var writer = new StreamWriter("loggs"))
                {
                    writer.WriteLine(ex.Message);
                }
            }

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            lblDuomError.Content = "";


            if (txtNameProd.Text == "" ||
                txtKategorijaProd.Text == "" ||
                txtDescProd.Text == "" ||
                txtPriceProd.Text == "" ||
                txtQuantityProd.Text == "" ||
                Bitmap == null)
            {
                lblDuomError.Content = "Kazkurie duomenys nebuvo ivesti";
            }
            else
            {
                var product = new ProductModel()
                {
                    ImageData = Bitmap,
                    Name = txtNameProd.Text,
                    Description = txtDescProd.Text,
                    Price = int.Parse(txtPriceProd.Text),
                    Quantity = int.Parse(txtQuantityProd.Text),
                    Category = txtKategorijaProd.Text
                };


                var httpClient = new HttpClient();

                var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("http://foreshop-001-site1.atempurl.com/api/Product", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Sekmingai pridejote produkta");
                }
                else
                {
                    MessageBox.Show("Ivyko klaida ");
                }
            }
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

        private async void btnDeleteAll_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Ar tikrai norite ištrinti visus produktus?", "Patvirtinimas", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync("http://foreshop-001-site1.atempurl.com/api/Product");

                var responseContent = await response.Content.ReadAsStringAsync();

                var productList = JsonConvert.DeserializeObject<List<ProductModel>>(responseContent);

                for (int i = 0; i <= productList.Count; i++)
                {
                    await httpClient.DeleteAsync("http://foreshop-001-site1.atempurl.com/api/Product/" + i);
                }
            }
        }

        private async void btnDeleteById_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Ar tikrai norite ištrinti šį produktą?", "Patvirtinimas", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    var httpClient = new HttpClient();

                    await httpClient.DeleteAsync("http://foreshop-001-site1.atempurl.com/api/Product/" + txtProdID.Text);
                }
            }
            catch (Exception ex)
            {
                using (var writer = new StreamWriter("loggs"))
                {
                    writer.WriteLine(ex.Message);
                }
            }

        }

        private async void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Ar tikrai norite ištrinti šį produktą?", "Patvirtinimas", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    var httpClient = new HttpClient();

                    await httpClient.DeleteAsync("http://foreshop-001-site1.atempurl.com/api/User/" + txtProdID_Copy.Text);
                }
            }
            catch (Exception ex)
            {
                using (var writer = new StreamWriter("loggs"))
                {
                    writer.WriteLine(ex.Message);
                }
            }
        }

        private async void btnChange_Click(object sender, RoutedEventArgs e)
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync("http://foreshop-001-site1.atempurl.com/api/User/username/" + txtUsername.Text);

            var responseContent = await response.Content.ReadAsStringAsync();

            var user = JsonConvert.DeserializeObject<UserModel>(responseContent);

           

            if (user != null)
            {
                user.Password = "default";
                var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                var response1 = await httpClient.PatchAsync("http://foreshop-001-site1.atempurl.com/api/User/" + user.Id, content);

                if (response1.IsSuccessStatusCode)
                {
                    MessageBox.Show("Informacija buvo sekmingai pakeista");
                }
                else
                {
                    MessageBox.Show("Informacija nebuvo pakeista, ivyko klaida");
                }
            }
        }

        private async void btnGive_Click(object sender, RoutedEventArgs e)
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync("http://foreshop-001-site1.atempurl.com/api/User/username/" + txtUsernameGive.Text);

            var responseContent = await response.Content.ReadAsStringAsync();

            var user = JsonConvert.DeserializeObject<UserModel>(responseContent);


            if (user != null)
            {
                var httpClient1 = new HttpClient();
                user.Premissions = "admin";
                var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                var response1 = await httpClient1.PatchAsync("http://foreshop-001-site1.atempurl.com/api/User/" + user.Id, content);

                if (response1.IsSuccessStatusCode)
                {
                    MessageBox.Show("Informacija buvo sekmingai pakeista");
                }
                else
                {
                    MessageBox.Show("Informacija nebuvo pakeista, ivyko klaida");
                }
            }
        }
    }
}
