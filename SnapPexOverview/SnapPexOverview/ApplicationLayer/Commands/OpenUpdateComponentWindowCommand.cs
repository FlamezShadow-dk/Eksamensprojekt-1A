using SnapPexOverview.UILayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SnapPexOverview.ApplicationLayer.Commands
{
    public class OpenUpdateComponentWindowCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly MainViewModel mvm;

        public OpenUpdateComponentWindowCommand(MainViewModel mainViewModel)
        {
            mvm = mainViewModel;
        }

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            UpdateComponentWindow window = new UpdateComponentWindow();

            window.DataContext = mvm;

            if (window.ShowDialog() == true)
            {
                mvm.UpdateComponent(
                        mvm.ComponentName,
                        mvm.AmountPerMachine,
                        mvm.ImagePath);
            }
        }
    }
}
