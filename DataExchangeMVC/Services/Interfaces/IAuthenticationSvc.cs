using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataExchangeMVC.Services.Interfaces
{
    public interface IAuthenticationSvc : IService
    {
        bool Authenticate(string userName, string password);
    }
}
