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
using System.Windows.Shapes;

namespace VideoRentalSystem
{
    public partial class RentVideoWindow : Window
    {
        DataAccess db = new DataAccess();
        private List<RentedVideo> availableVideos = new List<RentedVideo>();
        private List<RentedVideo> rentingVideos = new List<RentedVideo>();
        private Customer cust;
        private decimal total = 0.00M;

        public RentVideoWindow(Customer custIn, List<Video> vids)
        {
            InitializeComponent();
            cust = custIn;
            CheckForAvailability(vids);
            CustomerName_label.Content = String.Format("{0} {1}", cust.FirstName, cust.LastName);
        }

        private void CheckForAvailability(List<Video> vids)
        {
            List<int> rentedTitles = db.CheckRented(cust);
            foreach (Video vid in vids)
            {
                if (vid.NumIn > 0 && !rentedTitles.Contains(vid.ID))
                {
                    decimal price;
                    if (vid.Category == "VCD") price = 25.00M;
                    else price = 50.00M;

                    DateTime dueDate = DateTime.Today.AddDays(vid.RentDays);

                    VideoName_comboBox.Items.Add(String.Format("{0} ({1} - {2} day/s)", vid.Title, vid.Category, vid.RentDays));
                    availableVideos.Add(new RentedVideo
                    { VideoID = vid.ID, VideoTitle = vid.Title, VideoCategory = vid.Category, DateDue = String.Format("{0:yyyy-MM-dd}", dueDate),
                        DateRented = String.Format("{0:yyyy-MM-dd}", DateTime.Today), VideoPrice = price});
                }
            }
        }

        private void Add_button_Click(object sender, RoutedEventArgs e)
        {
            RentedVideo vid = availableVideos[VideoName_comboBox.SelectedIndex];

            Renting_listView.Items.Add(vid);
            rentingVideos.Add(vid);

            total += vid.VideoPrice;
            total_Label.Content = String.Format("PHP {0}", total);

            VideoName_comboBox.Items.Remove(VideoName_comboBox.SelectedItem);
            availableVideos.Remove(vid);
            VideoName_comboBox.SelectedIndex = -1;
            Add_button.IsEnabled = false;
            Checkout_button.IsEnabled = true;
        }

        private void Checkout_button_Click(object sender, RoutedEventArgs e)
        {
            db.CustomerCheckout(cust.ID, rentingVideos, total);
            string msg;

            if (rentingVideos.Count == 1) msg = String.Format("{0} has been rented to {1} {2}", rentingVideos[0].VideoTitle, cust.FirstName, cust.LastName);
            else msg = String.Format("{0} titles have been rented to {1} {2}", rentingVideos.Count(), cust.FirstName, cust.LastName);

            MessageBox.Show(msg, "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);

            Close();
        }

        private void VideoName_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (VideoName_comboBox.SelectedItem != null) Add_button.IsEnabled = true;
            else Add_button.IsEnabled = false;
        }

        private void Cancel_button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

    public class RentedVideo
    {
        public int RenterID { get; set; }
        public int VideoID { get; set; }
        public string VideoTitle { get; set; }
        public string VideoCategory { get; set; }
        public decimal VideoPrice { get; set; }
        public string DateRented { get; set; }
        public string DateDue { get; set; }
        public int DaysOverdue { get; set; }
        public decimal OverdueFee { get; set; }
    }
}
