using FlightSearch.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSearch.Service.Repository
{
    public interface IFlightSearchRepository
    {
        public List<FlightSearchOutputModel> GetFlightSearchResult(FlightSearchInputModel flightSearchInput);
    }
}
