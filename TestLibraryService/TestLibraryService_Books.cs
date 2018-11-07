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
        private static List<Books> _books;

        /// <summary>
        /// Initializes the library instance with some books.
        /// </summary>
        /// <param name="context">The context.</param>
        [TestInitialize]
        public void Initialize()
        {
            _books = new List<Books> {
                new Books(){ Id = 1, Title = "Test1", NumberOfPages = 100, Year = 100 },
                new Books(){ Id = 2, Title = "Test2", NumberOfPages = 100, Year = -100 }
            };
        }

        /// <summary>
        /// Tests the getting of all books.
        /// </summary>
        [TestMethod]
        public void TestGetBooks()
        {
            var library = new MockDataProvider().MockSetBooks(_books).Object;

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
            var library = new MockDataProvider().MockSetBooks(_books).Object;

            Books result = new LibraryObjectService(library).GetBook(id);

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
            var library = new MockDataProvider().MockSetBooks(_books).Object;

            Books result = new LibraryObjectService(library).GetBook(id);

            Assert.IsNull(result);
        }

        /// <summary>
        /// Tests correct book adding.
        /// </summary>
        /// <param name="title">The title of a book.</param>
        /// <param name="numberOfPages">The number of pages.</param>
        /// <param name="year">The year of publishing.</param>
        [TestMethod]
        [DataRow(3, "Test3", 100, 100)]
        [DataRow(4, "new test", 20, 100)]
        public void TestAddBook_Correct(int id, string title, int numberOfPages, int year)
        {
            var library = new MockDataProvider().MockSetBooks(_books).Object;

            Books result = new LibraryObjectService(library).
                           CreateBook(new Books() { Id = id, Title = title, NumberOfPages = numberOfPages, Year = year });

            Assert.AreEqual(result, new Books() { Id = id, Title = title, NumberOfPages = numberOfPages, Year = year });
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
        public void TestUpdateBook_Correct(int id, string title, int numberOfPages, int year)
        {
            var library = new MockDataProvider().MockSetBooks(_books).Object;

            Books result = new LibraryObjectService(library).
                           UpdateBook(id, new Books() { Id = id, Title = title, NumberOfPages = numberOfPages, Year = year });

            Assert.AreEqual(result, new Books() { Id = id, Title = title, NumberOfPages = numberOfPages, Year = year });
        }

        /// <summary>
        /// Tests incorrect book updating.
        /// </summary>
        /// <param name="id">The identifier of a book.</param>
        /// <param name="title">The title of a book.</param>
        /// <param name="numberOfPages">The number of pages.</param>
        /// <param name="year">The year of publishing.</param>
        [TestMethod]
        [DataRow(5, "Test3", 100, 100)]
        [DataRow(-1, "new test", 20, 100)]
        public void TestUpdateBook_InCorrect(int id, string title, int numberOfPages, int year)
        {
            var library = new MockDataProvider().MockSetBooks(_books).Object;

            Books result = new LibraryObjectService(library).
                           UpdateBook(id, new Books() { Id = id, Title = title, NumberOfPages = numberOfPages, Year = year });

            Assert.IsNull(result);
        }

        /// <summary>
        /// Tests correct deleting of an book.
        /// </summary>
        /// <param name="id">The identifier of a book.</param>
        [TestMethod]
        [DataRow(2)]
        [DataRow(1)]
        public void TestDeleteBook_Correct(int id)
        {
            var library = SetTestData(id);
            var result = new LibraryObjectService(library);

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
            var library = new MockDataProvider().MockSetBooks(_books).Object;

            var result = new LibraryObjectService(library);
            result.DeleteBook(id);
        }

        /// <summary>
        /// Sets the test data.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Test data.</returns>
        private IDataProvider SetTestData(int id)
        {
            // Setting test data for mock to test correct deletion of a book
            _mockData = new MockDataProvider().MockSetBooks(_books).
                                               MockSetAuthors(new List<Author> { new Author() { Id = 1, FullName = "Test", Country = "Testcountry" } }).
                                               MockSetGenres(new List<Genre> { new Genre() { Id = 1, Naming = "Test" } }).
                                               MockSetBookAuthorPair(new List<BookAuthorPair> { new BookAuthorPair() { Book_Id = id, Author_Id = 1 } }).
                                               MockSetBookGenrePair(new List<BookGenrePair> { new BookGenrePair() { Book_Id = id, Genre_Id = 1 } });
            return _mockData.Object;
        }
    }
}
