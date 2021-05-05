using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Alkemy_Proyect_1.Models;

namespace Alkemy_Proyect_1.Controllers
{
    public class AccessController : Controller
    {
        // GET: Access
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string User, string Pass)
        {
            Session["User"] = null;

            int userRole;
            int adminRole = 1;

            try
            {
                using (Alkemy_ProyectEntities6 db = new Alkemy_ProyectEntities6())
                {
                    var oUser = (from d in db.Users2
                                 where d.Dni.ToString() == User.Trim() && d.Docket.ToString() == Pass
                                 select d).FirstOrDefault();

                    if (oUser == null)
                    {
                        ViewBag.Error = "User or password invalid";
                        return View();
                    }
                    StudentsController.MyId(oUser.Id);

                    userRole = (int)oUser.Id_rol;

                    Session["User"] = oUser;
                }

                if (userRole == adminRole)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }
    }

}