//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Alkemy_Proyect_1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Inscriptions
    {
        public int Id { get; set; }
        public int Id_student { get; set; }
        public int Id_subject { get; set; }
    
        public virtual Users2 Users2 { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Subject Subject1 { get; set; }
    }
}