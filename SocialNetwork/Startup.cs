using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialNetwork.Models.Users;
using SocialNetwork.Data.UoW;
using SocialNetwork.Data.Repository;
using SocialNetwork.Extentions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Data.Context;

namespace SocialNetwork
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            string connection = Configuration.GetConnectionString("DefaultConnection");

            services
               .AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection))
               .AddUnitOfWork()
               .AddCustomRepository<Message, MessageRepository>()
               .AddCustomRepository<Friend, FriendsRepository>()
               .AddIdentity<User, IdentityRole>(opts => {
                   opts.Password.RequiredLength = 5;
                   opts.Password.RequireNonAlphanumeric = false;
                   opts.Password.RequireLowercase = false;
                   opts.Password.RequireUppercase = false;
                   opts.Password.RequireDigit = false;
               })
                   .AddEntityFrameworkStores<ApplicationDbContext>();

            var mapperConfig = new MapperConfiguration((v) =>
            {
                v.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddScoped<IMapper>(provider => new Mapper(mapperConfig, provider.GetService));
            services.AddSingleton<IMapper>(provider => new Mapper(mapperConfig, provider.GetService));
           
            services.AddSingleton(mapper);

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IRepository<Message>, MessageRepository>();

            services.AddControllersWithViews();
            services.AddRazorPages();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
              
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            var cachePeriod = "0";
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age={cachePeriod}");
                }
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // определение маршрутов
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

    }
}
