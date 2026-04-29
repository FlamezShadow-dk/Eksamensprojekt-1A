using SnapPexOverview.UILayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SnapPexOverview.ApplicationLayer.Commands
{
    public class OpenAddComponentWindowCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly MainViewModel mvm;

        public OpenAddComponentWindowCommand(MainViewModel mainViewModel)
        {
            mvm = mainViewModel;
        }

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            AddComponentWindow window = new AddComponentWindow();

            // populates the ExistingComponentNames in the dialogwindow
            window.ExistingComponentNames = mvm.Components
                .Select(c => c.ComponentName)
                .ToList();

            window.DataContext = window;

            if (window.ShowDialog() == true)
            {
                // validates input so it doesnt crash if someone types "abc"
                if (int.TryParse(window.AmountPerMachine, out int perMachine) &&
                    int.TryParse(window.AmountInStock, out int inStock))
                {
                    mvm.AddOrUpdateComponent(window.ComponentName, perMachine, inStock, window.ImagePath);
                }
            }
        }
    }
}
