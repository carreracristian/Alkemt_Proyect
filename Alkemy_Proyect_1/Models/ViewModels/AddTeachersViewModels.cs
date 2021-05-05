using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Alkemy_Proyect_1.Models.ViewModels
{
    public class AddTeachersViewModels
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name="NAME")]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "LASTNAME")]
        public string Lastname { get; set; }
        [Required]
        [StringLength(10)]
        [Display(Name = "DNI")]
        public string Dni { get; set; }
        [Required]
        [Display(Name = "ACTIVE (Enter 1 if active or 0 if inactive)")]
        public string Active { get; set; }
    }
}