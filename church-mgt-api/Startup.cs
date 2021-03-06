using church_mgt_api.extensions;
using church_mgt_api.Extensions;
using church_mgt_core.services.implementations;
using church_mgt_core.services.interfaces;
using church_mgt_database;
using church_mgt_database.seeder;
using church_mgt_models;
using church_mgt_utilities;
using church_mgt_utilities.Settings;
using FluentValidation.AspNetCore;
using HotelMgt.API.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

namespace church_mgt_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
            StaticConfig = configuration;
        }
        public static IConfiguration StaticConfig { get; private set; }
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();

            // configure environment variable and dbcontext
            services.AddDbContextAndConfigurations(Environment, Configuration);

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddControllers();

            services.AddMvc().AddFluentValidation(fv => {
                fv.RegisterValidatorsFromAssemblyContaining<Startup>();
                fv.ImplicitlyValidateChildProperties = true;
            });
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "church_mgt_api", Version = "v1" });
            //});

            // configure identity
            services.ConfigureIdentity();

            // configure authentication
            services.ConfigureAuthentication();

            // configure dependency injection
            services.AddDependencyInjection();

            // configure Automapper
            services.AddAutoMapper(typeof(AutoMaps));

            // configure mail service
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddTransient<IEmailService, EmailService>();

            // configure CORS for mail service
            services.AddCors(cors =>
            {
                cors.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });

            services.AddRazorPages();

            // configure swagger
            services.AddSwagger();

            // register serilog
            services.AddSingleton(Log.Logger);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            ChurchDbContext dbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "church_mgt_api v1"));

            // use church mgt seeder class
            ChurchMgtSeeder.SeedData(dbContext, userManager, roleManager).GetAwaiter().GetResult();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
