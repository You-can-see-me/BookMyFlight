using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSearch.Service.ViewModels
{
    public class FlightSearchInputModel
    {
        public string Departure { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public bool? IsRoundTrip { get; set; }
        public DateTime? ReturnDateTime { get; set; }
    }
}
