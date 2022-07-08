using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightUser.Service.ViewModels
{
    public class FlightBookingResponse
    {
        public int BookingId { get; set; }
        public string Pnr { get; set; }
        public decimal TicketCost { get; set; }

        public string ResponseMessage { get; set; }
    }
}
