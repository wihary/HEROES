namespace AlmaIt.dotnet.Heroes.Server.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using AlmaIt.dotnet.Heroes.Server.Data.AccessLayer.Interface;
    using AlmaIt.dotnet.Heroes.Shared.Models;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class ComicBookController : Controller
    {
        private readonly IComicBookAccessLayer comicBookContext;

        /// <summary>
        ///     ctro of <see cref="ComicBookController"/>
        /// </summary>
        /// <param name="ComicBookContext">DI for comic book context</param>
        public ComicBookController(IComicBookAccessLayer ComicBookContext)
        {
            this.comicBookContext = ComicBookContext;
        }

        /// <summary>
        ///     API endpoint use to retrieve a comic book info
        /// </summary>
        /// <param name="id">Comic book id to retrieve</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromQuery] int id)
        {
            var result = await this.comicBookContext.GetAsync(id);

            if (result == null)
                return NoContent();

            return Ok(result);
        }

        /// <summary>
        ///     API endpoint use to retrieve comic book info by its name
        /// </summary>
        /// <param name="name">Comic book name</param>
        /// <returns></returns>
        [HttpGet("{name}")]
        public IActionResult GetByName([FromQuery] string name)
        {
            var result = this.comicBookContext.Where(x => x.Title.Contains(name)).FirstOrDefault();

            if (result == null)
                return NoContent();

            return Ok(result);
        }

        /// <summary>
        ///     API endpoint use to retrieve all comic books info
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = this.comicBookContext.GetAllAsync().ToEnumerable();

            if (result == null)
                return NoContent();
                
            return Ok(result);
        }

        /// <summary>
        ///     API endpoint use to create a new comic book
        /// </summary>
        /// <param name="comicBook">Comic book model to create</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] ComicBook comicBook)
        {
            var result = await this.comicBookContext.AddAsync(comicBook);
            return Ok(result);
        }

        /// <summary>
        ///     API endpoint to remove a comic book by its Id
        /// </summary>
        /// <param name="id">Id of the comic book to remove</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync([FromQuery] int id)
        {
            var comicBook = await this.comicBookContext.GetAsync(id);

            if (comicBook != null)
            {
                var result = await this.comicBookContext.RemoveAsync(comicBook);
                return Ok(result);
            }

            return NoContent();
        }


        /// <summary>
        ///     API endpoint use to update an existing comic book
        /// </summary>
        /// <param name="comicBook">Comic book model to create</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ComicBook comicBook)
        {
            var result = await this.comicBookContext.UpdateAsync(comicBook);
            return Ok(result);
        }
    }
}