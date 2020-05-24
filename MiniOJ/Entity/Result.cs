using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniOJ.Entity
{
    public class Result
    {
        public bool IsSuccess { set; get; } = true;
        public string Message { set; get; } = "Sucess";
    }

    public class Result<T> : Result
    {
        public T Data { set; get; }
        public Result(T data)
        {
            Data = data;
        }
        public Result() : base()
        {

        }
    }
}
