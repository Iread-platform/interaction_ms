using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Consul;
using iread_interaction_ms.DataAccess.Data;
using iread_interaction_ms.DataAccess.Interface;
using iread_interaction_ms.DataAccess.Repository;
using iread_interaction_ms.Web.Profile;
using iread_interaction_ms.Web.Service;
using iread_story.Web.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace iread_interaction_ms
{
    public class Startup
    {
         public static readonly Microsoft.Extensions.Logging.LoggerFactory _myLoggerFactory =
            new LoggerFactory(new[] {
        new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()
            });

        public Startup(IConfiguration configuration)
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile(Directory.GetCurrentDirectory() + "/Properties/launchSettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile(Directory.GetCurrentDirectory() + "/appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public static IConfiguration Configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

             // for routing
            services.AddControllers();


            // for connection of DB
            services.AddDbContext<AppDbContext>(
                options => { options.UseLoggerFactory(_myLoggerFactory).UseMySQL(Configuration.GetConnectionString("DefaultConnection"));
                });
            
            // for consul
            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {
                var address = Configuration.GetValue<string>("ConsulConfig:Host");
                consulConfig.Address = new Uri(address);
            }));
            services.AddConsulConfig(Configuration);
            services.AddHttpClient<IConsulHttpClientService,ConsulHttpClientService>();


            // return only msg of errors as a list when get invalid ModelState in background
            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0)
                .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                var errors = context.ModelState.Values.SelectMany(x => x.Errors.Select (y => y.ErrorMessage));
                    return new BadRequestObjectResult(errors);
                };
           });
            
            // for swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "iread_interaction", Version = "v1" });
            });
            
            // Inject the public repository
            services.AddScoped<IPublicRepository, PublicRepository>();
            
            services.AddScoped<AudioServices>();
            services.AddScoped<CommentsService>();
            services.AddScoped<InteractionsService>();
            services.AddScoped<DrawingService>();
             services.AddScoped<HighLightService>();
            
            IMapper mapper = new MapperConfiguration(config=>{
                config.AddProfile<AutoMapperProfile>();
            }).CreateMapper();
            services.AddSingleton(mapper);

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "iread_interaction v1"));
            }

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.Migrate();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseConsul(Configuration);
        }
    }
}
