namespace BusinessLogic_BookAPI.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Class for Book-Author pair
    /// </summary>
    public class BookAuthorPair
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
        /// Gets the author identifier.
        /// </summary>
        /// <value>
        /// The author identifier.
        /// </value>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Id should be natural number!")]
        public int Author_Id { get; set; }

        /// <summary>
        /// Changes author for this instance of Book-Author pair.
        /// </summary>
        /// <param name="book">New author id.</param>
        public void ChangeAuthor(int author_id)
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