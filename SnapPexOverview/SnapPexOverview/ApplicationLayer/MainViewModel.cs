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

        public void AddComponent(string name, int perMachine, int inStock)
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
}
