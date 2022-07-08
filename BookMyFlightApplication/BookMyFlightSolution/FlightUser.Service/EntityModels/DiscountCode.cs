using System;
using System.Collections.Generic;

#nullable disable

namespace FlightUser.Service.EntityModels
{
    public partial class DiscountCode
    {
        public int Id { get; set; }
        public string DiscountCode1 { get; set; }
        public decimal DiscountPercent { get; set; }
    }
}
