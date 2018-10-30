namespace BusinessLogic_BookAPI.Services
{
    using BusinessLogic_BookAPI.Models;
    using System.Collections.Generic;

    /// <summary>
    /// Library manager abstraction
    /// </summary>
    /// <seealso cref="BusinessLogic_BookAPI.Services.IAuthorService" />
    /// <seealso cref="BusinessLogic_BookAPI.Services.IBookShelf" />
    /// <seealso cref="BusinessLogic_BookAPI.Services.IGenreService" />  
    public interface ILibraryService : IAuthorService, IBookShelf, IGenreService
    {
        /// <summary>
        /// Adds the genre to book.
        /// </summary>
        /// <param name="book_id">The book identifier.</param>
        /// <param name="genre_id">The genre identifier.</param>
        /// <returns>Is added new link.</returns>
        bool AddGenreToBook(long book_id, long genre_id);

        /// <summary>
        /// Adds the author of book.
        /// </summary>
        /// <param name="book_id">The book identifier.</param>
        /// <param name="author_id">The author identifier.</param>
        /// <returns>Is added new link.</returns>
        bool AddAuthorOfBook(long book_id, long author_id);

        /// <summary>
        /// Gets list of authors.
        /// </summary>
        /// <returns>Enumeration of all authors.</returns>
        IEnumerable<Author> GetAllAuthors();

        /// <summary>
        /// Gets all books.
        /// </summary>
        /// <returns>Enumeration of all books</returns>
        IEnumerable<Book> GetAllBooks();

        /// <summary>
        /// Gets all genres.
        /// </summary>
        /// <returns>Enumeration of all genres</returns>
        IEnumerable<Genre> GetAllGenres();

        /// <summary>
        /// Gets the books written by chozen author.
        /// </summary>
        /// <param name="author_Id">The author identifier.</param>
        /// <returns>Enumeration of books.</returns>
        IEnumerable<Book> GetAuthorBooks(long author_Id);

        /// <summary>
        /// Gets all books of supposed genre.
        /// </summary>
        /// <param name="genre_Id">The genre identifier.</param>
        /// <returns>Enumeration of books.</returns>
        IEnumerable<Book> GetAllGenreBooks(long genre_Id);
    }
}
