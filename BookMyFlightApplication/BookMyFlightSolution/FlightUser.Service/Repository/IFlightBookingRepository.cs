using FlightUser.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightUser.Service.Repository
{
    public interface IFlightBookingRepository
    {
        public FlightBookingResponse BookFlight(FlightBookingRequest flightBookingRequest);
        public string CancelFlight(string pnr);
        public GetTicketDetailsModel GetTicketDetails(string pnr);
        public List<GetBookingHistoryModel> GetBookingHistory(string emailId);

    }
}
