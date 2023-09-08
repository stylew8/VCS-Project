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
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public UserModel User { get; set; }
        public Setups Setup { get; set; }

        public RegistrationWindow(UserModel user,Setups setups)
        {
            InitializeComponent();
            User = user;
            Setup = setups;
            lblBagCounter1.Content = setups.BagCounter;
        }
        private void btnBag_Click(object sender, RoutedEventArgs e)
        {
            var page = new BagWindow(User, Setup);

            page.Show();

            this.Close();
        }

        private void btnTaip_Click_1(object sender, RoutedEventArgs e)
        {
            var newPage = new MainWindow(User, Setup);

            newPage.Show();

            this.Close();
        }

        private async void btnRegistr_Click(object sender, RoutedEventArgs e)
        {
            lblDuomError.Content = "";
            lblSlaptik.Content = "";

            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(txtboxPassword.SecurePassword);
            string password = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);

            IntPtr ptr1 = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(txtboxPasswordRepeat.SecurePassword);
            string passwordRepeat = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr1);
            System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr1);

            if (txtboxEmail.Text == "" ||
                txtboxName.Text == "" ||
                password == "" ||
                passwordRepeat == "" ||
                txtboxSurname.Text == "" ||
                txtboxUsername.Text == "")
            {
                lblDuomError.Content = "Kazkurie duomenys nebuvo ivesti";
            }
            else if (password != passwordRepeat)
            {
                lblSlaptik.Content = "Slaptazodziai nera identiski";
            }
            else
            {
                var user = new UserModel()
                {
                   Username = txtboxUsername.Text,
                   Password = password,
                   Name = txtboxName.Text,
                   Surname = txtboxSurname.Text,
                   Email = txtboxEmail.Text,
                   Premissions = "user"
                };


                var httpClient = new HttpClient();

                var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("http://foreshop-001-site1.atempurl.com/api/User", content);// foreshop-001-site1.atempurl.com

                var nextPage = new PaskyraLoggedWindow(user,Setup);

                nextPage.Show();

                this.Close();
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
