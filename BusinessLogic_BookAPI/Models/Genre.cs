namespace BusinessLogic_BookAPI.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Genre entity
    /// </summary>
    public class Genre
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Id is always required!")]
        [Range(1, int.MaxValue, ErrorMessage = "Id should be natural number!")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the naming.
        /// </summary>
        /// <value>
        /// The naming.
        /// </value>
        [Required(ErrorMessage = "Genre should have his name!")]
        [StringLength(50)]
        [MinLength(2)]
        public string Naming { get; set; }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="genre">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object genre)
        {
            Genre genreToCompare = genre as Genre;
            if (this.Naming == genreToCompare.Naming)
            {
                return true;
            }

            return false;
        }
    }
}
