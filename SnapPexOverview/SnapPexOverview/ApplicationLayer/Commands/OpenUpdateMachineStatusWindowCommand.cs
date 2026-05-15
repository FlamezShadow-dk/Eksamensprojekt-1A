using SnapPexOverview.UILayer;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Windows.Input;

namespace SnapPexOverview.ApplicationLayer.Commands
{
    public class OpenUpdateMachineStatusWindowCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly MainViewModel mvm;

        public OpenUpdateMachineStatusWindowCommand(MainViewModel mainViewModel)
        {
            mvm = mainViewModel;
        }

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            UpdateMachineStatusWindow window = new UpdateMachineStatusWindow();
            window.DataContext = mvm;

            if (window.ShowDialog() == true)
            {
                mvm.UpdateMachineStatus(mvm.SelectedMachineStatus);
            }
        }

    }
}
