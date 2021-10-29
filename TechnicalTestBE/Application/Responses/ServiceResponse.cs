using Domain.Dtos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Responses
{
    public static class ServiceResponse<T>
    {
        public static ResponseDto<IEnumerable<T>> Correct(IEnumerable<T> dataResult)
        {
            return new ResponseDto<IEnumerable<T>>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Operación Exitosa",
                Data = dataResult
            };
        }

        public static ResponseDto<T> Correct(T data)
        {
            return new ResponseDto<T>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Operación Exitosa",
                Data = data
            };
        }
        public static ResponseDto<T> Correct(string Message, T data)
        {
            return new ResponseDto<T>
            {
                StatusCode = HttpStatusCode.OK,
                Message = Message,
                Data = data
            };
        }


        public static ResponseDto<T> ServerError(string message, T data)
        {
            return new ResponseDto<T>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = message,
                Data = data
            };
        }

        public static ResponseDto<T> NoContent(string message, T data)
        {
            return new ResponseDto<T>
            {
                StatusCode = HttpStatusCode.NoContent,
                Message = message,
                Data = data
            };
        }

        public static ResponseDto<T> Conflict(string message, T data)
        {
            return new ResponseDto<T>
            {
                StatusCode = HttpStatusCode.Conflict,
                Message = message,
                Data = data
            };
        }
    }
}
