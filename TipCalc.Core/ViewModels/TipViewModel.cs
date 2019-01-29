using MvvmCross.ViewModels;
using TipCalc.Core.Services;
using System.Threading.Tasks;
using MvvmCross.Commands;
using System.Windows.Input;
using Plugin.TextToSpeech.Abstractions;

namespace TipCalc.Core.ViewModels
{
    public class TipViewModel : MvxViewModel
    {
        readonly ICalculationService _calculationService;

        public TipViewModel(ICalculationService calculationService, 
            ITextToSpeech textToSpeech)
        {
            _calculationService = calculationService ?? throw new System.ArgumentNullException(nameof(calculationService));

            ClickCommand3 = new MvxCommand(clickCommand_Execute);

            SpeakCommand = new MvxCommand(() => textToSpeech.Speak($"You spent {SubTotal}?!"));
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            _subTotal = 100;
            _generosity = 10;

            Recalculate();
        }

        private double _subTotal;
        public double SubTotal
        {
            get => _subTotal;
            set
            {
                _subTotal = value;
                RaisePropertyChanged(() => SubTotal);

                Recalculate();
            }
        }

        private int _generosity;
        public int Generosity
        {
            get => _generosity;
            set
            {
                _generosity = value;
                RaisePropertyChanged(() => Generosity);

                Recalculate();
            }
        }

        private double _tip;
        public double Tip
        {
            get => _tip;
            set
            {
                _tip = value;
                RaisePropertyChanged(() => Tip);
            }
        }

        private void Recalculate()
        {
            Tip = _calculationService.TipAmount(SubTotal, Generosity);
        }

        public void ClickMe()
        {
            SubTotal *= 10;
        }

        private MvxCommand clickCommand;

        public MvxCommand ClickCommand => clickCommand ?? (clickCommand = new MvxCommand(() => SubTotal *= 10));
        public ICommand ClickCommand2
        {
            get
            {
                if(clickCommand == null)
                {
                    clickCommand = new MvxCommand(clickCommand_Execute);
                }
                return clickCommand;
            }
        }
        public ICommand ClickCommand3 { get; private set; }
        public MvxCommand SpeakCommand { get; }

        private void clickCommand_Execute()
        {
            SubTotal *= 10;
        }
    }
}