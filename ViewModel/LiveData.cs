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
        private double _soilMoisture;
        private double _weatherHumidity;
        private double _weatherTemperature;
        private double _temperature;

        public double Temperature
        {
            get { return _temperature; }
            set
            {
                if (_temperature != value)
                {
                    _temperature = value;
                    OnPropertyChanged(nameof(Temperature));
                }
            }
        }

        public double SoilMoisture
        {
            get { return _soilMoisture; }
            set
            {
                if (_soilMoisture != value)
                {
                    _soilMoisture = value;
                    OnPropertyChanged(nameof(SoilMoisture));
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
