using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Alkemy_Proyect_1.Filters;
using Alkemy_Proyect_1.Models;
using Alkemy_Proyect_1.Models.ViewModels;

namespace Alkemy_Proyect_1.Controllers
{
    public class TeachersController : Controller
    {
        // GET: Teachers

        [AuthorizeUser(idRole: 1)]
        public ActionResult Index()
        {
            List<ListTeachersViewModel> lst;
            List<ListTeachersViewModel> lst2 = new List<ListTeachersViewModel>();
            using (Alkemy_ProyectEntities6 db = new Alkemy_ProyectEntities6())
            {
                lst = (from d in db.Teachers
                       select new ListTeachersViewModel
                       {
                           Id = d.Id,
                           Name = d.Name,
                           Lastname = d.Lastname,
                           Dni = d.Dni,
                           Active = d.Active
                       }).ToList();
            }
            foreach (var item in lst)
            {
                if (item.Id>1006)
                {
                    lst2.Add(item);
                }
            }
            return View(lst2);
        }

        [AuthorizeUser(idRole: 1)]
        public ActionResult AddNew()
        {
            return View();
        }

        [AuthorizeUser(idRole: 1)]
        [HttpPost]
        public ActionResult AddNew(AddTeachersViewModels model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Alkemy_ProyectEntities6 db = new Alkemy_ProyectEntities6())
                    {
                        var newTeacher = new Teachers();
                        newTeacher.Name = model.Name;
                        newTeacher.Lastname = model.Lastname;
                        newTeacher.Dni = int.Parse(model.Dni);
                        newTeacher.Active = int.Parse(model.Active);

                        db.Teachers.Add(newTeacher);
                        db.SaveChanges();
                    }

                    return Redirect("/Teachers/");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [AuthorizeUser(idRole: 1)]
        public ActionResult Edit(int id)
        {
            AddTeachersViewModels model = new AddTeachersViewModels();
            using (Alkemy_ProyectEntities6 db = new Alkemy_ProyectEntities6())
            {
                var editFile = db.Teachers.Find(id);
                model.Name = editFile.Name;
                model.Lastname = editFile.Lastname;
                model.Dni = editFile.Dni.ToString();
                model.Active = editFile.Active.ToString();
                model.Id = editFile.Id;
            }
            return View(model);
        }

        [AuthorizeUser(idRole: 1)]
        [HttpPost]
        public ActionResult Edit(AddTeachersViewModels model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Alkemy_ProyectEntities6 db = new Alkemy_ProyectEntities6())
                    {
                        var editFile = db.Teachers.Find(model.Id);
                        editFile.Name = model.Name;
                        editFile.Lastname = model.Lastname;
                        editFile.Dni = int.Parse(model.Dni);
                        editFile.Active = int.Parse(model.Active);

                        db.Entry(editFile).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }

                    return Redirect("/Teachers/");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [AuthorizeUser(idRole: 1)]
        public ActionResult Delete(int id)
        {
            try
            {
                using (Alkemy_ProyectEntities6 db = new Alkemy_ProyectEntities6())
                {
                    var deleteFile = db.Teachers.Find(id);
                    db.Teachers.Remove(deleteFile);
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                return Redirect("/Alert/Create");
            }
            
            return Redirect("/Teachers/");
        }
    }
}