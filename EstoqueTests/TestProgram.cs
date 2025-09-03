using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using EstoqueService.Data;

namespace EstoqueTests
{
    public class TestProgram
    {
        public static WebApplication BuildApp()
        {
            var builder = WebApplication.CreateBuilder();

            builder.Services.AddDbContext<EstoqueDbContext>(options =>
                options.UseInMemoryDatabase("TestDb"));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            return app;
        }
    }
}
