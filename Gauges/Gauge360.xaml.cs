using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;

namespace Smart_Irrigation_System.Gauges
{
    /// <summary>
    /// Interaction logic for Gauge360.xaml
    /// </summary>
    public partial class Gauge360 : UserControl
    {
        public Gauge360()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(Gauge360), new PropertyMetadata(default(double), OnValueChanged));

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Gauge360 gauge)
            {
                gauge.UpdateGaugeValue((double)e.NewValue);
            }
        }

        public void UpdateGaugeValue(double humidityValue)
        {
            gaugeSensor.Value = humidityValue;
        }
    }
}
