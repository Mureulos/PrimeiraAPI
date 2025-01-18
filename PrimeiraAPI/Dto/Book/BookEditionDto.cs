using PrimeiraAPI.Models;

namespace PrimeiraAPI.Dto.Book
{
    public class BookEditionDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public AuthorModel Author { get; set; }
    }
}
