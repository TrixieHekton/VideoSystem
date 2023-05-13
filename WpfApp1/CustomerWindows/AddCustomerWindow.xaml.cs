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
    public partial class AddCustomerWin : Window
    {
        public AddCustomerWin()
        {
            InitializeComponent();
        }

        private void ClickSubmit(object sender, RoutedEventArgs e)
        {
            string fname = textBox_firstName.Text;
            string lname = textBox_lastName.Text;

            if(!string.IsNullOrWhiteSpace(fname) && !string.IsNullOrWhiteSpace(lname))
            {
                DataAccess db = new DataAccess();

                db.AddCustomer(char.ToUpper(lname[0]) + lname.Substring(1), char.ToUpper(fname[0]) + fname.Substring(1));
                MessageBox.Show(String.Format("{0} {1} has been added to the database", fname, lname), 
                    "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                Close();
            }
            else
            {
                MessageBox.Show("One or more of the entries is invalid. Please check your input again.", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
