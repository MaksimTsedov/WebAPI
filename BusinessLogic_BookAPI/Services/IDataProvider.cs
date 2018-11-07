namespace BusinessLogic_BookAPI.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BusinessLogic_BookAPI.Models;

    /// <summary>
    /// Abstraction of data
    /// </summary>
    public interface IDataProvider
    {
        /// <summary>
        /// Gets or sets book enumeration.
        /// </summary>
        IList<Books> Books { get; set; }

        /// <summary>
        /// Gets or sets the author enumeration.
        /// </summary>
        IList<Author> Authors { get; set; }

        /// <summary>
        /// Gets or sets the genre enumeration.
        /// </summary>
        IList<Genre> Genres { get; set; }

        /// <summary>
        /// Gets or sets links between books and authors.
        /// </summary>
        IList<BookAuthorPair> BookAuthorPairs { get; set; }

        /// <summary>
        /// Gets or sets links between books and genres.
        /// </summary>
        IList<BookGenrePair> BookGenrePairs { get; set; }
    }
}
