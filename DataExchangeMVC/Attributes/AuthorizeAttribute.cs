using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataExchangeMVC
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        private string _notifyUrl = "/Shared/NotAuthorized";

        public string NotifyUrl
        {
            get { return _notifyUrl; }
            set { _notifyUrl = value; }
        }
        
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated && NotifyUrl != null)
            {
                //filterContext.Result = new System.Web.Mvc.HttpStatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
                filterContext.Result = new RedirectResult(NotifyUrl);
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}