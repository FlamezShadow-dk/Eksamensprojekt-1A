using SnapPexOverview.ApplicationLayer.Commands;
using SnapPexOverview.DomainLayer;
using SnapPexOverview.PersistenceLayer;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace SnapPexOverview.ApplicationLayer
{
    public class MainViewModel : BaseViewModel
    {
        // repository references
        private readonly ComponentRepository _componentRepo;
        private readonly MachineRepository _machineRepo;

        // observable collection for inotify
        private ObservableCollection<ComponentViewModel> _components = new();
        public ObservableCollection<ComponentViewModel> Components
        {
            get => _components;
            set { _components = value; OnPropertyChanged(); }
        }
        private ObservableCollection<MachineViewModel> _machines = new();
        public ObservableCollection<MachineViewModel> Machines
        {
            get => _machines;
            set { _machines = value; OnPropertyChanged(); }
        }
        
        public ICommand OpenAddComponentWindowCommand { get; }

        public MainViewModel()
        {
            // instantiate repository (dependency)
            _componentRepo = new ComponentRepository();

            // populate observable collection and wrap domain objects
            foreach (Component component in _componentRepo.GetAll())
                Components.Add(new ComponentViewModel(component));

            // instantiate commands
            OpenAddComponentWindowCommand = new OpenAddComponentWindowCommand(this);
        }

        // adds new component row if they dont exist, and updates existing ones.
        public void AddOrUpdateComponent(string name, int perMachine, int inStock)
        {
            // checks db for component
            Component existing = _componentRepo.GetByName(name);
            // try find matching viewmodel in UI list (returns null if not found)
            ComponentViewModel vm = Components.FirstOrDefault(c => c.ComponentName == name);

            if (existing != null)
            {
                //updates perMachine and inStock if component already added
                existing.AmountPerMachine = perMachine;
                existing.AmountInStock += inStock;
                _componentRepo.Update(existing);

                if (vm != null)
                {
                    vm.AmountInStock = existing.AmountInStock;
                    vm.AmountPerMachine = existing.AmountPerMachine;
                }
            }
            else
            {
                Component comp = new Component
                {
                    ComponentName = name,
                    AmountPerMachine = perMachine,
                    AmountInStock = inStock
                };
                // add it to database & wrap it
                _componentRepo.Add(comp);
                Components.Add(new ComponentViewModel(comp));
            }
        }

        public void AddMachine(int nr, int stat)
        {
            Machine mac = new Machine(nr)
            {
                Status = (MachineStatus)stat
            };
            _machineRepo.Add(mac);
            Machines.Add(new MachineViewModel(mac));
        }

    }
}
