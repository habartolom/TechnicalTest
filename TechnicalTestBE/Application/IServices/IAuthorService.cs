using Domain.Dtos;

using Persistence.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IAuthorService
    {
        ResponseDto<IEnumerable<AuthorDto>> GetAuthors();
        ResponseDto<AuthorDto> GetAuthorById(int id);
        ResponseDto<AuthorDto> GetAuthorByName(string name);
        ResponseDto<AuthorDto> CreateAuthor(AuthorDto author);
        ResponseDto<bool> UpdateAuthor(AuthorDto author);
        ResponseDto<bool> DeleteAuthorById(int id);
    }
}
