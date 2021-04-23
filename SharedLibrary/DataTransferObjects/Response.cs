using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SharedLibrary.DataTransferObjects
{
    public class Response<TEntity> where TEntity : class
    {
        public TEntity Data { get; private set; }
        public int StatusCode { get; private set; }
        public ErrorDTOs Error { get; private set; }
        [JsonIgnore]
        public bool IsSuccessful { get; private set; }
        public static Response<TEntity> Success(TEntity data, int statusCode)
        {
            return new Response<TEntity> { Data = data, StatusCode = statusCode, IsSuccessful = true };
        }

        public static Response<TEntity> Success(int statusCode)
        {
            return new Response<TEntity> { Data = default, StatusCode = statusCode, IsSuccessful = true };
        }

        public static Response<TEntity> Fail(ErrorDTOs errorDTO, int statusCode)
        {
            return new Response<TEntity> { Error = errorDTO, StatusCode = statusCode , IsSuccessful = false};
        }
        public static Response<TEntity> Fail(string errorMessage, int statusCode, bool isShow)
        {
            return new Response<TEntity> { Error = new ErrorDTOs(errorMessage, isShow), StatusCode = statusCode, IsSuccessful = false };
        }
    }
}
