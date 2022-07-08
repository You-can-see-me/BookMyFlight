using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightUser.Service.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string EmailId { get; set; }
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
