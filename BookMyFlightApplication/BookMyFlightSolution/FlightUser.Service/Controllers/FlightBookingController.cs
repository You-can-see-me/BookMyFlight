using FlightUser.Service.Repository;
using FlightUser.Service.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FlightUser.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FlightBookingController : ControllerBase
    {
        IFlightBookingRepository flightBookingRepository;
        public FlightBookingController(IFlightBookingRepository _flightBookingRepository)
        {
            flightBookingRepository = _flightBookingRepository;
        }
        [HttpPost]
        [Route("BookFlight/{flightId}")]
        public FlightBookingResponse BookFlight(int flightId,FlightBookingRequest flightBookingRequest)
        {

            var result = flightBookingRepository.BookFlight(flightBookingRequest);
            return result;

        }

        [HttpDelete]
        [Route("CancelFlight")]
        public IActionResult CancelFlight(string pnr)
        {
            string result = "";
            try
            {
                result = flightBookingRepository.CancelFlight(pnr);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }

            return new JsonResult(result);

        }

        [HttpGet]
        [Route("GetTicketDetails/{pnr}")]
        public IActionResult GetTicketDetails(string pnr)
        {
            GetTicketDetailsModel ticketDetails = flightBookingRepository.GetTicketDetails(pnr);
            if(ticketDetails != null)
            {
                return new JsonResult(ticketDetails);
            }
            else
            {
                return BadRequest("Invalid PNR");
            }
            
        }

        [HttpGet]
        [Route("GetBookingHistory/{emailId}")]
        public IActionResult GetBookingHistory(string emailId)
        {
            List<GetBookingHistoryModel> bookingHistory = flightBookingRepository.GetBookingHistory(emailId);
            if (bookingHistory != null)
            {
                return new JsonResult(bookingHistory);
            }
            else
            {
                return NotFound("No bookings available");
            }

        }
    }
}
