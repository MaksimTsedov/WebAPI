namespace BusinessLogic_BookAPI.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Class for Book-Author pair
    /// </summary>
    public class BookAuthorPair
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
        /// Changes author for this instance of Book-Author pair.
        /// </summary>
        /// <param name="book">New author id.</param>
        public void ChangeAuthor(long author_id)
        {
            this.Author_Id = author_id;
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