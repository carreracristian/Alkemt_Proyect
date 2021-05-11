using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alkemy_Proyect_1.Models.ViewModels
{
    public class ListSubjectsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Schedule { get; set; }
        public int IdTeacher { get; set; }
        public int NumberOfStudents { get; set; }
        public string Description { get; set; }
        public string NameOfTeacher { get; set; }
        public string LNameOfTeacher { get; set; }
    }
}