using MvvmCross.ViewModels;
using TipCalc.Core.Services;
using System.Threading.Tasks;
using MvvmCross.Commands;
using System.Windows.Input;
using Plugin.TextToSpeech.Abstractions;
using System.Threading;
using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using MvvmCross.Navigation;

namespace TipCalc.Core.ViewModels
{
    public class Computer
    {
        public string SerialNumber { get; set; }
        public string MacAddress { get; set; }
        public int Position { get; set; }
        public override string ToString()
        {
            return $"{SerialNumber} ({MacAddress}) @ {Position}";
        }
    }

    public class TipViewModel : MvxViewModel
    {
        readonly ICalculationService _calculationService;
        private readonly IMvxNavigationService navigationService;
        private List<Computer> computers;
        public IEnumerable<Computer> Computers { get => computers; }
        string filePath = null;

        public TipViewModel(ICalculationService calculationService, 
            ITextToSpeech textToSpeech,
            IMvxNavigationService navigationService)
        {            
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            filePath = Path.Combine(path, "computers.json");

            if (File.Exists(filePath))
            {
                var fileContents = File.ReadAllText(filePath);
                computers = JsonConvert.DeserializeObject<List<Computer>>(fileContents);
                FileContents = computers[0].ToString();
            }
            else
                computers = new List<Computer>();
            
            _calculationService = calculationService ?? throw new System.ArgumentNullException(nameof(calculationService));
            this.navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            ClickCommand3 = new MvxCommand(clickCommand_Execute);

            SpeakCommand = new MvxCommand(() =>
            {
                //textToSpeech.Speak($"You spent {SubTotal}?!");

                //var fileContents = JsonConvert.SerializeObject(computers);
                //File.WriteAllText(filePath, fileContents);

                navigationService.Navigate<DetailViewModel>();
            });
        }

        public string FileContents { get; set; }

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

                Task.Run(()=>Recalculate());
            }
        }

        private double _tip;
        public double Tip
        {
            get => _tip;
            set => SetProperty(ref _tip, value);
        }

        private void Recalculate()
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));
            var localTip = _calculationService.TipAmount(SubTotal, Generosity);
            AsyncDispatcher.ExecuteOnMainThreadAsync(() => Tip = localTip);
        }

        public void ClickMe()
        {
            SubTotal *= 10;
        }

        private MvxCommand clickCommand;

        public MvxCommand ClickCommand => clickCommand ?? (clickCommand = new MvxCommand(() =>
        {
            SubTotal *= 10;
            computers.Add(new Computer
            {
                MacAddress = DateTime.Now.ToLongDateString(),
                SerialNumber = DateTime.Now.Ticks.ToString(),
                Position = computers.Count
            });
        }));
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