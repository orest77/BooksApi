using System.ComponentModel.DataAnnotations;

namespace BooksApi.Models
{
    public class Book
    {
        [Key]
        public long Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }
    }
}
