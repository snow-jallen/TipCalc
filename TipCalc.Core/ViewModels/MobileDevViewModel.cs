using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace TipCalc.Core.ViewModels
{
    public class SimpleCommand : ICommand
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public SimpleCommand(Action execute, Func<bool> canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return canExecute();
        }

        public void Execute(object parameter)
        {
            if(canExecute())
                execute();
        }
    }

    public class MobileDevViewModel : INotifyPropertyChanged
    {


        private ICommand doCalculation;
        public ICommand DoCalculation => doCalculation ?? (doCalculation = new SimpleCommand(
            //execute
            ()=> 
            {
                //whatever we'd do to actually execute
                InterestRate += 5;
            },
            //can execute
            ()=> 
            {
                return (InterestRate > 2);
            })
            );

        private decimal intRate;
        public decimal InterestRate
        {
            get => intRate;
            set
            {
                SetField(ref intRate, value);
            }
        }

        

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        #endregion

    }
}
