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

namespace TipCalc.Core.ViewModels
{
    public class DetailViewModel : MvxViewModel
    {
        public DetailViewModel()
        {
            Details = DateTime.Now.ToString();
        }

        public string Details { get; set; }

        public override async Task Initialize()
        {
            await base.Initialize();
        }        
    }
}