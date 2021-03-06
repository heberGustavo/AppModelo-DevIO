using DevIO.UI.AppModelo.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.UI.AppModelo
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RazorViewEngineOptions>(options => {
                options.AreaPageViewLocationFormats.Clear();
                options.AreaPageViewLocationFormats.Add("/Modulos/{2}/Views/{1}/{0}.cshtml");
                options.AreaPageViewLocationFormats.Add("/Modulos/{2}/Views/Shared/{0}.cshtml");
                options.AreaPageViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            });
            services.AddControllersWithViews(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);
            services.AddRazorPages();

            //Inje��o de dependencia
            services.AddTransient<IPedidoRepository, PedidoRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(); //Chama os arquivos estaticos, CSS e JS

            app.UseMvc(routes => 
            {
                routes.MapRoute("default","{controller=Home}/{action=Index}/{id?}");

                //Necessario para usar AREAS
                routes.MapRoute("areas", "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                //routes.MapAreaRoute("AreaProdutos", "Produtos", "Produtos/{controller=Cadastro}/{action=Index}/{id?}");
                //routes.MapAreaRoute("AreaVendas", "Vendas", "Vendas/{controller=Pedidos}/{action=Index}/{id?}");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
