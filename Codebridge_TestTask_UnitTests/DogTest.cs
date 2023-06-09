using Codebridge_TestTask;
using Codebridge_TestTask.Controllers;
using Codebridge_TestTask.Services;
using Codebridge_TestTask_UnitTests.MockData;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Codebridge_TestTask_UnitTests
{
    public class DogControllerTest : IDisposable
    {
        private readonly ApplicationContext _context;

        public DogControllerTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            _context = new ApplicationContext(options);
        }

        [Fact]
        public void GetPing_Should_Return_200_Status_Code()
        {
            var service = new Mock<IDogService>();
            service.Setup(x => x.GetPing()).Returns("Dogs house service. Version 1.0.1");
            var controller = new DogController(service.Object);

            var result = controller.GetPing();

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetDogs_Should_Return_200_Status_Code()
        {
            var service = new Mock<IDogService>();
            service.Setup(x => x.GetDogs()).ReturnsAsync(ControllerMockData.GetDogs());
            var controller = new DogController(service.Object);

            var result = await controller.GetDogs();

            result.GetType().Should().Be(typeof(OkObjectResult));
        }

        [Fact]
        public async Task GetPaginationSortedDogs_Should_Return_200_Status_Code()
        {
            var service = new Mock<IDogService>();
            service.Setup(x => x.PaginationSortDogs("Name", "desc", 0, 3))
                .ReturnsAsync(ControllerMockData.GetDogs());
            var controller = new DogController(service.Object);

            var result = await controller.GetPaginationSortedDogs("Name", "desc", 0, 3);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task AddDog_Should_Return_201_Status_Code()
        {
            var service = new Mock<IDogService>();
            service.Setup(x => x.AddDog(ControllerMockData.AddDog())).ReturnsAsync(true);
            var controller = new DogController(service.Object);

            var result = await controller.AddDog(ControllerMockData.AddDog());

            result.Should().BeOfType<CreatedResult>();
        }

        [Fact]
        public async Task AddDog_Same_Name_Should_Return_400_Status_Code()
        {
            await _context.Dogs.AddAsync(ControllerMockData.InitialDog());
            await _context.SaveChangesAsync();
            var service = new DogService(_context);
            var controller = new DogController(service);

            var result = await controller.AddDog(ControllerMockData.AddDog());

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task AddDog_Negative_Length_Should_Return_201_Status_Code()
        {
            var service = new DogService(_context);
            var controller = new DogController(service);

            var result = await controller.AddDog(ControllerMockData.AddDogNegativeLength());

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task AddDog_Negative_Weight_Should_Return_201_Status_Code()
        {
            var service = new DogService(_context);
            var controller = new DogController(service);

            var result = await controller.AddDog(ControllerMockData.AddDogNegativeWeight());

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        public async void Dispose()
        {
            await _context.Database.EnsureDeletedAsync();
            await _context.DisposeAsync();
        }
    }
}
