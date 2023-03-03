using AspNetCoreJwtAuthTemplate.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreJwtAuthTemplate.Tests.Systems.Controllers
{
    public class DummyTestController
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturn200Status()
        {
            var dummyController = new DummyController();
            var result = await dummyController.GetAllAsync();


            // Assert
            result.GetType().Should().Be(typeof(OkObjectResult));
        }
    }
}