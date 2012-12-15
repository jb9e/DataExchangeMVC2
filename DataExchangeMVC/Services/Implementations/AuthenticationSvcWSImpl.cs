using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataExchangeMVC.Services.Interfaces;

namespace DataExchangeMVC.Services.Implementations
{
    public class AuthenticationSvcWSImpl : IAuthenticationSvc
    {
        public bool Authenticate(string userName, string password)
        {
            try
            {
                AuthenticationWSReference.Service ws = new AuthenticationWSReference.Service();
                return ws.Authenticate(userName, password);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}