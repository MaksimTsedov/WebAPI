namespace BusinessLogic_BookAPI.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Class for Book-Author pair
    /// </summary>
    public class BookAuthorPair : IEquatable<BookAuthorPair>
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
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, used in a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Book_Id.GetHashCode() + Author_Id.GetHashCode();
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return obj is BookAuthorPair && Equals((BookAuthorPair)obj);
        }

        /// <summary>
        /// Checks for equality
        /// </summary>
        /// <param name="bookAuthorPair">The book author pair.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="BookAuthorPair" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(BookAuthorPair bookAuthorPair)
        {
            return this.Book_Id == bookAuthorPair.Book_Id && this.Author_Id == bookAuthorPair.Author_Id;
        }
    }
}