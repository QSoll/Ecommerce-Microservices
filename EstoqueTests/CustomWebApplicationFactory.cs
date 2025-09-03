using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using EstoqueService;

namespace EstoqueTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(Microsoft.AspNetCore.Hosting.IWebHostBuilder builder)
        {
            // Carrega a configuração de testes
            builder.ConfigureAppConfiguration((context, configBuilder) =>
            {
                configBuilder.AddJsonFile("appsettings.Test.json");
            });
        }
    }
}
