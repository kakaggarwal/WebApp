using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Helpers
{
    public class KKAuthorizeAttribute : AuthorizeAttribute
    {
        public KKAuthorizeAttribute()
        {

        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;

            try
            {
                // Authorization Logic
            }
            catch (Exception ex)
            {
                authorize = false;
                Logger.LogException(ex);
            }

            return authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Account/Unauthorized");
        }
    }
}