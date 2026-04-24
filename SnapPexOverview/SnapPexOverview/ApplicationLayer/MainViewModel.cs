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

        private string _newComponentName;
        public string NewComponentName
        {
            get => _newComponentName;
            set { _newComponentName = value; OnPropertyChanged(); }
        }

        private int _newAmountPerMachine;
        public int NewAmountPerMachine
        {
            get => _newAmountPerMachine;
            set { _newAmountPerMachine = value; OnPropertyChanged(); }
        }
        private int _newAmountInStock;
        public int NewAmountInStock
        {
            get => _newAmountInStock;
            set { _newAmountInStock = value; OnPropertyChanged(); }
        }
        public ICommand AddComponentCommand { get; }

        public MainViewModel()
        {
            // instantiate repository (dependency)
            _componentRepo = new ComponentRepository();

            // populate observable collection and wrap domain objects
            foreach (Component component in _componentRepo.GetAll())
                Components.Add(new ComponentViewModel(component));
        }

        public void AddComponent()
        {
            Component comp = _componentRepo.CreateComponent(NewComponentName, NewAmountPerMachine, NewAmountInStock );
            Components.Add(new ComponentViewModel(comp));
        }


    }
}
