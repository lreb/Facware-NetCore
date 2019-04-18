using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Facware.Library.Utility.GlobalErrorHandling
{
    public class ErrorDetails
    {
        public ErrorDetails()
        {
        }
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public bool Warning { get; set; }
        public bool Fail { get; set; }
        public string Message { get; set; }
        public List<string> MessageDetail { get; set; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
