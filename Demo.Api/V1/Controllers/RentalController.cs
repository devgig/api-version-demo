using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Api.Services;
using Demo.Shared.Model;

namespace Demo.Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;
        public RentalController(IRentalService rentalService )
        {
            _rentalService = rentalService;
        }
        /// <summary>
        /// Get Rentals based on criteria
        /// </summary>
        /// <param name="criteria">Criteria used to return the rental.</param>
        /// <returns>The requested rental.</returns>
        /// <response code="200">Rentals successfully retrieved.</response>
        /// <response code="404">No rentals found for the criteria.</response>
        [HttpGet, MapToApiVersion("1.0")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<Rental>>> Get(string criteria)
        {
            var rentals = await _rentalService.GetRentalsByCriteria(criteria);
            if (rentals != null && rentals.Any())
                return Ok(rentals);
            else
                return NotFound();
        }

        /// <summary>
        /// Uploads Rentals
        /// </summary>
        /// <param name="rentals">List of rentals.</param>
        /// <response code="201">The upload was successfull.</response>
        /// <response code="400">The upload is invalid.</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Put([FromBody] Rental[] rentals)
        {
            var result = await _rentalService.SaveRentals(rentals);

            if (result)
                return Ok();
            else
                return BadRequest();
        }



    }


}
