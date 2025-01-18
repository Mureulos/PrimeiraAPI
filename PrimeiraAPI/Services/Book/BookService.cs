using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Data;
using PrimeiraAPI.Dto.Author;
using PrimeiraAPI.Dto.Book;
using PrimeiraAPI.Models;

namespace PrimeiraAPI.Services.Book
{
    public class BookService : IBookInterface
    {
        private readonly AppDbContext _context;

        public BookService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<BookModel>>> GetBooks()
        {
            ResponseModel<List<BookModel>> response = new ResponseModel<List<BookModel>>();

            try
            {
                var book = await _context.Books.Include(a => a.Author).ToListAsync();

                response.Data = book;
                response.Message = "All books were obtained";
                return response;

            } catch (Exception ex) 
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<BookModel>> GetBookById(int idBook)
        {
            ResponseModel<BookModel> response = new ResponseModel<BookModel>();

            try
            {
                var book = await _context.Books
                        .Include(a => a.Author)
                        .FirstOrDefaultAsync(bookBase => bookBase.Id == idBook);

                if (book == null)
                {
                    response.Message = "Book not found!";
                    return response;
                }

                response.Data = book;
                response.Message = "Book found!";
                return response;
            }
            catch (Exception ex) 
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<BookModel>>> GetBookByAuthorId(int idAuthor)
        {
            ResponseModel<List<BookModel>> response = new ResponseModel<List<BookModel>>();

            try
            {
                var book = await _context.Books
                    .Include(a => a.Author)
                    .Where(bookBase => bookBase.Author.Id == idAuthor)
                    .ToListAsync();

                if (book == null)
                {
                    response.Message = "Book not found!";
                    return response;
                }

                response.Data = book;
                response.Message = "Book found!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<BookModel>>> CreateBook(BookCreationDto bookCreationDto)
        {
            ResponseModel<List<BookModel>> response = new ResponseModel<List<BookModel>>();

            try
            {
                var author = await _context.Authors
                        .FirstOrDefaultAsync(authorBase => authorBase.Id == bookCreationDto.Author.Id);

                if (author == null)
                {
                    response.Message = "Author found!";
                    return response;
                }

                var book = new BookModel()
                {
                    Title = bookCreationDto.Title,
                    Author = author,
                };

                _context.Add(book);
                await _context.SaveChangesAsync();

                response.Data = await _context.Books.Include(a => a.Author).ToListAsync();
                response.Message = "Book create with successfully";
                return response;

            } catch (Exception ex) 
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<BookModel>>> UpdateBook(BookEditionDto bookEditionDto)
        {
            ResponseModel<List<BookModel>> response = new ResponseModel<List<BookModel>>();

            try
            {
                var book = await _context.Books
                    .Include(a => a.Author)
                    .FirstOrDefaultAsync(bookBase => bookBase.Id == bookEditionDto.Id);

                var author = await _context.Authors
                    .FirstOrDefaultAsync(authorBase => authorBase.Id == bookEditionDto.Author.Id);

                if (book == null)
                {
                    response.Message = "Book not found!";
                    return response;
                }

                if (author == null)
                {
                    response.Message = "Author found!";
                    return response;
                }

                book.Title = bookEditionDto.Title;
                book.Author = author;

                _context.Update(book);
                await _context.SaveChangesAsync();

                response.Data = await _context.Books.ToListAsync();
                response.Message = "Book updated with successfully";
                return response;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<BookModel>>> DeleteBook(int idBook)
        {
            ResponseModel<List<BookModel>> response = new ResponseModel<List<BookModel>>();

            try
            {
                var book = _context.Books.FirstOrDefaultAsync(bookBase => bookBase.Id == idBook);

                if (book == null)
                {
                    response.Message = "Book bot found";
                    return response;
                }

                _context.Remove(book);
                await _context.SaveChangesAsync();

                response.Data = await _context.Books.ToListAsync();
                response.Message = "Book delete with successfully";
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
