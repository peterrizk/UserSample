using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TestProject.WebAPI.Configuration;
using TestProject.WebAPI.Middleware;
using TestProject.WebAPI.Models;
using TestProject.WebAPI.Resolvers;
using TestProject.WebAPI.Services;

namespace TestProject.WebAPI
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
            var dbOptions = Configuration.GetSection("Database").Get<DatabaseOptions>();

            var server = dbOptions.Server;
            var port = dbOptions.Port;
            var user = dbOptions.User;
            var password = dbOptions.Password;
            var database = dbOptions.DatabaseName;

            services.AddDbContext<DataContext>(options =>
                    options.UseSqlServer($"Server={server},{port};Initial Catalog={database};User ID={user};Password={password}"));

            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile(
                     provider.GetService<UserEmailResolver>()
                    ));
            }).CreateMapper());

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<UserEmailResolver>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "User Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                SeedData.SeedData.Populate(app);
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ControllerExceptionHandlerMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Api");
                });
        }
    }
}
