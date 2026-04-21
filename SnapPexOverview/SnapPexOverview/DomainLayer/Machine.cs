using System;
using System.Collections.Generic;
using System.Text;

namespace SnapPexOverview.DomainLayer
{
    public class Machine
    {
        public string MachineNr { get; set; }

        public enum MachineStatus
        {
            Lager,
            Udlejet,
            Reparation
        }
    }
}
