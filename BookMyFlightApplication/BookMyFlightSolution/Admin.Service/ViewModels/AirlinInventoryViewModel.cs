using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Service.ViewModels
{
    public class AirlinInventoryViewModel
    {
        public string AirlineName { get; set; }
        ////public byte[] AirlineLogo { get; set; }
        public string AirlineStatus { get; set; }
        public string FlightNumber { get; set; }
        public string Departure { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime DestinationDateTime { get; set; }
        public string ScheduledDays { get; set; }
        public string ModelType { get; set; }
        public int NoOfBusinessClassSeats { get; set; }
        public int NoOfNonBusinessClassSeats { get; set; }
        public decimal TicketCost { get; set; }
        public int NumberOfRows { get; set; }
        public string Meal { get; set; }

    }
}
