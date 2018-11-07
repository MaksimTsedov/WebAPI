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
            this.Books = new List<Books>
            {
                new Books(){ Title = "451 fahrenheit", NumberOfPages = 158, Year = 1953 },
                new Books(){ Title = "1984", NumberOfPages = 328, Year = 1949 },
                new Books(){ Title = "Odyssey", NumberOfPages = 800, Year = -800 },
                new Books(){ Title = "Dandelion wine", NumberOfPages = 164,Year = 1957 },
                new Books(){ Title = "Folk tails", NumberOfPages = 160,Year = 1890 }
            };

            this.Authors = new List<Author>
            {
                new Author(){ FullName = "Ray Bradbury", Country = "USA" },
                new Author(){ FullName = "George Orwell", Country = "Great Britain" },
                new Author(){ FullName = "Homer", Country = "Ancient Greece" }
            };

            this.Genres = new List<Genre>
            {
                new Genre(){ Naming = "Dystopia" },
                new Genre(){ Naming = "Fiction" },
                new Genre(){ Naming = "Epos" },
                new Genre(){ Naming = "Fairy tail" }
            };

            this.BookAuthorPairs = new List<BookAuthorPair>
            {
                new BookAuthorPair(){ Book_Id = 1, Author_Id = 1 },
                new BookAuthorPair(){ Book_Id = 2, Author_Id = 2 },
                new BookAuthorPair(){ Book_Id = 3, Author_Id = 3 },
                new BookAuthorPair(){ Book_Id = 4, Author_Id = 1 }
            };

            this.BookGenrePairs = new List<BookGenrePair>
            {
                new BookGenrePair(){ Book_Id = 1, Genre_Id = 1 },
                new BookGenrePair(){ Book_Id = 1, Genre_Id = 2 },
                new BookGenrePair(){ Book_Id = 2, Genre_Id = 1 },
                new BookGenrePair(){ Book_Id = 3, Genre_Id = 3 },
                new BookGenrePair(){ Book_Id = 5, Genre_Id = 4 }
            };
        }

        /// <summary>
        /// Gets or sets book enumeration.
        /// </summary>
        public IList<Books> Books { get; set; }

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
