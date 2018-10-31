namespace BusinessLogic_BookAPI.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BusinessLogic_BookAPI.Models;

    /// <summary>
    /// Library manager
    /// </summary>
    /// <seealso cref="BusinessLogic_BookAPI.Services.ILibraryService" />
    public class LibraryServiceForObjects : ILibraryService
    {
        #region Initializing

        /// <summary>
        /// List of books
        /// </summary>
        private List<Book> _books = new List<Book>();

        /// <summary>
        /// List of authors
        /// </summary>
        private List<Author> _authors = new List<Author>();

        /// <summary>
        /// List of genres
        /// </summary>
        private List<Genre> _genres = new List<Genre>();

        /// <summary>
        /// Set of book-genre link
        /// </summary>
        private HashSet<BookGenrePair> _bookGenrePair
            = new HashSet<BookGenrePair>();

        /// <summary>
        /// Set of book-author pair
        /// </summary>
        private HashSet<BookAuthorPair> _bookAuthorPair
            = new HashSet<BookAuthorPair>();

        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryServiceForObjects"/> class.
        /// </summary>
        public LibraryServiceForObjects(IDataProvider data)
        {
            _books = data.GetBooks().ToList();
            _authors = data.GetAuthors().ToList();
            _genres = data.GetGenres().ToList();
            _bookAuthorPair = new HashSet<BookAuthorPair>(data.GetBooksAuthors());
            _bookGenrePair = new HashSet<BookGenrePair>(data.GetBooksGenres());
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
            _authors.Add(author);
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
            _books.Add(book);
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
            _genres.Add(genre);
            return genre;
        }

        /// <summary>
        /// Adds the genre to book.
        /// </summary>
        /// <param name="book_id">The book identifier.</param>
        /// <param name="genre_id">The genre identifier.</param>
        /// <exception cref="ArgumentOutOfRangeException">Throws if book or genre id is not exist.</exception>
        public bool AddGenreToBook(long book_id, long genre_id)
        {
            bool result = false;
            if (GetBook(book_id) != null && _genres.Any((genre) => genre.Id == genre_id))
            {
                result = _bookGenrePair.Add(new BookGenrePair(book_id, genre_id));
            }

            return result;
        }

        /// <summary>
        /// Adds the author of book.
        /// </summary>
        /// <param name="book_id">The book identifier.</param>
        /// <param name="author_id">The author identifier.</param>
        /// <exception cref="ArgumentOutOfRangeException">Throws if book or author id is not exist.</exception>
        public bool AddAuthorOfBook(long book_id, long author_id)
        {
            bool result = false;
            if (GetBook(book_id) != null && GetAuthor(author_id) != null)
            {
                result = _bookAuthorPair.Add(new BookAuthorPair(book_id, author_id));
            }

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
            Author authorToDelete = _authors.FirstOrDefault((author) => author.Id == id);
            if (authorToDelete == null)
            {
                throw new ArgumentNullException();
            }

            _authors.Remove(authorToDelete);
            _bookAuthorPair.RemoveWhere((bookAuthorPair) => bookAuthorPair.Author_Id == id);
        }

        /// <summary>
        /// Deletes a book by its id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="ArgumentNullException">It is thrown if there is no book with such id.</exception>
        public void DeleteBook(long id)
        {
            Book bookToDelete = _books.FirstOrDefault((book) => book.Id == id);
            if (bookToDelete == null)
            {
                throw new ArgumentNullException();
            }

            _books.Remove(bookToDelete);
            _bookAuthorPair.RemoveWhere((bookAuthorPair) => bookAuthorPair.Author_Id == id);
            _bookGenrePair.RemoveWhere((bookGenrePair) => bookGenrePair.Genre_Id == id);
        }

        /// <summary>
        /// Deletes a genre by its id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="ArgumentNullException">It is thrown if there is no genre with such id.</exception>
        /// <exception cref="FormatException">It is thrown if there are exist some books of this genre.</exception>
        public void DeleteGenre(long id)
        {
            Genre genreToDelete = _genres.FirstOrDefault((genre) => genre.Id == id);
            if (genreToDelete == null)
            {
                throw new ArgumentNullException();
            }

            if (_bookGenrePair.Any(bookGenrePair => bookGenrePair.Genre_Id == id))
            {
                throw new FormatException(); //The condition for deleting genre is absence of books of this genre
            }
            else
            {
                _genres.Remove(genreToDelete);
            }          
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
            return _authors;
        }

        /// <summary>
        /// Gets all books.
        /// </summary>
        /// <returns>
        /// Enumeration of all books
        /// </returns>
        public IEnumerable<Book> GetAllBooks()
        {
            return _books;
        }

        /// <summary>
        /// Gets all genres.
        /// </summary>
        /// <returns>
        /// Enumeration of all genres
        /// </returns>
        public IEnumerable<Genre> GetAllGenres()
        {
            return _genres;
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
            return _authors.FirstOrDefault((author) => author.Id == id);
        }

        /// <summary>
        /// Gets the book by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The chozen book.</returns>
        public Book GetBook(long id)
        {
            return _books.FirstOrDefault((book) => book.Id == id);
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
            Author authorToUpdate = _authors.FirstOrDefault((oldauthor) => oldauthor.Id == id);
            if (authorToUpdate != null)
            {
                authorToUpdate.Clone(author);
            }

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
            Book bookToUpdate = _books.FirstOrDefault((oldbook) => oldbook.Id == id);
            if (bookToUpdate != null)
            {
                bookToUpdate.Clone(book);
            }

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
            foreach (var authorBook in _bookAuthorPair.Where(bookAuthorPair => bookAuthorPair.Author_Id == author_Id))
            {
                yield return _books.FirstOrDefault((book) => book.Id == authorBook.Book_Id);
            }
        }

        /// <summary>
        /// Gets all books of supposed genre.
        /// </summary>
        /// <param name="genre_Id">The genre identifier.</param>
        /// <returns>Enumeration of books.</returns>
        public IEnumerable<Book> GetAllGenreBooks(long genre_Id)
        {
            foreach (var genreBook in _bookGenrePair.Where(bookGenrePair => bookGenrePair.Genre_Id == genre_Id))
            {
                yield return _books.FirstOrDefault((book) => book.Id == genreBook.Book_Id);
            }
        }

        #endregion
    }
}
