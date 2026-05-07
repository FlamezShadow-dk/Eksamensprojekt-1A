using SnapPexOverview.ApplicationLayer.Commands;
using SnapPexOverview.DomainLayer;
using SnapPexOverview.PersistenceLayer;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
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

        public ICommand OpenUpdateComponentWindowCommand { get; }

        public ICommand OpenProduceMachineWindowCommand { get; }

        public MainViewModel()
        {
            // instantiate repository (dependency)
            _componentRepo = new ComponentRepository();
            _machineRepo = new MachineRepository();

            // populate observable collection and wrap domain objects
            foreach (Component component in _componentRepo.GetAll())
                Components.Add(new ComponentViewModel(component));
            foreach (Machine machine in _machineRepo.GetAll())
                Machines.Add(new MachineViewModel(machine));

            // instantiate commands
            OpenAddComponentWindowCommand = new OpenAddComponentWindowCommand(this);

            OpenUpdateComponentWindowCommand = new OpenUpdateComponentWindowCommand(this);

            OpenProduceMachineWindowCommand = new OpenProduceMachineWindowCommand(this);
        }
        
        public void UpdateComponent(string name, int perMachine, string imagePath)
        {
            // checks db for component
            Component existing = _componentRepo.GetByName(name);
            // try find matching viewmodel in UI list (returns null if not found)
            ComponentViewModel vm = Components.FirstOrDefault(c => c.ComponentName == name);

            if (existing != null)
            {
                //updates perMachine and inStock if component already added
                existing.AmountPerMachine = perMachine;
                existing.AmountInStock += AmountToAdd;

                if (!string.IsNullOrEmpty(imagePath))
                {
                    existing.ImagePath = imagePath;
                }

                _componentRepo.Update(existing);

                if (vm != null)
                {
                    vm.AmountInStock = existing.AmountInStock;
                    vm.AmountPerMachine = existing.AmountPerMachine;

                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        vm.ImagePath = imagePath;
                    }
                }
            }
        }

        public void AddComponent(string name, int perMachine, int inStock, string imagePath)
        {
            // checks db for component
            Component existing = _componentRepo.GetByName(name);

            if (existing != null)
            {
                MessageBox.Show(
                        $"Komponent: {existing.ComponentName} eksistere allerede",
                        "Component Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                return;
            }
            else
            {
                Component comp = new Component
                {
                    ComponentName = name,
                    AmountPerMachine = perMachine,
                    AmountInStock = inStock,
                    ImagePath = imagePath
                };
                // add it to database & wrap it
                _componentRepo.Add(comp);
                Components.Add(new ComponentViewModel(comp));
            }
        }


        // removed after demo
        private ComponentViewModel _selectedComponent;
        public ComponentViewModel SelectedComponent
        {
            get => _selectedComponent;
            set
            {
                if (_selectedComponent != value)
                {
                    _selectedComponent = value;
                    OnPropertyChanged();

                    if (value != null)
                    {
                        ComponentName = value.ComponentName;
                        
                        AmountPerMachine = value.AmountPerMachine;

                        AmountInStock = value.AmountInStock;

                        ImagePath = value.ImagePath;
                    }
                }
            }
        }

        private string _componentName;
        public string ComponentName
        {
            get => _componentName;
            set
            {
                if (_componentName != value)
                {
                    _componentName = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _amountInStock;
        public int AmountInStock
        {
            get => _amountInStock;
            set
            {
                if (_amountInStock != value)
                {
                    _amountInStock = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _amountPerMachine;
        public int AmountPerMachine
        {
            get => _amountPerMachine;
            set
            {
                if (_amountPerMachine != value)
                {
                    _amountPerMachine = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _imagePath;
        public string ImagePath
        {
            get => _imagePath;
            set
            {
                if (_imagePath != value)
                {
                    _imagePath = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _amountToAdd = 0;
        public int AmountToAdd
        {
            get => _amountToAdd;
            set
            {
                _amountToAdd = value;
                OnPropertyChanged();
            }
        }

        // machine stuff
        public void ProduceMachines(int amount)
        {
            string compNames = "";
            foreach (ComponentViewModel comp in Components)
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
                MessageBox.Show(
                        $"Ikke nok antal af:\n\n{compNames}",
                        "Production Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                return;
            }

            foreach (ComponentViewModel comp in Components)
            {
                // update viewmodel
                comp.AmountInStock -= comp.AmountPerMachine * amount;

                // db update
                Component updated = new Component
                {
                    ComponentName = comp.ComponentName,
                    AmountPerMachine = comp.AmountPerMachine,
                    AmountInStock = comp.AmountInStock,
                    ImagePath = comp.ImagePath
                };
                _componentRepo.Update(updated);
            }

            // create machine
            for (int i = 0; i < amount; i++)
            {
                Machine machine = new Machine(0)
                {
                    Status = MachineStatus.InStock
                };

                int newMachineNr = _machineRepo.Add(machine);
                machine.MachineNr = newMachineNr;
                Machines.Add(new MachineViewModel(machine));
            }
        }

        private int _machineAmountToProduce;
        public int MachineAmountToProduce
        {
            get => _machineAmountToProduce;
            set
            {
                _machineAmountToProduce = value;
                OnPropertyChanged();
            }
        }

    }

}
