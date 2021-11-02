using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace church_mgt_dtos.Dtos
{
    public class Response<T>
    {
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public string Errors { get; set; }

        public Response(int statusCode, bool success, string msg, T data, string errors)
        {
            Data = data;
            Succeeded = success;
            StatusCode = statusCode;
            Message = msg;
            Errors = errors;
        }
        public Response()
        {
        }

        /// <summary>
        /// Sets the data to the appropriate response
        /// at run time
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static Response<T> Fail(string errorMessage, int statusCode = StatusCodes.Status404NotFound)
        {
            return new Response<T> { Succeeded = false, Message = errorMessage, StatusCode = statusCode };
        }
        public static Response<T> Success(string successMessage, T data, int statusCode = StatusCodes.Status200OK)
        {
            return new Response<T> { Succeeded = true, Message = successMessage, Data = data, StatusCode = statusCode };
        }
        public override string ToString() => JsonConvert.SerializeObject(this);
       
    }
}
