using SnapPexOverview;
using SnapPexOverview.ApplicationLayer;

namespace SnapPex_Test
{
    [TestClass]
    public class UnitTest1
    {


        [TestMethod]
        public void CheckIfAddMachineWorks()
        {
            // Arrange
            MainViewModel mvm = new MainViewModel();

            // Act
            mvm.AddMachine(1,1);

            // Assert
            Assert.AreEqual(1, mvm.Machines.Count);
        }
    }
}
