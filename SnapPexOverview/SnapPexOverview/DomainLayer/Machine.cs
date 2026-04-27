using System;
using System.Collections.Generic;
using System.Text;

namespace SnapPexOverview.DomainLayer
{
    public enum MachineStatus
    {
        Lager,
        Udlejet,
        Reparation
    }

    public class Machine
    {
        public int MachineNr { get; set; }
        public MachineStatus Status { get; set; }

        public Machine (int machineNr)
        {
            MachineNr = machineNr;
        }

    }
}
