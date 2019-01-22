namespace AlmaIt.dotnet.Heroes.Server.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using AlmaIt.dotnet.Heroes.Server.Data.AccessLayer.Interface;
    using AlmaIt.dotnet.Heroes.Shared.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [Authorize]
    public class ComicSerieController : Controller
    {
        private readonly IComicSeriesAccessLayer comicSerieContext;
        private readonly IComicBookAccessLayer comicBookContext;

        /// <summary>
        ///     ctro of <see cref="ComicSerieController"/>
        /// </summary>
        /// <param name="ComicSerieContext">DI for comic series context</param>
        /// <param name="ComicBookContext">DI for comic book context</param>
        public ComicSerieController(IComicSeriesAccessLayer ComicSerieContext, IComicBookAccessLayer ComicBookContext)
        {
            this.comicSerieContext = ComicSerieContext;
            this.comicBookContext = ComicBookContext;
        }

        /// <summary>
        ///     API endpoint use to retrieve a comic serie info
        /// </summary>
        /// <param name="id">Comic serie id to retrieve</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Policy = "ReadOnlyUsers")]
        public async Task<IActionResult> GetAsync([FromQuery] int id)
        {
            var result = await this.comicSerieContext.GetAsync(id);

            if (result == null)
                return NoContent();

            return Ok(result);
        }

        /// <summary>
        ///     API endpoint use to retrieve comic serie info by its name
        /// </summary>
        /// <param name="name">Comic serie name</param>
        /// <returns></returns>
        [HttpGet("{name}")]
        [Authorize(Policy = "ReadOnlyUsers")]
        public IActionResult GetByName([FromQuery] string name)
        {
            var result = this.comicSerieContext.Where(x => x.Name.Contains(name)).FirstOrDefault();

            if (result == null)
                return NoContent();

            return Ok(result);
        }

        /// <summary>
        ///     API endpoint use to retrieve all comic series info
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = "ReadOnlyUsers")]
        public IActionResult GetAll()
        {
            var result = this.comicSerieContext.GetAllAsync().ToEnumerable();

            if (result == null)
                return NoContent();

            return Ok(result);
        }

        /// <summary>
        ///     API endpoint use to create a new comic serie
        /// </summary>
        /// <param name="ComicSeries">Comic serie model to create</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = "WriteUsers")]
        public async Task<IActionResult> AddAsync([FromBody] ComicSeries comicserie)
        {
            var result = await this.comicSerieContext.AddAsync(comicserie);
            return Ok(result);
        }

        /// <summary>
        ///     API endpoint to remove a comic serie by its Id
        /// </summary>
        /// <param name="id">Id of the comic serie to remove</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = "Administrators")]
        public async Task<IActionResult> RemoveAsync([FromRoute] int id)
        {
            if(this.comicBookContext.Where(book => book.ComicSerieId == id).Any())
            {
                return BadRequest($"Some comic book still reference this serie, therefor it cannot be deleted");
            }

            var comicSerie = await this.comicSerieContext.GetAsync(id);

            if (comicSerie != null)
            {
                var result = await this.comicSerieContext.RemoveAsync(comicSerie);
                return Ok(result);
            }

            return NoContent();
        }


        /// <summary>
        ///     API endpoint use to update an existing comic serie
        /// </summary>
        /// <param name="ComicSeries">Comic serie model to create</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Policy = "WriteUsers")]
        public async Task<IActionResult> UpdateAsync([FromBody] ComicSeries comicserie)
        {
            var result = await this.comicSerieContext.UpdateAsync(comicserie);
            return Ok(result);
        }
    }
}