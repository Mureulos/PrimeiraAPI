using Azure;
using Microsoft.AspNetCore.Components.Authorization;
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

        public async Task<ResponseModel<List<AuthorModel>>> GetAuthors()
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
                response.Message = err.Message;
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

        public async Task<ResponseModel<List<AuthorModel>>> CreateAuthor(AuthorCreationDto authorCreationDto)
        {
            ResponseModel<List<AuthorModel>> response = new ResponseModel<List<AuthorModel>>();

            try
            {
                var author = new AuthorModel()
                {
                    Name = authorCreationDto.Name,
                    LastName = authorCreationDto.LastName,
                };

                _context.Add(author);
                await _context.SaveChangesAsync();

                response.Data = await _context.Authors.ToListAsync();
                response.Message = "Author's creation was successfully";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Can't create a author";
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<AuthorModel>>> UpdateAuthor(AuthorEditionDto authorEditionDto)
        {
            ResponseModel<List<AuthorModel>> response = new ResponseModel<List<AuthorModel>>();

            try
            {
                var author = await _context.Authors.FirstOrDefaultAsync(authorBase => authorBase.Id == authorEditionDto.Id);

                if (author == null)
                {
                    response.Message = "Author not found";
                    return response;
                }

                author.Name = authorEditionDto.Name;
                author.LastName = authorEditionDto.LastName;

                _context.Update(author);
                await _context.SaveChangesAsync();

                response.Data = await _context.Authors.ToListAsync();
                response.Message = "Author updated with successfully";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<AuthorModel>>> DeleteAuthor(int idAuthor)
        {
            ResponseModel<List<AuthorModel>> response = new ResponseModel<List<AuthorModel>>();

            try
            {
                var author = await _context.Authors.FirstOrDefaultAsync(authorBase => authorBase.Id == idAuthor);

                if (author == null)
                {
                    response.Message = "Author not found!";
                    return response;
                }

                _context.Remove(author);
                await _context.SaveChangesAsync();

                response.Data = await _context.Authors.ToListAsync();
                response.Message = "Author deleted with successafully!";
                return response;

            } catch (Exception ex) 
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }
    }
}
