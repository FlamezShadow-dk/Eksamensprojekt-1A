using SnapPexOverview.DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnapPexOverview.ApplicationLayer
{
    public class MachineViewModel : BaseViewModel
    {
        private readonly Machine _machine;
        public int MachineNr
        {
            get => _machine.MachineNr;
            set
            {
                _machine.MachineNr = value;
                OnPropertyChanged();
            }
        }

        public int Status
        {
            get => (int)_machine.Status;
            set
            {
                _machine.Status = (MachineStatus)value;
                OnPropertyChanged();
            }
        }
        public MachineViewModel(Machine machine)
        {
            _machine = machine;
        }
    }
}
