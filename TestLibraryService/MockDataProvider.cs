namespace TestLibraryService
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessLogic_BookAPI.Services;
    using BusinessLogic_BookAPI.Models;
    using Moq;

    public class MockDataProvider : Mock<IDataProvider>
    {
        public MockDataProvider MockSetAuthors(List<Author> authors)
        {
            Setup(lib => lib.SetAuthors()).Returns(authors);
            return this;
        }

        public MockDataProvider MockSetBooks(List<Book> books)
        {
            Setup(lib => lib.SetBooks()).Returns(books);
            return this;
        }

        public MockDataProvider MockSetGenres(List<Genre> genres)
        {
            Setup(lib => lib.SetGenres()).Returns(genres);
            return this;
        }

        public MockDataProvider MockSetBookAuthorPair(List<BookAuthorPair> bookAuthorPair)
        {
            Setup(lib => lib.SetBooksAuthors()).Returns(bookAuthorPair);
            return this;
        }

        public MockDataProvider MockSetBookGenrePair(List<BookGenrePair> bookGenrePairs)
        {
            Setup(lib => lib.SetBooksGenres()).Returns(bookGenrePairs);
            return this;
        }
    }
}
