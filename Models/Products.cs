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
        public required  double Temperature { get; set; }
        public required double Humidity { get; set; }
    }
}
