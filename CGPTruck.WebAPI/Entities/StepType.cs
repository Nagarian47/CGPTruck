//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CGPTruck.WebAPI.Entities
{
    using System;
    
    public enum StepType : int
    {
        Waiting = 0,
        PickupProgressing = 1,
        PickingUp = 2,
        DeliveryProgressing = 3,
        Delivering = 4,
        Returning = 5,
        Failure = 6,
        DisasterRecovery = 7,
        Aborted = 8
    }
}
