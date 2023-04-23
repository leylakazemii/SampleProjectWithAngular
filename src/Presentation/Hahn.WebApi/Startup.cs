using Hahn.Application.Services.Customers.Commands;
using Hahn.Domain.Model.Customers;
using Hahn.Infrastructure.EfCore.Context;
using Hahn.Infrastructure.EfCore.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hahn.WebApi

{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<AppDbContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<ICustomerRepository, CustomerEfRepository>();
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(typeof(Startup).Assembly, typeof(CreateCustomerCommandHandler).Assembly);
            });
            services.AddMvc();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                // c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blazor API V1");
            });

        }
    }
}
