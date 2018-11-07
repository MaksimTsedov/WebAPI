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
        bool AddGenreToBook(int book_id, int genre_id);

        /// <summary>
        /// Adds the author of book.
        /// </summary>
        /// <param name="book_id">The book identifier.</param>
        /// <param name="author_id">The author identifier.</param>
        /// <returns>Is added new link.</returns>
        bool AddAuthorOfBook(int book_id, int author_id);

        /// <summary>
        /// Updates the genre of a book.
        /// </summary>
        /// <param name="book_id">The book identifier.</param>
        /// <param name="genre_id">The genre identifier.</param>
        /// <param name="newGenre_id">The identifier of a new genre.</param>
        /// <returns>Is updated.</returns>
        bool UpdateGenreOfBook(int book_id, int genre_id, int newGenre_id);

        /// <summary>
        /// Updates the author of book.
        /// </summary>
        /// <param name="book_id">The book identifier.</param>
        /// <param name="author_id">The author identifier.</param>
        /// <param name="newAuthor_id">The identifier of a new author.</param>
        /// <returns>Is updated.</returns>
        bool UpdateAuthorOfBook(int book_id, int author_id, int newAuthor_id);

        /// <summary>
        /// Deletes the genre of a book.
        /// </summary>
        /// <param name="book_id">The book identifier.</param>
        /// <param name="genre_id">The genre identifier.</param>
        /// <returns>Is deleted.</returns>
        bool DeleteGenreOfBook(long book_id, long genre_id);

        /// <summary>
        /// Deletes the author of book.
        /// </summary>
        /// <param name="book_id">The book identifier.</param>
        /// <param name="author_id">The author identifier.</param>
        /// <returns>Is deleted.</returns>
        bool RemoveAuthorOfBook(long book_id, long author_id);
    }
}