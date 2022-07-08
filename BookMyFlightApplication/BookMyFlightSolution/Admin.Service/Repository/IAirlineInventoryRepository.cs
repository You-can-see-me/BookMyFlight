using Admin.Service.ViewModels;
//using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Service.Repository
{
    public interface IAirlineInventoryRepository
    {
        public bool AddAirlineDetails(AirlinInventoryViewModel airlinInventoryDetails);
        //public bool UpdateSeatDetails(Ticket ticketDetails);
    }
}
