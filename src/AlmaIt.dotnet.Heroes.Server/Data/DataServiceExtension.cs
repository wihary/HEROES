namespace AlmaIt.Dotnet.Heroes.Server.Data
{
    using System;

    using AlmaIt.Dotnet.Heroes.Server.Data.AccessLayer;
    using AlmaIt.Dotnet.Heroes.Server.Data.AccessLayer.Interface;

    using Microsoft.Extensions.DependencyInjection;

    /// <summary>Service collection extension class use to setup data access.</summary>
    public static class DataServiceExtension
    {
        /// <summary>Extension method use to initialize data access.</summary>
        /// <param name="services">Collection of the available services for the app.</param>
        /// <returns>Returns edited services collection.</returns>
        public static IServiceCollection AddData(this IServiceCollection services)
        {
            services.AddDbContext<HeroesContext>();

            services.AddTransient<IComicBookAccessLayer, ComicBookAccessLayer>();
            services.AddTransient<IComicSeriesAccessLayer, ComicSeriesAccessLayer>();
            services.AddTransient<IObjectTagAccessLayer, ObjectTagAccessLayer>();
            services.AddTransient<InitializeDb>();

            return services;
        }

        /// <summary>Method called to initialized Db.</summary>
        /// <param name="provider">Application services provider.</param>
        public static void InitData(this IServiceProvider provider) => provider.GetRequiredService<InitializeDb>().Init();
    }
}
