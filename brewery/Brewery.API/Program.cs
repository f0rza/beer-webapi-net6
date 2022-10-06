namespace Brewery.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddServices();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddOpenApiDocumentation();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseOpenApiDocumentation();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}