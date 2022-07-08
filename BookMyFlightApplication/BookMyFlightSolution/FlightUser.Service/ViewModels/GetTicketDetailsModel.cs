using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightUser.Service.ViewModels
{
    public class GetTicketDetailsModel
    {
        public int Id { get; set; }
        public string EmailId { get; set; }
        public string Name { get; set; }
        public int NoOfSeats { get; set; }
        public string BookedBy { get; set; }
        public string BookedOn { get; set; }
        public int InventoryId { get; set; }
        public string DiscountUsed { get; set; }
        public string Pnr { get; set; }
        public int UserId { get; set; }
        public string SeatType { get; set; }
        public string BookingStatus { get; set; }
        public decimal TicketCost { get; set; }
        public List<PassengerDetailList> passengerDetailList { get; set; }

    }
}
