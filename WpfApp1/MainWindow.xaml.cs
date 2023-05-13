using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VideoRentalSystem
{
    public partial class MainWindow : Window
    {
        public List<Video> videos = new List<Video>();
        public List<Customer> customers = new List<Customer>();
        DataAccess db = new DataAccess();

        public MainWindow()
        {
            InitializeComponent();
            label_DateTime.Content = DateTime.Now.ToString("dddd, MMMM dd, yyyy");
            GetVideoList();
            GetCustomerList();
        }

        /*
         *================
         * CUSTOMER INFO
         *================
        */

        private void GetCustomerList() //refreshes customer list
        {
            customers = db.GetCustomers();

            customers = customers.OrderBy(c => c.LastName).ToList(); //sorts customers by last name in alphabetical order
            Customer_listView.ItemsSource = customers;
        }

        private void SelectCustomer(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = (ListViewItem)sender;
            Customer cust = (Customer)Customer_listView.SelectedItem;

            if (item != null && item.IsSelected)
            {
                btn_EditCustomer.IsEnabled = true;
                btn_MoreCustomerInfo.IsEnabled = true;
                if(cust.Overdue == 0) btn_RentToCustomer.IsEnabled = true; //no renting allowed if customer has overdue videos
                if(cust.Rented > 0) btn_ReturnFromCustomer.IsEnabled = true; //no need to return if customer has no rented videos
            }
            else
            {
                btn_EditCustomer.IsEnabled = false;
                btn_MoreCustomerInfo.IsEnabled = false;
                btn_RentToCustomer.IsEnabled = false;
                btn_ReturnFromCustomer.IsEnabled = false;
            }
        }

        private void Btn_MoreCustomerInfo_Click(object sender, RoutedEventArgs e)
        {
            //TODO: more comprehensive customer information
            Customer cust = (Customer)Customer_listView.SelectedItems[0];
            string info = db.GetCustomerInfo(cust);
            MessageBox.Show(info, "Customer Report", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Btn_Add_Customer_Click(object sender, RoutedEventArgs e)
        {
            AddCustomerWin addCustomerWin = new AddCustomerWin();
            addCustomerWin.ShowDialog();
            GetCustomerList();
        }

        private void Btn_EditCustomer_Click(object sender, RoutedEventArgs e)
        {
            Customer selected = (Customer)Customer_listView.SelectedItems[0];
            EditCustomerWin editCustomerWin = new EditCustomerWin(selected);
            editCustomerWin.ShowDialog();
            GetCustomerList();
        }

        private void Btn_RentToCustomer_Click(object sender, RoutedEventArgs e)
        {
            RentVideoWindow rentWindow = new RentVideoWindow((Customer)Customer_listView.SelectedItems[0], videos);
            rentWindow.ShowDialog();
            GetCustomerList();
            GetVideoList();
        }

        private void Btn_ReturnFromCustomer_Click(object sender, RoutedEventArgs e)
        {
            //TODO: return rented videos from customer
            Customer selected = (Customer)Customer_listView.SelectedItems[0];
            ReturnVideoWindow returnVideoWindow = new ReturnVideoWindow(selected);
            returnVideoWindow.ShowDialog();
            GetCustomerList();
            GetVideoList();
        }

        private void Btn_Refresh_Customers_Click(object sender, RoutedEventArgs e)
        {
            GetCustomerList();
            btn_MoreCustomerInfo.IsEnabled = false;
            btn_EditCustomer.IsEnabled = false;
            btn_RentToCustomer.IsEnabled = false;
            btn_ReturnFromCustomer.IsEnabled = false;
            MessageBox.Show("Entries refreshed.", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /*
         *=============
         * VIDEO INFO
         *=============
        */

        public void GetVideoList() //refreshes video list
        {
            videos = db.GetVideos();

            videos = videos.OrderBy(v => v.Title).ToList(); //sorts video by title in alphabetical order
            Video_listView.ItemsSource = videos;
        }

        public void AddVideoToList(String video_title, String video_category, int qty)
        {
            videos.Add(new Video() { Title = video_title, Category = video_category, NumIn = qty, NumOut = 0 });
            Video_listView.ItemsSource = null;
            Video_listView.ItemsSource = videos;
        }

        private void RefreshVideos(object sender, RoutedEventArgs e)
        {
            GetVideoList();
            MessageBox.Show("Entries refreshed.", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SelectVideo(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListViewItem item && item.IsSelected)
            {
                btn_EditVideo.IsEnabled = true;
                btn_DeleteVideo.IsEnabled = true;
            }
            else
            {
                btn_EditVideo.IsEnabled = false;
                btn_DeleteVideo.IsEnabled = false;
            }
        }

        private void AddVideo(object sender, RoutedEventArgs e)
        {
            AddVideoWindow addVideoWin = new AddVideoWindow();
            addVideoWin.ShowDialog();
            GetVideoList();
        }

        private void Btn_EditVideo_Click(object sender, RoutedEventArgs e)
        {
            Video selected = (Video)Video_listView.SelectedItems[0];
            if (selected.NumOut > 0)
            {
                MessageBox.Show(String.Format("All copies of '{0}' must be returned before editing its details.", selected.Title),
                    "Access Denied", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                EditVideoWindow editCustomerWin = new EditVideoWindow(selected);
                editCustomerWin.ShowDialog();
                GetVideoList();
            }
        }

        private void Btn_DeleteVideo_Click(object sender, RoutedEventArgs e)
        {
            Video selected = (Video)Video_listView.SelectedItems[0];
            if(selected.NumOut > 0)
            {
                MessageBox.Show(String.Format("All copies of '{0}' must be returned before removing it from the database.", selected.Title),
                    "Access Denied", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (MessageBox.Show(String.Format("Are you sure you want to delete the video '{0}'?", selected.Title),
                "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    db.DeleteVideo(selected);
                    MessageBox.Show(String.Format("'{0}' has been deleted from the database.", selected.Title),
                        "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    GetVideoList();
                }
            }
        }
    }
}
