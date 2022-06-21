using MessagingService.Core.Entities.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Serilog;
using Serilog.Core;

namespace MessagingService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Logger logger = new LoggerConfiguration()
              .CreateLogger();

            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((context, config) =>
                {
                    config.ReadFrom.Configuration(context.Configuration);
                    config.WriteTo.MongoDBBson(cfg =>
                    {
                        // custom MongoDb configuration
                        var mongoDbSettings = new MongoClientSettings
                        {
                            UseTls = true,
                            AllowInsecureTls = true,
                            Credential = MongoCredential.CreateCredential(context.Configuration.GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.DatabaseValue).Value, string.Empty, string.Empty),
                            Server = new MongoServerAddress("127.0.0.1")
                        };

                        var mongoDbInstance = new MongoClient(mongoDbSettings).GetDatabase(context.Configuration.GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.DatabaseValue).Value);

                        // sink will use the IMongoDatabase instance provided
                        cfg.SetMongoDatabase(mongoDbInstance);
                        cfg.SetCollectionName("log");
                    });
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
