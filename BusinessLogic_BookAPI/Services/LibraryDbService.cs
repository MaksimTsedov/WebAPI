﻿namespace BusinessLogic_BookAPI.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BusinessLogic_BookAPI.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Library manager.
    /// </summary>
    /// <seealso cref="BusinessLogic_BookAPI.Services.ILibraryService" />
    public class LibraryDbService : ILibraryService, ILibraryPairCreationManager,
                                            IBookShelf, IAuthorService, IGenreService
    {
        #region Initializing

        /// <summary>
        /// Data for <see cref="LibraryObjectService"/>
        /// </summary>
        private readonly LibraryDatabase _data;

        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryObjectService"/> class.
        /// </summary>
        public LibraryDbService(LibraryDatabase database)
        {
            _data = database;
        }

        #endregion

        #region Creating

        /// <summary>
        /// Creates and adds to list an author.
        /// </summary>
        /// <param name="author">The author.</param>
        /// <returns>
        /// Created author.
        /// </returns>
        public Author CreateAuthor(Author author)
        {
            _data.Authors.Add(author);
            _data.SaveChanges();
            return author;
        }

        /// <summary>
        /// Creates and adds to list a book.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns>
        /// Created book.
        /// </returns>
        public Books CreateBook(Books book)
        {
            _data.Books.Add(book);
            _data.SaveChanges();
            return book;
        }

        /// <summary>
        /// Creates and adds to list a new genre.
        /// </summary>
        /// <param name="genre">The genre.</param>
        /// <returns>
        /// Created genre.
        /// </returns>
        public Genre CreateGenre(Genre genre)
        {
            _data.Genres.Add(genre);
            _data.SaveChanges();
            return genre;
        }

        /// <summary>
        /// Adds the genre to book.
        /// </summary>
        /// <param name="book_id">The book identifier.</param>
        /// <param name="genre_id">The genre identifier.</param>
        /// <exception cref="ArgumentException">Throws if such pair already exists.</exception>
        public bool AddGenreToBook(int book_id, int genre_id)
        {
            bool result = false;
            if (GetBook(book_id) != null && GetGenre(genre_id) != null)
            {
                if (_data.BookGenrePairs.FirstOrDefault(pair => pair.Book_Id == book_id
                                                             && pair.Genre_Id == genre_id) == null)
                {
                    _data.BookGenrePairs.Add(new BookGenrePair { Book_Id = book_id, Genre_Id = genre_id });
                    result = true;
                }
                else
                {
                    throw new ArgumentException("Pair already exists.");
                }
            }

            _data.SaveChanges();
            return result;
        }

        /// <summary>
        /// Adds the author of book.
        /// </summary>
        /// <param name="book_id">The book identifier.</param>
        /// <param name="author_id">The author identifier.</param>
        /// <exception cref="ArgumentException">Throws if such pair already exists.</exception>
        public bool AddAuthorOfBook(int book_id, int author_id)
        {
            bool result = false;
            if (GetBook(book_id) != null && GetAuthor(author_id) != null)
            {
                if (_data.BookAuthorPairs.FirstOrDefault(pair => pair.Book_Id == book_id
                                                             && pair.Author_Id == author_id) == null)
                {
                    _data.BookAuthorPairs.Add(new BookAuthorPair { Book_Id = book_id, Author_Id = author_id });
                    result = true;
                }
                else
                {
                    throw new ArgumentException("Pair already exists.");
                }
            }

            _data.SaveChanges();
            return result;
        }

        #endregion

        #region Deleting

        /// <summary>
        /// Deletes writer and his books by his id.
        /// </summary>
        /// <param name="id">The identifier of author.</param>
        /// <exception cref="ArgumentNullException">It is thrown if there is no author with such id.</exception>
        public void DeleteAuthor(long id)
        {
            Author authorToDelete = _data.Authors.FirstOrDefault((author) => author.Id == id);
            if (authorToDelete == null)
            {
                throw new ArgumentNullException();
            }

            _data.Authors.Remove(authorToDelete);
            List<BookAuthorPair> pairsToDelete =
                _data.BookAuthorPairs.Where((bookAuthorPair) => bookAuthorPair.Author_Id == id).ToList();
            foreach (var author in pairsToDelete)
            {
                _data.BookAuthorPairs.Remove(author);
            }

            _data.SaveChanges();
        }

        /// <summary>
        /// Deletes a book by its id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="ArgumentNullException">It is thrown if there is no book with such id.</exception>
        public void DeleteBook(long id)
        {
            Books bookToDelete = _data.Books.FirstOrDefault((book) => book.Id == id);
            if (bookToDelete == null)
            {
                throw new ArgumentNullException();
            }

            _data.Books.Remove(bookToDelete);
            List<BookAuthorPair> authorPairsToDelete =
                _data.BookAuthorPairs.Where((bookAuthorPair) => bookAuthorPair.Book_Id == id).ToList();
            foreach (var author in authorPairsToDelete)
            {
                _data.BookAuthorPairs.Remove(author);
            }

            List<BookGenrePair> genrePairsToDelete =
                _data.BookGenrePairs.Where((bookGenrePair) => bookGenrePair.Book_Id == id).ToList();
            foreach (var genre in genrePairsToDelete)
            {
                _data.BookGenrePairs.Remove(genre);
            }

            _data.SaveChanges();
        }

        /// <summary>
        /// Deletes a genre by its id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="ArgumentNullException">It is thrown if there is no genre with such id.</exception>
        /// <exception cref="FormatException">It is thrown if there are exist some books of this genre.</exception>
        public void DeleteGenre(long id)
        {
            Genre genreToDelete = _data.Genres.FirstOrDefault((genre) => genre.Id == id);
            if (genreToDelete == null)
            {
                throw new ArgumentNullException();
            }

            if (_data.BookGenrePairs.Any(bookGenrePair => bookGenrePair.Genre_Id == id))
            {
                throw new FormatException(); //The condition for deleting genre is absence of books of this genre
            }
            else
            {
                _data.Genres.Remove(genreToDelete);
            }

            _data.SaveChanges();
        }

        /// <summary>
        /// Deletes the genre of a book.
        /// </summary>
        /// <param name="book_id">The book identifier.</param>
        /// <param name="genre_id">The genre identifier.</param>
        /// <returns>Is deleted.</returns>
        public bool DeleteGenreOfBook(long book_id, long genre_id)
        {
            bool result = false;
            BookGenrePair bookGenrePair = _data.BookGenrePairs.FirstOrDefault((pair) => pair.Book_Id == book_id
                                                                          && pair.Genre_Id == genre_id);
            if (bookGenrePair != null)
            {
                _data.BookGenrePairs.Remove(bookGenrePair);
                result = true;
            }

            _data.SaveChanges();
            return result;
        }

        /// <summary>
        /// Deletes the author of book.
        /// </summary>
        /// <param name="book_id">The book identifier.</param>
        /// <param name="author_id">The author identifier.</param>
        /// <returns>Is deleted.</returns>
        public bool RemoveAuthorOfBook(long book_id, long author_id)
        {
            bool result = false;
            BookAuthorPair bookAuthorPair = _data.BookAuthorPairs.FirstOrDefault((pair) => pair.Book_Id == book_id
                                                                          && pair.Author_Id == author_id);
            if (bookAuthorPair != null)
            {
                _data.BookAuthorPairs.Remove(bookAuthorPair);
                result = true;
            }

            _data.SaveChanges();
            return result;
        }


        #endregion

        #region Getting all entities

        /// <summary>
        /// Gets list of authors.
        /// </summary>
        /// <returns>
        /// Enumeration of all authors.
        /// </returns>
        public IEnumerable<Author> GetAllAuthors()
        {
            return _data.Authors;
        }

        /// <summary>
        /// Gets all books.
        /// </summary>
        /// <returns>
        /// Enumeration of all books
        /// </returns>
        public IEnumerable<Books> GetAllBooks()
        {
            return _data.Books;
        }

        /// <summary>
        /// Gets all genres.
        /// </summary>
        /// <returns>
        /// Enumeration of all genres
        /// </returns>
        public IEnumerable<Genre> GetAllGenres()
        {
            return _data.Genres;
        }

        #endregion

        #region Getting by id

        /// <summary>
        /// Gets the author.
        /// </summary>
        /// <param name="id">The identifier of author.</param>
        /// <returns>
        /// Certain author.
        /// </returns>
        public Author GetAuthor(long id)
        {
            return _data.Authors.FirstOrDefault((author) => author.Id == id);
        }

        /// <summary>
        /// Gets the book by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The chozen book.</returns>
        public Books GetBook(long id)
        {
            return _data.Books.FirstOrDefault((book) => book.Id == id);
        }

        /// <summary>
        /// Gets the genre.
        /// </summary>
        /// <param name="id">The identifier of genre.</param>
        /// <returns>
        /// Genre.
        /// </returns>
        public Genre GetGenre(long id)
        {
            return _data.Genres.FirstOrDefault((genre) => genre.Id == id);
        }

        #endregion

        #region Updating

        /// <summary>
        /// Updates information of chozen author.
        /// </summary>
        /// <param name="id">The identifier of author.</param>
        /// <param name="author">The author.</param>
        /// <returns>
        /// Updated writer
        /// </returns>
        public Author UpdateAuthor(long id, Author author)
        {
            Author authorToUpdate = _data.Authors.FirstOrDefault((oldAuthor) => oldAuthor.Id == id);
            if (authorToUpdate != null)
            {
                authorToUpdate.Clone(author);
            }

            _data.SaveChanges();
            return authorToUpdate;
        }

        /// <summary>
        /// Updates a book by its id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="book">The book.</param>
        /// <returns>
        /// Updated book.
        /// </returns>
        public Books UpdateBook(long id, Books book)
        {
            Books bookToUpdate = _data.Books.FirstOrDefault((oldBook) => oldBook.Id == id);
            if (bookToUpdate != null)
            {
                bookToUpdate.Clone(book);
            }

            _data.SaveChanges();
            return bookToUpdate;
        }

        /// <summary>
        /// Updates the genre of a book.
        /// </summary>
        /// <param name="book_id">The book identifier.</param>
        /// <param name="genre_id">The genre identifier.</param>
        /// <param name="newGenre_id">The identifier of a new genre.</param>
        /// <returns>
        /// Is updated.
        /// </returns>
        /// <exception cref="ArgumentException">Throws if receiving pair already exists.</exception>
        public bool UpdateGenreOfBook(int book_id, int genre_id, int newGenre_id)
        {
            bool result = false;
            BookGenrePair bookGenrePair = _data.BookGenrePairs.First((pair) => pair.Book_Id == book_id
                                                                      && pair.Genre_Id == genre_id);
            if (bookGenrePair != null && GetGenre(newGenre_id) != null)
            {
                if (_data.BookGenrePairs.FirstOrDefault(pair => pair.Book_Id == book_id
                                                             && pair.Genre_Id == newGenre_id) == null)
                {
                    bookGenrePair.ChangeGenre(newGenre_id);
                    result = true;
                }
                else
                {
                    throw new ArgumentException("Receiving pair already exists.");
                }
            }

            _data.SaveChanges();
            return result;
        }

        /// <summary>
        /// Updates the author of book.
        /// </summary>
        /// <param name="book_id">The book identifier.</param>
        /// <param name="author_id">The author identifier.</param>
        /// <returns>Is updated.</returns>
        /// <exception cref="ArgumentException">Throws if receiving pair already exists.</exception>
        public bool UpdateAuthorOfBook(int book_id, int author_id, int newAuthor_id)
        {
            bool result = false;
            BookAuthorPair bookAuthorPair = _data.BookAuthorPairs.First((pair) => pair.Book_Id == book_id
                                                                      && pair.Author_Id == author_id);
            if (bookAuthorPair != null && GetAuthor(newAuthor_id) != null)
            {
                if (_data.BookAuthorPairs.FirstOrDefault(pair => pair.Book_Id == book_id
                                                             && pair.Author_Id == newAuthor_id) == null)
                {
                    bookAuthorPair.ChangeAuthor(newAuthor_id);
                    result = true;
                }
                else
                {
                    throw new ArgumentException("Receiving pair already exists.");
                }
            }

             _data.SaveChanges();
            return result;
        }

        #endregion

        #region Get genre or author books

        /// <summary>
        /// Gets all books of author.
        /// </summary>
        /// <param name="author_Id">The identifier of author.</param>
        /// <returns>Enumeration of books.</returns>
        public IEnumerable<Books> GetAuthorBooks(long author_Id)
        {
            foreach (var authorBook in _data.BookAuthorPairs.Where(bookAuthorPair => bookAuthorPair.Author_Id == author_Id))
            {
                yield return _data.Books.FirstOrDefault((book) => book.Id == authorBook.Book_Id);
            }
        }

        /// <summary>
        /// Gets all books of supposed genre.
        /// </summary>
        /// <param name="genre_Id">The genre identifier.</param>
        /// <returns>Enumeration of books.</returns>
        public IEnumerable<Books> GetAllGenreBooks(long genre_Id)
        {
            foreach (var genreBook in _data.BookGenrePairs.Where(bookGenrePair => bookGenrePair.Genre_Id == genre_Id))
            {
                yield return _data.Books.FirstOrDefault((book) => book.Id == genreBook.Book_Id);
            }
        }

        #endregion
    }
}
