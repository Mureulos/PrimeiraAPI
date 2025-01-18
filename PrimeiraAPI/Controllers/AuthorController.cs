using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeiraAPI.Dto.Author;
using PrimeiraAPI.Models;
using PrimeiraAPI.Services.Author;

namespace PrimeiraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorInterface _authorInterface;

        public AuthorController(IAuthorInterface authorInterface) 
        {
            _authorInterface = authorInterface;
        }

        [HttpGet("GetAuthors")]
        public async Task<ActionResult<ResponseModel<List<AuthorModel>>>> GetAuthors() 
        {
            var authors = await _authorInterface.GetAuthors();
            return Ok(authors);
        }

        [HttpGet("GetAuthorById/{idAuthor}")]
        public async Task<ActionResult<ResponseModel<AuthorModel>>> GetAuthorById(int idAuthor)
        {
            var author = await _authorInterface.GetAuthorById(idAuthor);
            return Ok(author);
        }

        [HttpGet("GetAuthorByBookId/{idBook}")]
        public async Task<ActionResult<ResponseModel<AuthorModel>>> GetAuthorByBookId(int idBook)
        {
            var author = await _authorInterface.GetAuthorByBookId(idBook);
            return Ok(author);
        }

        [HttpPost("CreateAuthor")]
        public async Task<ActionResult<ResponseModel<List<AuthorModel>>>> CreateAuthor(AuthorCreationDto authorCreationDto) 
        {
            var author = await _authorInterface.CreateAuthor(authorCreationDto);
            return Ok(author);
        }

        [HttpPut("UpdateAuthor")]
        public async Task<ActionResult<ResponseModel<List<AuthorModel>>>> UpdateAuthor(AuthorEditionDto authorEditionDto)
        {
            var author = await _authorInterface.UpdateAuthor(authorEditionDto);
            return Ok(author);
        }

        [HttpDelete("DeleteAuthor")]
        public async Task<ActionResult<ResponseModel<List<AuthorModel>>>> DeleteAuthor(int idAuthor)
        {
            var author = await _authorInterface.DeleteAuthor(idAuthor);
            return Ok(author);
        }
    }
}
