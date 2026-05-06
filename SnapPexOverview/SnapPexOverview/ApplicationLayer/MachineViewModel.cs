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

        public MachineStatus Status
        {
            get => _machine.Status;
            set
            {
                _machine.Status = value;
                OnPropertyChanged();
            }
        }

        public string StatusText
        {
            get
            {
                switch (_machine.Status)
                {
                    case MachineStatus.InStock:
                        return "På Lager";
                    case MachineStatus.RentedOut:
                        return "Udlejet";
                    case MachineStatus.Repair:
                        return "Reperation";
                    default:
                        return "";
                }
            }
        }

        public MachineViewModel(Machine machine)
        {
            _machine = machine;
        }
    }
}
