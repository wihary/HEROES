namespace AlmaIt.Dotnet.Heroes.Server.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using AlmaIt.Dotnet.Heroes.Server.Data.AccessLayer.Interface;
    using AlmaIt.Dotnet.Heroes.Shared.Extensions;
    using AlmaIt.Dotnet.Heroes.Shared.Models;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controller class for ComicSerie resource <see cref="ComicSeries"/>.
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class ComicSerieController : Controller
    {
        private readonly IComicBookAccessLayer comicBookContext;

        private readonly IComicSeriesAccessLayer comicSerieContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComicSerieController"/> class.</summary>
        /// <param name="comicSerieContext">DI for comic series context.</param>
        /// <param name="comicBookContext">DI for comic book context.</param>
        public ComicSerieController(IComicSeriesAccessLayer comicSerieContext, IComicBookAccessLayer comicBookContext)
        {
            this.comicSerieContext = comicSerieContext;
            this.comicBookContext = comicBookContext;
        }

        /// <summary>API endpoint use to create a new comic serie.</summary>
        /// <param name="comicSerie">Comic serie model to create.</param>
        /// <returns>Return the id of added object.</returns>
        [HttpPost]
        [Authorize(Policy = "WriteUsers")]
        public async Task<IActionResult> AddAsync([FromBody] ComicSeries comicSerie)
        {
            var result = await this.comicSerieContext.AddAsync(comicSerie);
            return this.Ok(result);
        }

        /// <summary>API endpoint use to retrieve all comic series info.</summary>
        /// <param name="sortBy">String defini by which field the list will be sort.</param>
        /// <returns>Return all comic serie sorted.</returns>
        [HttpGet]
        [Authorize(Policy = "ReadOnlyUsers")]
        public IActionResult GetAll([FromQuery] string sortBy)
        {
            var result = this.comicSerieContext.GetAllAsync().ToEnumerable().AsQueryable().Sort(sortBy);

            return result == null ? (IActionResult)this.NoContent() : this.Ok(result);
        }

        /// <summary>API endpoint use to retrieve a comic serie info.</summary>
        /// <param name="id">Comic serie id to retrieve.</param>
        /// <returns>Return a comic serie corresponding to the id.</returns>
        [HttpGet("{id}")]
        [Authorize(Policy = "ReadOnlyUsers")]
        public async Task<IActionResult> GetAsync([FromQuery] int id)
        {
            var result = await this.comicSerieContext.GetAsync(id);

            if (result == null)
            {
                return this.NoContent();
            }

            return this.Ok(result);
        }

        /// <summary>API endpoint use to retrieve comic serie info by its name.</summary>
        /// <param name="name">Comic serie name.</param>
        /// <returns>Return a comic serie corresponding to the name.</returns>
        [HttpGet("{name}")]
        [Authorize(Policy = "ReadOnlyUsers")]
        public IActionResult GetByName([FromQuery] string name)
        {
            var result = this.comicSerieContext.Where(x => x.Name.Contains(name)).FirstOrDefault();

            if (result == null)
            {
                return this.NoContent();
            }

            return this.Ok(result);
        }

        /// <summary>API endpoint to remove a comic serie by its Id.</summary>
        /// <param name="id">Id of the comic serie to remove.</param>
        /// <returns>Return the count of comic serie removed.</returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = "WriteUsers")]
        public async Task<IActionResult> RemoveAsync([FromRoute] int id)
        {
            if (this.comicBookContext.Where(book => book.ComicSerieId == id).Any())
            {
                return this.BadRequest("Some comic book still reference this serie, therefor it cannot be deleted");
            }

            var serie = await this.comicSerieContext.GetAsync(id);

            if (serie == null)
            {
                return this.NoContent();
            }

            var result = await this.comicSerieContext.RemoveAsync(serie);
            return this.Ok(result);
        }

        /// <summary>API endpoint use to update an existing comic serie.</summary>
        /// <param name="comicSerie">Comic serie model to create.</param>
        /// <returns>Return the count of comic serie updated.</returns>
        [HttpPut]
        [Authorize(Policy = "WriteUsers")]
        public async Task<IActionResult> UpdateAsync([FromBody] ComicSeries comicSerie)
        {
            var result = await this.comicSerieContext.UpdateAsync(comicSerie);
            return this.Ok(result);
        }
    }
}
