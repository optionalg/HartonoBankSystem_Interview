using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HartonoBankSystem.Data;
using HartonoBankSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace HartonoBankSystem.Controllers
{
    public class BaseController : Controller
    {
        public const string SessionKeyAccountName = "_AccountName";
        public const string SessionKeyAccountId = "_AccountId";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string actionName = (string)context.RouteData.Values["action"];
            string controllerName = (string)context.RouteData.Values["controller"];
            var accountName = HttpContext.Session.GetString(SessionKeyAccountName);
            if (controllerName == "Account" && actionName == "Logout")
            {
                HttpContext.Session.Clear();
                HttpContext.Session.Remove(SessionKeyAccountName);
                HttpContext.Session.Remove(SessionKeyAccountId);
                HttpContext.Session.Clear();
            }
            else if (controllerName != "Account" && actionName != "Login")
            {
                if (string.IsNullOrEmpty(accountName))
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Account" }, { "action", "Login" } });
                }
            }
            else if (controllerName == "Account" && actionName == "Login")
            {
                if (!string.IsNullOrEmpty(accountName))
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Home" }, { "action", "Index" } });
                }
            }
            

        }
    }
}
