namespace TestLibraryService
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BusinessLogic_BookAPI.Services;
    using BusinessLogic_BookAPI.Models;
    using Moq;

    /// <summary>
    /// Test class for Book-Author pair
    /// </summary>
    [TestClass]
    public class TestLibraryService_BookAuthorLinks
    {
        /// <summary>
        /// The mock of data
        /// </summary>
        private static Mock<IDataProvider> _mockData;

        /// <summary>
        /// List of authors
        /// </summary>
        private static List<Author> _authors;

        /// <summary>
        /// List of books
        /// </summary>
        private static List<Book> _books;

        /// <summary>
        /// List of pairs
        /// </summary>
        private static List<BookAuthorPair> _bookAuthorPairs;

        /// <summary>
        /// Initializes the library instance with some authors, books and their connection.
        /// </summary>
        /// <param name="context">The context.</param>
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            _authors = new List<Author> {
                new Author("Test1", "Testcountry1"),
                new Author("Test2", "Testcountry2")
            };

            _books = new List<Book> {
                new Book("Test1", 100, 100),
                new Book("Test2", 21, -100)
            };

            _bookAuthorPairs = new List<BookAuthorPair> {
                new BookAuthorPair(1, 1),
                new BookAuthorPair(2, 1)
            };

            _mockData = new MockDataProvider().MockSetAuthors(_authors).MockSetBooks(_books).MockSetBookAuthorPair(_bookAuthorPairs);

        }

        /// <summary>
        /// Tests correct adding author of book.
        /// </summary>
        [TestMethod]
        public void TestAddAuthorOfBook_Correct()
        {
            var library = _mockData.Object;

            bool result = new LibraryServiceForObjects(library).AddAuthorOfBook(book_id: 2, author_id: 2);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests unsuccessful adding author of book.
        /// </summary>
        [TestMethod]
        [DataRow(2, 3)]
        [DataRow(3, 1)]
        [DataRow(0, -2)]
        public void TestAddAuthorOfBook_InCorrect(long book_id, long author_id)
        {
            var library = _mockData.Object;

            bool result = new LibraryServiceForObjects(library).AddAuthorOfBook(book_id, author_id);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tests correct execute of getting all books of chosen author.
        /// </summary>
        [TestMethod]
        public void TestGetAuthorBooks_Correct()
        {
            var library = _mockData.Object;

            int result = new LibraryServiceForObjects(library).GetAuthorBooks(author_Id: 1).Count();

            Assert.IsTrue(result == 2);
        }

        /// <summary>
        /// Tests incorrect execute of getting all books of chosen author.
        /// </summary>
        [TestMethod]
        [DataRow(3)]
        [DataRow(-1)]
        [DataRow(0)]
        public void TestGetAuthorBooks_InCorrect(long id)
        {
            var library = _mockData.Object;

            int result = new LibraryServiceForObjects(library).GetAuthorBooks(author_Id: id).Count();

            Assert.IsTrue(result == 0);
        }
    }
}
