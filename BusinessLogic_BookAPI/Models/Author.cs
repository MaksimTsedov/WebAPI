namespace BusinessLogic_BookAPI.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Author entity
    /// </summary>
    public class Author
    {
        /// <summary>
        /// The global count for id autoincrement
        /// </summary>
        private static int _globalCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="Author"/> class.
        /// </summary>
        /// <param name="fullname">The fullname of author.</param>
        /// <param name="country">The country.</param>
        public Author(string fullname, string country)
        {
            this.FullName = fullname;
            this.Country = country;
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
        /// Gets or sets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        [Required(ErrorMessage = "Author should have his name or alias!")]
        [StringLength(200)]
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the native country of author.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        [StringLength(100)]
        public string Country { get; set; }

        /// <summary>
        /// Clones writers information.
        /// </summary>
        /// <param name="author">The author information to clone.</param>
        public void Clone(Author author)
        {
            this.FullName = author.FullName;
            this.Country = author.Country;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="author">The <see cref="Author" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="Author" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object author)
        {
            Author authorToCompare = author as Author;
            if (this.FullName == authorToCompare.FullName
             && this.Country == authorToCompare.Country)
            {
                return true;
            }

            return false;
        }
    }
}
