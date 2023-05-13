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
    public partial class AddVideoWindow : Window
    {
        public AddVideoWindow()
        {
            InitializeComponent();
        }

        private void SubmitEntry(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(textBox_Stock.Text.ToString(), out int qty) && CheckEntries())
            {
                DataAccess db = new DataAccess();
                db.AddVideo(textBox_Title.Text, comboBox_Type.Text, qty, rental_ComboBox.SelectedIndex+1);

                MessageBox.Show(String.Format("Video title {0} has been added to the database", textBox_Title.Text),
                    "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                Close();
            }
            else
            {
                MessageBox.Show("One of the entries is invalid. Please check your input again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TextBox_Title_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox_Title.Text))
            {
                comboBox_Type.IsEnabled = true;
                textBox_Stock.IsEnabled = true;
                rental_ComboBox.IsEnabled = true;
            }
            else
            {
                comboBox_Type.SelectedIndex = -1;
                textBox_Stock.Text = "";
                rental_ComboBox.SelectedIndex = -1;

                comboBox_Type.IsEnabled = false;
                textBox_Stock.IsEnabled = false;
                rental_ComboBox.IsEnabled = false;
            }
        }

        private void TextBox_Stock_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CheckEntries())
            {
                btn_Submit.IsEnabled = true;
            }

            if (string.IsNullOrEmpty(textBox_Stock.Text))
            {
                btn_Submit.IsEnabled = false;
            }
        }

        private void ComboBox_Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CheckEntries())
            {
                btn_Submit.IsEnabled = true;
            }
        }

        private void Rental_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CheckEntries())
            {
                btn_Submit.IsEnabled = true;
            }
        }

        private bool CheckEntries()
        {
            return (!string.IsNullOrWhiteSpace(textBox_Title.Text)
                && !string.IsNullOrWhiteSpace(textBox_Stock.Text)
                && comboBox_Type.SelectedIndex > -1
                && rental_ComboBox.SelectedIndex > -1);
        }
    }
}
