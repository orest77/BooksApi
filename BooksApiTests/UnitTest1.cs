using BooksApi.Controllers;
using BooksApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetApiBooksTest()
        {
            //Arrange
            Book user = new Book() {Id = 1, Title = "Book", Author = "Man"};
            Mock<InMemoryRepository> mockRepo = new Mock<InMemoryRepository>();
            CancellationToken cancellationToken = default;
            mockRepo.Setup(p => p.Books.ToListAsync(cancellationToken)).ReturnsAsync(GetAllBooks());
            BooksController booksController = new BooksController(mockRepo.Object);
            //Act
            ActionResult<IEnumerable<Book>> result = await booksController.GetBooks();
            //
            Assert.AreEqual(result, user);
        }

        private List<Book> GetAllBooks()
        {
            List<Book> book = new List<Book>();
            book.Add(new Book() {Id = 1, Title = "Book", Author = "Man"});
            return book;
        }

        [Test]
        public async Task PostApiBookTest()
        {
            //Arrange
            CancellationToken cancellationToken = default;
            Book newUser = new Book() {Id = 3, Title = "Book about space", Author = "NLO"};
            Mock<InMemoryRepository> mockRepo = new Mock<InMemoryRepository>(MockBehavior.Loose);
            mockRepo.Setup(x => x.Books.AddAsync(newUser, cancellationToken));
            BooksController booksController = new BooksController(mockRepo.Object);

            //Act
            ActionResult<Book> actionResult = await booksController.PostBook(newUser);
            ////Assert
            Assert.That(actionResult, Is.EqualTo(GetAllBooks()));
        }

        [Test]
        public async Task GetApiBookTest()
        {
            //Arrange
            long indexUser = 1;
            Mock<InMemoryRepository> mockRepo = new Mock<InMemoryRepository>(MockBehavior.Loose);
            mockRepo.Setup(x => x.Books.FindAsync(indexUser))
                .ReturnsAsync(new Book() {Id = 1, Title = "Book", Author = "Man"});
            BooksController booksController = new BooksController(mockRepo.Object);

            //Act
            ActionResult<Book> actionResult = await booksController.GetBook(indexUser);
            ////Assert
            Assert.That(actionResult, Is.EqualTo(GetAllBooks()));
        }

        [Test]
        public async Task PutApiBookTest()
        {
            //Arrange
            CancellationToken cancellationToken = default;
            long Id = 1;
            Book newUser = new Book() {Id = 1, Title = "Book about space2", Author = "NLO"};
            Book oldUser = new Book {Title = "Learning Python", Author = "Mark Lutz"};
            Mock<InMemoryRepository> mockRepo = new Mock<InMemoryRepository>(MockBehavior.Strict);
            mockRepo.Setup(x => x.Entry(oldUser)).Verifiable();
            BooksController booksController = new BooksController(mockRepo.Object);

            //Act
            IActionResult actionResult = await booksController.PutBook(Id, newUser);
            ////Assert
        }

        [Test]
        public async Task DeleteApiBookTest()
        {
            //Arrange
            long rmId = 2;
            Book rmUser = new Book { Title = "Learning Java", Author = " Patrick Niemeyer, Daniel Leuck" };
            Mock<InMemoryRepository> mockRepo = new Mock<InMemoryRepository>(MockBehavior.Strict);
            mockRepo.Setup(x => x.Books.Remove(rmUser));
            BooksController booksController = new BooksController(mockRepo.Object);

            //Act
            IActionResult actionResult = await booksController.DeleteBook(rmId);
            ////Assert
        }
    }
}