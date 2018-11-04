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
    /// Test class for CRUD operations for genres
    /// </summary>
    [TestClass]
    public class TestLibraryService_Genres
    {
        /// <summary>
        /// The mock of data
        /// </summary>
        private static Mock<IDataProvider> _mockData;

        /// <summary>
        /// List of genres
        /// </summary>
        private static List<Genre> _genres;

        /// <summary>
        /// Initializes the library instance with some genres.
        /// </summary>
        /// <param name="context">The context.</param>
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            _genres = new List<Genre> {
                new Genre("Test1"),
                new Genre("Test2")
            };           
        }

        /// <summary>
        /// Tests the getting of all genres.
        /// </summary>
        [TestMethod]
        public void TestGetGenres()
        {
            var library = new MockDataProvider().MockSetGenres(_genres).Object;

            int result = new LibraryObjectService(library).GetAllGenres().Count();

            Assert.IsTrue(result == 2);
        }

        /// <summary>
        /// Tests correct genre adding.
        /// </summary>
        /// <param name="naming">The naming of genre.</param>
        [TestMethod]
        [DataRow("Test")]
        [DataRow("NewTest")]
        public void TestAddGenre_Correct(string naming)
        {
            var library = new MockDataProvider().MockSetGenres(_genres).Object;

            Genre result = new LibraryObjectService(library).CreateGenre(new Genre(naming));

            Assert.AreEqual(result, new Genre(naming));
        }

        /// <summary>
        /// Tests correct deleting of an genre.
        /// </summary>
        /// <param name="id">The identifier of a genre.</param>
        [TestMethod]
        [DataRow(2)]
        [DataRow(1)]
        public void TestDeleteGenre_Correct(long id)
        {
            var library = new MockDataProvider().MockSetGenres(_genres).
                                                 MockSetBooks(new List<Book> { new Book("Test", 1, 1) }).
                                                 MockSetBookGenrePair(new List<BookGenrePair> { }).Object;
            var result = new LibraryObjectService(library);

            result.DeleteGenre(id);

            Assert.IsNull(result.GetAllGenres().FirstOrDefault(genre => genre.Id == id));
        }

        /// <summary>
        /// Tests deleting of an genre if there are books with that genre.
        /// </summary>
        /// <param name="id">The identifier of a genre.</param>
        [TestMethod]
        [DataRow(2)]
        [DataRow(1)]
        [ExpectedException(typeof(FormatException))]
        public void TestDeleteGenre_FormatExceptionThrow(long id)
        {
            var library = SetTestBooks(id);
            var result = new LibraryObjectService(library);

            result.DeleteGenre(id);
        }

        /// <summary>
        /// Tests incorrect deleting of an genre.
        /// </summary>
        /// <param name="id">The identifier of a genre.</param>
        [TestMethod]
        [DataRow(3)]
        [DataRow(-11)]
        [DataRow(0)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDeleteAuthor_ArgumentNullExceptionThrow(long id)
        {
            var library = new MockDataProvider().MockSetGenres(_genres).Object;
            var result = new LibraryObjectService(library);

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
            _mockData = new MockDataProvider().MockSetGenres(_genres).
                                               MockSetBooks(new List<Book> { new Book("Test", 1, 1) }).
                                               MockSetBookGenrePair(new List<BookGenrePair> { new BookGenrePair(1, id) });
            return _mockData.Object;
        }
    }
}