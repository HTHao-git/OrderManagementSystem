using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderManagementSystem.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (Session["UserID"] == null &&
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerName != "Account")
            {
                filterContext.Result = new RedirectResult("~/Account/Login");
                return;
            }
        }

        protected bool IsAdmin()
        {
            return Session["IsAdmin"] != null && (bool)Session["IsAdmin"];
        }
    }
}