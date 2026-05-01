using Microsoft.Win32;
using SnapPexOverview.ApplicationLayer;
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

        public AddComponentWindow()
        {
            InitializeComponent();
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        // always png files, not jpg etc..
        private void OnChooseImageClick(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as MainViewModel;
            if (vm == null) return;
            
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                if (string.IsNullOrWhiteSpace(vm.ComponentName))
                {
                    MessageBox.Show("Indtast navn før du vælger billede.");
                    return;
                }

                string extension = Path.GetExtension(ofd.FileName); // finds image type (png, jpeg etc)
                string fileName = vm.ComponentName + extension; //name of component image

                string imagesFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "UILayer", "ComponentImages"); //location and name of component image folder

                Directory.CreateDirectory(imagesFolder);

                string savePath = Path.Combine(imagesFolder, fileName); //where image is saved

                File.Copy(ofd.FileName, savePath, true);

                vm.ImagePath = savePath;
            }
        }
    }
}
