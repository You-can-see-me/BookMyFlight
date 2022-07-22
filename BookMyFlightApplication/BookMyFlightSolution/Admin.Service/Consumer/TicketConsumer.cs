using Admin.Service.Repository;
using MassTransit;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Service.Consumer
{
    public class TicketConsumer : IConsumer<Ticket>
    {
        IAirlineInventoryRepository airlineInventoryRepository;
        public TicketConsumer(IAirlineInventoryRepository _airlineInventoryRepository)
        {
            airlineInventoryRepository = _airlineInventoryRepository;
        }
        public async Task Consume(ConsumeContext<Ticket> context)
        {
            var data = context.Message;
            this.airlineInventoryRepository.UpdateSeatDetails(data);
        }
    }
}
