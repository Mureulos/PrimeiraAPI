﻿using PrimeiraAPI.Dto.Author;
using PrimeiraAPI.Models;

namespace PrimeiraAPI.Services.Author
{
    public interface IAuthorInterface
    {
        Task<ResponseModel<List<AuthorModel>>> GetAuthors();
        Task<ResponseModel<AuthorModel>> GetAuthorById(int idAuthor);
        Task<ResponseModel<AuthorModel>> GetAuthorByBookId(int idBook);
        Task<ResponseModel<List<AuthorModel>>> CreateAuthor(AuthorCreationDto authorCriacaoDto);
        Task<ResponseModel<List<AuthorModel>>> UpdateAuthor(AuthorEditionDto authorEditionDto);
        Task<ResponseModel<List<AuthorModel>>> DeleteAuthor(int idAuthor);

    }
}
