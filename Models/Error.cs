using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public Error (string Code, string Message)
        {
            this.Code = Code;
            this.Message = Message;
        }
        public Error()
        {

        }
    }
}
