using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Alkemy_Proyect_1.Models;
using Alkemy_Proyect_1.Models.ViewModels;
using Alkemy_Proyect_1.Filters;

namespace Alkemy_Proyect_1.Controllers
{
    public class StudentsController : Controller
    {
        // GET: Students
        public static int MyIden = 0;
        public static int MyIdRol = 0;

        [AuthorizeUser(idRole: 1)]
        public ActionResult Index()
        {
            List<ListStudentViewModel> lst;
            List<ListStudentViewModel> lst2 = new List<ListStudentViewModel>();
            using (Alkemy_ProyectEntities6 db =new Alkemy_ProyectEntities6())
            {
                lst = (from d in db.Users2
                       select new ListStudentViewModel
                       {
                           Id=d.Id,
                           Name=d.Name,
                           Lastname=d.LastName,
                           Docket=d.Docket.ToString(),
                           Dni=d.Dni.ToString(),
                           Id_rol=d.Id_rol.ToString()
                       }).ToList();

                foreach (var item in lst)
                {
                    if (item.Id_rol=="2" && item.Id>3)
                    {
                        lst2.Add(item);
                    }
                }


            }
            return View(lst2);
        }

        [AuthorizeUser(idRole: 1)]
        public ActionResult AddNewStudent()
        {
            return View();
        }

        [AuthorizeUser(idRole: 1)]
        [HttpPost]
        public ActionResult AddNewStudent(AddStudentsViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Alkemy_ProyectEntities6 db = new Alkemy_ProyectEntities6())
                    {
                        var newStudent = new Users2();
                        newStudent.Name = model.Name;
                        newStudent.LastName = model.Lastname;
                        newStudent.Dni = int.Parse(model.Dni);
                        newStudent.Docket = int.Parse(model.Docket);
                        newStudent.Id_rol = 2;

                        db.Users2.Add(newStudent);
                        db.SaveChanges();
                    }

                    return Redirect("/Students/Index");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [AuthorizeUser(idRole: 1)]
        public ActionResult EditStudent(int id)
        {
            AddStudentsViewModel model = new AddStudentsViewModel();
            using (Alkemy_ProyectEntities6 db = new Alkemy_ProyectEntities6())
            {
                var editFile = db.Users2.Find(id);
                model.Name = editFile.Name;
                model.Lastname = editFile.LastName;
                model.Dni = editFile.Dni.ToString();
                model.Docket = editFile.Docket.ToString();
                model.Id = editFile.Id;
            }
            return View(model);
        }

        [AuthorizeUser(idRole: 1)]
        [HttpPost]
        public ActionResult EditStudent(AddStudentsViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Alkemy_ProyectEntities6 db = new Alkemy_ProyectEntities6())
                    {
                        var editFile = db.Users2.Find(model.Id);
                        editFile.Name = model.Name;
                        editFile.LastName = model.Lastname;
                        editFile.Dni = int.Parse(model.Dni);
                        editFile.Docket = int.Parse(model.Docket);

                        db.Entry(editFile).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }

                    return Redirect("/Students/");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        [AuthorizeUser(idRole: 2)]
        [HttpGet]
        public ActionResult Register(int id)
        {
            var MyInscriptions = new List<Inscriptions>();
            using (Alkemy_ProyectEntities6 db = new Alkemy_ProyectEntities6())
            {
                var lstSubject = db.Subject;
                var lstInscriptions = db.Inscriptions;
                int capacity = 0;
                Inscriptions newInscription = new Inscriptions();
                newInscription.Id_subject = id;
                newInscription.Id_student = MyIden;

                foreach (var item in lstInscriptions)
                {
                    if (item.Id_student == MyIden)
                    {
                        MyInscriptions.Add(item);
                    }
                }
                var cantidad = (from i in db.Inscriptions where i.Id_student == MyIden && i.Id_subject == id select i).Count(); 
                if (cantidad == 0)
                {
                    foreach (var item2 in lstSubject)
                    {
                        if (item2.Id == newInscription.Id_subject)
                        {
                            capacity = (int)item2.Number_of_stufrnts--;
                        }
                    }
                    if (capacity > 0)
                    {
                        db.Inscriptions.Add(newInscription);
                        db.SaveChanges();
                    }
                    else
                    {
                        ViewData["cuposLlenos"] = "No space for registration";
                    }
                }
            }

            return Redirect("/Students/MyInscriptions/");
        }

        
        [AuthorizeUser(idRole: 1)]
        public ActionResult DeleteStudent(int id)
        {
            try
            {
                using (Alkemy_ProyectEntities6 db = new Alkemy_ProyectEntities6())
                {
                    var deleteFile = db.Users2.Find(id);
                    db.Users2.Remove(deleteFile);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                return Redirect("/Alert/Create");
            }
            
            return Redirect("/Students/Index");
        }
        public ActionResult MyInscriptions()
        {
            List<ListSubjectsViewModel> lstSubjects;
            List<ListTeachersViewModel> lstTeachers;
            List<ListStudentViewModel> lstStudents;
            List<ListStudentViewModel> myLstStudents = new List<ListStudentViewModel>();
            List<ListInscriptionsViewModel> lstInscriptions;
            List<ListInscriptionsViewModel> myLstInscriptions = new List<ListInscriptionsViewModel>();

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
                                       Id = d.Id,
                                       IdStudent = d.Id_student.ToString(),
                                       IdSubject = d.Id_subject.ToString()

                                   }).ToList();

            }
            foreach (var item in lstStudents)
            {
                if (item.Id == MyIden)
                {
                    myLstStudents.Add(item);
                }
            }
            foreach (var item in lstInscriptions)
            {
                foreach (var item2 in myLstStudents)
                {
                    if (int.Parse(item.IdStudent)==item2.Id)
                    {
                        myLstInscriptions.Add(item);
                    }

                }

            }
            foreach (var item in myLstInscriptions)
            {
                foreach (var item2 in myLstStudents)
                {
                    foreach (var item3 in lstSubjects)
                    {
                        foreach (var item4 in lstTeachers)
                        {
                            if (int.Parse(item.IdStudent) == item2.Id && int.Parse(item.IdSubject) == item3.Id)
                            {
                                item.StudentName = item2.Name;
                                item.SubjectName = item3.Name;
                                if (item3.IdTeacher == item4.Id)
                                {
                                    item.TeacherName = item4.Name;
                                    item.ScheduleSubject = item3.Schedule;
                                }
                            }
                        }
                    }

                }

            }
            return View(myLstInscriptions);
        }
        public static void MyId(int id)
        {
            MyIden = id;
        }
        public static void MyRol(int id)
        {
            MyIdRol = id;
        }
    }
}