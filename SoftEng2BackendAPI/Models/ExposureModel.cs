using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftEng2BackendAPI.Models
{
    public class ExposureModel
    {
        public int Exposure_ID { get; set; }
        public int User_ID { get; set; }
        public int Exposed_To_ID { get; set; }
        public DateTime Exposed_Date { get; set; }
    }
}
