using SnapPexOverview.ApplicationLayer.Commands;
using SnapPexOverview.DomainLayer;
using SnapPexOverview.PersistenceLayer;
using SnapPexOverview.ServiceLayer;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace SnapPexOverview.ApplicationLayer.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        // repository references
        private readonly ComponentService _componentService;
        private readonly MachineService _machineService;

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

        public ICommand OpenUpdateMachineStatusWindowCommand { get; }

        public MainViewModel()
        {
            //repositories
            ComponentRepository componentRepo = new ComponentRepository();
            MachineRepository machineRepo = new MachineRepository();

            //services
            _componentService = new ComponentService(componentRepo);
            _machineService = new MachineService(machineRepo, componentRepo);

            // populate collections from services
            foreach (Component comp in _componentService.Components)
                Components.Add(new ComponentViewModel(comp));
            foreach (Machine mach in _machineService.Machines)
                Machines.Add(new MachineViewModel(mach));
            

            // instantiate commands
            OpenAddComponentWindowCommand = new OpenAddComponentWindowCommand(this);

            OpenUpdateComponentWindowCommand = new OpenUpdateComponentWindowCommand(this);

            OpenProduceMachineWindowCommand = new OpenProduceMachineWindowCommand(this);

            OpenUpdateMachineStatusWindowCommand = new OpenUpdateMachineStatusWindowCommand(this);
        }
        
        public void UpdateComponent(string name, int perMachine, string imagePath)
        {
            try
            {
                _componentService.UpdateComponent(name, perMachine, AmountToAdd, imagePath);
                Components.Clear();

                foreach (Component comp in _componentService.Components)
                {
                    Components.Add(new ComponentViewModel(comp));
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message,
                    "Component Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        public void AddComponent(string name, int perMachine, int inStock, string imagePath)
        {
            try
            {
                _componentService.AddComponent(name, perMachine, inStock, imagePath);
                Components.Clear();

                foreach (Component comp in _componentService.Components)
                {
                    Components.Add(new ComponentViewModel(comp));
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message,
                    "Component Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
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
        public void ProduceMachines(int amount, string productionReference)
        {
            try
            {
                ObservableCollection<Component> domainComponents = new();

                foreach(ComponentViewModel vm in Components)
                {
                    domainComponents.Add(new Component
                    {
                        ComponentName = vm.ComponentName,
                        AmountPerMachine = vm.AmountPerMachine,
                        AmountInStock = vm.AmountInStock,
                        ImagePath = vm.ImagePath
                    });
                }
                _machineService.ProduceMachines(amount, productionReference, domainComponents);
                Components.Clear();

                foreach(Component comp in domainComponents)
                {
                    Components.Add(new ComponentViewModel(comp));
                }
                Machines.Clear();

                foreach (Machine machine in _machineService.Machines)
                {
                    Machines.Add(new MachineViewModel(machine));
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message,
                    "Production Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
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

        private string _productionReference = "";
        public string ProductionReference
        {
            get => _productionReference;
            set
            {
                _productionReference = value?.ToUpper() ?? "";
                OnPropertyChanged();
            }
        }

        //machine status stuff
        private MachineViewModel _selectedMachine;
        public MachineViewModel SelectedMachine
        {
            get => _selectedMachine;
            set
            {
                _selectedMachine = value;
                OnPropertyChanged();

                if (value != null)
                {
                    SelectedMachineStatus = value.StatusText;
                }
            }
        }

        private string _selectedMachineStatus;
        public string SelectedMachineStatus
        {
            get => _selectedMachineStatus;
            set
            {
                _selectedMachineStatus = value;
                OnPropertyChanged();
            }
        }

        public List<string> MachineStatuses { get; } = new()
        {
            "På Lager",
            "Udlejet",
            "Reparation"
        };

        public void UpdateMachineStatus(string statusText)
        {
            Machine domainMachine = new Machine(SelectedMachine.MachineNr)
            {
                Status = SelectedMachine.Status,
                ProductionReference = SelectedMachine.ProductionReference
            };

            _machineService.UpdateMachineStatus(domainMachine, statusText);

            SelectedMachine.Status = domainMachine.Status;
        }
    }
}
