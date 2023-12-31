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
        private double _weatherHumidity;
        private double _weatherTemperature;

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
        public double WeatherHumidity
        {
            get { return _weatherHumidity; }
            set
            {
                if (_weatherHumidity != value)
                {
                    _weatherHumidity = value;
                    OnPropertyChanged(nameof(WeatherHumidity));
                }
            }
        }

        public double WeatherTemperature
        {
            get { return _weatherTemperature; }
            set
            {
                if (_weatherTemperature != value)
                {
                    _weatherTemperature = value;
                    OnPropertyChanged(nameof(WeatherTemperature));
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
