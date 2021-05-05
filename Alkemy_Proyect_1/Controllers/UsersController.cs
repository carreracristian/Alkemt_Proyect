using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Alkemy_Proyect_1.Models;
using Alkemy_Proyect_1.Models.ViewModels;

namespace Alkemy_Proyect_1.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            List<ListUsersViewModel> lst;
            using (Alkemy_ProyectEntities6 db = new Alkemy_ProyectEntities6())
            {
                lst = (from d in db.Users2
                           select new ListUsersViewModel
                           {
                               Id = d.Id,
                               Name = d.Name,
                               Lastname = d.LastName,
                               Dni = d.Dni.ToString()
                           }).ToList();
            }
            return View(lst);
        }
    }
}