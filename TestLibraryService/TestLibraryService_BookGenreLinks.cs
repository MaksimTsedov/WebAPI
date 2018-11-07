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
    /// Test class for Book-Author pair
    /// </summary>
    [TestClass]
    public class TestLibraryService_BookGenreLinks
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
        /// List of books
        /// </summary>
        private static List<Books> _books;

        /// <summary>
        /// List of pairs
        /// </summary>
        private static List<BookGenrePair> _bookGenrePairs;

        /// <summary>
        /// Initializes the library instance with some genres, books and their connection.
        /// </summary>
        /// <param name="context">The context.</param>
        [TestInitialize]
        public void Initialize()
        {
            _genres = new List<Genre> {
                new Genre(){ Id = 1, Naming = "Test1" },
                new Genre(){ Id = 2, Naming = "Test2" }
            };

            _books = new List<Books> {
                new Books(){ Id = 1, Title = "Test1", NumberOfPages = 100, Year = 100 },
                new Books(){ Id = 2, Title = "Test2", NumberOfPages = 21, Year = -100 }
            };

            _bookGenrePairs = new List<BookGenrePair> {
                new BookGenrePair(){ Book_Id = 1, Genre_Id = 1 },
                new BookGenrePair(){ Book_Id = 2, Genre_Id = 1 }
            };
        }

        /// <summary>
        /// Tests correct adding genre of book.
        /// </summary>
        [TestMethod]
        public void TestAddGenreOfBook_Correct()
        {
            var library = new MockDataProvider().MockSetGenres(_genres).
                                                 MockSetBooks(_books).
                                                 MockSetBookGenrePair(_bookGenrePairs).Object;

            bool result = new LibraryObjectService(library).AddGenreToBook(book_id: 2, genre_id: 2);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests unsuccessful adding genre of book.
        /// </summary>
        [TestMethod]
        [DataRow(2, 3)]
        [DataRow(3, 1)]
        [DataRow(0, -2)]
        public void TestAddGenreOfBook_InCorrect(int book_id, int genre_id)
        {
            var library = new MockDataProvider().MockSetGenres(_genres).
                                                 MockSetBooks(_books).
                                                 MockSetBookGenrePair(_bookGenrePairs).Object;

            bool result = new LibraryObjectService(library).AddGenreToBook(book_id, genre_id);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tests correct execute of getting all books of chosen genre.
        /// </summary>
        [TestMethod]
        public void TestGetAllGenreBooks_Correct()
        {
            var library = new MockDataProvider().MockSetGenres(_genres).
                                                 MockSetBooks(_books).
                                                 MockSetBookGenrePair(_bookGenrePairs).Object;

            int result = new LibraryObjectService(library).GetAllGenreBooks(genre_Id: 1).Count();

            Assert.IsTrue(result == 2);
        }

        /// <summary>
        /// Tests incorrect execute of getting all books of chosen genre .
        /// </summary>
        [TestMethod]
        [DataRow(3)]
        [DataRow(-1)]
        [DataRow(0)]
        public void TestGetAllGenreBooks_InCorrect(long id)
        {
            var library = new MockDataProvider().MockSetGenres(_genres).
                                                 MockSetBooks(_books).
                                                 MockSetBookGenrePair(_bookGenrePairs).Object;

            int result = new LibraryObjectService(library).GetAllGenreBooks(genre_Id: id).Count();

            Assert.IsTrue(result == 0);
        }
    }
}
