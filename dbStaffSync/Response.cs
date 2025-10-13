using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace dbStaffSync
{
    public class Response<T>
    {
        public bool IsSuccess { get; set; }   // success/failure flag
        public string ErrorMessage { get; set; }  // error details if failed
        public T Data { get; set; }   // actual data (int, model, etc.)

        // Helper constructors / methods
        public static Response<T> Success(T data)
        {
            return new Response<T>
            {
                IsSuccess = true,
                Data = data
            };
        }

        public static Response<T> Fail(string errorMessage)
        {
            return new Response<T>
            {
                IsSuccess = false,
                ErrorMessage = errorMessage
            };
        }

    }
}
