namespace AlmaIt.dotnet.Heroes.Server.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System;

    using AlmaIt.dotnet.Heroes.Server.Data.AccessLayer.Interface;
    using AlmaIt.dotnet.Heroes.Shared.Business;
    using AlmaIt.dotnet.Heroes.Shared.Enumeration;
    using AlmaIt.dotnet.Heroes.Shared.Helpers;
    using AlmaIt.dotnet.Heroes.Shared.Models;

    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class ComicBookController : Controller
    {
        private readonly IComicBookAccessLayer comicBookContext;
        private readonly IComicSeriesAccessLayer comicSerieContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComicBookController"/> class.
        /// </summary>
        /// <param name="ComicSerieContext">DI for comic series context</param>
        /// <param name="ComicBookContext">DI for comic book context</param>
        public ComicBookController(IComicBookAccessLayer ComicBookContext, IComicSeriesAccessLayer ComicSerieContext)
        {
            this.comicBookContext = ComicBookContext;
            this.comicSerieContext = ComicSerieContext;
        }

        /// <summary>
        ///     API endpoint use to retrieve a comic book info
        /// </summary>
        /// <param name="id">Comic book id to retrieve</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromQuery] int id)
        {
            var result = await this.comicBookContext.GetAsync(id).ConfigureAwait(false);

            if (result == null)
                return this.NoContent();

            return this.Ok(result);
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
                return this.NoContent();

            return this.Ok(result);
        }

        /// <summary>
        ///     API endpoint use to retrieve all comic books info
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string sortBy)
        {
            var result = await this.comicBookContext.GetAllComcisAndSerieInfo().ConfigureAwait(false);

            if (result == null)
                return this.NoContent();

            return this.Ok(result.AsQueryable().Sort(sortBy));
        }

        /// <summary>
        ///     API endpoint use to retrieve all comic books info
        /// </summary>
        /// <returns></returns>
        [HttpGet("{page}/{size}")]
        public async Task<IActionResult> GetAllAsync(int page, int size, [FromQuery] string sortBy)
        {
            var response = new PageResponseData<ComicBook>();
            var result = await this.comicBookContext.GetAllComcisAndSerieInfo().ConfigureAwait(false);

            if (result == null)
                return this.NoContent();
            result = result.AsQueryable().Sort(sortBy);
            response.TotalResult = result.Count();
            response.MaxPage = (int) Math.Ceiling(result.Count() / (decimal) size);
            response.Result = result.Skip((page - 1) * size).Take(size);
            return this.Ok(response);
        }

        /// <summary>
        ///     API endpoint use to retrieve all comic books info
        /// </summary>
        /// <returns></returns>
        [HttpGet("type/{status}/{page}/{size}")]
        public async Task<IActionResult> GetByStatusAsync([FromRoute] ComicBookStatus status, [FromRoute] int page, [FromRoute] int size, [FromQuery] string sortBy)
        {
            return await this.GetByStatusAsync(status, page, size, sortBy, string.Empty).ConfigureAwait(false);
        }

        /// <summary>
        ///     API endpoint use to retrieve all comic books info
        /// </summary>
        /// <returns></returns>
        [HttpGet("type/{status}/{page}/{size}/{filter}")]
        public async Task<IActionResult> GetByStatusAsync([FromRoute] ComicBookStatus status, [FromRoute] int page, [FromRoute] int size, [FromQuery] string sortBy, [FromRoute] string filter = "")
        {
            var response = new PageResponseData<ComicBook>();
            var result = (await this.comicBookContext.GetAllComcisAndSerieInfo().ConfigureAwait(false)).Where(book => book.Status == status);

            if (!string.IsNullOrEmpty(filter))
                result = result.Where(book => book.Title.Contains(filter, StringComparison.InvariantCultureIgnoreCase) || book.ComicSerie.Name.Contains(filter, StringComparison.InvariantCultureIgnoreCase));

            if (result == null)
                return this.NoContent();
            result = result.AsQueryable().Sort(sortBy);
            response.TotalResult = result.Count();
            response.MaxPage = (int) Math.Ceiling(result.Count() / (decimal) size);
            response.Result = result.Skip((page - 1) * size).Take(size);
            return this.Ok(response);
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
            return this.Ok(result);
        }

        /// <summary>
        ///     API endpoint to remove a comic book by its Id
        /// </summary>
        /// <param name="id">Id of the comic book to remove</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync([FromRoute] int id)
        {
            var comicBook = await this.comicBookContext.GetAsync(id);

            if (comicBook != null)
            {
                var result = await this.comicBookContext.RemoveAsync(comicBook);
                return this.Ok(result);
            }

            return this.NoContent();
        }

        /// <summary>
        ///     API endpoint use to update an existing comic book
        /// </summary>
        /// <param name="comicBook">Comic book model to create</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ComicBook comicBook)
        {
            // Empty Navigation property which should not be send while updating entity
            comicBook.ComicSerie = null;

            // Check if comic book series exists, in order to avoid Access Layer error
            if (!comicBook.ComicSerieId.HasValue || !this.comicSerieContext.Exists(comicBook.ComicSerieId.Value))
                comicBook.ComicSerieId = null;

            var result = await this.comicBookContext.UpdateAsync(comicBook);
            return this.Ok(result);
        }
    }
}
