namespace AlmaIt.Dotnet.Heroes.Server.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AlmaIt.Dotnet.Heroes.Server.Data.AccessLayer.Interface;
    using AlmaIt.Dotnet.Heroes.Shared.Business;
    using AlmaIt.Dotnet.Heroes.Shared.Enumeration;
    using AlmaIt.Dotnet.Heroes.Shared.Extensions;
    using AlmaIt.Dotnet.Heroes.Shared.Models;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controller class for ComicSerie resource <see cref="ComicBook"/>.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    public class ComicBookController : Controller
    {
        private readonly IComicBookAccessLayer comicBookLayer;

        private readonly IComicSeriesAccessLayer comicSerieLayer;

        private readonly IObjectTagAccessLayer objectTagLayer;

        /// <summary>Initializes a new instance of the <see cref="ComicBookController" /> class.</summary>
        /// <param name="comicSerieLayer">DI for comic series context.</param>
        /// <param name="comicBookLayer">DI for comic book context.</param>
        /// <param name="objectTagLayer">DI for tags context.</param>
        public ComicBookController(IComicBookAccessLayer comicBookLayer, IComicSeriesAccessLayer comicSerieLayer, IObjectTagAccessLayer objectTagLayer)
        {
            this.comicBookLayer = comicBookLayer;
            this.comicSerieLayer = comicSerieLayer;
            this.objectTagLayer = objectTagLayer;
        }

        /// <summary>API endpoint use to create a new comic book.</summary>
        /// <param name="comicBook">Comic book model to create.</param>
        /// <returns>Returns Id of newly created data object.</returns>
        [HttpPost]
        [Authorize(Policy = "WriteUsers")]
        public async Task<IActionResult> AddAsync([FromBody] ComicBook comicBook)
        {
            var tagList = new List<ObjectTag>(comicBook.Tags);
            comicBook.RelatedTags = new List<ComicBookTags>();

            foreach (var linkedTag in tagList)
            {
                comicBook.RelatedTags.Add(
                    new ComicBookTags
                    {
                        Tag = await this.objectTagLayer.GetAsync(linkedTag.Id),
                    });
            }

            var result = await this.comicBookLayer.AddAsync(comicBook);
            return this.Ok(result);
        }

        /// <summary>API endpoint use to retrieve all comic books info.</summary>
        /// <param name="sortBy">String defini by which field the list will be sort.</param>
        /// <returns>Return all comic book sorted.</returns>
        [HttpGet]
        [Authorize(Policy = "ReadOnlyUsers")]
        public async Task<IActionResult> GetAll([FromQuery] string sortBy)
        {
            var result = await this.comicBookLayer.GetAllComcisAndSerieInfo().ConfigureAwait(false);

            if (result == null)
            {
                return this.NoContent();
            }

            return this.Ok(result.AsQueryable().Sort(sortBy));
        }

        /// <summary>API endpoint use to retrieve all comic books info.</summary>
        /// <param name="page">Page to return.</param>
        /// <param name="size">Size of the page.</param>
        /// <param name="sortBy">String defini by which field the list will be sort.</param>
        /// <returns>Return paginated comic book sorted.</returns>
        [HttpGet("{page}/{size}")]
        [Authorize(Policy = "ReadOnlyUsers")]
        public async Task<IActionResult> GetAllAsync(int page, int size, [FromQuery] string sortBy)
        {
            var result = await this.comicBookLayer.GetAllComcisAndSerieInfo().ConfigureAwait(false);

            if (result == null)
            {
                return this.NoContent();
            }

            var sortedList = result.AsQueryable().Sort(sortBy);
            return this.Ok(BuildPagedResponse(sortedList, page, size));
        }

        /// <summary>API endpoint use to retrieve a comic book info.</summary>
        /// <param name="id">Comic book id to retrieve.</param>
        /// <returns>Return a comic book corresponding to the id.</returns>
        [HttpGet("{id}")]
        [Authorize(Policy = "ReadOnlyUsers")]
        public async Task<IActionResult> GetAsync([FromQuery] int id)
        {
            var result = await this.comicBookLayer.GetAsync(id).ConfigureAwait(false);

            return result == null ? (IActionResult)this.NoContent() : this.Ok(result);
        }

        /// <summary>API endpoint use to retrieve comic book info by its name.</summary>
        /// <param name="name">Comic book name.</param>
        /// <returns>Return a comic book corresponding to the name.</returns>
        [HttpGet("{name}")]
        [Authorize(Policy = "ReadOnlyUsers")]
        public IActionResult GetByName([FromQuery] string name)
        {
            var result = this.comicBookLayer.Where(x => x.Title.Contains(name)).FirstOrDefault();

            return result == null ? (IActionResult)this.NoContent() : this.Ok(result);
        }

        /// <summary>API endpoint use to retrieve all comic books info.</summary>
        /// <param name="status">Status of list to return.</param>
        /// <param name="page">Page to return.</param>
        /// <param name="size">Size of the page.</param>
        /// <param name="sortBy">String defini by which field the list will be sort.</param>
        /// <returns>Return paginated comic book sorted corresponding to status.</returns>
        [HttpGet("type/{status}/{page}/{size}")]
        [Authorize(Policy = "ReadOnlyUsers")]
        public async Task<IActionResult> GetByStatusAsync([FromRoute] ComicBookStatus status, [FromRoute] int page, [FromRoute] int size, [FromQuery] string sortBy)
            => await this.GetByStatusAsync(status, page, size, sortBy, string.Empty).ConfigureAwait(false);

        /// <summary>API endpoint use to retrieve all comic books info.</summary>
        /// <param name="status">Status of list to return.</param>
        /// <param name="page">Page to return.</param>
        /// <param name="size">Size of the page.</param>
        /// <param name="sortBy">String defini by which field the list will be sort.</param>
        /// <param name="filter">Filter to search in list of comics.</param>
        /// <returns>Return paginated comic book sorted corresponding to status and filter.</returns>
        [HttpGet("type/{status}/{page}/{size}/{filter}")]
        [Authorize(Policy = "ReadOnlyUsers")]
        public async Task<IActionResult> GetByStatusAsync(
            [FromRoute] ComicBookStatus status, [FromRoute] int page, [FromRoute] int size, [FromQuery] string sortBy, [FromRoute] string filter)
        {
            var result = (await this.comicBookLayer.GetAllComcisAndSerieInfo().ConfigureAwait(false)).Where(book => book.Status == status).ToList();

            if (!result.Any())
            {
                return this.NoContent();
            }

            if (!string.IsNullOrEmpty(filter))
            {
                result = result.Where(book => book.Title.Contains(filter, StringComparison.InvariantCultureIgnoreCase) ||
                                                    book.ComicSerie.Name.Contains(filter, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }

            if (!result.Any())
            {
                return this.NoContent();
            }

            var sortedList = result.AsQueryable().Sort(sortBy);
            return this.Ok(BuildPagedResponse(sortedList, page, size));
        }

        /// <summary>API endpoint to remove a comic book by its Id.</summary>
        /// <param name="id">Id of the comic book to remove.</param>
        /// <returns>Return the count of comic removed.</returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = "Administrators")]
        public async Task<IActionResult> RemoveAsync([FromRoute] int id)
        {
            var comicBook = await this.comicBookLayer.GetAsync(id);

            if (comicBook == null)
            {
                return this.NoContent();
            }

            var result = await this.comicBookLayer.RemoveAsync(comicBook);
            return this.Ok(result);
        }

        /// <summary>API endpoint use to update an existing comic book.</summary>
        /// <param name="model">Comic book model to create.</param>
        /// <returns>Return the count of comic updated.</returns>
        [HttpPut]
        [Authorize(Policy = "WriteUsers")]
        public async Task<IActionResult> UpdateAsync([FromBody] ComicBook model)
        {
            var tagList = new List<ObjectTag>(model.Tags);

            // Empty Navigation property which should not be send while updating entity
            model.ComicSerie = null;

            // Check if comic book series exists, in order to avoid Access Layer error
            if (!model.ComicSerieId.HasValue || !this.comicSerieLayer.Exists(model.ComicSerieId.Value))
            {
                model.ComicSerieId = null;
            }

            // Update comic changes
            await this.comicBookLayer.UpdateAsync(model);

            // Handle related tags, so that we can update relation if tag have been added or removed
            // First we get model object from db, we clear all related tags
            // related tags or then rebuild from what the client sent
            var comicBookUpdated = await this.comicBookLayer.GetAsync(model.Id);
            comicBookUpdated.RelatedTags.Clear();

            foreach (var linkedTag in tagList)
            {
                comicBookUpdated.RelatedTags.Add(
                    new ComicBookTags
                    {
                        Tag = await this.objectTagLayer.GetAsync(linkedTag.Id),
                    });
            }

            var result = await this.comicBookLayer.UpdateAsync(comicBookUpdated);

            return this.Ok(result);
        }

        private static PageResponseData<ComicBook> BuildPagedResponse(IQueryable<ComicBook> sortedList, int page, int size)
        {
            var response = new PageResponseData<ComicBook>
            {
                TotalResult = sortedList.Count(),
                MaxPage = (int)Math.Ceiling(sortedList.Count() / (decimal)size),
                Result = sortedList.Skip((page - 1) * size).Take(size),
            };
            return response;
        }
    }
}
