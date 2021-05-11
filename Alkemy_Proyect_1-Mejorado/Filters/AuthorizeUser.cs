using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Alkemy_Proyect_1.Models;

namespace Alkemy_Proyect_1.Filters
{
    using System;
    using System.Collections.Generic;

    
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeUser : AuthorizeAttribute
    {
        private Users2 oUser;
        private Alkemy_ProyectEntities6 db = new Alkemy_ProyectEntities6();
        private int idRole;

        public AuthorizeUser(int idRole)
        {
            this.idRole = idRole;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            String nombreOperacion = "";
            String nombreModulo = "";
            try
            {
                oUser = (Users2)HttpContext.Current.Session["User"];

                if (oUser != null)
                {
                    if (idRole != oUser.Id_rol)
                    {
                        filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?operacion=" + nombreOperacion + "&modulo=" + nombreModulo + "&msjeErrorExcepcion=");
                    }

                }
            }
            catch (Exception ex)
            {
                filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?operacion=" + nombreOperacion + "&modulo=" + nombreModulo + "&msjeErrorExcepcion=" + ex.Message);
            }
        }
    }
}