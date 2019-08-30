using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using Demo.Api.Services;


namespace Demo.Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IRentalImportService _importService;

        public UploadController(IRentalImportService importRentalService)
        {
            _importService = importRentalService;
        }

        
        /// <summary>
        /// Uploads Rental file
        /// </summary>
        /// <param name="file">The rental file.</param>
        /// <response code="201">The upload was successfull.</response>
        /// <response code="400">The upload is invalid.</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Upload(IFormFile file)
        {

            try
            {
              
                if (IsValidExtension(file))
                {
                    if (file.Length > 0)
                    {
                        using (Stream stream = file.OpenReadStream())
                        {
                            var isUploaded = await _importService.Import(stream);
                            if (isUploaded)
                                return Ok();
                            else
                                return BadRequest("Failed to upload");
                        }
                    }
                }
                else
                {
                    return BadRequest("Unsupported media");
                }

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private bool IsValidExtension(IFormFile file)
        {
            return file.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase);
        }


    }
}
