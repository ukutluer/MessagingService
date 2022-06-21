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
                        
                        string connectionString = context.Configuration.GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.ConnectionStringValue).Value;
                        var mobdbsetting = new MongoDbSettings
                        {
                            ConnectionString = connectionString,
                            Database = context.Configuration.GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.DatabaseValue).Value,
                        }; 
                        var mongoClient = new MongoClient(mobdbsetting.ConnectionString);
                        var db = mongoClient.GetDatabase(mobdbsetting.Database);
                        cfg.SetMongoDatabase(db);
                        cfg.SetCollectionName("log");
                    });
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
