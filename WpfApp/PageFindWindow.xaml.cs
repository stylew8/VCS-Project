﻿using Newtonsoft.Json;
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
    /// Interaction logic for PageFindWindow.xaml
    /// </summary>
    public partial class PageFindWindow : Window
    {
        public UserModel User { get; set; }
        public Setups Setup { get; set; }
        public string TxtFind { get; set; }
        public PageFindWindow(UserModel user, Setups setup, string txtFind)
        {
            InitializeComponent();
            User = user;
            Setup = setup;
            lblBagCounter1.Content = Setup.BagCounter;
            TxtFind = txtFind;

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

        private async void dtgBag_Loaded(object sender, RoutedEventArgs e)
        {
            var httpClient = new HttpClient();

<<<<<<< HEAD
            var response = await httpClient.GetAsync("http://foreshop-001-site1.atempurl.com/Product/name/" + TxtFind);
=======
            var response = await httpClient.GetAsync("http://foreshop-001-site1.atempurl.com/api/Product/name/" + TxtFind);
>>>>>>> b23c86a131a78484650aed5c9ffe74fbf34b3b6f

            var responseContent = await response.Content.ReadAsStringAsync();

            var productList = JsonConvert.DeserializeObject<List<ProductModel>>(responseContent);

            dtgBag.ItemsSource = productList;

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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var item = dtgBag.SelectedItem as ProductModel;

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
