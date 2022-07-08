using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightUser.Service.ViewModels
{
    public class GetBookingHistoryModel
    {
        public string Pnr { get; set; }
        public int BookingId { get; set; }
        public string AirlineName { get; set; }
        public decimal TicketCost { get; set; }
        public DateTime DateOfJourney { get; set; }
        public string BookingStatus { get; set; }
    }
}
