using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
    }
}
