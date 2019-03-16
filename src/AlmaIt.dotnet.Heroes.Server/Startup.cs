namespace AlmaIt.Dotnet.Heroes.Server
{
    using System.Linq;
    using System.Net.Mime;

    using AlmaIt.Dotnet.Heroes.Server.Data;

    using global::Dotnet.JsonIdentityProvider.Controllers;
    using global::Dotnet.JsonIdentityProvider.Services;

    using Microsoft.AspNetCore.Blazor.Server;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.ResponseCompression;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Newtonsoft.Json;

    /// <summary>
    /// Class where configuration is provided.
    /// </summary>
    public class Startup
    {
        /// <summary>Initializes a new instance of the <see cref="Startup" /> class.</summary>
        /// <param name="configuration">Configuration of application.</param>
        public Startup(IConfiguration configuration)
            => this.Configuration = configuration;

        /// <summary>
        /// Gets the application configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        ///     This method gets called by the runtime.
        ///     Use this method to add services to the container.
        ///     For more information on how to configure your application, visit
        ///     https://go.microsoft.com/fwlink/?LinkID=398940.
        /// </summary>
        /// <param name="services">Collection of services for dependency injection.</param>
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
                    .AddApplicationPart(typeof(AuthController).Assembly)
                    .AddApplicationPart(typeof(UserController).Assembly)
                    .AddControllersAsServices()
                    .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddLogging();

            services.AddResponseCompression(options =>
            {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    MediaTypeNames.Application.Octet,
                    WasmMediaTypeNames.Application.Wasm,
                });
            });

            // Add data context and Db provider based on entity core
            services.AddData();
        }

        /// <summary>This method gets called by the runtime. Use this method to configure the HTTP request pipeline.</summary>
        /// <param name="app">The application builder, where middleware are registered.</param>
        /// <param name="env">Object which represent the environment.</param>
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
            app.UseMvc(routes => { routes.MapRoute("default", "{controller}/{action}/{id?}"); });

            app.UseBlazor<Client.Startup>();
        }
    }
}
