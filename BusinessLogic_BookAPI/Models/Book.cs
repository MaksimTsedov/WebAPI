namespace BusinessLogic_BookAPI.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Book entity
    /// </summary>
    public class Book
    {
        /// <summary>
        /// The global count for id autoincrement
        /// </summary>
        private static int _globalCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="Book"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="author">The author.</param>
        /// <param name="numberOfPages">The number of pages.</param>
        /// <param name="year">The year.</param>
        public Book(string title, int numberOfPages, int year)
        {
            this.Title = title;
            this.NumberOfPages = numberOfPages;
            this.Year = year;
            this.Id = ++_globalCount;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Required(ErrorMessage = "Id is always required!")]
        [Range(1, long.MaxValue, ErrorMessage = "Id should be natural number!")]
        public long Id { get; private set; }

        /// <summary>
        /// Gets or sets the title of a book.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [Required(ErrorMessage = "Every book has its own naming!")]
        [StringLength(100)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the number of pages.
        /// </summary>
        /// <value>
        /// The number of pages.
        /// </value>
        [Range(1, 2500, ErrorMessage = "Book number of pages should be natural and be less 2500")]
        public int NumberOfPages { get; set; }

        /// <summary>
        /// Gets or sets the year of publishing.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        [Range(-2000, 2018, ErrorMessage = "Wrong year parameter (never heard about books from that time)!")]
        public int Year { get; set; }

        /// <summary>
        /// Clones the book information.
        /// </summary>
        /// <param name="book">The book to clone.</param>
        public void Clone(Book book)
        {
            this.Title = book.Title;
            this.NumberOfPages = book.NumberOfPages;
            this.Year = book.Year;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="book">The <see cref="Book" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="Book" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object book)
        {
            Book newbook = book as Book;
            if (this.Title == newbook.Title
             && this.NumberOfPages == newbook.NumberOfPages
             && this.Year == newbook.Year)
            {
                return true;
            }

            return false;
        }
    }
}