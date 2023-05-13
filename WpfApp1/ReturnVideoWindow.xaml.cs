using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

namespace VideoRentalSystem
{
    public partial class ReturnVideoWindow : Window
    {
        DataAccess db = new DataAccess();
        Customer cust;
        List<RentedVideo> rentedVideos = new List<RentedVideo>();
        List<RentedVideo> returningVideos = new List<RentedVideo>();
        decimal rentalFees, overdueFees, total;

        public ReturnVideoWindow(Customer custIn)
        {
            InitializeComponent();
            cust = custIn;
            GetRentedVideos(cust);
            custName_label.Content = String.Format("{0} {1}", cust.FirstName, cust.LastName);
        }

        private void GetRentedVideos(Customer cust)
        {
            rentedVideos = db.GetRentedVideos(cust);

            foreach(RentedVideo rentedVid in rentedVideos)
            {
                string dateRentedStr = String.Format("{0:yyyy-MM-dd}", DateTime.ParseExact(rentedVid.DateRented, "MM/dd/yyyy HH:mm:ss",
                    CultureInfo.InvariantCulture));
                string dateDueStr = String.Format("{0:yyyy-MM-dd}", DateTime.ParseExact(rentedVid.DateDue, "MM/dd/yyyy HH:mm:ss",
                    CultureInfo.InvariantCulture));

                rentedVideos[rentedVideos.IndexOf(rentedVid)].DateRented = dateRentedStr;
                rentedVideos[rentedVideos.IndexOf(rentedVid)].DateDue = dateDueStr;
            }

            rentedVideos_listView.ItemsSource = rentedVideos;
        }

        private void ReturnFrom_button_Click(object sender, RoutedEventArgs e)
        {
            List<RentedVideo> selectedItems = rentedVideos_listView.SelectedItems.Cast<RentedVideo>().ToList();

            foreach (RentedVideo selected in selectedItems)
            {
                rentedVideos.Remove(selected);
                returningVideos.Add(selected);
                rentalFees += selected.VideoPrice;
                overdueFees += selected.OverdueFee;
                total += (selected.VideoPrice + overdueFees);
            }

            rentalFees_label.Content = rentalFees;
            overdueFees_label.Content = overdueFees;
            total_label.Content = total;

            returningVideos_listView.ItemsSource = null;
            returningVideos_listView.ItemsSource = returningVideos;

            rentedVideos_listView.ItemsSource = null;
            rentedVideos_listView.ItemsSource = rentedVideos;

            ReturnFrom_button.IsEnabled = false;
            Confirm_btn.IsEnabled = true;
        }

        private void Confirm_btn_Click(object sender, RoutedEventArgs e)
        {
            int returnCount = returningVideos.Count();
            db.ReturnFromCustomer(cust, returningVideos);
            MessageBox.Show(String.Format("{0} title(s) have been successfully returned from {1} {2}", returnCount, cust.FirstName, cust.LastName), 
                "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);

            Close();
            //TODO: Return
        }

        private void Cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RentedVideos_listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ReturnFrom_button.IsEnabled = true;
        }
    }
}
