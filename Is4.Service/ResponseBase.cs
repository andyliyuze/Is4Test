using System;
using System.Collections.Generic;
using System.Text;

namespace Is4.Service
{
    public class ResponseBase<T>  
    {
        public T Data { get; set; }

        public string Message { get; set; }
    }
}
