namespace BookAPI.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using BusinessLogic_BookAPI.Services;
    using BusinessLogic_BookAPI.Models;
    using Microsoft.AspNetCore.Mvc;
    using System;

    /// <summary>
    /// Controller for realization of CRUD operations about genres
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        /// <summary>
        /// The interface reference for DI inverse
        /// </summary>
        private readonly ILibraryService _genres;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenresController"/> class.
        /// </summary>
        /// <param name="genres">The instance of genres.</param>
        public GenresController(ILibraryService genres)
        {
            _genres = genres;
        }

        /// <summary>
        /// Creates and adds the genre.
        /// </summary>
        /// <param name="genre">The genre.</param>
        /// <returns>HTTP result of operation execution.</returns>
        [HttpPost]
        public IActionResult AddGenre(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid genre information input.");
            }

            _genres.CreateGenre(genre);
            return Created("genres", genre);
        }

        /// <summary>
        /// Deletes the genre.
        /// </summary>
        /// <param name="id">The identifier of a genre.</param>
        /// <returns>HTTP result of operation execution.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid genre id");
            }

            try
            {
                _genres.DeleteGenre(id);
            }
            catch (ArgumentNullException)
            {
                return NotFound("No genre with such Id!");
            }
            catch (FormatException)
            {
                return Conflict("Can`t delete genre while it has books of this genre");               
            }

            return Ok();
        }

        /// <summary>
        /// Gets all genre.
        /// </summary>
        /// <returns>HTTP result of operation execution.</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Genre> genreList = _genres.GetAllGenres().ToList();
            if (genreList.Count == 0)
            {
                return NotFound("Any genre are not recorded!");
            }

            return Ok(genreList);
        }

        /// <summary>
        /// Gets all books of the genre by identifier.
        /// </summary>
        /// <param name="id">The identifier of genre.</param>
        /// <returns>HTTP result of operation execution.</returns>
        [HttpGet("{id}/books")]
        public IActionResult GetBooksOfGenre(long id)
        {
            List<Book> books = _genres.GetAllGenreBooks(id).ToList();
            if (books.Count == 0)
            {
                return NotFound("There is no book of this genre!");
            }

            return Ok(books);
        }
    }
}
