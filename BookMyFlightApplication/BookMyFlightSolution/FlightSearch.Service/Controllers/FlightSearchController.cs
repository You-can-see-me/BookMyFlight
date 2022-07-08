using FlightSearch.Service.Repository;
using FlightSearch.Service.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSearch.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightSearchController : ControllerBase
    {
        IFlightSearchRepository flightSearchRepository;
        public FlightSearchController(IFlightSearchRepository _flightSearchRepository)
        {
            flightSearchRepository = _flightSearchRepository;
        }
        [HttpPost("SearchFlight")]
        public List<FlightSearchOutputModel> GetFlightSearchResult(FlightSearchInputModel flightSearchInput)
        {
            
            var result = flightSearchRepository.GetFlightSearchResult(flightSearchInput);
            return result;

        }
        
    }
}
