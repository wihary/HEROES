namespace AlmaIt.dotnet.Heroes.Server.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AlmaIt.dotnet.Heroes.Server.Data.AccessLayer.Interface;
    using AlmaIt.dotnet.Heroes.Shared.Business;
    using AlmaIt.dotnet.Heroes.Shared.Enumeration;
    using AlmaIt.dotnet.Heroes.Shared.Models;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class ComicBookController : Controller
    {
        private readonly IComicBookAccessLayer comicBookContext;
        private readonly IComicSeriesAccessLayer comicSerieContext;
        private readonly IObjectTagAccessLayer objectTagContext;

        /// <summary>
        ///     ctro of <see cref="ComicBookController"/>
        /// </summary>
        /// <param name="comicSerieContext">DI for comic series context</param>
        /// <param name="comicBookContext">DI for comic book context</param>
        /// <param name="objectTagContext">DI for tags context</param>
        public ComicBookController(IComicBookAccessLayer comicBookContext, IComicSeriesAccessLayer comicSerieContext, IObjectTagAccessLayer objectTagContext)
        {
            this.comicBookContext = comicBookContext;
            this.comicSerieContext = comicSerieContext;
            this.objectTagContext = objectTagContext;
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
            var result = this.comicBookContext.GetAllComcisAndSerieInfo();

            if (result == null)
                return NoContent();

            return Ok(result);
        }

        /// <summary>
        ///     API endpoint use to retrieve all comic books info
        /// </summary>
        /// <returns></returns>
        [HttpGet("{page}/{size}")]
        public async Task<IActionResult> GetAllAsync(int page, int size)
        {
            var response = new PageResponseData<ComicBook>();
            var result = await this.comicBookContext.GetAllComcisAndSerieInfo();

            if (result == null)
                return NoContent();

            response.TotalResult = result.Count();
            response.MaxPage = (int)Math.Ceiling(result.Count() / (decimal)size);
            response.Result = result.Skip((page - 1) * size).Take(size);
            return Ok(response);
        }

        /// <summary>
        ///     API endpoint use to retrieve all comic books info
        /// </summary>
        /// <returns></returns>
        [HttpGet("type/{status}/{page}/{size}")]
        public async Task<IActionResult> GetByStatusAsync([FromRoute]ComicBookStatus status, [FromRoute] int page, [FromRoute] int size)
        {
            return await this.GetByStatusAsync(status, page, size, string.Empty);
        }

        /// <summary>
        ///     API endpoint use to retrieve all comic books info
        /// </summary>
        /// <returns></returns>
        [HttpGet("type/{status}/{page}/{size}/{filter}")]
        public async Task<IActionResult> GetByStatusAsync([FromRoute]ComicBookStatus status, [FromRoute] int page, [FromRoute] int size, [FromRoute]string filter = "")
        {
            var response = new PageResponseData<ComicBook>();
            var result = (await this.comicBookContext.GetAllComcisAndSerieInfo()).Where(book => book.Status == status);

            if (!string.IsNullOrEmpty(filter))
                result = result.Where(book => book.Title.Contains(filter, StringComparison.InvariantCultureIgnoreCase) || book.ComicSerie.Name.Contains(filter, StringComparison.InvariantCultureIgnoreCase));

            if (result == null)
                return NoContent();

            response.TotalResult = result.Count();
            response.MaxPage = (int)Math.Ceiling(result.Count() / (decimal)size);
            response.Result = result.Skip((page - 1) * size).Take(size);
            return Ok(response);
        }

        /// <summary>
        ///     API endpoint use to create a new comic book
        /// </summary>
        /// <param name="comicBook">Comic book model to create</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] ComicBook comicBook)
        {
            var tagList = new List<ObjectTag>(comicBook.Tags);
            comicBook.RelatedTags = new List<ComicBookTags>();

            foreach (var linkedTag in tagList)
            {
                comicBook.RelatedTags.Add(
                    new ComicBookTags
                    {
                        Tag = await this.objectTagContext.GetAsync(linkedTag.Id)
                    }
                );
            }

            var result = await this.comicBookContext.AddAsync(comicBook);
            return Ok(result);
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
                return Ok(result);
            }

            return NoContent();
        }


        /// <summary>
        ///     API endpoint use to update an existing comic book
        /// </summary>
        /// <param name="model">Comic book model to create</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ComicBook model)
        {
            var tagList = new List<ObjectTag>(model.Tags);

            // Empty Navigation property which should not be send while updating entity
            model.ComicSerie = null;

            // Check if comic book series exists, in order to avoid Access Layer error
            if (!model.ComicSerieId.HasValue || !this.comicSerieContext.Exists(model.ComicSerieId.Value))
            { model.ComicSerieId = null; }

            // Update comic changes
            await this.comicBookContext.UpdateAsync(model);

            // Handle related tags, so that we can update relation if tag have been added or removed
            // First we get model object from db, we clear all related tags
            // related tags or then rebuild from what the client sent
            var comicBookUpdated = await this.comicBookContext.GetAsync(model.Id);
            comicBookUpdated.RelatedTags.Clear();

            foreach (var linkedTag in tagList)
            {
                comicBookUpdated.RelatedTags.Add(
                    new ComicBookTags
                    {
                        Tag = await this.objectTagContext.GetAsync(linkedTag.Id)
                    }
                );
            }
            var result = await this.comicBookContext.UpdateAsync(comicBookUpdated);

            return Ok(result);
        }
    }
}