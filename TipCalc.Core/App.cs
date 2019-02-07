using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.TextToSpeech;
using Plugin.TextToSpeech.Abstractions;
using TipCalc.Core.Services;
using TipCalc.Core.ViewModels;

namespace TipCalc.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            //Mvx.IoCProvider.RegisterType<IMvxNavigationService, MvxNavigationService>();
            Mvx.IoCProvider.RegisterType<ICalculationService, CalculationService>();
            Mvx.RegisterSingleton(CrossTextToSpeech.Current);

            RegisterAppStart<TipViewModel>();
        }
    }
}