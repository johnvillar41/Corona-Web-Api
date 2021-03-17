using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftEng2BackendAPI.Models
{
    public class MclTotalModel
    {
        public int TotalConfirmed { get; set; }
        public int TotalTested { get; set; }
        public int TotalDeaths { get; set; }
        public int TotalRecovered { get; set; }
    }
}
