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
    /// Interaction logic for LoggInWindow.xaml
    /// </summary>
    public partial class LoggInWindow : Window
    {
        public UserModel User { get; set; }
        
        public Setups Setup { get; set; }

        public LoggInWindow(UserModel user,Setups setups)
        {
            InitializeComponent();
            User = user;
            Setup = setups;
            lblBagCounter.Content = setups.BagCounter;

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
        private void btnTaip_Click_1(object sender, RoutedEventArgs e)
        {
            var newPage = new MainWindow(User, Setup);

            newPage.Show();

            this.Close();
        }

        private async void btnLogg_Click(object sender, RoutedEventArgs e)
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync("http://foreshop-001-site1.atempurl.com/api/User/username/" + txtboxUsername.Text);

            var responseContent = await response.Content.ReadAsStringAsync();

            var user = JsonConvert.DeserializeObject<UserModel>(responseContent);

            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(txtPassword.SecurePassword);
            string password = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);

            if (user != null && user.Password == password)
            {
                User = user;
                Setup.Logging = "user";

                var page = new MainWindow(User, Setup);
                page.Show();

                this.Close();
            }
            else
            {
                lblLogginError.Content = "Toks username nebuvo rastas arba jusu slaptazodis ivestas neteisingai";
            }

        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            var page = new RegistrationWindow(User, Setup);

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

        private void btnLogg_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnLogg_Click(sender, e);
            }
        }

        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnLogg_Click(sender, e);
            }
        }
    }
}
