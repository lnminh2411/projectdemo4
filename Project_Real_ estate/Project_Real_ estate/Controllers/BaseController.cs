using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Project_Real__estate.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected  override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = Session["UserId"];
            if(session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Admin", action = "Login" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}