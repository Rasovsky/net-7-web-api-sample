using BookCatalogApi.Models;
using BookCatalogApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq; 
using BookCatalogApi.Tests.MockRepositories;
using Assert = Xunit.Assert;

namespace BookCatalogApi.Tests
{
    public  class BookServiceTests
    {
        [Test]
        public void GetBooks_ReturnsAllBooks()
        {
            var books = new List<Book>
        {
            new Book { Id = 1, Title = "Book 1", Author = "Author 1", ISBN = "ISBN 1" },
            new Book { Id = 2, Title = "Book 2", Author = "Author 2", ISBN = "ISBN 2" },
        };

            var mockBookRepository = new Mock<IBookRepository>();
            mockBookRepository.Setup(repo => repo.GetBooks()).Returns(books);
            var bookService = new BookService(mockBookRepository.Object);

            var result = bookService.GetBooks();

            Assert.Equal(books, result);
        }

        [Test]
        public void GetBookById_ValidId_ReturnsCorrectBook()
        {
            int id = 1;
            var book = new Book { Id = id, Title = "Book 1", Author = "Author 1", ISBN = "ISBN 1" };

            var mockBookRepository = new Mock<IBookRepository>();
            mockBookRepository.Setup(repo => repo.GetBookById(id)).Returns(book);
            var bookService = new BookService(mockBookRepository.Object);

            var result = bookService.GetBookById(id);

            Assert.Equal(book, result);
        }

        [Test]
        public void AddBook_ValidBook_AddsBookToRepository()
        {
            var newBook = new Book { Title = "New Book", Author = "New Author", ISBN = "New ISBN" };

            var mockBookRepository = new Mock<IBookRepository>();
            var bookService = new BookService(mockBookRepository.Object);

            bookService.AddBook(newBook);

            mockBookRepository.Verify(repo => repo.AddBook(newBook), Times.Once());
        }

        [Test]
        public void UpdateBook_ValidBook_UpdatesExistingBook()
        {
            var existingBook = new Book { Id = 1, Title = "Existing Book", Author = "Author 1", ISBN = "ISBN 1" };
            var updatedBook = new Book { Id = 1, Title = "Updated Book", Author = "Updated Author", ISBN = "Updated ISBN" };

            var mockBookRepository = new Mock<IBookRepository>();
            mockBookRepository.Setup(repo => repo.GetBookById(existingBook.Id)).Returns(existingBook);
            var bookService = new BookService(mockBookRepository.Object);

            bookService.UpdateBook(updatedBook);

            mockBookRepository.Verify(repo => repo.UpdateBook(updatedBook), Times.Once());
        }

        [Test]
        public void DeleteBook_ExistingBook_RemovesBookFromRepository()
        {
            var existingBook = new Book { Id = 1, Title = "Existing Book", Author = "Author 1", ISBN = "ISBN 1" };

            var mockBookRepository = new Mock<IBookRepository>();
            mockBookRepository.Setup(repo => repo.GetBookById(existingBook.Id)).Returns(existingBook);
            var bookService = new BookService(mockBookRepository.Object);

            bookService.DeleteBook(existingBook.Id);

            mockBookRepository.Verify(repo => repo.DeleteBook(existingBook.Id), Times.Once());
        }

    }
}
