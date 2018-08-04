using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V4Udaan.Api.Models
{
    public class ApiResponseWrapper<T> where T : class
    {
        public bool IsSuccessful { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

        public ApiResponseWrapper(bool IsSuccessful = false, T Data = null, string Message = "")
        {
            this.IsSuccessful = IsSuccessful;
            this.Data = Data;
            this.Message = Message;
        }
    }
}
