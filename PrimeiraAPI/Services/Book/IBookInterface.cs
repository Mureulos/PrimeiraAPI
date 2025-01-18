using PrimeiraAPI.Dto.Author;
using PrimeiraAPI.Dto.Book;
using PrimeiraAPI.Models;

namespace PrimeiraAPI.Services.Book
{
    public interface IBookInterface
    {
        Task<ResponseModel<List<BookModel>>> GetBooks();
        Task<ResponseModel<BookModel>> GetBookById(int idBook);
        Task<ResponseModel<List<BookModel>>> GetBookByAuthorId(int idAuthor);
        Task<ResponseModel<List<BookModel>>> CreateBook(BookCreationDto bookCreationDto);
        Task<ResponseModel<List<BookModel>>> UpdateBook(BookEditionDto bookEditionDto);
        Task<ResponseModel<List<BookModel>>> DeleteBook(int idBook);
    }
}
