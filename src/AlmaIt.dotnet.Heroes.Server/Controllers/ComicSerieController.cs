namespace AlmaIt.dotnet.Heroes.Server.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using AlmaIt.dotnet.Heroes.Server.Data.AccessLayer.Interface;
    using AlmaIt.dotnet.Heroes.Shared.Models;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class ComicSerieController : Controller
    {
        private readonly IComicSeriesAccessLayer comicSerieContext;

        /// <summary>
        ///     ctro of <see cref="ComicSerieController"/>
        /// </summary>
        /// <param name="ComicSerieContext">DI for comic series context</param>
        public ComicSerieController(IComicSeriesAccessLayer ComicSerieContext)
        {
            this.comicSerieContext = ComicSerieContext;
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
                return NoContent();

            return Ok(result);
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
                return NoContent();

            return Ok(result);
        }

        /// <summary>
        ///     API endpoint use to retrieve all comic series info
        /// </summary>
        /// <returns></returns>
        [HttpGet]
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
        public async Task<IActionResult> RemoveAsync([FromRoute] int id)
        {
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
        public async Task<IActionResult> UpdateAsync([FromBody] ComicSeries comicserie)
        {
            var result = await this.comicSerieContext.UpdateAsync(comicserie);
            return Ok(result);
        }
    }
}