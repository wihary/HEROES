using AlmaIt.Dotnet.Heroes.Server.Data;
using AlmaIt.Dotnet.Heroes.Server.Data;
using Dotnet.JsonIdentityProvider.Services;
using Microsoft.AspNetCore.Blazor.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Net.Mime;

namespace AlmaIt.Dotnet.Heroes.Server
{
    public class Startup
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IConfiguration Configuration { get; }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to add services to the container.
        ///     For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Initialize external library that implements the custom IdentityProvider
            services.AddJsonIdentityProvider(this.Configuration);

            // Define all API policies based on client claims
            services.AddAuthorization(config =>
            {
                config.AddPolicy("SuperUsers", policy => policy.RequireClaim("SuperUser", "True"));
                config.AddPolicy("Administrators", policy => policy.RequireClaim("Admin", "True"));
                config.AddPolicy("WriteUsers", policy => policy.RequireClaim("Write", "True"));
                config.AddPolicy("ReadOnlyUsers", policy => policy.RequireClaim("Read", "True"));
            });

            // Initialize base core MVC pattern, also add external assembly controller
            services.AddMvc()
                .AddApplicationPart(typeof(global::Dotnet.JsonIdentityProvider.Controllers.AuthController).Assembly)
                .AddApplicationPart(typeof(global::Dotnet.JsonIdentityProvider.Controllers.UserController).Assembly)
                .AddControllersAsServices()
                .AddJsonOptions(
                    options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            services.AddLogging();

            services.AddResponseCompression(options =>
            {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    MediaTypeNames.Application.Octet,
                    WasmMediaTypeNames.Application.Wasm
                });
            });

            // Add data context and Db provider based on entity core
            services.AddData();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller}/{action}/{id?}");
            });

            app.UseBlazor<Client.Startup>();
        }
    }
}
