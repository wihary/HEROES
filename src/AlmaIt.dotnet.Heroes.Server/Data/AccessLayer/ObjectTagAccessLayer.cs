namespace AlmaIt.Dotnet.Heroes.Server.Data.AccessLayer
{
    using AlmaIt.Dotnet.Heroes.Server.Data.AccessLayer.Interface;
    using AlmaIt.Dotnet.Heroes.Shared.Models;

    /// <summary>Class allowing access to <see cref="ObjectTag" /> data object.</summary>
    internal class ObjectTagAccessLayer : BaseAccessLayer<HeroesContext, ObjectTag, int>, IObjectTagAccessLayer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectTagAccessLayer"/> class.
        /// </summary>
        /// <param name="context">Database context.</param>
        public ObjectTagAccessLayer(HeroesContext context)
            : base(context)
        {
        }
    }
}
