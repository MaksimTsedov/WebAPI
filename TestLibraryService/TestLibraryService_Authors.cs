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
    /// Test class for CRUD operations for authors
    /// </summary>
    [TestClass]
    public class TestLibraryService_Authors
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
        /// Initializes the library instance with some authors.
        /// </summary>
        /// <param name="context">The context.</param>
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            _authors = new List<Author> {
                new Author("Test1","Testcountry1"),
                new Author("Test2","Testcountry2")
            };
            _mockData = new MockDataProvider().MockSetAuthors(_authors);
        }

        /// <summary>
        /// Tests the getting of all authors.
        /// </summary>
        [TestMethod]
        public void TestGetAuthors()
        {
            var library = _mockData.Object;

            int result = new LibraryServiceForObjects(library).GetAllAuthors().Count();

            Assert.IsTrue(result == 2);
        }

        /// <summary>
        /// Tests correct author getting.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public void TestGetAuthor_Correct(long id)
        {
            var library = _mockData.Object;

            Author result = new LibraryServiceForObjects(library).GetAuthor(id);

            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Tests the getting of chosen author incorrect.
        /// </summary>
        /// <param name="id">The identifier of a author.</param>
        [TestMethod]
        [DataRow(7)]
        [DataRow(-1)]
        [DataRow(0)]
        public void TestGetAuthor_InCorrect(long id)
        {
            var library = _mockData.Object;

            Author result = new LibraryServiceForObjects(library).GetAuthor(id);

            Assert.IsNull(result);
        }

        /// <summary>
        /// Tests correct adding author.
        /// </summary>
        /// <param name="fullname">Fullname.</param>
        /// <param name="country">The country.</param>
        [TestMethod]
        [DataRow("Testauthor", "Lemuria")]
        [DataRow("Hello", "Ukraine")]
        public void TestAddAuthor_Correct(string fullname, string country)
        {
            var library = _mockData.Object;

            Author result = new LibraryServiceForObjects(library).CreateAuthor(new Author(fullname, country));

            Assert.AreEqual(result, new Author(fullname, country));
        }

        /// <summary>
        /// Tests update of an author is correct.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="fullname">Fullname.</param>
        /// <param name="country">The country.</param>
        [TestMethod]
        [DataRow(1, "Testing", "Ukraine")]
        [DataRow(2, "Testing2", "Ukraine")]
        public void TestUpdateAuthor_Correct(long id, string fullname, string country)
        {
            var library = _mockData.Object;

            Author result = new LibraryServiceForObjects(library).UpdateAuthor(id, new Author(fullname, country));

            Assert.AreEqual(result, new Author(fullname, country));
        }

        /// <summary>
        /// Tests the update of an author is incorrect.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="fullname">The fullname.</param>
        /// <param name="country">The country.</param>
        [TestMethod]
        [DataRow(3, "TestingWrong", "Ukraine")]
        [DataRow(-1, "TestingWronginRome", "Rome")]
        public void TestUpdateAuthor_InCorrect(long id, string fullname, string country)
        {
            var library = _mockData.Object;

            Author result = new LibraryServiceForObjects(library).UpdateAuthor(id, new Author(fullname, country));

            Assert.IsNull(result);
        }

        /// <summary>
        /// Tests correct deleting of an author.
        /// </summary>
        /// <param name="id">The identifier of a author.</param>
        [TestMethod]
        [DataRow(2)]
        [DataRow(1)]
        public void TestDeleteAuthor_Correct(long id)
        {
            var library = _mockData.Object;
            library = SetTestBooks(id);

            ILibraryService result = new LibraryServiceForObjects(library);

            result.DeleteAuthor(id);

            Assert.IsNull(result.GetAllAuthors().FirstOrDefault(author => author.Id == id));
            Assert.IsTrue(result.GetAuthorBooks(id).Count() == 0);
        }

        /// <summary>
        /// Tests incorrect deleting of an author.
        /// </summary>
        /// <param name="id">The identifier of a author.</param>
        [TestMethod]
        [DataRow(3)]
        [DataRow(-11)]
        [DataRow(0)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDeleteAuthor_ArgumentNullExceptionThrow(long id)
        {
            var library = _mockData.Object;

            ILibraryService result = new LibraryServiceForObjects(library);
            result.DeleteAuthor(id);
        }

        /// <summary>
        /// Sets the test books.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Test data.</returns>
        private static IDataProvider SetTestBooks(long id)
        {
            // Setting test data for mock to test correct deletion of an author
            _mockData = new MockDataProvider().MockSetAuthors(_authors).
                                               MockSetBooks(new List<Book> { new Book("Test", 100, 100) }).
                                               MockSetBookAuthorPair(new List<BookAuthorPair> { new BookAuthorPair(1, id) });
            return _mockData.Object;
        }
    }
}
