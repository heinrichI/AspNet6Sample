using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Text;
using System.Text.Json;

namespace IntegrationTest
{
    public class UserControllerTest
    {

        [Fact]
        public async Task UserRegisterTest()
        {
            using var testServer = Host.CreateDefaultBuilder()
            .ConfigureWebHost(builder =>
            {
                builder.UseStartup(typeof(AspNet6.Startup))
                // Use the test server and point to the application's startup
                .UseTestServer();
            })
            .ConfigureServices(services =>
            {
                // Replace the service
                //services.AddSingleton<IHelloService, MockHelloService>();
            })
            .Build();

            await testServer.StartAsync();
            var client = testServer.GetTestClient();

            var user = new 
            { 
                Name = "Vasa",
                Password = "12345",
                Email = "vasa@creml.ru",
            };
            var bodyJson = JsonSerializer.Serialize(user);
            var stringContent = new StringContent(bodyJson, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/user/register", stringContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}