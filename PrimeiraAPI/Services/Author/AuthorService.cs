using Azure;
using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Data;
using PrimeiraAPI.Dto.Author;
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

        public async Task<ResponseModel<AuthorModel>> GetAuthorByBookId(int idBook)
        {
            ResponseModel<AuthorModel> response = new ResponseModel<AuthorModel>();

            try
            {
                var book = await _context.Books
                    .Include(a => a.Author)
                    .FirstOrDefaultAsync(bookBase => bookBase.Id == idBook);

                if (book == null) {
                    response.Message = "No records found";
                    return response;
                }

                response.Data = book.Author;
                response.Message = "Authour found!";
                return response;
            }
            catch (Exception ex) 
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<AuthorModel>> GetAuthorById(int idAuthor)
        {
            ResponseModel<AuthorModel> response = new ResponseModel<AuthorModel>();

            try
            {
                var author = await _context.Authors.FirstOrDefaultAsync(authorBase => authorBase.Id == idAuthor);

                if (author == null) 
                {
                    response.Message = "No records found!";
                    return response;
                }

                response.Data = author;
                response.Message = "Author found!";
                return response;
            }
            catch (Exception ex) 
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
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

        public async Task<ResponseModel<List<AuthorModel>>> CreateAuthor(AuthorCreationDto authorCriacaoDto)
        {
            ResponseModel<List<AuthorModel>> response = new ResponseModel<List<AuthorModel>>();

            try
            {
                var author = new AuthorModel()
                {
                    Name = authorCriacaoDto.Name,
                    LastName = authorCriacaoDto.LastName,
                };

                _context.Add(author);
                await _context.SaveChangesAsync();

                response.Data = await _context.Authors.ToListAsync();
                response.Message = "Author's creation was successfully completed";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Can't create a author";
                response.Status = false;
                return response;
            }
        }
    }
}
