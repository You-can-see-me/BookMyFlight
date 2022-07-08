using System;
using System.Collections.Generic;

#nullable disable

namespace FlightSearch.Service.EntityModels
{
    public partial class PassengerDetail
    {
        public int Id { get; set; }
        public int BookingHistoryId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Meal { get; set; }
        public string SeatNumber { get; set; }

        public virtual BookingHistory BookingHistory { get; set; }
    }
}
