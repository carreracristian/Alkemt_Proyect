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
using Alkemy_Proyect_1.Filters;

namespace Alkemy_Proyect_1.Controllers
{
    public class InscriptionsController : Controller
    {
        // GET: Inscriptions

        [AuthorizeUser(idRole: 1)]
        public ActionResult Index()
        {

            List<ListSubjectsViewModel> lstSubjects;
            List<ListTeachersViewModel> lstTeachers;
            List<ListStudentViewModel> lstStudents;
            List<ListInscriptionsViewModel> lstInscriptions;
            
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
                                   NumberOfStudents = (int)d.Number_of_stufrnts
                               }).ToList();
                lstTeachers = (from d in db.Teachers
                               select new ListTeachersViewModel
                               {
                                   Id = d.Id,
                                   Name = d.Name,
                                   Lastname = d.Lastname,
                                   Dni = d.Dni,
                                   Active = d.Active
                               }).ToList();
                lstStudents = (from d in db.Users2
                               select new ListStudentViewModel
                               {
                                   Id = d.Id,
                                   Name = d.Name,
                                   Lastname = d.LastName,
                                   Dni = d.Dni.ToString(),
                                   Docket = d.Docket.ToString()
                               }).ToList();
                lstInscriptions = (from d in db.Inscriptions
                               select new ListInscriptionsViewModel
                               {
                                   Id=d.Id,
                                   IdStudent=d.Id_student.ToString(),
                                   IdSubject=d.Id_subject.ToString()

                               }).ToList();

            }
            foreach (var item in lstInscriptions)
            {
                foreach (var item2 in lstStudents)
                {
                    foreach (var item3 in lstSubjects)
                    {
                        foreach (var item4 in lstTeachers)
                        {
                            if (int.Parse(item.IdStudent)==item2.Id && int.Parse(item.IdSubject)==item3.Id)
                            {
                                item.StudentName = item2.Name;
                                item.SubjectName = item3.Name;
                                if (item3.IdTeacher==item4.Id)
                                {
                                    item.TeacherName = item4.Name;
                                    item.ScheduleSubject = item3.Schedule;
                                }

                            }
                        }
                    }
                    
                }

            }
            return View(lstInscriptions);
        }
        [AuthorizeUser(idRole: 1)]
        public ActionResult AddNewInscription()
        {
            return View();
        }

        [AuthorizeUser(idRole: 1)]
        [HttpPost]
        public ActionResult AddNewInscription(AddInscriptionsViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    using (Alkemy_ProyectEntities6 db = new Alkemy_ProyectEntities6())
                    {
                        var lstSubject = db.Subject;
                        int capacity = 0 ;
                        var newInscription = new Inscriptions();
                        newInscription.Id_student = int.Parse(model.Id_student);
                        newInscription.Id_subject = int.Parse(model.Id_subject);

                        foreach (var item in lstSubject)
                        {
                            if (item.Id==newInscription.Id_subject)
                            {
                                capacity = (int)item.Number_of_stufrnts--;
                            }
                        }
                        if (capacity>0)
                        {
                            db.Inscriptions.Add(newInscription);
                            db.SaveChanges();
                        }
                        else
                        {
                            ViewBag.Error = "No space for registration";
                        }
                    }

                    return Redirect("/Inscriptions/");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [AuthorizeUser(idRole: 1)]
        public ActionResult EditInscription(int id)
        {
            AddInscriptionsViewModel model = new AddInscriptionsViewModel();
            using (Alkemy_ProyectEntities6 db = new Alkemy_ProyectEntities6())
            {
                var editFile = db.Inscriptions.Find(id);
                model.Id_student = editFile.Id_student.ToString();
                model.Id_subject = editFile.Id_subject.ToString();
            }
            return View(model);
        }

        [AuthorizeUser(idRole: 1)]
        [HttpPost]
        public ActionResult EditInscription(AddInscriptionsViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Alkemy_ProyectEntities6 db = new Alkemy_ProyectEntities6())
                    {
                        var editFile = db.Inscriptions.Find(model.Id);
                        editFile.Id_student = int.Parse(model.Id_student);
                        editFile.Id_subject = int.Parse(model.Id_subject);

                        db.Entry(editFile).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }

                    return Redirect("/Inscriptions/");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [AuthorizeUser(idRole: 1)]
        public ActionResult DeleteInscription(int id)
        {
            using (Alkemy_ProyectEntities6 db = new Alkemy_ProyectEntities6())
            {
                var deleteFile = db.Inscriptions.Find(id);
                db.Inscriptions.Remove(deleteFile);
                db.SaveChanges();
            }
            return Redirect("/Inscriptions/Index");
        }
    }
}