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
        private static List<Books> _books;

        /// <summary>
        /// List of pairs
        /// </summary>
        private static List<BookAuthorPair> _bookAuthorPairs;

        /// <summary>
        /// Initializes the library instance with some authors, books and their connection.
        /// </summary>
        /// <param name="context">The context.</param>
        [TestInitialize]
        public void Initialize()
        {
            _authors = new List<Author> {
                new Author(){ Id = 1, FullName ="Test1", Country = "Testcountry1" },
                new Author(){ Id = 2, FullName ="Test2", Country = "Testcountry2" }
            };

            _books = new List<Books> {
                new Books(){ Id = 1, Title = "Test1", NumberOfPages = 100, Year = 100 },
                new Books(){ Id = 2, Title = "Test2", NumberOfPages = 21, Year = -100 }
            };

            _bookAuthorPairs = new List<BookAuthorPair> {
                new BookAuthorPair(){ Book_Id = 1, Author_Id  = 1},
                new BookAuthorPair(){ Book_Id = 2, Author_Id = 1}
            };
        }

        /// <summary>
        /// Tests correct adding author of book.
        /// </summary>
        [TestMethod]
        public void TestAddAuthorOfBook_Correct()
        {
            var library = new MockDataProvider().MockSetAuthors(_authors).
                                                 MockSetBooks(_books).
                                                 MockSetBookAuthorPair(_bookAuthorPairs).Object;

            bool result = new LibraryObjectService(library).AddAuthorOfBook(book_id: 2, author_id: 2);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests unsuccessful adding author of book.
        /// </summary>
        [TestMethod]
        [DataRow(2, 3)]
        [DataRow(3, 1)]
        [DataRow(0, -2)]
        public void TestAddAuthorOfBook_InCorrect(int book_id, int author_id)
        {
            var library = new MockDataProvider().MockSetAuthors(_authors).
                                                             MockSetBooks(_books).
                                                             MockSetBookAuthorPair(_bookAuthorPairs).Object;

            bool result = new LibraryObjectService(library).AddAuthorOfBook(book_id, author_id);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tests correct execute of getting all books of chosen author.
        /// </summary>
        [TestMethod]
        public void TestGetAuthorBooks_Correct()
        {
            var library = new MockDataProvider().MockSetAuthors(_authors).
                                                             MockSetBooks(_books).
                                                             MockSetBookAuthorPair(_bookAuthorPairs).Object;
            int result = new LibraryObjectService(library).GetAuthorBooks(author_Id: 1).Count();

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
            var library = new MockDataProvider().MockSetAuthors(_authors).
                                                             MockSetBooks(_books).
                                                             MockSetBookAuthorPair(_bookAuthorPairs).Object;
            int result = new LibraryObjectService(library).GetAuthorBooks(author_Id: id).Count();

            Assert.IsTrue(result == 0);
        }
    }
}
