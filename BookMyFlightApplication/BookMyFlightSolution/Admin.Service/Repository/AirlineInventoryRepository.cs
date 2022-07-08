using Admin.Service.EntityModels;
using Admin.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Service.Repository
{
    public class AirlineInventoryRepository : IAirlineInventoryRepository
    {
        BookMyFlightDBContext bookMyFlightDBContext;
        public AirlineInventoryRepository(BookMyFlightDBContext _bookMyFlightDBContext)
        {
            bookMyFlightDBContext = _bookMyFlightDBContext;
        }
        public bool AddAirlineDetails(AirlinInventoryViewModel airlinInventoryDetails)
        {
            bool result = false;
            try
            {
                FlightInventory flightInventory = new FlightInventory()
                {
                    AirlineName = airlinInventoryDetails.AirlineName,
                    AirlineStatus = airlinInventoryDetails.AirlineStatus,
                    FlightNumber = airlinInventoryDetails.FlightNumber,
                    Departure = airlinInventoryDetails.Departure,
                    DepartureDateTime = airlinInventoryDetails.DepartureDateTime,
                    Destination = airlinInventoryDetails.Destination,
                    DestinationDateTime = airlinInventoryDetails.DestinationDateTime,
                    ScheduledDays = airlinInventoryDetails.ScheduledDays,
                    ModelType = airlinInventoryDetails.ModelType,
                    NoOfBusinessClassSeats = airlinInventoryDetails.NoOfBusinessClassSeats,
                    NoOfNonBusinessClassSeats = airlinInventoryDetails.NoOfNonBusinessClassSeats,
                    TicketCost = airlinInventoryDetails.TicketCost,
                    NumberOfRows = airlinInventoryDetails.NumberOfRows,
                    Meal = airlinInventoryDetails.Meal

                };
                bookMyFlightDBContext.FlightInventories.Add(flightInventory);
                bookMyFlightDBContext.SaveChanges();
                result = true;
                return result;

            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
