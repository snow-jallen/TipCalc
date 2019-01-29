using MvvmCross.Tests;
using NUnit.Framework;
using TipCalc.Core.Services;
using TipCalc.Core.ViewModels;

namespace Tests
{
    public class Tests : MvxIoCSupportingTest
    {
        [SetUp]
        public void Setup()
        {
            base.Setup();
        }

        [Test]
        public void BogusCalculationServiceTest()
        {
            var calcSvc = new CalculationService(); 
            var vm = new TipViewModel(calcSvc, null);

            vm.SubTotal = 1234;
            vm.Generosity = 10;

            var actual = vm.Tip;
            var expected = 123.4;

            Assert.AreEqual(expected, actual, "Expected tip amount does not match.");
        }

        [Test]
        public void TestCommand()
        {
            var vm = new TipViewModel(new CalculationService(), null);

            double originalSubTotal = 25;
            vm.SubTotal = originalSubTotal;

            vm.ClickCommand.Execute();

            Assert.AreEqual(originalSubTotal * 10, vm.SubTotal);
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