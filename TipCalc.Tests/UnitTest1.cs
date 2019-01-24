using NUnit.Framework;
using TipCalc.Core.Services;
using TipCalc.Core.ViewModels;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void BogusCalculationServiceTest()
        {
            var calcSvc = new WaitersDreamCalculationService(); 
            var vm = new TipViewModel(calcSvc);

            vm.SubTotal = 1234;
            vm.Generosity = 10;

            var actual = vm.Tip;
            var expected = 123.4;

            Assert.AreEqual(expected, actual, "Expected tip amount does not match.");
        }
    }

    public class WaitersDreamCalculationService : ICalculationService
    {
        public double TipAmount(double subTotal, int generosity)
        {
            return double.PositiveInfinity;
        }
    }
}