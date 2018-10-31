namespace BusinessLogic_BookAPI.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BusinessLogic_BookAPI.Models;

    /// <summary>
    /// Data initialization
    /// </summary>
    /// <seealso cref="BusinessLogic_BookAPI.Services.IDataProvider" />
    public class DataProvider : IDataProvider
    {
        /// <summary>
        /// Gets the author enumeration.
        /// </summary>
        /// <returns>
        /// Enumeration of authors
        /// </returns>
        public IEnumerable<Author> GetAuthors()
        {
            IEnumerable<Author> authors = new List<Author>
            {
                new Author("Ray Bradbury", "USA"),
                new Author("George Orwell", "Great Britain"),
                new Author("Homer", "Ancient Greece")
            };

            return authors;
        }

        /// <summary>
        /// Gets book enumeration.
        /// </summary>
        /// <returns>
        /// Enumeration of books
        /// </returns>
        public IEnumerable<Book> GetBooks()
        {
            IEnumerable<Book> books = new List<Book>
            {
                new Book("451 fahrenheit", 158, 1953),
                new Book("1984", 328, 1949),
                new Book("Odyssey", 800, -800),
                new Book("Dandelion wine", 164, 1957),
                new Book("Folk tails", 160, 1890)
            };

            return books;
        }

        /// <summary>
        /// Gets links between books and authors.
        /// </summary>
        /// <returns>
        /// Enumeration of Book-Author pair
        /// </returns>
        public IEnumerable<BookAuthorPair> GetBooksAuthors()
        {

            IEnumerable<BookAuthorPair> bookAuthorPairs = new List<BookAuthorPair>
            {
                new BookAuthorPair(1, 1),
                new BookAuthorPair(2, 2),
                new BookAuthorPair(3, 3),
                new BookAuthorPair(4, 1)
            };

            return bookAuthorPairs;
        }

        /// <summary>
        /// Gets links between books and genres.
        /// </summary>
        /// <returns>
        /// Enumeration of Book-Genre pair
        /// </returns>
        public IEnumerable<BookGenrePair> GetBooksGenres()
        {
            IEnumerable<BookGenrePair> bookGenrePairs = new SortedSet<BookGenrePair>
            {
                new BookGenrePair(1, 1),
                new BookGenrePair(1, 2),
                new BookGenrePair(2, 1),
                new BookGenrePair(3, 3),
                new BookGenrePair(5, 4)
            };

            return bookGenrePairs;
        }

        /// <summary>
        /// Gets the genre enumeration.
        /// </summary>
        /// <returns>
        /// Enumeration of genres
        /// </returns>
        public IEnumerable<Genre> GetGenres()
        {
            IEnumerable<Genre> genres = new List<Genre>
            {
                new Genre("Dystopia"),
                new Genre("Fiction"),
                new Genre("Epos"),
                new Genre("Fairy tail")
            };

            return genres;
        }
    }
}
