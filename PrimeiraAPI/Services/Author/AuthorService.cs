using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Data;
using PrimeiraAPI.Models;

namespace PrimeiraAPI.Services.Author
{
    public class AuthorService : IAuthorInterface
    {
        private readonly AppDbContext _context;
        public AuthorService(AppDbContext context) 
        {
            _context = context;
        }
        public Task<ResponseModel<AuthorModel>> GetAuthorByBookId(int bookId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<AuthorModel>> GetAuthorById(int idAuthor)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<List<AuthorModel>>> ListAuthors()
        {
            ResponseModel<List<AuthorModel>> response = new ResponseModel<List<AuthorModel>>();

            try
            {
                var authors = await _context.Authors.ToListAsync();
                response.Data = authors;
                response.Message = "All authors were obtained";
                return response;
            }
            catch (Exception err)
            {
                response.Message  = err.Message;
                response.Status = false;
                return response;
            }
        }
    }
}
