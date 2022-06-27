using BusinessLogic;
using DataBase;
using DataBaseMigration;

namespace AspNet6
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Manually create an instance of the Startup class
            var startup = new Startup(builder.Configuration);

            // Manually call ConfigureServices()
            startup.ConfigureServices(builder.Services);

            var app = builder.Build();

            // Call Configure(), passing in the dependencies
            startup.Configure(app, app.Environment);

            var conString = builder.Configuration.GetConnectionString("myDb");
            MigrateDB.Migrate(conString);

            app.Run();
        }      
    }
}