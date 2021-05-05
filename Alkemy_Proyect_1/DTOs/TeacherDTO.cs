using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alkemy_Proyect_1.DTOs
{
    public class TeacherDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public int Dni { get; set; }
        public int Active { get; set; }
    }
}