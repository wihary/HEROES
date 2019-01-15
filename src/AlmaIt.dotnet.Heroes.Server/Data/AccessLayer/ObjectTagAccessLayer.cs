namespace AlmaIt.dotnet.Heroes.Server.Data.AccessLayer
{
    using AlmaIt.dotnet.Heroes.Server.Data.AccessLayer.Interface;
    using AlmaIt.dotnet.Heroes.Shared.Models;

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