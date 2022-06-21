using MessagingService.API.Filters;
using MessagingService.Core.Entities.Base;
using MessagingService.Core.Entities.Settings;
using MessagingService.DataAccess.Abstract;
using MessagingService.DataAccess.Concrete;
using MessagingService.Extensions;
using MessagingService.Services.Implementations;
using MessagingService.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Text;

namespace MessagingService.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddControllers(config =>
            {
                config.Filters.Add(new MessagingServiceResponseFilter());
            });

            #region mongo db configuration
            string connectionString = Configuration.GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.ConnectionStringValue).Value;
            services.Configure<MongoDbSettings>(options =>
            {
                options.ConnectionString = connectionString;
                options.Database = Configuration.GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.DatabaseValue).Value;
            }); ;

            services.AddSingleton<IUserDal, UserMongoDbDal>();
            services.AddSingleton<IUserHistoryDal, UserHistoryMongoDbDal>();
            services.AddSingleton<IMessageDal, MessageMongoDbDal>();
            #endregion

            #region swagger configuration
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MessagingService.API", Version = "v1" });
            });
            #endregion

            #region jwt token settings
            // set JWT authentication settings
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            #endregion

            #region dependency injection
            //for dependency injection settings.
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IUserHistoryService, UserHistoryService>();
            services.AddSingleton<IAuditService, AuditService>();
            services.AddSingleton<IMessageService, MessageService>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsJsonAsync(new BaseMessagingServiceResponse()
                            {
                                IsSuccess = false,
                                StatusCode = (int)HttpStatusCode.InternalServerError,
                                ErrorMessage = error.Error.Message
                            });
                        }
                    });
                });
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MessagingService.API v1"));
            }
            else
            {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsJsonAsync(new BaseMessagingServiceResponse()
                            {
                                IsSuccess = false,
                                StatusCode = (int)HttpStatusCode.InternalServerError,
                                ErrorMessage = error.Error.Message
                            });
                        }
                    });
                });
            }



            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
