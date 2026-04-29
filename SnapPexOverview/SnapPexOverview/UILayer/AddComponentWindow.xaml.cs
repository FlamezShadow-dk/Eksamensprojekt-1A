using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace SnapPexOverview.UILayer
{
    /// <summary>
    /// Interaction logic for AddComponentWindow.xaml
    /// </summary>
    public partial class AddComponentWindow : Window
    {
        // input fields data of dialog window
        public string ComponentName { get; set; }
        public string AmountPerMachine { get; set; }
        public string AmountInStock { get; set; }
        public string ImagePath { get; set; }

        // component name list
        public List<string> ExistingComponentNames { get; set; }

        public AddComponentWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        // always png files, not jpg etc..
        private void OnChooseImageClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                if (string.IsNullOrWhiteSpace(ComponentName))
                {
                    MessageBox.Show("Indtast navn før du vælger billede.");
                    return;
                }

                string extension = Path.GetExtension(ofd.FileName); // finds image type (png, jpeg etc)
                string fileName = ComponentName + extension; //name of component image

                string imagesFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "UILayer", "ComponentImages"); //location and name of component image folder

                Directory.CreateDirectory(imagesFolder);

                string savePath = Path.Combine(imagesFolder, fileName); //where image is saved

                File.Copy(ofd.FileName, savePath, true);

                ImagePath = savePath;

                DataContext = null;
                DataContext = this;
            }
        }
    }
}
