using SnapPexOverview.DomainLayer;
using SnapPexOverview.PersistenceLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SnapPexOverview.ServiceLayer
{
    public class MachineService
    {
        private readonly MachineRepository _machineRepo;
        private readonly ComponentRepository _componentRepo;

        private ObservableCollection<Machine> _machines;
        public ObservableCollection<Machine> Machines
        {
            get => _machines;
        }

        public MachineService(MachineRepository machineRepo, ComponentRepository componentRepo)
        {
            _machineRepo = machineRepo;
            _componentRepo = componentRepo;

            _machines = new ObservableCollection<Machine>(_machineRepo.GetAll());
        }

        public void UpdateMachineStatus(Machine machine, string statusText)
        {
            MachineStatus newStatus = statusText switch
            {
                "På Lager" => MachineStatus.InStock,
                "Udlejet" => MachineStatus.RentedOut,
                "Reparation" => MachineStatus.Repair,
                _ => MachineStatus.InStock //default case prevents crash
            };

            machine.Status = newStatus;

            _machineRepo.Update(machine);
        }

        public void ProduceMachines(int amount, string productionReference, ObservableCollection<Component> components)
        {
            string compNames = "";

            foreach (Component comp in components)
            {
                // validation
                int required = comp.AmountPerMachine * amount;
                if (comp.AmountInStock < required)
                {
                    compNames += $"{comp.ComponentName}\n";
                }
            }

            if (!string.IsNullOrWhiteSpace(compNames))
            {
                throw new ArgumentException(
                        $"Ikke nok antal af:\n\n{compNames}");
            }

            foreach (Component comp in components)
            {
                comp.AmountInStock -= comp.AmountPerMachine * amount;

                _componentRepo.Update(comp);
            }

            // create machine
            for (int i = 0; i < amount; i++)
            {
                Machine machine = new Machine(0)
                {
                    Status = MachineStatus.InStock,
                    ProductionReference = productionReference
                };

                int newMachineNr = _machineRepo.Add(machine);
                machine.MachineNr = newMachineNr;
                _machines.Add(machine);
            }
        }
    }
}
