namespace BusinessLogic_BookAPI.Services
{
    /// <summary>
    /// Interface for adding library pairs for resolving problem of M:M connection
    /// </summary>
    public interface ILibraryPairCreationManager
    {
        /// <summary>
        /// Adds the genre to book.
        /// </summary>
        /// <param name="book_id">The book identifier.</param>
        /// <param name="genre_id">The genre identifier.</param>
        /// <returns>Is added new link.</returns>
        bool AddGenreToBook(long book_id, long genre_id);

        /// <summary>
        /// Adds the author of book.
        /// </summary>
        /// <param name="book_id">The book identifier.</param>
        /// <param name="author_id">The author identifier.</param>
        /// <returns>Is added new link.</returns>
        bool AddAuthorOfBook(long book_id, long author_id);
    }
}