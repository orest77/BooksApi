using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using BooksApi.Controllers;
using BooksApi.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.Language.Flow;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }


        //[Test]
        public async Task GetApiBooksTest()
        {
            //Arrange
            var testBooks = GetAllBooks();
            Mock<BooksController> mockRepo = new Mock<BooksController>();
            //BooksController booksController = new BooksController(mockRepo.Object);
            //InMemoryRepository m = new InMemoryRepository();

            //Act
            ////Assert

        }

        private List<Book> GetAllBooks()
        {
            List<Book> book = new List<Book>();
            book.Add(new Book(){Id = 1, Title = "Book",Author = "Man"});
            return book;
        }



        [Test]
        public void GetApiBookTest()
        {
            //Arrange
            
            Mock<InMemoryRepository> mockRepo = new Mock<InMemoryRepository>();
            mockRepo.Setup(x => x.Books);
            BooksController booksController = new BooksController(mockRepo.Object);

            //Act
            var actionResult =  booksController.GetBook(1);
            ////Assert
            Console.WriteLine($"{actionResult.GetAwaiter()}");

            Assert.That(actionResult, Is.EqualTo("Learning Python"));
        }

    }
}