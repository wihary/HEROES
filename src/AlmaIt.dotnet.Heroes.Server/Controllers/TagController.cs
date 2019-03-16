namespace AlmaIt.Dotnet.Heroes.Server.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using AlmaIt.Dotnet.Heroes.Server.Data.AccessLayer.Interface;
    using AlmaIt.Dotnet.Heroes.Shared.Models;

    using Microsoft.AspNetCore.Mvc;

    /// <summary>Controller class for Tag resource <see cref="ObjectTag" />.</summary>
    [Route("api/[controller]")]
    public class TagController : Controller
    {
        private readonly IObjectTagAccessLayer objectTagAccess;

        /// <summary>Initializes a new instance of the <see cref="TagController" /> class.</summary>
        /// <param name="objectTagAccess">DI for object tag context.</param>
        public TagController(IObjectTagAccessLayer objectTagAccess)
            => this.objectTagAccess = objectTagAccess;

        /// <summary>API endpoint use to retrieve a Object Tags info.</summary>
        /// <param name="id">Object Tags id to retrieve.</param>
        /// <returns>Return a tag corresponding to the id.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromQuery] int id)
        {
            var result = await this.objectTagAccess.GetAsync(id);

            return result == null ? (IActionResult)this.NoContent() : this.Ok(result);
        }

        /// <summary>API endpoint use to retrieve Object Tags info by its name.</summary>
        /// <param name="name">Object Tags name.</param>
        /// <returns>Return a tag corresponding to the name.</returns>
        [HttpGet("{name}")]
        public IActionResult GetByName([FromQuery] string name)
        {
            var result = this.objectTagAccess.Where(x => x.Name.Contains(name)).FirstOrDefault();

            return result == null ? (IActionResult)this.NoContent() : this.Ok(result);
        }

        /// <summary>API endpoint use to retrieve all Object Tagss info.</summary>
        /// <returns>Return all comic tags.</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = this.objectTagAccess.GetAllAsync().ToEnumerable();

            return result == null ? (IActionResult)this.NoContent() : this.Ok(result);
        }

        /// <summary>API endpoint use to create a new Object Tags.</summary>
        /// <param name="tag">Object Tags model to create.</param>
        /// <returns>Returns Id of newly created data object.</returns>
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] ObjectTag tag)
        {
            if (tag == null)
            {
                return this.BadRequest();
            }

            var result = await this.objectTagAccess.AddAsync(tag);
            return this.Ok(result);
        }

        /// <summary>API endpoint to remove a Object Tags by its Id.</summary>
        /// <param name="id">Id of the Object Tags to remove.</param>
        /// <returns>Return the count of tag removed.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync([FromRoute] int id)
        {
            var tagObject = await this.objectTagAccess.GetAsync(id);

            if (tagObject == null)
            {
                return this.NoContent();
            }

            var result = await this.objectTagAccess.RemoveAsync(tagObject);
            return this.Ok(result);
        }

        /// <summary>API endpoint use to update an existing Object Tags.</summary>
        /// <param name="tag">Object Tags model to create.</param>
        /// <returns>Return the count of comic serie updated.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ObjectTag tag)
        {
            var result = await this.objectTagAccess.UpdateAsync(tag);
            return this.Ok(result);
        }
    }
}
