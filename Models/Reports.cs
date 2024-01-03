using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Irrigation_System.Models
{
    public class Report
    {
        public int Id { get; set; }
        public required string City_Name { get; set; }
        public required string Product_Name { get; set; }
        public required double Before_Irrigation_Humidity { get; set; }
        public required double After_Irrigation_Humidity { get; set; }
        public required string Description {  get; set; }
        public required string Date {  get; set; }
    }
}
