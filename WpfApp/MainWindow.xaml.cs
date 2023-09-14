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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public UserModel User { get; set; }

        public Setups Setup { get; set; } = new Setups();


        public MainWindow()
        {
            InitializeComponent();

            if (User == null)
            {
                btnAtsijungti.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        public MainWindow(UserModel user, Setups setups)
        {
            InitializeComponent();
            User = user;
            Setup = setups;

            lblBagCounter1.Content = Setup.BagCounter;

            if (User == null)
            {
                btnAtsijungti.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void btnTaip_Click(object sender, RoutedEventArgs e)
        {
            var newPage = new MainWindow(User, Setup);

            newPage.Show();

            this.Close();
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

        private void btnAtsijungti_Initialized(object sender, EventArgs e)
        {

        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            var page = new PageFindWindow(User, Setup, txtFind.Text);

            page.Show();

            this.Close();
        }
    }
}
