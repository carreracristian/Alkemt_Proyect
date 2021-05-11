using Alkemy_Proyect_1.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Alkemy_Proyect_1.Models.ViewModels
{
    public class AddSubjectsViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "NAME")]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "SHEDULE")]
        public string Schedule { get; set; }
        [Required]
        [StringLength(10)]
        [Display(Name = "CAPACITY OF STUDENTS")]
        public string NumberOfStudents { get; set; }
        [Required]
        [Display(Name = "DESCRIPTION")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "ID OF TEACHER")]
        public string IdOfTeacher { get; set; }
        public List<TeacherDTO> Teachers { get; set; }
    }
}