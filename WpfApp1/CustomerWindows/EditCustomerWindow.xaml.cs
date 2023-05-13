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
    public partial class EditCustomerWin : Window
    {
        private Customer cust;

        public EditCustomerWin(Customer cust_in)
        {
            InitializeComponent();
            cust = cust_in;
            textBox_firstName.Text = cust.FirstName;
            textBox_lastName.Text = cust.LastName;
        }

        private void ClickSubmit(object sender, RoutedEventArgs e)
        {
            string fname = textBox_firstName.Text;
            string lname = textBox_lastName.Text;

            if(!string.IsNullOrWhiteSpace(fname) && !string.IsNullOrWhiteSpace(lname))
            {
                DataAccess db = new DataAccess();
                db.EditCustomer(cust, lname, fname);
                MessageBox.Show("The details of this customer have been successfully modified",
                        "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                Close();
            }
            else
            {
                MessageBox.Show("One or more of the entries is invalid. Please check your input again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
