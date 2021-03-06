﻿using CGPTruck.WebAPI.Entities;
using CGPTruck.WebAPI.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CGPTruck.WebAPI.BLL
{
    /// <summary>
    /// Business Logic Layer des places
    /// </summary>
    public class BLLPlaces
    {
        /// <summary>
        /// Permet d'obtenir une instance rapidement
        /// </summary>
        public static BLLPlaces Current { get; set; } = new BLLPlaces();

        /// <summary>
        /// Permet d'obtenir toutes les places active de la BDD
        /// </summary>
        /// <returns></returns>
        public List<Place> GetPlaces()
        {
            using (CGPTruckEntities context = new CGPTruckEntities())
            {
                return (from place in context.Places.Include(p => p.Position)
                        where place.Active
                        select place).ToList();
            }
        }

        /// <summary>
        /// Permet d'obtenir toutes les places du type spécifié
        /// </summary>
        /// <returns></returns>
        public List<Place> GetPlacesOfType(PlaceType placeType)
        {
            using (CGPTruckEntities context = new CGPTruckEntities())
            {
                return (from place in context.Places.Include(p => p.Position)
                        where place.Active && place.Place_Type == placeType
                        select place).ToList();
            }
        }


        /// <summary>
        /// Permet d'obtenir les détails d'une place
        /// </summary>
        /// <param name="placeId">ID de la place</param>
        /// <returns></returns>
        public Place GetPlace(int placeId)
        {
            using (CGPTruckEntities context = new CGPTruckEntities())
            {
                return (from place in context.Places.Include(p => p.Position)
                        where place.Id == placeId
                        select place).SingleOrDefault();
            }
        }
    }
}
