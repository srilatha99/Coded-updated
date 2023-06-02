using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CodedWebTest.Data;
using CodedWebTest.Entities;
using CodedWebTest.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CodedWebTest
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure session cookie
            services.AddSession(o =>
            {
                o.IdleTimeout = TimeSpan.FromMinutes(30);
                o.Cookie.HttpOnly = true;
                o.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                o.Cookie.IsEssential = true;
                o.Cookie.Name = ".CodedWebTest.Session";
            });

            services.AddControllersWithViews();

            // TODO: Configure SessionDataService

            // TODO: Configure WebTestDBContext

            // Seed Database
            SeedDatabase();
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        #region Do not edit

        private static void SeedDatabase()
        {
            var options = new DbContextOptionsBuilder<WebTestDBContext>().UseInMemoryDatabase(databaseName: "WebTestDatabase").Options;

            using var ctx = new WebTestDBContext(options);

            ctx.EmailAddress.Add(new EmailAddress { EmailAddressUid = Guid.NewGuid(), Address = "joseph@codedinc.net", CreatedDate = DateTime.ParseExact("09/01/2022", "MM/dd/yyyy", CultureInfo.InvariantCulture) });
            ctx.EmailAddress.Add(new EmailAddress { EmailAddressUid = Guid.NewGuid(), Address = "ed@codedinc.net", CreatedDate = DateTime.ParseExact("02/01/2012", "MM/dd/yyyy", CultureInfo.InvariantCulture) });
            ctx.EmailAddress.Add(new EmailAddress { EmailAddressUid = Guid.NewGuid(), Address = "jose@codedinc.net", CreatedDate = DateTime.ParseExact("06/19/2022", "MM/dd/yyyy", CultureInfo.InvariantCulture) });
            ctx.EmailAddress.Add(new EmailAddress { EmailAddressUid = Guid.NewGuid(), Address = "edward@codedinc.net", CreatedDate = DateTime.ParseExact("06/19/2022", "MM/dd/yyyy", CultureInfo.InvariantCulture) });

            ctx.SaveChanges();
        }

        #endregion
    }
}
