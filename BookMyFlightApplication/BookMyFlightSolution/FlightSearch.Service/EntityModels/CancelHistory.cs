using System;
using System.Collections.Generic;

#nullable disable

namespace FlightSearch.Service.EntityModels
{
    public partial class CancelHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CancelledOn { get; set; }
        public string Pnr { get; set; }

        public virtual LoginUser User { get; set; }
    }
}
