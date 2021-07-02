using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Hermes.Api
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
            var assemblies = new[]
            {
                Assembly.Load("Hermes.Api"),
                Assembly.Load("Hermes.Data"),
                Assembly.Load("Hermes.EF"),
                Assembly.Load("Hermes.Features"),
            };
            services.AddMediatR(assemblies);
            services.AddAutoMapper(assemblies);
            services.AddLogging(builder =>
            {
                builder.ClearProviders();

                var logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(Configuration)
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .CreateLogger();

                builder.AddSerilog(logger);
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}