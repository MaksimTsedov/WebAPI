namespace BusinessLogic_BookAPI.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Class for Book-Genre pair
    /// </summary>
    public class BookGenrePair : IComparable<BookGenrePair>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookGenrePair"/> class.
        /// </summary>
        /// <param name="book_id">The book identifier.</param>
        /// <param name="genre_id">The genre identifier.</param>
        public BookGenrePair(long book_id, long genre_id)
        {
            this.Book_Id = book_id;
            this.Genre_Id = genre_id;
        }

        /// <summary>
        /// Gets the book identifier.
        /// </summary>
        /// <value>
        /// The book identifier.
        /// </value>
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Id should be natural number!")]
        public long Book_Id { get; private set; }

        /// <summary>
        /// Gets the genre identifier.
        /// </summary>
        /// <value>
        /// The genre identifier.
        /// </value>
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Id should be natural number!")]
        public long Genre_Id { get; private set; }

        /// <summary>
        /// Compares this instance to Book-Genre pair parameter.
        /// </summary>
        /// <param name="bookGenrePair">Book-Genre pair.</param>
        /// <returns>Integer of comparison result.</returns>
        public int CompareTo(BookGenrePair bookGenrePair)
        {
            return Book_Id.CompareTo(bookGenrePair.Book_Id);
        }
    }
}