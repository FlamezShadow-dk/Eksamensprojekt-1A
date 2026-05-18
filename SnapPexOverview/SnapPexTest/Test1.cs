using SnapPexOverview.DomainLayer;
using SnapPexOverview.PersistenceLayer;
using SnapPexOverview.ServiceLayer;
using System.Collections.ObjectModel;

namespace SnapPexTest
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void ProduceMachines_SubtractsCorrectAmountOfComponents()
        {
            //arrange
            MachineRepository machineRepo = new MachineRepository();
            ComponentRepository componentRepo = new ComponentRepository();
            MachineService service = new MachineService(machineRepo, componentRepo);
            ObservableCollection<Component> comps = new ObservableCollection<Component>
            {
                new Component
                {
                    ComponentName = "Y-ror",
                    AmountPerMachine = 2,
                    AmountInStock = 10,
                    ImagePath = "test.png"
                }
            };

            //act
            service.ProduceMachines(3, "TEST", comps);

            //assert
            Assert.AreEqual(4, comps[0].AmountInStock);
        }

        [TestMethod]
        public void AddComponent_AddsComponentSuccessfully()
        {
            //arrange
            ComponentRepository componentRepo = new ComponentRepository();
            ComponentService componentService = new ComponentService(componentRepo);
            int before = componentService.Components.Count;

            //act
            componentService.AddComponent("Y-ror", 5, 10, "test.png");

            //assert
            Assert.AreEqual(before + 1, componentService.Components.Count);
        }
    }
}
