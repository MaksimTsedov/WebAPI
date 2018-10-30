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
        /// Sets book enumeration.
        /// </summary>
        /// <returns>Setted enumeration of books</returns>
        IEnumerable<Book> SetBooks();

        /// <summary>
        /// Sets the author enumeration.
        /// </summary>
        /// <returns>Setted enumeration of authors</returns>
        IEnumerable<Author> SetAuthors();

        /// <summary>
        /// Sets the genre enumeration.
        /// </summary>
        /// <returns>Setted enumeration of genres</returns>
        IEnumerable<Genre> SetGenres();

        /// <summary>
        /// Sets links between books and authors.
        /// </summary>
        /// <returns>Setted enumeration of Book-Author pair</returns>
        IEnumerable<BookAuthorPair> SetBooksAuthors();

        /// <summary>
        /// Sets links between books and genres.
        /// </summary>
        /// <returns>Setted enumeration of Book-Genre pair</returns>
        IEnumerable<BookGenrePair> SetBooksGenres();
    }
}
