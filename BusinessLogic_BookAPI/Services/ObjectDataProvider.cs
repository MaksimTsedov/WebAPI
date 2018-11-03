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
    public class ObjectDataProvider : IDataProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectDataProvider"/> class.
        /// </summary>
        public ObjectDataProvider()
        {
            this.Books = new List<Book>
            {
                new Book("451 fahrenheit", 158, 1953),
                new Book("1984", 328, 1949),
                new Book("Odyssey", 800, -800),
                new Book("Dandelion wine", 164, 1957),
                new Book("Folk tails", 160, 1890)
            };

            this.Authors = new List<Author>
            {
                new Author("Ray Bradbury", "USA"),
                new Author("George Orwell", "Great Britain"),
                new Author("Homer", "Ancient Greece")
            };

            this.Genres = new List<Genre>
            {
                new Genre("Dystopia"),
                new Genre("Fiction"),
                new Genre("Epos"),
                new Genre("Fairy tail")
            };

            this.BookAuthorPairs = new List<BookAuthorPair>
            {
                new BookAuthorPair(1, 1),
                new BookAuthorPair(2, 2),
                new BookAuthorPair(3, 3),
                new BookAuthorPair(4, 1)
            };

            this.BookGenrePairs = new List<BookGenrePair>
            {
                new BookGenrePair(1, 1),
                new BookGenrePair(1, 2),
                new BookGenrePair(2, 1),
                new BookGenrePair(3, 3),
                new BookGenrePair(5, 4)
            };
        }

        /// <summary>
        /// Gets or sets book enumeration.
        /// </summary>
        public IList<Book> Books { get; set; }

        /// <summary>
        /// Gets or sets the author enumeration.
        /// </summary>
        public IList<Author> Authors { get; set; }

        /// <summary>
        /// Gets or sets the genre enumeration.
        /// </summary>
        public IList<Genre> Genres { get; set; }

        /// <summary>
        /// Gets or sets links between books and authors.
        /// </summary>
        public IList<BookAuthorPair> BookAuthorPairs { get; set; }

        /// <summary>
        /// Gets or sets links between books and genres.
        /// </summary>
        public IList<BookGenrePair> BookGenrePairs { get; set; }
    }
}
