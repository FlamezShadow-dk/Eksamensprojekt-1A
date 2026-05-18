using SnapPexOverview.ApplicationLayer.ViewModels;
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
    /// Interaction logic for ProduceMachineWindow.xaml
    /// </summary>
    public partial class ProduceMachineWindow : Window
    {
        public ProduceMachineWindow()
        {
            InitializeComponent();
        }

        private void OnProduceClick(object sender, RoutedEventArgs e)
        {
            MainViewModel mvm = (MainViewModel)DataContext;

            if (mvm.MachineAmountToProduce <= 0)
            {
                MessageBox.Show(
                    "Indtast et gyldigt antal maskiner.",
                    "Validation Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(mvm.ProductionReference))
            {
                MessageBox.Show(
                    "Indtast en produktionsreference.",
                    "Validation Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            DialogResult = true;
            Close();
        }
    }
}
