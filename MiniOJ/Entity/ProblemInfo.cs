using DataAccessor;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniOJ.Entity
{
    public class InputProblemInfo
    {
        public Problem SimpleInfo { set; get; }
        public IFormFile Content { get; set; }
        public IFormFile TestCase { get; set; }
        public IFormFile RightResult { get; set; }
    }

    public class OutputProblemInfo
    {
        public Problem SimpleInfo { set; get; }
        public string Content { get; set; }
        public string TestCase { get; set; }
        public string RightResult { get; set; }
    }
}
