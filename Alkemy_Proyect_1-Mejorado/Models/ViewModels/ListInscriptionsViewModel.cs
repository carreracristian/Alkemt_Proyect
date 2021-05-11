using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alkemy_Proyect_1.Models.ViewModels
{
    public class ListInscriptionsViewModel
    {
        public int Id { get; set; }
        public string IdStudent { get; set; }
        public string IdSubject { get; set; }
        public string StudentName { get; set; }
        public string SubjectName { get; set; }
        public string TeacherName { get; set; }
        public string ScheduleSubject { get; set; }
    }
}