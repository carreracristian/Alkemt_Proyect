using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Alkemy_Proyect_1.Models;

namespace Alkemy_Proyect_1.Controllers
{
    public class AlertController : Controller
    {
        //  
        // GET: /Home/  

        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View(new OrderFood());
        }

        [HttpPost]
        public ActionResult Create(OrderFood OrderFood)
        {
            if (ModelState.IsValid)
            {

            }
            else
            {

            }
            return View(OrderFood);
        }
    }
}