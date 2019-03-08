namespace AlmaIt.Dotnet.Heroes.Server.Data.AccessLayer
{
    using AlmaIt.Dotnet.Heroes.Server.Data.AccessLayer.Interface;
    using AlmaIt.Dotnet.Heroes.Shared.Models;
    using AlmaIt.Dotnet.Heroes.Server.Data;
    using AlmaIt.Dotnet.Heroes.Server.Data.AccessLayer;

    /// <summary>
    ///     Class allowing access to <see cref="ObjectTag"/> data object.
    /// </summary>
    internal class ObjectTagAccessLayer : BaseAccessLayer<HeroesContext, ObjectTag, int>, IObjectTagAccessLayer
    {
        public ObjectTagAccessLayer(HeroesContext context)
            : base(context)
        {
        }
    }
}