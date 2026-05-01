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

        // observable collection for inotify
        private ObservableCollection<ComponentViewModel> _components = new();
        public ObservableCollection<ComponentViewModel> Components
        {
            get => _components;
            set { _components = value; OnPropertyChanged(); }
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
        
        public void AddOrUpdateComponent(string name, int perMachine, int inStock, string imagePath)
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
    }

}
