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
        [TestInitialize]
        public void Initialize()
        {
            _authors = new List<Author> {
                new Author(){ Id = 1, FullName = "Test1", Country = "Testcountry1" },
                new Author(){ Id = 2, FullName = "Test2", Country = "Testcountry2" }
            };
        }

        /// <summary>
        /// Tests the getting of all authors.
        /// </summary>
        [TestMethod]
        public void TestGetAuthors()
        {
            var library = new MockDataProvider().MockSetAuthors(_authors).Object;

            int result = new LibraryObjectService(library).GetAllAuthors().Count();

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
            var library = new MockDataProvider().MockSetAuthors(_authors).Object;

            Author result = new LibraryObjectService(library).GetAuthor(id);

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
            var library = new MockDataProvider().MockSetAuthors(_authors).Object;

            Author result = new LibraryObjectService(library).GetAuthor(id);

            Assert.IsNull(result);
        }

        /// <summary>
        /// Tests correct adding author.
        /// </summary>
        /// <param name="fullname">Fullname.</param>
        /// <param name="country">The country.</param>
        [TestMethod]
        [DataRow(3, "Testauthor", "Lemuria")]
        [DataRow(4, "Hello", "Ukraine")]
        public void TestAddAuthor_Correct(int id, string fullname, string country)
        {
            var library = new MockDataProvider().MockSetAuthors(_authors).Object;

            Author result = new LibraryObjectService(library).CreateAuthor(new Author(){ Id = id, FullName = fullname, Country = country });

            Assert.AreEqual(result, new Author(){ Id = id, FullName = fullname, Country = country });
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
            var library = new MockDataProvider().MockSetAuthors(_authors).Object;

            Author result = new LibraryObjectService(library).UpdateAuthor(id, new Author() { FullName = fullname, Country = country });

            Assert.AreEqual(result, new Author() { FullName = fullname, Country = country });
        }

        /// <summary>
        /// Tests the update of an author is incorrect.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="fullname">The fullname.</param>
        /// <param name="country">The country.</param>
        [TestMethod]
        [DataRow(5, "TestingWrong", "Ukraine")]
        [DataRow(-1, "TestingWronginRome", "Rome")]
        public void TestUpdateAuthor_InCorrect(long id, string fullname, string country)
        {
            var library = new MockDataProvider().MockSetAuthors(_authors).Object;

            Author result = new LibraryObjectService(library).UpdateAuthor(id, new Author() { FullName = fullname, Country = country });

            Assert.IsNull(result);
        }

        /// <summary>
        /// Tests correct deleting of an author.
        /// </summary>
        /// <param name="id">The identifier of a author.</param>
        [TestMethod]
        [DataRow(2)]
        [DataRow(1)]
        public void TestDeleteAuthor_Correct(int id)
        {
            var library = SetTestBooks(id);

            var result = new LibraryObjectService(library);

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
            var library = new MockDataProvider().MockSetAuthors(_authors).Object;

            var result = new LibraryObjectService(library);
            result.DeleteAuthor(id);
        }

        /// <summary>
        /// Sets the test books.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Test data.</returns>
        private static IDataProvider SetTestBooks(int id)
        {
            // Setting test data for mock to test correct deletion of an author
            _mockData = new MockDataProvider().MockSetAuthors(_authors).
                                               MockSetBooks(new List<Books> { new Books() { Id = 1, Title = "Test", NumberOfPages = 100, Year = 100 } }).
                                               MockSetBookAuthorPair(new List<BookAuthorPair> { new BookAuthorPair() { Book_Id = 1, Author_Id = id } });
            return _mockData.Object;
        }
    }
}
