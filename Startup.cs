using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApiAngularStorm.Models;

namespace WebApiAngularStorm
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase());

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            
            //var context = app.ApplicationServices.GetService<ApiContext>();
            //SeedDatabase(context);

            app.UseDefaultFiles(); // serves up our wwwroot files
            app.UseStaticFiles(); // allows serving of static files
            app.UseMvc(); // sets MVC routes for webapi
        }

        private static void SeedDatabase(ApiContext context)
        {
            context.Todos.Add(new Todo
                {
                    Id = 1,
                    Description = "My First Todo",
                    Completed = false,
                    User = "me@mymail.com"
                });

            context.Todos.Add(new Todo
                {
                    Id = 2,
                    Description = "My Second Todo",
                    Completed = false,
                    User = "me@mymail.com"
                });

            context.SaveChanges();
        }
    }
}
