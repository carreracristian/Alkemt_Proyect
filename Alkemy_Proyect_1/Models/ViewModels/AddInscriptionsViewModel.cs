using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Alkemy_Proyect_1.Models.ViewModels
{
    public class AddInscriptionsViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "ID OF STUDENT")]
        public string Id_student { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "ID OF SUBJECT")]
        public string Id_subject { get; set; }

    }
}