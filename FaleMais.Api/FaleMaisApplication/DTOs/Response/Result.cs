﻿namespace FaleMaisApplication.DTOs.Response
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? ErrorMessage { get; set; }

        public static Result<T> SuccessResult(T data)
        {
            return new Result<T> { Success = true, Data = data };
        }

        public static Result<T> ErrorResult(string errorMessage)
        {
            return new Result<T> { Success = false, ErrorMessage = errorMessage };
        }
    }
}
