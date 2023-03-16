using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class CustomResponseDTO<T>
    {
        public T Data { get; set; }
        public List<string> Errors { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }

        // Factor Design Pattern --  static factor metot
        public static CustomResponseDTO<T> Success(T data,int StatusCode) {
            return new CustomResponseDTO<T> { Data= data,StatusCode=StatusCode };
        }
        public static CustomResponseDTO<T> Success(int StatusCode)
        {
            return new CustomResponseDTO<T> { StatusCode = StatusCode };
        }

        public static CustomResponseDTO<T> Fail(List<string> errors, int StatusCode)
        {
            return new CustomResponseDTO<T> {  StatusCode = StatusCode,Errors = errors };
        }

        public static CustomResponseDTO<T> Fail(string error, int StatusCode)
        {
            return new CustomResponseDTO<T> { StatusCode = StatusCode, Errors = new List<string> { error} };
        }
    }
}
