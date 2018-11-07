namespace BusinessLogic_BookAPI.Services
{
    using System.Collections.Generic;
    using BusinessLogic_BookAPI.Models;

    /// <summary>
    /// Abstraction of book service to solve the problem of DI
    /// </summary>
    public interface IBookShelf
    {
        /// <summary>
        /// Gets the book by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Chozen book.</returns>
        Books GetBook(long id);

        /// <summary>
        /// Creates the book.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns>Created book.</returns>
        Books CreateBook(Books book);

        /// <summary>
        /// Updates a book by its id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="book">The book.</param>
        /// <returns>Updated book.</returns>
        Books UpdateBook(long id, Books book);

        /// <summary>
        /// Deletes a book by its id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteBook(long id);
    }
}
