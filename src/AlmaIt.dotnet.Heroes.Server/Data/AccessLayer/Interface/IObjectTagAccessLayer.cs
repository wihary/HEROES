using AlmaIt.dotnet.Heroes.Shared.Models;
using AlmaIt.Dotnet.Heroes.Server.Data.AccessLayer.Interface;

namespace AlmaIt.dotnet.Heroes.Server.Data.AccessLayer.Interface
{
    /// <summary>
    ///     Interface defining how to interact with <see cref="ObjectTag"/> data object
    /// </summary>
    public interface IObjectTagAccessLayer : IBaseAccessLayer<ObjectTag, int>
    {

    }
}