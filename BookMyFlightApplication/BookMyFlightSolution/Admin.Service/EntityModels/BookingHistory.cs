using System;
using System.Collections.Generic;

#nullable disable

namespace Admin.Service.EntityModels
{
    public partial class BookingHistory
    {
        public BookingHistory()
        {
            PassengerDetails = new HashSet<PassengerDetail>();
        }

        public int Id { get; set; }
        public string EmailId { get; set; }
        public string Name { get; set; }
        public int NoOfSeats { get; set; }
        public string Pnr { get; set; }
        public string BookedBy { get; set; }
        public string BookedOn { get; set; }
        public int InventoryId { get; set; }
        public string DiscountUsed { get; set; }
        public int UserId { get; set; }
        public string SeatType { get; set; }
        public string BookingStatus { get; set; }

        public virtual FlightInventory Inventory { get; set; }
        public virtual LoginUser User { get; set; }
        public virtual ICollection<PassengerDetail> PassengerDetails { get; set; }
    }
}
