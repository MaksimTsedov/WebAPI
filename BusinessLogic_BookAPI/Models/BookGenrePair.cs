namespace BusinessLogic_BookAPI.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Class for Book-Genre pair
    /// </summary>
    public class BookGenrePair : IEquatable<BookGenrePair>
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
        /// Changes genre for this instance of Book-Genre pair.
        /// </summary>
        /// <param name="book">New genre id.</param>
        public void ChangeGenre(long genre_id)
        {
            this.Genre_Id = genre_id;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, used in a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Book_Id.GetHashCode() + Genre_Id.GetHashCode();
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
            return obj is BookGenrePair && Equals((BookGenrePair)obj);
        }

        /// <summary>
        /// Checks for equality
        /// </summary>
        /// <param name="bookAuthorPair">The book genre pair.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="BookGenrePair" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(BookGenrePair bookGenrePair)
        {
            return this.Book_Id == bookGenrePair.Book_Id && this.Genre_Id == bookGenrePair.Genre_Id;
        }
    }
}