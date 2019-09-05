using Demo.Api.V1.Controllers;
using Demo.Shared.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Ioc.Autofac;

namespace Demo.Api.Tests
{

    [UseAutofacTestFramework]
    public class RentalControllerV1Tests
    {
       
        public RentalControllerV1Tests() { }
        
        private readonly RentalController _rentalController;

        public RentalControllerV1Tests(RentalController rentalController)
        {
            _rentalController = rentalController;
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = await _rentalController.GetAll();

            var result = okResult.Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public  async Task Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var actionResult = await  _rentalController.GetAll();

            var result = actionResult.Result as OkObjectResult;

            // Assert
            var items = Assert.IsAssignableFrom<IEnumerable<RentalResult>>(result.Value);
            Assert.Equal(2, items.Count());
        }
    }
}
