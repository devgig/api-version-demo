﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Api.Services;
using Demo.Shared.Model;
using Microsoft.AspNetCore.Authorization;

namespace Demo.Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;
        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        /// <summary>
        /// Get all rentals 
        /// </summary>
        /// <returns>The requested rental.</returns>
        /// <response code="200">Rentals successfully retrieved.</response>
        /// <response code="404">No rentals found for the criteria.</response>
        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<RentalResult>>> GetAll()
        {
            var rentals = await _rentalService.GetAllRentals();
            if (rentals != null && rentals.Any())
                return Ok(rentals.ToArray());
            else
                return NotFound();
        }

        [HttpGet]
        [Route("GetByCriteria/{criteria}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<RentalResult>>> GetByCritieria(string criteria)
        {
            var rentals = await _rentalService.GetRentalsByCriteria(criteria, 0);
            return Ok(rentals);
        }

        /// <summary>
        /// Get Rentals based on criteria
        /// </summary>
        /// <param name="criteria">Criteria used to return the rental.</param>
        /// <param name="numberOfDays">Number of days to calculate Total Rental Cost</param>
        /// <returns>The requested rental.</returns>
        /// <response code="200">Rentals successfully retrieved.</response>
        /// <response code="404">No rentals found for the criteria.</response>
        [HttpGet, MapToApiVersion("1.0")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<RentalResult>>> Get(string criteria, int numberOfDays)
        {
            var rentals = await _rentalService.GetRentalsByCriteria(criteria, numberOfDays);
            if (rentals != null && rentals.Any())
                return Ok(rentals);
            else
                return NotFound();
        }

    


    }


}
