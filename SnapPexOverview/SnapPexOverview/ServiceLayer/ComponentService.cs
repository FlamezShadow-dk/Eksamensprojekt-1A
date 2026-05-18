using SnapPexOverview.DomainLayer;
using SnapPexOverview.PersistenceLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SnapPexOverview.ServiceLayer
{
    public class ComponentService
    {
        private readonly ComponentRepository _componentRepo;

        private ObservableCollection<Component> _components;
        public ObservableCollection<Component> Components
        {
            get => _components;
        }

        public ComponentService(ComponentRepository componentRepo)
        {
            _componentRepo = componentRepo;

            _components = new ObservableCollection<Component>(_componentRepo.GetAll());
        }

        public void AddComponent(string name, int perMachine, int inStock, string imagePath)
        {
            // checks db for component
            Component existing = _componentRepo.GetByName(name);

            if (existing != null)
            {
                throw new ArgumentException(
                    $"Komponent: {existing.ComponentName} eksistere allerede");
            }

            Component comp = new Component
            {
                ComponentName = name,
                AmountPerMachine = perMachine,
                AmountInStock = inStock,
                ImagePath = imagePath
            };

            // add it to database & wrap it
            _componentRepo.Add(comp);

            _components.Add(comp);
        }

        public void UpdateComponent(string name, int perMachine, int amountToAdd, string imagePath)
        {
            // checks db for component
            Component existing = _componentRepo.GetByName(name);

            if (existing == null)
            {
                throw new ArgumentException(
                    "Komponenten findes ikke.");
            }
            existing.AmountPerMachine = perMachine;
            existing.AmountInStock += amountToAdd;

            if (!string.IsNullOrWhiteSpace(imagePath))
            {
                existing.ImagePath = imagePath;
            }
            _componentRepo.Update(existing);
        }
    }
}
