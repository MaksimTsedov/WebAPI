namespace BusinessLogic_BookAPI.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Class for Book-Genre pair
    /// </summary>
    public class BookGenrePair : IEquatable<BookGenrePair>
    {
        /// <summary>
        /// Gets pair identifier.
        /// </summary>
        /// <value>
        /// The pair identifier.
        /// </value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Id should be natural number!")]
        public int Id { get; set; }

        /// <summary>
        /// Gets the book identifier.
        /// </summary>
        /// <value>
        /// The book identifier.
        /// </value>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Id should be natural number!")]
        public int Book_Id { get; set; }

        /// <summary>
        /// Gets the genre identifier.
        /// </summary>
        /// <value>
        /// The genre identifier.
        /// </value>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Id should be natural number!")]
        public int Genre_Id { get; set; }

        /// <summary>
        /// Changes genre for this instance of Book-Genre pair.
        /// </summary>
        /// <param name="book">New genre id.</param>
        public void ChangeGenre(int genre_id)
        {
            this.Genre_Id = genre_id;
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