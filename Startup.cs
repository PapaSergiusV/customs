using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace customs
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
            services.AddControllersWithViews();
            // Add service to connect with database context
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<Context>(options => options.UseNpgsql(connection));//.EnableSensitiveDataLogging());

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/SignIn");
                });

            services.AddAuthorization();
            
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IUserService, UserService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Context db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UsePHPFilter();
            // app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // app.Use(async (context, next) => {
            //     string id = context.User.Claims.Where(c => c.Type == "id").Select(c => c.Value).SingleOrDefault();
            //     context.Items["current_user"] = db.Users.Find(id);
            //     await next();
            // });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Store}/{action=Index}/{id?}");
            });

            app.Run(async (context) => {
                string agent = context.Request.Headers["User-Agent"];
                context.Response.Redirect("/Store");
                await Task.CompletedTask;
            });
        }
    }
}
