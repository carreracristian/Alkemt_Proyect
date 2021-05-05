using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Alkemy_Proyect_1.Controllers;
using Alkemy_Proyect_1.Models;

namespace Alkemy_Proyect_1.Filters
{
    public class VerifySession : ActionFilterAttribute
    {
        private Users2 oUser;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);

                oUser = (Users2)HttpContext.Current.Session["User"];

                if (oUser == null)
                {
                    if (filterContext.Controller is AccessController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/Access/Login");
                    }
                }
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Access/Login");
            }

        }
    }
}