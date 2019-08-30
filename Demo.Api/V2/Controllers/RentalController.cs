﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Api.Services;
using Demo.Api.V1.Models;

namespace Demo.Api.Controllers
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;
        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        /// <summary>
        /// Get Rentals based on year, make or model
        /// </summary>
        /// <param name="year">Rental year.</param>
        /// <param name="make">Rental year.</param>
        /// <param name="model">Rental year.</param>
        /// <returns>The requested rental.</returns>
        /// <response code="200">Rentals successfully retrieved.</response>
        /// <response code="404">No rentals found for the criteria.</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<Rental>>> Get(string year, string make, string model)
        {
            var rentals = await _rentalService.GetRental(year, make, model);
            if (rentals != null && rentals.Any())
                return Ok(rentals);
            else
                return NotFound();
        }

    }
}