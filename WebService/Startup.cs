using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization; // acrescentado
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebService.Data;
using WebService.Services;
using System.Globalization; // acrescentado

namespace WebService
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<WebServiceContext>(options => // aqui está sendo passado o tipo de DbContext da nossa aplicação, que está em Dara: WebService
                    options.UseMySql(Configuration.GetConnectionString("WebServiceContext"), builder =>
                        builder.MigrationsAssembly("WebService")));

            services.AddScoped<SeedingService>(); //registra nosso serviço no gestor de injeção de dependência da nossa aplicação //
            services.AddScoped<SellerService>(); // agora essa classe pode ser injetada em outras classes //
            services.AddScoped<DepartmentService>();
            services.AddScoped<SalesRecordService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, SeedingService seedingService)   // ele aceita que eu acrescente outros parâmetros nele //
        {
            var enUS = new CultureInfo("en-US");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(enUS),
                SupportedCultures = new List<CultureInfo> { enUS },
                SupportedUICultures = new List<CultureInfo> { enUS }
            }; //definição das opções de localização //

            app.UseRequestLocalization(localizationOptions); // configurado então que nosso aplicativo agora vai ter como localização, a localização dos Estados Unidos (US) //


            if (env.IsDevelopment())   // se eu estiver no perfil de desenvolvimento
            {
                app.UseDeveloperExceptionPage();
                seedingService.Seed(); //assim vou popular minha base de dados, caso ela não esteja populada ainda //
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"); // id é opcional, por isso o ponto de interrogação //
            });                                   // se não digitar nenhuma ação, será ação index
        }
    }
}
