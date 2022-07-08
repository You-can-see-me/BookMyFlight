using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSearch.Service.ViewModels
{
    public class RoundTripFlightSearchOutputModel
    {
        public List<FlightSearchOutputModel> ToFlightSearchOutputList { get; set; }
        public List<FlightSearchOutputModel> FromFlightSearchOutputList { get; set; }
    }
}
