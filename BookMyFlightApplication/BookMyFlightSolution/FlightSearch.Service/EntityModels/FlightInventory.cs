using System;
using System.Collections.Generic;

#nullable disable

namespace FlightSearch.Service.EntityModels
{
    public partial class FlightInventory
    {
        public FlightInventory()
        {
            BookingHistories = new HashSet<BookingHistory>();
        }

        public int Id { get; set; }
        public string AirlineName { get; set; }
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
        public byte[] AirlineLogo { get; set; }
        public string AirlineStatus { get; set; }

        public virtual ICollection<BookingHistory> BookingHistories { get; set; }
    }
}
