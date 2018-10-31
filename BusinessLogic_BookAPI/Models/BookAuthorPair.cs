namespace BusinessLogic_BookAPI.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Class for Book-Author pair
    /// </summary>
    public class BookAuthorPair : IComparable<BookAuthorPair>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookAuthorPair"/> class.
        /// </summary>
        /// <param name="book_id">The book identifier.</param>
        /// <param name="author_id">The author identifier.</param>
        public BookAuthorPair(long book_id, long author_id)
        {
            this.Book_Id = book_id;
            this.Author_Id = author_id;
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
        /// Gets the author identifier.
        /// </summary>
        /// <value>
        /// The author identifier.
        /// </value>
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Id should be natural number!")]
        public long Author_Id { get; private set; }

        /// <summary>
        /// Compares this instance to Book-Author pair parameter.
        /// </summary>
        /// <param name="bookAuthorPair">Book-Author pair.</param>
        /// <returns>Integer of comparison result.</returns>
        public int CompareTo(BookAuthorPair bookAuthorPair)
        {
            if (this.Book_Id > bookAuthorPair.Book_Id)
            {
                return 1;
            }
            else if (this.Book_Id < bookAuthorPair.Book_Id)
            {
                return -1;
            }
            else if (this.Author_Id > bookAuthorPair.Author_Id)
            {
                return 1;
            }
            else if (this.Author_Id < bookAuthorPair.Author_Id)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}