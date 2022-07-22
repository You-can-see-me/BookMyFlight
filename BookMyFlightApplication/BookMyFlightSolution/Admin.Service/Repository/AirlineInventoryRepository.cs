using Admin.Service.EntityModels;
using Admin.Service.ViewModels;
using Shared.Models;
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

        public void UpdateSeatDetails(Ticket ticket)
        {
            if (bookMyFlightDBContext.FlightInventories.Any(x => x.Id == ticket.FlightInventoryId))
            {
                FlightInventory flightInventory = bookMyFlightDBContext.FlightInventories.Where(x => x.Id == ticket.FlightInventoryId).FirstOrDefault();
                if(ticket.SeatType == "Business")
                {
                    flightInventory.NoOfBusinessClassSeats = flightInventory.NoOfBusinessClassSeats - ticket.NoOfSeats;
                }
                else if(ticket.SeatType == "Non-Business")
                {
                    flightInventory.NoOfNonBusinessClassSeats = flightInventory.NoOfNonBusinessClassSeats - ticket.NoOfSeats;
                }
                bookMyFlightDBContext.Update(flightInventory);
                bookMyFlightDBContext.SaveChanges();
            }

        }
    }
}
