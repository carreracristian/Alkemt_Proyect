using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Alkemy_Proyect_1.Models;
using Alkemy_Proyect_1.Models.ViewModels;
using Alkemy_Proyect_1.Controllers;
using System.Data.Entity.Validation;
using System.Diagnostics;
using Alkemy_Proyect_1.DTOs;
using Alkemy_Proyect_1.Filters;

namespace Alkemy_Proyect_1.Controllers
{
    public class SubjectsController : Controller
    {
        // GET: Subjects
            public ActionResult Index()
            {
                List<ListSubjectsViewModel> lstSubjects;
                List<ListSubjectsViewModel> lstSubjects2 = new List<ListSubjectsViewModel>();
                List<ListTeachersViewModel> lstTeachers;
                using (Alkemy_ProyectEntities6 db = new Alkemy_ProyectEntities6())
                {
                    lstSubjects = (from d in db.Subject
                           select new ListSubjectsViewModel
                           {
                               Id = d.Id,
                               Name = d.Name,
                               Schedule = d.Schedule,
                               IdTeacher = (int)d.Id_teacher,
                               Description = d.Description,
                               NumberOfStudents=(int)d.Number_of_stufrnts
                           }).ToList();
                    lstTeachers = (from d in db.Teachers
                               select new ListTeachersViewModel
                               {
                                   Id = d.Id,
                                   Name = d.Name,
                                   Lastname= d.Lastname,
                                   Dni = d.Dni,
                                   Active = d.Active
                               }).ToList();

                }
                foreach (var item in lstSubjects)
                {
                    foreach (var item2 in lstTeachers)
                    {
                        if (item.IdTeacher == item2.Id)
                            item.NameOfTeacher = item2.Name;
                    }
                    if (item.Id > 3)
                    {
                        lstSubjects2.Add(item);
                    }

            }
                return View(lstSubjects2);
            }

        [AuthorizeUser(idRole: 1)]
        public ActionResult AddNewSubject()
            {
                    
                return View();
            }
            [HttpPost]

        [AuthorizeUser(idRole: 1)]
        public ActionResult AddNewSubject(AddSubjectsViewModel model)
            {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Alkemy_ProyectEntities6 db = new Alkemy_ProyectEntities6())
                    {
                        var newSubject = new Subject();
                        newSubject.Name = model.Name;
                        newSubject.Schedule = model.Schedule;
                        newSubject.Number_of_stufrnts = int.Parse(model.NumberOfStudents);
                        newSubject.Description = model.Description;
                        newSubject.Id_teacher = int.Parse( model.IdOfTeacher);

                        db.Subject.Add(newSubject);
                        db.SaveChanges();
                    }

                    return Redirect("/Subjects/");
                }
                return View(model);
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                            validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }
                return Redirect("/Subjects/");
            }
        }

        /*[AuthorizeUser(idRole: 1)]
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
        private AddSubjectsViewModel TeachersList()
        {
            AddSubjectsViewModel subjectsViewModels = new AddSubjectsViewModel();
            using (Alkemy_ProyectEntities6 db = new Alkemy_ProyectEntities6())
            {
                subjectsViewModels.Teachers = (from d in db.Teachers
                               select new TeacherDTO
                               {
                                   Id = d.Id,
                                   Name = d.Name,
                                   Lastname = d.Lastname,
                                   Dni = d.Dni,
                                   Active = d.Active
                               }).ToList();
            }
            return subjectsViewModels;
        }*/

        [AuthorizeUser(idRole: 1)]
        public ActionResult EditSubject(int id)
            {
                AddSubjectsViewModel model = new AddSubjectsViewModel();
                using (Alkemy_ProyectEntities6 db = new Alkemy_ProyectEntities6())
                {
                    var editFile = db.Subject.Find(id);
                    model.Name = editFile.Name;
                    model.Schedule = editFile.Schedule;
                    model.IdOfTeacher = editFile.Id_teacher.ToString();
                    model.NumberOfStudents = editFile.Number_of_stufrnts.ToString();
                    model.Description = editFile.Description;
                    model.Id = editFile.Id;
                }
                return View(model);
        }
        [AuthorizeUser(idRole: 1)]
        [HttpPost]
            public ActionResult EditSubject(AddSubjectsViewModel model)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        using (Alkemy_ProyectEntities6 db = new Alkemy_ProyectEntities6())
                        {
                            var editFile = db.Subject.Find(model.Id);
                            editFile.Name = model.Name;
                            editFile.Schedule = model.Schedule;
                            editFile.Id_teacher = int.Parse(model.IdOfTeacher);
                            editFile.Number_of_stufrnts = int.Parse(model.NumberOfStudents);
                            editFile.Description = model.Description;

                            db.Entry(editFile).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }

                        return Redirect("/Subjects/");
                    }
                    return View(model);
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
        }
        [AuthorizeUser(idRole: 1)]
        public ActionResult DeleteSubject(int id)
            {
                using (Alkemy_ProyectEntities6 db = new Alkemy_ProyectEntities6())
                {
                    var deleteFile = db.Subject.Find(id);
                    db.Subject.Remove(deleteFile);
                    db.SaveChanges();
                }
                return Redirect("/Subjects/");
            }
        
    }
}