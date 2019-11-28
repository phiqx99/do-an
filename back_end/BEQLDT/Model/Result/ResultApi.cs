using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BEQLDT.Model.Result
{
    public class ResultApi
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int ErrorCode { get; set; }
        public object Data { get; set; }

        public ResultApi(bool success = true)
        {
            Success = success;
        }
    }
}
