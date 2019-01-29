using Moq;
using MvvmCross.Tests;
using NUnit.Framework;
using Plugin.TextToSpeech.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using TipCalc.Core.Services;
using TipCalc.Core.ViewModels;

namespace TipCalc.Tests
{
    public class AddDrinksTests : MvxIoCSupportingTest
    {
        [Test]
        public void AddDrinksIncreasesSubtotalBy10x()
        {
            base.Setup();
            var calcSvcMock = new Mock<ICalculationService>();            
            calcSvcMock.Setup(m => m.TipAmount(It.IsAny<double>(), It.IsAny<int>())).Returns(4);
            
            var vm = new TipViewModel(calcSvcMock.Object, null);

            var origSubtotal = 50;
            vm.SubTotal = origSubtotal;

            vm.ClickCommand2.Execute(this);

            var newSubTotal = vm.SubTotal;

            Assert.AreEqual(origSubtotal * 10, newSubTotal);

        }
    }
}
