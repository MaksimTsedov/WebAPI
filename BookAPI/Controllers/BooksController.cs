namespace BookAPI.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using BusinessLogic_BookAPI.Services;
    using BusinessLogic_BookAPI.Models;
    using Microsoft.AspNetCore.Mvc;
    using System;

    /// <summary>
    /// Controller for realization of CRUD operations about books
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        /// <summary>
        /// The interface reference of books for DI inverse
        /// </summary>
        private readonly IBookShelf _books;

        /// <summary>
        /// The interface reference of library for DI inverse
        /// </summary>
        private readonly ILibraryService _libraryService;

        /// <summary>
        /// The interface reference of library for DI inverse
        /// </summary>
        private readonly ILibraryPairCreationManager _pairManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="BooksController"/> class.
        /// </summary>
        /// <param name="books">The instance of books.</param>
        public BooksController(IBookShelf books, ILibraryService library, ILibraryPairCreationManager pair)
        {
            _books = books;
            _libraryService = library;
            _pairManager = pair;
        }

        /// <summary>
        /// Creates and adds the book.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns>HTTP result of operation execution.</returns>
        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid book input.");
            }

            _books.CreateBook(book);
            return Created("books", book);
        }

        /// <summary>
        /// Adds the book author.
        /// </summary>
        /// <param name="book_id">The book identifier.</param>
        /// <param name="author_id">The author identifier.</param>
        /// <returns>HTTP result of operation execution.</returns>
        [HttpPost("{book_id}/authors/{author_id}")]
        public IActionResult AddBookAuthor(long book_id, long author_id)
        {
            try
            {
                if (_pairManager.AddAuthorOfBook(book_id, author_id))
                {
                    return Ok("Added author to book");
                }
            }
            catch (ArgumentException ex)
            {

                return Conflict(ex.Message);
            }

            return NotFound("No book or/and author with such ids found!");
        }

        /// <summary>
        /// Adds genre to the book.
        /// </summary>
        /// <param name="book_id">The book identifier.</param>
        /// <param name="genre_id">The genre identifier.</param>
        /// <returns>HTTP result of operation execution.</returns>
        [HttpPost("{book_id}/genres/{genre_id}")]
        public IActionResult AddBookGenre(long book_id, long genre_id)
        {
            try
            {
                if (_pairManager.AddGenreToBook(book_id, genre_id))
                {
                    return Ok("Added book genre");
                }
            }
            catch (ArgumentException ex)
            {

                return Conflict(ex.Message);
            }

            return NotFound("No book or/and genre with such ids found!");
        }

        /// <summary>
        /// Updates the book.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="item">The book.</param>
        /// <returns>HTTP result of operation execution.</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateBook(long id, Book item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            Book updatedBook = _books.UpdateBook(id, item);
            if (updatedBook == null)
            {
                return NotFound("No book with such id!");
            }

            return Ok(updatedBook);
        }

        /// <summary>
        /// Changes the book author.
        /// </summary>
        /// <param name="book_id">The book identifier.</param>
        /// <param name="author_id">The author identifier.</param>
        /// <param name="newAuthor_id">The identifier of a new author .</param>
        /// <returns>HTTP result of operation execution.</returns>
        [HttpPut("{book_id}/authors/{author_id}")]
        public IActionResult ChangeBookAuthor(long book_id, long author_id, long newAuthor_id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            try
            {
                if (!_pairManager.UpdateAuthorOfBook(book_id, author_id, newAuthor_id))
                {
                    return NotFound("There is no book or/and author with such id!");
                }
            }
            catch (ArgumentException ex)
            {

                return Conflict(ex.Message);
            }
            
            return Ok();
        }

        /// <summary>
        /// Changes the book genre.
        /// </summary>
        /// <param name="book_id">The book identifier.</param>
        /// <param name="author_id">The genre identifier.</param>
        /// <param name="newAuthor_id">The identifier of a new genre .</param>
        /// <returns>HTTP result of operation execution.</returns>
        [HttpPut("{book_id}/genres/{genre_id}")]
        public IActionResult ChangeBookGenre(long book_id, long genre_id, long newGenre_id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            try
            {
                if (!_pairManager.UpdateGenreOfBook(book_id, genre_id, newGenre_id))
                {
                    return NotFound("There is no book or/and genre with such id!");
                }
            }
            catch (ArgumentException ex)
            {

                return Conflict(ex.Message);
            }

            return Ok();
        }

        /// <summary>
        /// Deletes the book author.
        /// </summary>
        /// <param name="book_id">The book identifier.</param>
        /// <param name="author_id">The author identifier.</param>
        /// <returns>HTTP result of operation execution.</returns>
        [HttpDelete("{book_id}/authors/{author_id}")]
        public IActionResult DeleteBookAuthor(long book_id, long author_id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            if (!_pairManager.RemoveAuthorOfBook(book_id, author_id))
            {
                return NotFound("No pair such Id!");
            }

            return Ok();
        }

        /// <summary>
        /// Deletes the book genre.
        /// </summary>
        /// <param name="book_id">The book identifier.</param>
        /// <param name="author_id">The genre identifier.</param>
        /// <returns>HTTP result of operation execution.</returns>
        [HttpDelete("{book_id}/genres/{genre_id}")]
        public IActionResult DeleteBookGenre(long book_id, long genre_id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            if (!_pairManager.DeleteGenreOfBook(book_id, genre_id))
            {
                return NotFound("No pair such Id!");
            }

            return Ok();
        }

        /// <summary>
        /// Deletes the book.
        /// </summary>
        /// <param name="id">The identifier of a book.</param>
        /// <returns>HTTP result of operation execution.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid book id");
            }

            try
            {
                _books.DeleteBook(id);
            }
            catch (ArgumentNullException)
            {
                return NotFound("No book with such Id!");
            }

            return Ok();
        }

        /// <summary>
        /// Gets all books.
        /// </summary>
        /// <returns>HTTP result of operation execution.</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Book> booklist = _libraryService.GetAllBooks().ToList();
            if (booklist.Count == 0)
            {
                return NotFound("Any book are not recorded!");
            }

            return Ok(booklist);
        }

        /// <summary>
        /// Gets a book the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>HTTP result of operation execution.</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            Book bookToFind = _books.GetBook(id);
            if (bookToFind == null)
            {
                return NotFound("No book with such id!");
            }

            return Ok(bookToFind);
        }
    }
}
