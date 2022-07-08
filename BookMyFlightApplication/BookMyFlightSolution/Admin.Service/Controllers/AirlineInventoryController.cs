using Admin.Service.Repository;
using Admin.Service.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AirlineInventoryController : ControllerBase
    {
        IAirlineInventoryRepository airlineInventoryRepository;
        public AirlineInventoryController(IAirlineInventoryRepository _airlineInventoryRepository)
        {
            airlineInventoryRepository = _airlineInventoryRepository;
        }

        /// <summary>
        ///  Add flight to inventory
        /// </summary>
        /// <param name="AirlinInventoryViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddAirline")]
        public IActionResult AddAirlineDetails(AirlinInventoryViewModel airlinInventoryDetails)
        {
            var result = airlineInventoryRepository.AddAirlineDetails(airlinInventoryDetails);
            if (result)
            {
                return Ok("Airline successfully added to inventory.");
            }
            else
            {
                return BadRequest("Invalid Input.");
            }



        }

        
        //private Task<IActionResult> UpdateSeatDetails(Ticket ticketDetails)
        //{
        //    Ticket ticketRabbitMQ = new Ticket();
        //    ticketRabbitMQ.NoOfSeats = ticketDetails.NoOfSeats;
        //    ticketRabbitMQ.SeatType = ticketDetails.SeatType;

        //    Uri uri = new Uri("rabbitmq://localhost/ticketQueue");
        //    var endpoint = bus.GetSendEndpoint(uri);
        //    endpoint.;



        //}
    }
}
