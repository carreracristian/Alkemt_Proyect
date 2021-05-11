using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alkemy_Proyect_1.Models.ViewModels
{
    public class ListStudentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Dni { get; set; }
        public string Docket { get; set; }
        public string Id_rol { get; set; }
    }
}