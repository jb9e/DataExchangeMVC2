using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataExchangeMVC.Services.Interfaces;
using DataExchangeMVC.WCFAuthenticationSvcReference;

namespace DataExchangeMVC.Services.Implementations
{
    public class AuthenticationSvcWCFImpl : IAuthenticationSvc
    {
        public bool Authenticate(string userName, string password)
        {
            try
            {
                WCFAuthenticationSvcReference.WCFAuthenticationSvcClient proxy = new WCFAuthenticationSvcClient();
                return proxy.Authenticate(userName, password);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}