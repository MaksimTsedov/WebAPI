namespace BusinessLogic_BookAPI.Services
{
    using System;
    using System.Collections.Generic;
    using BusinessLogic_BookAPI.Models;

    /// <summary>
    /// Abstraction of data
    /// </summary>
    public interface IDataProvider
    {
        /// <summary>
        /// Gets book enumeration.
        /// </summary>
        /// <returns>Enumeration of books</returns>
        IEnumerable<Book> GetBooks();

        /// <summary>
        /// Gets the author enumeration.
        /// </summary>
        /// <returns>Enumeration of authors</returns>
        IEnumerable<Author> GetAuthors();

        /// <summary>
        /// Gets the genre enumeration.
        /// </summary>
        /// <returns>Enumeration of genres</returns>
        IEnumerable<Genre> GetGenres();

        /// <summary>
        /// Gets links between books and authors.
        /// </summary>
        /// <returns>Enumeration of Book-Author pair</returns>
        IEnumerable<BookAuthorPair> GetBooksAuthors();

        /// <summary>
        /// Gets links between books and genres.
        /// </summary>
        /// <returns>Enumeration of Book-Genre pair</returns>
        IEnumerable<BookGenrePair> GetBooksGenres();
    }
}
