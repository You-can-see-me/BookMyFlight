using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightUser.Service.ViewModels
{
    public class FlightBookingRequest
    {
        public string EmailId { get; set; }
        public string Name { get; set; }
        public int NoOfSeats { get; set; }
        public string BookedBy { get; set; }
        public string BookedOn { get; set; }
        public int InventoryId { get; set; }
        public string DiscountUsed { get; set; }
        public int UserId { get; set; }
        public string SeatType { get; set; }
        public List<PassengerDetailList> passengerDetailList { get; set; }
    }

    public class PassengerDetailList
    {
        public int BookingHistoryId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Meal { get; set; }
        public string SeatNumber { get; set; }
    }
}
