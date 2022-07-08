using FlightUser.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightUser.Service.Repository
{
    public interface IJWTMangerRepository
    {
        Tokens Authenticate(LoginViewModel users, bool IsRegister);
    }
}
