﻿namespace BusinessLogic_BookAPI.Services
{
    using BusinessLogic_BookAPI.Models;

    /// <summary>
    /// Abstraction of genre service
    /// </summary>
    public interface IGenreService
    {
        /// <summary>
        /// Creates the genre.
        /// </summary>
        /// <param name="genre">The genre.</param>
        /// <returns>Created genre.</returns>
        Genre CreateGenre(Genre genre);

        /// <summary>
        /// Gets the genre.
        /// </summary>
        /// <param name="id">The identifier of genre.</param>
        /// <returns>Genre.</returns>
        Genre GetGenre(long id);

        /// <summary>
        /// Deletes a genre by its id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteGenre(long id);
    }
}
