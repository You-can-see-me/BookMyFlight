using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSearch.Service.ViewModels
{
    public class FlightSearchOutputModel
    {
        public DateTime FlightDateTime { get; set; }
        public string AirlineName { get; set; }
        public decimal TicketCost { get; set; }

        public int FlightInventoryId { get; set; }
        public string FlightNumber { get; set; }
        public string Departure { get; set; }
        public string Destination { get; set; }
    }
}
