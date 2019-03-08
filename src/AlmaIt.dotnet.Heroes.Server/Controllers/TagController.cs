namespace AlmaIt.dotnet.Heroes.Server.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using AlmaIt.dotnet.Heroes.Server.Data.AccessLayer.Interface;
    using AlmaIt.dotnet.Heroes.Shared.Models;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class TagController : Controller
    {
        private readonly IObjectTagAccessLayer objectTagAccess;

        /// <summary>
        ///     ctro of <see cref="ComicSerieController"/>
        /// </summary>
        /// <param name="objectTagAccess">DI for object tag context</param>
        public TagController(IObjectTagAccessLayer objectTagAccess)
        {
            this.objectTagAccess = objectTagAccess;
        }

        /// <summary>
        ///     API endpoint use to retrieve a Object Tags info
        /// </summary>
        /// <param name="id">Object Tags id to retrieve</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromQuery] int id)
        {
            var result = await this.objectTagAccess.GetAsync(id);

            if (result == null)
            { return NoContent(); }

            return Ok(result);
        }

        /// <summary>
        ///     API endpoint use to retrieve Object Tags info by its name
        /// </summary>
        /// <param name="name">Object Tags name</param>
        /// <returns></returns>
        [HttpGet("{name}")]
        public IActionResult GetByName([FromQuery] string name)
        {
            var result = this.objectTagAccess.Where(x => x.Name.Contains(name)).FirstOrDefault();

            if (result == null)
            { return NoContent(); }

            return Ok(result);
        }

        /// <summary>
        ///     API endpoint use to retrieve all Object Tagss info
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = this.objectTagAccess.GetAllAsync().ToEnumerable();

            if (result == null)
            { return NoContent(); }

            return Ok(result);
        }

        /// <summary>
        ///     API endpoint use to create a new Object Tags
        /// </summary>
        /// <param name="ComicSeries">Object Tags model to create</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] ObjectTag tag)
        {
            if (tag == null)
            { return this.BadRequest(); }

            var result = await this.objectTagAccess.AddAsync(tag);
            return Ok(result);
        }

        /// <summary>
        ///     API endpoint to remove a Object Tags by its Id
        /// </summary>
        /// <param name="id">Id of the Object Tags to remove</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync([FromRoute] int id)
        {
            var tagObject = await this.objectTagAccess.GetAsync(id);

            if (tagObject != null)
            {
                var result = await this.objectTagAccess.RemoveAsync(tagObject);
                return Ok(result);
            }

            return NoContent();
        }


        /// <summary>
        ///     API endpoint use to update an existing Object Tags
        /// </summary>
        /// <param name="ComicSeries">Object Tags model to create</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ObjectTag tag)
        {
            var result = await this.objectTagAccess.UpdateAsync(tag);
            return Ok(result);
        }
    }
}