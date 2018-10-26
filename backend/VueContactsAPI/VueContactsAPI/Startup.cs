using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VueContactsAPI.Entities;
using VueContactsAPI.Middleware;
using VueContactsAPI.Repositories;
using VueContactsAPI.Services;
using VueContactsAPI.ViewModels;

namespace VueContactsAPI
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<MyContactsDbContext>(o => o.UseSqlServer(Configuration["ConnectionString:MyContactConnectionString"]));

            services.AddCors(options => {
                options.AddPolicy("CorsPolicy", builder => {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                           });
                });

            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IJwtService, JwtService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }


            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");

       //     app.UseMiddleware<SecurityMiddleware>();

            app.UseMvc();

            // Automapper
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ContactVM, Contact>();
                cfg.CreateMap<Contact, ContactVM>();
                cfg.CreateMap<Contact, Contact>();
            });
        }
    }
}
