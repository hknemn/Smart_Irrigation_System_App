using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Irrigation_System.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required  double Min_Temperature { get; set; }
        public required double Max_Temperature { get; set; }
        public required double Min_SoilMoisture { get; set; }
        public required double Max_SoilMoisture { get; set; }

    }
}
