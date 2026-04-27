using SnapPexOverview.DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnapPexOverview.ApplicationLayer
{
    public class ComponentViewModel : BaseViewModel
    {
        private readonly Component _component;

        public string ComponentName
        {
            get => _component.ComponentName;
            set
            {
                _component.ComponentName = value;
                OnPropertyChanged();
            }
        }

        public int AmountPerMachine
        {
            get => _component.AmountPerMachine;
            set
            {
                _component.AmountPerMachine = value;
                OnPropertyChanged();
            }
        }

        public int AmountInStock
        {
            get => _component.AmountInStock;
            set
            {
                _component.AmountInStock = value;
                OnPropertyChanged();
            }
        }

        public ComponentViewModel(Component component)
        {
            _component = component;
        }
    }
}
