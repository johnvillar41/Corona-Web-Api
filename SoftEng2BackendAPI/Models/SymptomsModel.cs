using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftEng2BackendAPI.Models
{
    public class SymptomsModel
    {
        public int Symptoms_ID { get; set; }
        public int User_ID { get; set; }
        public string Symptoms_Name { get; set; }       
    }
}
