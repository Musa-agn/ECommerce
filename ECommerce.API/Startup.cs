using ECommerce.API.CustomMiddleware;
using ECommerce.BusinessLayer.Abstract;
using ECommerce.BusinessLayer.Concrete;
using ECommerce.Data.Abstract;
using ECommerce.Data.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ECommerce.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICampaignRepository, CampaignRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICampaignService, CampaignService>();
            services.AddScoped<IIncreaseTimeService, IncreaseTimeService>();
            services.AddScoped<ICampaignAlgorithmService, CampaignAlgorithmService>();
            services.AddScoped<IScenarioService, ScenarioService>();

            services.AddSwaggerGen(c =>
           {
               c.SwaggerDoc("CoreSwagger", new Microsoft.OpenApi.Models.OpenApiInfo
               {
                   Title = "Swagger on ASP.NET Core",
                   Version = "1.0.0",
                   Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                   {
                       Name = "Musa Aðaçyetiþtiren",
                   },
               });
           });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseExceptionMiddleware();

            app.UseSwagger()
            .UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/CoreSwagger/swagger.json", "Swagger Test .Net Core");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
