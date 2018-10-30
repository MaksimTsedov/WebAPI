namespace BusinessLogic_BookAPI.Services
{
    using System.Collections.Generic;
    using BusinessLogic_BookAPI.Models;

    /// <summary>
    /// Authors abstraction
    /// </summary>
    public interface IAuthorService
    {  

        /// <summary>
        /// Gets the author.
        /// </summary>
        /// <param name="id">The identifier of author.</param>
        /// <returns>Certain author.</returns>
        Author GetAuthor(long id);

        /// <summary>
        /// Creates the author.
        /// </summary>
        /// <param name="author">The author.</param>
        /// <returns>Created author.</returns>
        Author CreateAuthor(Author author);

        /// <summary>
        /// Updates information of chozen author.
        /// </summary>
        /// <param name="id">The identifier of author.</param>
        /// <param name="author">The author.</param>
        /// <returns>Updated writer</returns>
        Author UpdateAuthor(long id, Author author);

        /// <summary>
        /// Deletes writer by his id.
        /// </summary>
        /// <param name="id">The identifier of author.</param>
        void DeleteAuthor(long id);
    }
}
