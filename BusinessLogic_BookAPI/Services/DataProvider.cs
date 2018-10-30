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
        /// Sets the author enumeration.
        /// </summary>
        /// <returns>
        /// Setted enumeration of authors
        /// </returns>
        public IEnumerable<Author> SetAuthors()
        {
            IEnumerable<Author> authors = new List<Author>
            {
                new Author("Ray Bradbury", "USA"),
                new Author("George Orwell", "Great Britain"),
                new Author("Homer", "Ancient Greece")
            };
            foreach (var author in authors)
            {
                yield return author;
            }
        }

        /// <summary>
        /// Sets book enumeration.
        /// </summary>
        /// <returns>
        /// Setted enumeration of books
        /// </returns>
        public IEnumerable<Book> SetBooks()
        {
            IEnumerable<Book> books = new List<Book>
            {
                new Book("451 fahrenheit", 158, 1953),
                new Book("1984", 328, 1949),
                new Book("Odyssey", 800, -800),
                new Book("Dandelion wine", 164, 1957),
                new Book("Folk tails", 160, 1890)
            };
            foreach (var book in books)
            {
                yield return book;
            }
        }

        /// <summary>
        /// Sets links between books and authors.
        /// </summary>
        /// <returns>
        /// Setted enumeration of Book-Author pair
        /// </returns>
        public IEnumerable<BookAuthorPair> SetBooksAuthors()
        {

            IEnumerable<BookAuthorPair> bookAuthorPairs = new List<BookAuthorPair>
            {
                new BookAuthorPair(1, 1),
                new BookAuthorPair(2, 2),
                new BookAuthorPair(3, 3),
                new BookAuthorPair(4, 1)
            };
            foreach (var pair in bookAuthorPairs)
            {
                yield return pair;
            }
        }

        /// <summary>
        /// Sets links between books and genres.
        /// </summary>
        /// <returns>
        /// Setted enumeration of Book-Genre pair
        /// </returns>
        public IEnumerable<BookGenrePair> SetBooksGenres()
        {
            IEnumerable<BookGenrePair> bookGenrePairs = new SortedSet<BookGenrePair>
            {
                new BookGenrePair(1, 1),
                new BookGenrePair(1, 2),
                new BookGenrePair(2, 1),
                new BookGenrePair(3, 3),
                new BookGenrePair(5, 4)
            };
            foreach (var pair in bookGenrePairs)
            {
                yield return pair;
            }
        }

        /// <summary>
        /// Sets the genre enumeration.
        /// </summary>
        /// <returns>
        /// Setted enumeration of genres
        /// </returns>
        public IEnumerable<Genre> SetGenres()
        {
            IEnumerable<Genre> genres = new List<Genre>
            {
                new Genre("Dystopia"),
                new Genre("Fiction"),
                new Genre("Epos"),
                new Genre("Fairy tail")
            };
            foreach (var genre in genres)
            {
                yield return genre;
            }
        }
    }
}
