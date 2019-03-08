namespace AlmaIt.dotnet.Heroes.Server.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using AlmaIt.dotnet.Heroes.Server.Data.AccessLayer.Interface;
    using AlmaIt.dotnet.Heroes.Shared.Models;
    using AlmaIt.dotnet.Heroes.Shared.Helpers;

    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class ComicSerieController : Controller
    {
        private readonly IComicSeriesAccessLayer comicSerieContext;
        private readonly IComicBookAccessLayer comicBookContext;

        /// <summary>
        ///     ctro of <see cref="ComicSerieController"/>
        /// </summary>
        /// <param name="comicSerieContext">DI for comic series context</param>
        /// <param name="comicBookContext">DI for comic book context</param>
        public ComicSerieController(IComicSeriesAccessLayer comicSerieContext, IComicBookAccessLayer comicBookContext)
        {
            this.comicSerieContext = comicSerieContext;
            this.comicBookContext = comicBookContext;
        }

        /// <summary>
        ///     API endpoint use to retrieve a comic serie info
        /// </summary>
        /// <param name="id">Comic serie id to retrieve</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromQuery] int id)
        {
            var result = await this.comicSerieContext.GetAsync(id);

            if (result == null)
            {
                return this.NoContent();
            }

            return this.Ok(result);
        }

        /// <summary>
        ///     API endpoint use to retrieve comic serie info by its name
        /// </summary>
        /// <param name="name">Comic serie name</param>
        /// <returns></returns>
        [HttpGet("{name}")]
        public IActionResult GetByName([FromQuery] string name)
        {
            var result = this.comicSerieContext.Where(x => x.Name.Contains(name)).FirstOrDefault();

            if (result == null)
            {
                return this.NoContent();
            }

            return this.Ok(result);
        }

        /// <summary>
        ///     API endpoint use to retrieve all comic series info
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll([FromQuery] string sortBy)
        {
            var result = this.comicSerieContext.GetAllAsync().ToEnumerable().AsQueryable().Sort(sortBy);

            if (result == null)
            {
                return this.NoContent();
            }

            return this.Ok(result);
        }

        /// <summary>
        ///     API endpoint use to create a new comic serie
        /// </summary>
        /// <param name="ComicSeries">Comic serie model to create</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] ComicSeries comicserie)
        {
            var result = await this.comicSerieContext.AddAsync(comicserie);
            return this.Ok(result);
        }

        /// <summary>
        ///     API endpoint to remove a comic serie by its Id
        /// </summary>
        /// <param name="id">Id of the comic serie to remove</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync([FromRoute] int id)
        {
            if (this.comicBookContext.Where(book => book.ComicSerieId == id).Any())
            {
                return this.BadRequest($"Some comic book still reference this serie, therefor it cannot be deleted");
            }

            var serie = await this.comicSerieContext.GetAsync(id);

            if (serie == null)
            {
                return this.NoContent();
            }

            var result = await this.comicSerieContext.RemoveAsync(serie);
            return this.Ok(result);

        }

        /// <summary>
        ///     API endpoint use to update an existing comic serie
        /// </summary>
        /// <param name="comicSerie">Comic serie model to create</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ComicSeries comicSerie)
        {
            var result = await this.comicSerieContext.UpdateAsync(comicSerie);
            return this.Ok(result);
        }
    }
}
