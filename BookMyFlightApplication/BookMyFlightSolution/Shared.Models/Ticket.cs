using System;

namespace Shared.Models
{
    public class Ticket
    {
        public int FlightInventoryId { get; set; }
        public int NoOfSeats { get; set; }

        public string SeatType { get; set; }
    }
}
