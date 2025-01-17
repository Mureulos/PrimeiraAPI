using PrimeiraAPI.Dto.Author;
using PrimeiraAPI.Models;

namespace PrimeiraAPI.Services.Author
{
    public interface IAuthorInterface
    {
        Task<ResponseModel<List<AuthorModel>>> ListAuthors();
        Task<ResponseModel<AuthorModel>> GetAuthorById(int idAuthor);
        Task<ResponseModel<AuthorModel>> GetAuthorByBookId(int idBook);
        Task<ResponseModel<List<AuthorModel>>> CreateAuthor(AuthorCreationDto authorCriacaoDto);
    }
}
