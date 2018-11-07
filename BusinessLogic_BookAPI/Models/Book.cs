namespace BusinessLogic_BookAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Book entity
    /// </summary>
    public class Books
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Required(ErrorMessage = "Id is always required!")]
        [Range(1, int.MaxValue, ErrorMessage = "Id should be natural number!")]
        public int Id { get; set; }

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
        [Required(ErrorMessage = "Book should have its number of pages!")]
        [Range(1, 2500, ErrorMessage = "Book number of pages should be natural and be less 2500")]
        public int NumberOfPages { get; set; }

        /// <summary>
        /// Gets or sets the year of publishing.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        [Range(-2000, 2018, ErrorMessage = "Wrong year parameter (never heard about books from that time)!")]
        public int? Year { get; set; }

        /// <summary>
        /// Clones the book information.
        /// </summary>
        /// <param name="book">The book to clone.</param>
        public void Clone(Books book)
        {
            this.Title = book.Title;
            this.NumberOfPages = book.NumberOfPages;
            this.Year = book.Year;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="book">The <see cref="Books" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="Books" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object book)
        {
            Books bookToCompare = book as Books;
            if (this.Title == bookToCompare.Title
             && this.NumberOfPages == bookToCompare.NumberOfPages
             && this.Year == bookToCompare.Year)
            {
                return true;
            }

            return false;
        }
    }
}