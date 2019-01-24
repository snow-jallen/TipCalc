using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TipCalc.Core.ViewModels
{
    public class SquareRootViewModel : INotifyPropertyChanged
    {
        public SquareRootViewModel()
        {
            Number1 = 5;
        }

        private int num1;
        public int Number1
        {
            get => num1;
            set
            {
                num1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Number1)));
            }
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { SetField(ref firstName, value); }
        }

        private int myVar;
        public int MyProperty
        {
            get { return myVar; }
            set { SetField(ref myVar, value); }
        }


        private int num2;
        public int Num2
        {
            get => num2;
            set
            {
                //update num2 and tell the world what "Num2" has a new value.
                SetField(ref num2, value);

                //update num1 and tell the world that "Number1" has a new value.
                SetField(ref num1, value, nameof(Number1));
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
