namespace BusinessLogic_BookAPI.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BusinessLogic_BookAPI.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Library manager
    /// </summary>
    /// <seealso cref="BusinessLogic_BookAPI.Services.ILibraryService" />
    public class LibraryServiceForDB : DbContext, ILibraryService
    {
        #region Initializing

        /// <summary>
        /// List of books
        /// </summary>
        private DbSet<Book> _Books { get; set; }

        /// <summary>
        /// List of authors
        /// </summary>
        private DbSet<Author> _Authors { get; set; }

        /// <summary>
        /// List of genres
        /// </summary>
        private DbSet<Genre> _Genres { get; set; }

        /// <summary>
        /// Sorted set of book-genre link
        /// </summary>
        private DbSet<BookGenrePair> _BookGenrePair { get; set; }

        /// <summary>
        /// Sorted set of book-author pair
        /// </summary>
        private DbSet<BookAuthorPair> _BookAuthorPair { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryServiceForObjects"/> class.
        /// </summary>
        public LibraryServiceForDB(IDataProvider data, DbContextOptions<LibraryServiceForDB> options):
            base (options)
        {
            Database.EnsureCreated();
            _Books.AddRange(data.SetBooks());
            _Authors.AddRange(data.SetAuthors());
            _Genres.AddRange(data.SetGenres());
            _BookAuthorPair.AddRange(data.SetBooksAuthors());
            _BookGenrePair.AddRange(data.SetBooksGenres());
            this.SaveChanges();
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
            _Authors.Add(author);
            this.SaveChanges();
            return author;
        }

        /// <summary>
        /// Creates and adds to list a book.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns>
        /// Created book.
        /// </returns>
        public Book CreateBook(Book book)
        {
            _Books.Add(book);
            this.SaveChanges();
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
            _Genres.Add(genre);
            this.SaveChanges();
            return genre;
        }

        /// <summary>
        /// Adds the genre to book.
        /// </summary>
        /// <param name="book_id">The book identifier.</param>
        /// <param name="genre_id">The genre identifier.</param>
        /// <returns>Is added new link.</returns>
        public bool AddGenreToBook(long book_id, long genre_id)
        {
            bool result = false;

            if (GetBook(book_id) != null && _Genres.Any((genre) => genre.Id == genre_id))
            {
                _BookGenrePair.Add(new BookGenrePair(book_id, genre_id));
                result = true;
            }

            this.SaveChanges();
            return result;
        }

        /// <summary>
        /// Adds the author of book.
        /// </summary>
        /// <param name="book_id">The book identifier.</param>
        /// <param name="author_id">The author identifier.</param>
        /// <returns>Is added new link.</returns>
        public bool AddAuthorOfBook(long book_id, long author_id)
        {
            bool result = false;
            if (GetBook(book_id) != null && GetAuthor(author_id) != null)
            {
                _BookAuthorPair.Add(new BookAuthorPair(book_id, author_id));
                result = true;
            }

            this.SaveChanges();
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
            Author authorToDelete = _Authors.FirstOrDefault((author) => author.Id == id);
            if (authorToDelete == null)
            {
                throw new ArgumentNullException();
            }

            this.SaveChanges();
            _Authors.Remove(authorToDelete);
            _BookAuthorPair.RemoveRange(from pair in _BookAuthorPair
                                        where pair.Author_Id == id
                                        select pair);

            this.SaveChanges();
        }

        /// <summary>
        /// Deletes a book by its id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="ArgumentNullException">It is thrown if there is no book with such id.</exception>
        public void DeleteBook(long id)
        {
            Book bookToDelete = _Books.FirstOrDefault((book) => book.Id == id);
            if (bookToDelete == null)
            {
                throw new ArgumentNullException();
            }

            _Books.Remove(bookToDelete);
            _BookAuthorPair.RemoveRange(from pair in _BookAuthorPair
                                        where pair.Author_Id == id
                                        select pair);
            _BookGenrePair.RemoveRange(from pair in _BookGenrePair
                                       where pair.Genre_Id == id
                                       select pair);

            this.SaveChanges();
        }

        /// <summary>
        /// Deletes a genre by its id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="ArgumentNullException">It is thrown if there is no genre with such id.</exception>
        /// <exception cref="FormatException">It is thrown if there are exist books of this genre.</exception>
        public void DeleteGenre(long id)
        {
            Genre genreToDelete = _Genres.FirstOrDefault((genre) => genre.Id == id);
            if (genreToDelete == null)
            {
                throw new ArgumentNullException();
            }

            if (_BookGenrePair.Any(bookGenrePair => bookGenrePair.Genre_Id == id))
            {
                throw new FormatException(); //The condition for deleting genre is absence of books of this genre
            }

            _Genres.Remove(genreToDelete);

            this.SaveChanges();
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
            foreach (var author in _Authors)
            {
                yield return author;
            }

        }

        /// <summary>
        /// Gets all books.
        /// </summary>
        /// <returns>
        /// Enumeration of all books
        /// </returns>
        public IEnumerable<Book> GetAllBooks()
        {
            foreach (var book in _Books)
            {
                yield return book;
            }
        }

        /// <summary>
        /// Gets all genres.
        /// </summary>
        /// <returns>
        /// Enumeration of all genres
        /// </returns>
        public IEnumerable<Genre> GetAllGenres()
        {
            foreach (var genre in _Genres)
            {
                yield return genre;
            }
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
            return _Authors.FirstOrDefault((author) => author.Id == id);
        }

        /// <summary>
        /// Gets the book by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The chozen book.</returns>
        public Book GetBook(long id)
        {
            return _Books.FirstOrDefault((book) => book.Id == id);
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
            Author authorToUpdate = _Authors.FirstOrDefault((oldauthor) => oldauthor.Id == id);
            if (authorToUpdate != null)
            {
                authorToUpdate.Clone(author);
            }

            this.SaveChanges();
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
        public Book UpdateBook(long id, Book book)
        {
            Book bookToUpdate = _Books.FirstOrDefault((oldbook) => oldbook.Id == id);
            if (bookToUpdate != null)
            {
                bookToUpdate.Clone(book);
            }

            this.SaveChanges();
            return bookToUpdate;
        }

        #endregion

        #region Get genre or author books

        /// <summary>
        /// Gets all books of author.
        /// </summary>
        /// <param name="author_Id">The identifier of author.</param>
        /// <returns>Enumeration of books.</returns>
        public IEnumerable<Book> GetAuthorBooks(long author_Id)
        {
            foreach (var authorBook in _BookAuthorPair.Where(bookAuthorPair => bookAuthorPair.Author_Id == author_Id))
            {
                yield return _Books.FirstOrDefault((book) => book.Id == authorBook.Book_Id);
            }
        }

        /// <summary>
        /// Gets all books of supposed genre.
        /// </summary>
        /// <param name="genre_Id">The genre identifier.</param>
        /// <returns>Enumeration of books.</returns>
        public IEnumerable<Book> GetAllGenreBooks(long genre_Id)
        {
            foreach (var genreBook in _BookGenrePair.Where(bookGenrePair => bookGenrePair.Genre_Id == genre_Id))
            {
                yield return _Books.FirstOrDefault((book) => book.Id == genreBook.Book_Id);
            }
        }

        #endregion
    }
}
