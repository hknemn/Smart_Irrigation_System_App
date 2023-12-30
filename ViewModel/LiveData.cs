using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Irrigation_System.ViewModel
{
    public class LiveData : INotifyPropertyChanged
    {
        private double _humidityValue;

        public double HumidityValue
        {
            get { return _humidityValue; }
            set
            {
                if (_humidityValue != value)
                {
                    _humidityValue = value;
                    OnPropertyChanged(nameof(HumidityValue));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
