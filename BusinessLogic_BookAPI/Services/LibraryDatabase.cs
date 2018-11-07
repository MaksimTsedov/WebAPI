namespace BusinessLogic_BookAPI.Services
{
    using BusinessLogic_BookAPI.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Database context
    /// </summary>
    public class LibraryDatabase : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryDatabase"/> class.
        /// </summary>
        /// <param name="options">Options for connecting to db.</param>
        public LibraryDatabase(DbContextOptions<LibraryDatabase> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets books.
        /// </summary>
        /// <value>
        /// Books.
        /// </value>
        public DbSet<Books> Books { get; set; }

        /// <summary>
        /// Gets or sets authors.
        /// </summary>
        /// <value>
        /// Authors.
        /// </value>
        public DbSet<Author> Authors { get; set; }

        /// <summary>
        /// Gets or sets genres.
        /// </summary>
        /// <value>
        /// Genres.
        /// </value>
        public DbSet<Genre> Genres { get; set; }

        /// <summary>
        /// Gets or sets the book-author pairs.
        /// </summary>
        /// <value>
        /// The book-author pairs.
        /// </value>
        public DbSet<BookAuthorPair> BookAuthorPairs { get; set; }

        /// <summary>
        /// Gets or sets the book-genre pairs.
        /// </summary>
        /// <value>
        /// The book-genre pairs.
        /// </value>
        public DbSet<BookGenrePair> BookGenrePairs { get; set; }

        // Connect to an existing database
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Initial Catalog=LibraryData;Integrated Security=True");
            }
        }
    }
}
