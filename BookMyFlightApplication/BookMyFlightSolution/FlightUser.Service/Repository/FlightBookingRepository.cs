using FlightUser.Service.EntityModels;
using FlightUser.Service.ViewModels;
using MassTransit;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightUser.Service.Repository
{
    public class FlightBookingRepository : IFlightBookingRepository
    {
        BookMyFlightDBContext bookMyFlightDBContext;
        private readonly IBus bus;
        public FlightBookingRepository(BookMyFlightDBContext _bookMyFlightDBContext, IBus _bus)
        {
            bookMyFlightDBContext = _bookMyFlightDBContext;
            bus = _bus;
        }

        public async Task<FlightBookingResponse> BookFlight(FlightBookingRequest flightBookingRequest)
        {
            decimal ticketCost = bookMyFlightDBContext.FlightInventories.Where(x => x.Id == flightBookingRequest.InventoryId).FirstOrDefault().TicketCost;
            decimal discountPercent = 0;
            if (bookMyFlightDBContext.DiscountCodes.Any(x => x.DiscountCode1 == flightBookingRequest.DiscountUsed))
            {
                discountPercent = bookMyFlightDBContext.DiscountCodes.Where(x => x.DiscountCode1 == flightBookingRequest.DiscountUsed).FirstOrDefault().DiscountPercent;

            }
            ticketCost = (ticketCost * (100 - discountPercent)) / 100;
            BookingHistory booking = new BookingHistory()
            {
                EmailId = flightBookingRequest.EmailId,
                Name = flightBookingRequest.Name,
                NoOfSeats = flightBookingRequest.NoOfSeats,
                Pnr = GeneratePNR(),
                BookedBy = flightBookingRequest.Name,
                BookedOn = DateTime.Now.ToString(),
                InventoryId = flightBookingRequest.InventoryId,
                DiscountUsed = flightBookingRequest.DiscountUsed,
                UserId = flightBookingRequest.UserId,
                SeatType = flightBookingRequest.SeatType,
                BookingStatus = "Active"
                
            };
            bookMyFlightDBContext.BookingHistories.Add(booking);
            bookMyFlightDBContext.SaveChanges();
            var bookingDetails = bookMyFlightDBContext.BookingHistories.OrderByDescending(x => x.Id).ToList().FirstOrDefault();
            FlightBookingResponse flightBookingResponse = new FlightBookingResponse()
            {
                BookingId = bookingDetails.Id,
                Pnr = bookingDetails.Pnr,
                TicketCost = ticketCost*booking.NoOfSeats,
                ResponseMessage = "Ticket booked successfully"
            };
            foreach (PassengerDetailList passenger in flightBookingRequest.passengerDetailList)
            {
                PassengerDetail passengerDetail = new PassengerDetail()
                {
                    BookingHistoryId = flightBookingResponse.BookingId,
                    FirstName = passenger.FirstName,
                    LastName = passenger.LastName,
                    Gender = passenger.Gender,
                    Age = passenger.Age,
                    Meal = passenger.Meal,
                    SeatNumber = passenger.SeatNumber

                };
                bookMyFlightDBContext.PassengerDetails.Add(passengerDetail);
                bookMyFlightDBContext.SaveChanges();
            }
            Ticket ticketRabbitMQ = new Ticket();
            ticketRabbitMQ.FlightInventoryId = booking.InventoryId;
            ticketRabbitMQ.NoOfSeats = booking.NoOfSeats;
            ticketRabbitMQ.SeatType = booking.SeatType;

            Uri uri = new Uri("rabbitmq://localhost/ticketQueue");
            var endpoint = await bus.GetSendEndpoint(uri);
            await endpoint.Send(ticketRabbitMQ);



            return flightBookingResponse;

        }

        private string GeneratePNR()
        {
            const string src = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            int length = 16;
            var sb = new StringBuilder();
            Random RNG = new Random();
            for (var i = 0; i < length; i++)
            {
                var c = src[RNG.Next(0, src.Length)];
                sb.Append(c);
            }
            return sb.ToString();
        }

        public string CancelFlight(string pnr)
        {
            string result = "";
            if (bookMyFlightDBContext.BookingHistories.Any(x => x.Pnr == pnr))
            {
                var ticketInfo = (from bh in bookMyFlightDBContext.BookingHistories
                                  join inv in bookMyFlightDBContext.FlightInventories
                                  on bh.InventoryId equals inv.Id
                                  where bh.Pnr == pnr

                                  select new { bh.Id, bh.InventoryId, bh.NoOfSeats });
                DateTime cancelDate = DateTime.Now.Date;
                var scheduledFlightDateList = from inv in bookMyFlightDBContext.FlightInventories
                                              where inv.Id == ticketInfo.FirstOrDefault().InventoryId
                                              select new { inv.DepartureDateTime };

                if (scheduledFlightDateList != null)
                {
                    DateTime scheduledFlightDate = scheduledFlightDateList.FirstOrDefault().DepartureDateTime.Date;
                    if (scheduledFlightDate > cancelDate)
                    {
                        var data = bookMyFlightDBContext.BookingHistories.Where(x => x.Pnr == pnr).FirstOrDefault();
                        data.BookingStatus = "Canceled";
                        bookMyFlightDBContext.BookingHistories.Update(data);
                        bookMyFlightDBContext.SaveChanges();
                        result = "Ticket cancelled successfully for PNR " + pnr;
                    }
                    else
                    {
                        result = "Cannot cancel ticket prior to 24 hrs.";
                    }
                }
                
            }
            else
            {
                result = "Failed. Incorrect Pnr entered";
            }
            
            return result;

        }

        public GetTicketDetailsModel GetTicketDetails(string pnr)
        {
            GetTicketDetailsModel ticketInfo = null;
            if (bookMyFlightDBContext.BookingHistories.Any(x => x.Pnr == pnr))
            {
                var bookingId = bookMyFlightDBContext.BookingHistories.Where(x => x.Pnr == pnr).FirstOrDefault().Id;
                
                ticketInfo = (from bh in bookMyFlightDBContext.BookingHistories
                                  join inv in bookMyFlightDBContext.FlightInventories
                                  on bh.InventoryId equals inv.Id
                                  where bh.Pnr == pnr

                                  select new GetTicketDetailsModel() 
                                  {
                                      Id = bh.Id,
                                      EmailId = bh.EmailId,
                                      Name = bh.Name,
                                      NoOfSeats = bh.NoOfSeats,
                                      Pnr = bh.Pnr,
                                      BookedBy = bh.Name,
                                      BookedOn = bh.BookedOn,
                                      InventoryId = bh.InventoryId,
                                      DiscountUsed = bh.DiscountUsed,
                                      UserId = bh.UserId,
                                      SeatType = bh.SeatType,
                                      BookingStatus = bh.BookingStatus,
                                      TicketCost = inv.TicketCost * bh.NoOfSeats,
                                      Source = inv.Departure,
                                      Destination = inv.Destination,
                                      AirlineName = inv.AirlineName

                                  }).FirstOrDefault();

                var passengerDetails = from pd in bookMyFlightDBContext.PassengerDetails
                                       where pd.BookingHistoryId == bookingId
                                       select new PassengerDetailList()
                                       {
                                           BookingHistoryId = pd.BookingHistoryId,
                                           FirstName = pd.FirstName,
                                           LastName = pd.LastName,
                                           Gender = pd.Gender,
                                           Age = pd.Age,
                                           Meal = pd.Meal,
                                           SeatNumber = pd.SeatNumber
                                       };
                decimal discountPercent = 0;
                if(bookMyFlightDBContext.DiscountCodes.Any(x=>x.DiscountCode1 == ticketInfo.DiscountUsed)){
                    discountPercent = bookMyFlightDBContext.DiscountCodes.Where(x => x.DiscountCode1 == ticketInfo.DiscountUsed).FirstOrDefault().DiscountPercent;

                }
                decimal ticketCost = (ticketInfo.TicketCost * (100 - discountPercent) / 100);
                ticketInfo.TicketCost = ticketCost;
                ticketInfo.passengerDetailList = passengerDetails.ToList();

                return ticketInfo;
            }
            else
            {
                return ticketInfo;
            }
        }
        
        
        public List<GetBookingHistoryModel> GetBookingHistory(string emailId)
        {
            List<GetBookingHistoryModel> ticketHistory = null;
            if (bookMyFlightDBContext.BookingHistories.Any(x => x.EmailId == emailId))
            {
                //var bookingId = bookMyFlightDBContext.BookingHistories.Where(x => x.Pnr == pnr).FirstOrDefault().Id;
                ticketHistory = (from bh in bookMyFlightDBContext.BookingHistories
                              join inv in bookMyFlightDBContext.FlightInventories
                              on bh.InventoryId equals inv.Id
                              where bh.EmailId == emailId

                              select new GetBookingHistoryModel()
                              {
                                  Pnr = bh.Pnr,
                                  BookingId = bh.Id,
                                  AirlineName = inv.AirlineName,
                                  TicketCost = inv.TicketCost * bh.NoOfSeats,
                                  DateOfJourney = inv.DepartureDateTime,
                                  BookingStatus = bh.BookingStatus

                              }).OrderByDescending(x=>x.BookingId).ToList();

                

                return ticketHistory;
            }
            else
            {
                return ticketHistory;
            }
        }
    }
}
