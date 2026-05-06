using SnapPexOverview.UILayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SnapPexOverview.ApplicationLayer.Commands
{
    public class OpenProduceMachineWindowCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly MainViewModel mvm;

        public OpenProduceMachineWindowCommand(MainViewModel mainViewModel)
        {
            mvm = mainViewModel;
        }

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            ProduceMachineWindow window = new ProduceMachineWindow();
            window.DataContext = mvm;

            if (window.ShowDialog() == true)
            {
                mvm.ProduceMachines(mvm.MachineAmountToProduce);
            }
        }
    }
}
