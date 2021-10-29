using Application.IServices;
using Application.Mappers;
using Application.Responses;

using Domain.Dtos;
using Persistence.Entities;
using Persistence.Factory;

using System;
using System.Collections.Generic;

namespace Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepositoryFactory _repository;
        
        public AuthorService(IRepositoryFactory repository)
        {
            _repository = repository;
        }

        public ResponseDto<IEnumerable<AuthorDto>> GetAuthors()
        {
            var authors = _repository.Authors.GetAll();
            var authorDtos = AuthorMapper.Mapper().CreateMapper().Map<IEnumerable<Author>, IEnumerable<AuthorDto>>(authors);

            return ServiceResponse<AuthorDto>.Correct(authorDtos);
        }

        public ResponseDto<AuthorDto> GetAuthorById(int id)
        {
            var author = _repository.Authors.GetById(id);
            var authorDto = AuthorMapper.Mapper().CreateMapper().Map<Author, AuthorDto>(author);

            return ServiceResponse<AuthorDto>.Correct(authorDto);
        }

        public ResponseDto<AuthorDto> GetAuthorByName(string name)
        {
            var author = _repository.Authors.GetFirstOrDefault(x => x.Name == name);
            var authorDto = AuthorMapper.Mapper().CreateMapper().Map<Author, AuthorDto>(author);

            return ServiceResponse<AuthorDto>.Correct(authorDto);

        }

        public ResponseDto<AuthorDto> CreateAuthor(AuthorDto autorDto)
        {
            try
            {
                var exists = _repository.Authors.GetFirstOrDefault(x => x.Name == autorDto.NombreCompleto);
                if (exists != null)
                    throw new Exception("El autor ya ha sido registrado");

                var author = AuthorMapper.Mapper().CreateMapper().Map<AuthorDto, Author>(autorDto);

                var authorInserted = _repository.Authors.Insert(author);
                _repository.Commit();

                var newAutor = AuthorMapper.Mapper().CreateMapper().Map<Author, AuthorDto>(authorInserted);
                return ServiceResponse<AuthorDto>.Correct(newAutor);
            }
            catch(Exception ex)
            {
                return ServiceResponse<AuthorDto>.ServerError(ex.Message, null);
            }
        }

        public ResponseDto<bool> UpdateAuthor(AuthorDto authorDto)
        {
            try
            {
                var author = AuthorMapper.Mapper().CreateMapper().Map<AuthorDto, Author>(authorDto);
                _repository.Authors.Update(author);
                return ServiceResponse<bool>.Correct(true);
            }
            catch(Exception ex)
            {
                return ServiceResponse<bool>.ServerError(ex.Message, false);
            }
        }

        public ResponseDto<bool> DeleteAuthorById(int id)
        {
            try
            {
                _repository.Authors.DeleteById(id);
                _repository.Commit();
                return ServiceResponse<bool>.Correct(true);
            }
            catch
            {
                return ServiceResponse<bool>.NoContent("Error al intentar borrar el Autor", false);
            }
        }
    }
}
