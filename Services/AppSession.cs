using Smart_Irrigation_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Irrigation_System.Services
{
    public class AppSession
    {
        public static  User? LoggedInUser { get; set; }
    }
}
