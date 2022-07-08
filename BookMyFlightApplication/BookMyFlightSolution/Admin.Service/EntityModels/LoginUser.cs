using System;
using System.Collections.Generic;

#nullable disable

namespace Admin.Service.EntityModels
{
    public partial class LoginUser
    {
        public LoginUser()
        {
            BookingHistories = new HashSet<BookingHistory>();
        }

        public int Id { get; set; }
        public string EmailId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }

        public virtual ICollection<BookingHistory> BookingHistories { get; set; }
    }
}
