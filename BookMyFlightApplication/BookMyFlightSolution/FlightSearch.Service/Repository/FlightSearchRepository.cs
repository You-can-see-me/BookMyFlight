using FlightSearch.Service.EntityModels;
using FlightSearch.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSearch.Service.Repository
{
    public class FlightSearchRepository : IFlightSearchRepository
    {
        BookMyFlightDBContext bookMyFlightDBContext;
        public FlightSearchRepository(BookMyFlightDBContext _bookMyFlightDBContext)
        {
            bookMyFlightDBContext =_bookMyFlightDBContext;
        }

        public List<FlightSearchOutputModel>  GetFlightSearchResult(FlightSearchInputModel flightSearchInput)
        {
            List<FlightSearchOutputModel> flightSearchOutput = null;
            flightSearchOutput = (from fi in bookMyFlightDBContext.FlightInventories
                                  where fi.Departure == flightSearchInput.Departure
                                  && fi.Destination == flightSearchInput.Destination
                                  && fi.AirlineStatus == "Active"
                                  select new FlightSearchOutputModel()
                                                                 {
                                                                     FlightDateTime = fi.DepartureDateTime,
                                                                     AirlineName = fi.AirlineName,
                                                                     TicketCost = fi.TicketCost,
                                                                     FlightInventoryId = fi.Id,
                                                                     FlightNumber = fi.FlightNumber,
                                                                     Departure = fi.Departure,
                                                                     Destination = fi.Destination
                                                                 }).ToList();
            flightSearchOutput = flightSearchOutput.Where(x => x.FlightDateTime.ToString("MM/dd/yyyy") == flightSearchInput.DepartureDateTime.ToString("MM/dd/yyyy")).ToList();
            if(flightSearchInput.IsRoundTrip.HasValue && flightSearchInput.IsRoundTrip.Value && flightSearchInput.ReturnDateTime.HasValue)
            {
                List<FlightSearchOutputModel> returnFlightSearchOutput = null;
                returnFlightSearchOutput = (from fi in bookMyFlightDBContext.FlightInventories
                                      where fi.Departure == flightSearchInput.Destination
                                      && fi.Destination == flightSearchInput.Departure
                                      && fi.AirlineStatus == "Active"
                                      select new FlightSearchOutputModel()
                                      {
                                          FlightDateTime = fi.DepartureDateTime,
                                          AirlineName = fi.AirlineName,
                                          TicketCost = fi.TicketCost,
                                          FlightInventoryId = fi.Id,
                                          FlightNumber = fi.FlightNumber,
                                          Departure = fi.Departure,
                                          Destination = fi.Destination
                                      }).ToList();

                returnFlightSearchOutput = returnFlightSearchOutput.Where(x => x.FlightDateTime.ToString("MM/dd/yyyy") == flightSearchInput.ReturnDateTime.Value.ToString("MM/dd/yyyy")).ToList();
                flightSearchOutput = flightSearchOutput.Concat(returnFlightSearchOutput).ToList();
            }
            return flightSearchOutput;
        }

       
    }
}
