﻿using CGPTruck.WebAPI.BLL;
using CGPTruck.WebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace CGPTruck.WebAPI.Controllers
{
    /// <summary>
    /// Controller concernant les lieux connus et enregistrés. Authentification requise
    /// </summary>
    [Authorize]
    public class PlacesController : BaseController
    {
        private BLLPlaces places = new BLLPlaces();

        // GET: api/Places
        /// <summary>
        /// Driver/Administrator/DecisionMaker : Obtient la liste de toutes les places
        /// </summary>
        /// <returns></returns>
        [Route("api/Places")]
        [HttpGet]
        [ResponseType(typeof(List<Place>))]
        public IHttpActionResult GetPlaces()
        {
            if (CurrentUser.AccountType == AccountType.Repairer)
            {
                return Unauthorized();
            }

            return Ok(places.GetPlaces());
        }

        // GET: api/Places
        /// <summary>
        /// Driver/Administrator/DecisionMaker : Obtient la liste de toutes les entrepôts
        /// </summary>
        /// <returns></returns>
        [Route("api/Places/warehouses")]
        [HttpGet]
        [ResponseType(typeof(List<Place>))]
        public IHttpActionResult GetWarehouses()
        {
            if (CurrentUser.AccountType == AccountType.Repairer)
            {
                return Unauthorized();
            }

            return Ok(places.GetPlacesOfType(PlaceType.Warehouse));
        }

        // GET: api/Places
        /// <summary>
        /// Driver/Administrator/DecisionMaker : Obtient la liste de tout les lieux de livraisons
        /// </summary>
        /// <returns></returns>
        [Route("api/Places/clients")]
        [HttpGet]
        [ResponseType(typeof(List<Place>))]
        public IHttpActionResult GetClients()
        {
            if (CurrentUser.AccountType == AccountType.Repairer)
            {
                return Unauthorized();
            }

            return Ok(places.GetPlacesOfType(PlaceType.Client));
        }

        // GET: api/Places/5
        /// <summary>
        /// Driver/Administrator/DecisionMaker : Obtient tout les détails d'un lieu
        /// </summary>
        /// <param name="placeId">Id de la place dont on veut les détails</param>
        /// <returns>Place avec toutes les informations la concernant</returns>
        [Route("api/Places/{placeId}")]
        [HttpGet]
        [ResponseType(typeof(Place))]
        public IHttpActionResult GetPlace(int placeId)
        {
            if (CurrentUser.AccountType == AccountType.Repairer)
            {
                return Unauthorized();
            }

            Place place = places.GetPlace(placeId);
            if (place == null)
            {
                return NotFound();
            }

            return Ok(place);
        }
    }
}
