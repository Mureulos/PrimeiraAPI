using PrimeiraAPI.Models;

namespace PrimeiraAPI.Dto.Book
{
    public class BookCreationDto
    {
        public string Title { get; set; }
        public AuthorModel Author { get; set; }

    }
}
