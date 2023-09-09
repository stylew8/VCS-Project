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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for PaskyraLoggedWindow.xaml
    /// </summary>
    public partial class PaskyraLoggedWindow : Window
    {
        public UserModel User { get; set; }
        public Setups Setup { get; set; }


        public PaskyraLoggedWindow(UserModel user, Setups setup)
        {
            InitializeComponent();

            txtName.Text = user.Name;
            txtSurname.Text = user.Surname;
            txtUsername.Text = user.Username;
            txtEmail.Text = user.Email;
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

        private void btnTaip_Click_1(object sender, RoutedEventArgs e)
        {
            var newPage = new MainWindow(User,Setup);

            newPage.Show();

            this.Close();
        }
        private void btnAtsijungti_Click(object sender, RoutedEventArgs e)
        {
            var page = new MainWindow();

            page.Show();

            this.Close();
        }


        private void btnPaskyra_Click(object sender, RoutedEventArgs e)
        {
            if (Setup.Logging == "unUser")
            {
                var pageLog = new LoggInWindow(User, Setup);

                pageLog.Show();

                this.Close();
            }
            else if (Setup.Logging == "user")
            {
                var pagePaskyra = new PaskyraLoggedWindow(User, Setup);

                pagePaskyra.Show();

                this.Close();
            }
            else
            {
                var pageAdmin = new PaskyraAdminWindow(User,Setup);

                pageAdmin.Show();

                this.Close();
            }
        }

        private async void btnUpdateinfo_Click(object sender, RoutedEventArgs e)
        {
            User.Username = txtUsername.Text;
            User.Name = txtName.Text;
            User.Surname = txtSurname.Text;
            User.Email = txtEmail.Text;

            var httpClient = new HttpClient();

            var content = new StringContent(JsonConvert.SerializeObject(User), Encoding.UTF8, "application/json");

            var response = await httpClient.PatchAsync("http://foreshop-001-site1.atempurl.com/api/User/" + User.Id, content);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Informacija buvo sekmingai pakeista");
            }
            else
            {
                MessageBox.Show("Informacija nebuvo pakeista, ivyko klaida");
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
    }
}
