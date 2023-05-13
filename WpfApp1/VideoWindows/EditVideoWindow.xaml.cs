using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Interaction logic for EditVideoWindow.xaml
    /// </summary>
    public partial class EditVideoWindow : Window
    {
        private Video video;

        public EditVideoWindow(Video vid)
        {
            video = vid;
            InitializeComponent();
            VideoTitle_textBox.Text = vid.Title;
            VideoCategory_comboBox.Text = vid.Category;
        }

        private void Submit_button_Click(object sender, RoutedEventArgs e)
        {
            string newTitle = VideoTitle_textBox.Text.ToString();
            string newCategory = VideoCategory_comboBox.Text.ToString();

            if (!string.IsNullOrEmpty(newTitle) && !string.IsNullOrEmpty(newCategory))
            {
                DataAccess db = new DataAccess();
                db.EditVideo(video, newTitle, newCategory);
                MessageBox.Show("The details of this video have been successfully modified",
                        "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("One or more of the entries is invalid. Please check your input again.", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
