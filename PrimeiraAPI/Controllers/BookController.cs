using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeiraAPI.Dto.Book;
using PrimeiraAPI.Models;
using PrimeiraAPI.Services.Author;
using PrimeiraAPI.Services.Book;

namespace PrimeiraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookInterface _bookInterface;

        public BookController(IBookInterface bookInterface)
        {
            _bookInterface = bookInterface;
        }  

        [HttpGet("GetBook")]
        public async Task<ActionResult<ResponseModel<List<BookModel>>>> GetBook()
        {
            var book = await _bookInterface.GetBooks();
            return Ok(book);
        }

        [HttpGet("GetBookById/{idBook}")]
        public async Task<ActionResult<ResponseModel<BookModel>>> GetBookById(int idBook)
        {
            var book = await _bookInterface.GetBookById(idBook);
            return Ok(book);
        }

        [HttpGet("GetBookByAuthorId/{idAuthor}")]
        public async Task<ActionResult<ResponseModel<BookModel>>> GetBookByAuthorId(int idBook)
        {
            var book = await _bookInterface.GetBookByAuthorId(idBook);
            return Ok(book);
        }

        [HttpPost("CreateBook")]
        public async Task<ActionResult<ResponseModel<List<BookModel>>>> CreateBook(BookCreationDto bookCreationDto)
        {
            var book = await _bookInterface.CreateBook(bookCreationDto);
            return Ok(book);
        }

        [HttpPut("UpdateBook")]
        public async Task<ActionResult<ResponseModel<List<BookModel>>>> UpdateBook(BookEditionDto bookEditionDto)
        {
            var book = await _bookInterface.UpdateBook(bookEditionDto);
            return Ok(book);
        }

        [HttpDelete("DeleteBook")]
        public async Task<ActionResult<ResponseModel<BookModel>>> DeleteBook(int idBook)
        {
            var book = await _bookInterface.DeleteBook(idBook);
            return Ok(book);
        }
    }
}
