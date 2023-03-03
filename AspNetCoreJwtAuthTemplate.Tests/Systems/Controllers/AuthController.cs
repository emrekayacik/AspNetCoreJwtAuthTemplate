using AspNetCoreJwtAuthTemplate.Controllers;
using FluentAssertions;
using Microsoft.Extensions.Configuration;

namespace AspNetCoreJwtAuthTemplate.Tests.Systems.Controllers
{
    public class AuthTestController
    {
        [Fact]
        public async void Get_ShouldReturnABearerToken()
        {
            var client = new HttpClient();
            IConfiguration config = new ConfigurationBuilder()
                .Build();
            AuthController authController = new AuthController(config);
            string token = authController.Get("myUserName","myMail@mail.com","myPassWord");
            
            token.Should().NotBe(null);
            Assert.NotEqual(0,token.Length);
        }
    }
}