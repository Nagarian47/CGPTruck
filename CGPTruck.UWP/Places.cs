//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CGPTruck.UWP
{
    using System;
    using System.Collections.Generic;
    
    public partial class Places
    {
        public int Id { get; set; }
        public int Position_Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public int Place_Type { get; set; }
    
        public virtual Positions Positions { get; set; }
    }
}