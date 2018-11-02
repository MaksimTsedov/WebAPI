namespace TestLibraryService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BusinessLogic_BookAPI.Services;
    using BusinessLogic_BookAPI.Models;
    using Moq;

    /// <summary>
    /// Test class for CRUD operations for books
    /// </summary>
    [TestClass]
    public class TestLibraryService_Books
    {
        /// <summary>
        /// The mock of data
        /// </summary>
        private static Mock<IDataProvider> _mockData;

        /// <summary>
        /// List of books
        /// </summary>
        private static List<Book> _books;

        /// <summary>
        /// Initializes the library instance with some books.
        /// </summary>
        /// <param name="context">The context.</param>
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            _books = new List<Book> {
                new Book("Test1", 100, 100),
                new Book("Test2", 21, -100)
            };

            _mockData = new MockDataProvider().MockSetBooks(_books);
        }

        /// <summary>
        /// Tests the getting of all books.
        /// </summary>
        [TestMethod]
        public void TestGetBooks()
        {
            var library = _mockData.Object;

            int result = new LibraryObjectService(library).GetAllBooks().Count();

            Assert.IsTrue(result == 2);
        }

        /// <summary>
        /// Tests correct book getting.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public void TestGetBook_Correct(long id)
        {
            var library = _mockData.Object;

            Book result = new LibraryObjectService(library).GetBook(id);

            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Tests the getting of chosen book incorrect.
        /// </summary>
        /// <param name="id">The identifier of a book.</param>
        [TestMethod]
        [DataRow(7)]
        [DataRow(-1)]
        [DataRow(0)]
        public void TestGetBook_InCorrect(long id)
        {
            var library = _mockData.Object;

            Book result = new LibraryObjectService(library).GetBook(id);

            Assert.IsNull(result);
        }

        /// <summary>
        /// Tests correct book adding.
        /// </summary>
        /// <param name="title">The title of a book.</param>
        /// <param name="numberOfPages">The number of pages.</param>
        /// <param name="year">The year of publishing.</param>
        [TestMethod]
        [DataRow("Test3", 100, 100)]
        [DataRow("new test", 20, 100)]
        public void TestAddBook_Correct(string title, int numberOfPages, int year)
        {
            var library = _mockData.Object;

            Book result = new LibraryObjectService(library).CreateBook(new Book(title, numberOfPages, year));

            Assert.AreEqual(result, new Book(title, numberOfPages, year));
        }

        /// <summary>
        /// Tests correct book updating.
        /// </summary>
        /// <param name="id">The identifier of a book.</param>
        /// <param name="title">The title of a book.</param>
        /// <param name="numberOfPages">The number of pages.</param>
        /// <param name="year">The year of publishing.</param>
        [TestMethod]
        [DataRow(1, "Test3", 100, 100)]
        [DataRow(2, "new test", 20, 100)]
        public void TestUpdateBook_Correct(long id, string title, int numberOfPages, int year)
        {
            var library = _mockData.Object;

            Book result = new LibraryObjectService(library).UpdateBook(id, new Book(title, numberOfPages, year));

            Assert.AreEqual(result, new Book(title, numberOfPages, year));
        }

        /// <summary>
        /// Tests incorrect book updating.
        /// </summary>
        /// <param name="id">The identifier of a book.</param>
        /// <param name="title">The title of a book.</param>
        /// <param name="numberOfPages">The number of pages.</param>
        /// <param name="year">The year of publishing.</param>
        [TestMethod]
        [DataRow(3, "Test3", 100, 100)]
        [DataRow(-1, "new test", 20, 100)]
        public void TestUpdateBook_InCorrect(long id, string title, int numberOfPages, int year)
        {
            var library = _mockData.Object;

            Book result = new LibraryObjectService(library).UpdateBook(id, new Book(title, numberOfPages, year));

            Assert.IsNull(result);
        }

        /// <summary>
        /// Tests correct deleting of an book.
        /// </summary>
        /// <param name="id">The identifier of a book.</param>
        [TestMethod]
        [DataRow(2)]
        [DataRow(1)]
        public void TestDeleteBook_Correct(long id)
        {
            var library = _mockData.Object;
            library = SetTestData(id);
            ILibraryService result = new LibraryObjectService(library);

            result.DeleteBook(id);

            Assert.IsNull(result.GetAllBooks().FirstOrDefault(book => book.Id == id));
            Assert.IsTrue(result.GetAuthorBooks(id).Count() == 0);
            Assert.IsTrue(result.GetAllGenreBooks(id).Count() == 0);
        }

        /// <summary>
        /// Tests incorrect deleting of an book.
        /// </summary>
        /// <param name="id">The identifier of a book.</param>
        [TestMethod]
        [DataRow(3)]
        [DataRow(-11)]
        [DataRow(0)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDeleteBook_ArgumentNullExceptionThrow(long id)
        {
            var library = _mockData.Object;

            ILibraryService result = new LibraryObjectService(library);
            result.DeleteBook(id);
        }

        /// <summary>
        /// Sets the test data.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Test data.</returns>
        private IDataProvider SetTestData(long id)
        {
            // Setting test data for mock to test correct deletion of a book
            _mockData = new MockDataProvider().MockSetBooks(_books).
                                               MockSetAuthors(new List<Author> { new Author("Test", "Testcountry") }).
                                               MockSetGenres(new List<Genre> { new Genre("Test") }).
                                               MockSetBookAuthorPair(new List<BookAuthorPair> { new BookAuthorPair(id, 1) }).
                                               MockSetBookGenrePair(new List<BookGenrePair> { new BookGenrePair(id, 1) });
            return _mockData.Object;
        }
    }
}
