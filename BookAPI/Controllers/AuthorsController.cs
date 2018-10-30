namespace BookAPI.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using BusinessLogic_BookAPI.Services;
    using BusinessLogic_BookAPI.Models;
    using Microsoft.AspNetCore.Mvc;
    using System;

    /// <summary>
    /// Controller for realization of CRUD operations about authors
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        /// <summary>
        /// The interface reference for DI inverse
        /// </summary>
        private readonly ILibraryService _authors;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorsController"/> class.
        /// </summary>
        /// <param name="authors">The instance of authors.</param>
        public AuthorsController(ILibraryService authors)
        {
            _authors = authors;
        }

        /// <summary>
        /// Creates and adds the author.
        /// </summary>
        /// <param name="author">The writer.</param>
        /// <returns>HTTP result of operation execution.</returns>
        [HttpPost]
        public IActionResult AddAuthor(Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid author information input.");
            }

            _authors.CreateAuthor(author);
            return Created("authors", author);
        }

        /// <summary>
        /// Updates the author.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="item">The author.</param>
        /// <returns>HTTP result of operation execution.</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(long id, Author item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            Author updatedAuthor = _authors.UpdateAuthor(id, item);
            if (updatedAuthor == null)
            {
                return NotFound("No author with such id!");
            }

            return Ok(updatedAuthor);
        }

        /// <summary>
        /// Deletes the author.
        /// </summary>
        /// <param name="id">The identifier of a author.</param>
        /// <returns>HTTP result of operation execution.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid author id");
            }

            try
            {
                _authors.DeleteAuthor(id);
            }
            catch (ArgumentNullException)
            {
                return NotFound("No author with such Id!");
            }

            return Ok();
        }

        /// <summary>
        /// Gets all authors.
        /// </summary>
        /// <returns>HTTP result of operation execution.</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Author> authorlist = _authors.GetAllAuthors().ToList();
            if (authorlist.Count == 0)
            {
                return NotFound("Any writer are not recorded!");
            }

            return Ok(authorlist);
        }

        /// <summary>
        /// Gets all books of the author by identifier.
        /// </summary>
        /// <param name="id">The identifier of author.</param>
        /// <returns>HTTP result of operation execution.</returns>
        [HttpGet("{id}/books")]
        public IActionResult GetBooksOfAuthor(long id)
        {
            List<Book> books = _authors.GetAuthorBooks(id).ToList();
            if (books.Count == 0)
            {
                return NotFound("No books written by that author!");
            }

            return Ok(books);
        }

        /// <summary>
        /// Gets a author the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>HTTP result of operation execution.</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            Author authorToFind = _authors.GetAuthor(id);
            if (authorToFind == null)
            {
                return NotFound("No author with such id!");
            }

            return Ok(authorToFind);
        }
    }
}
