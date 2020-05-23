using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniOJ.Entity;
using MiniOJ.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiniOJ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemController : ControllerBase
    {
        [HttpPost("AddProblem")]
        public Result AddProblem()
        {
            if (HttpContext.Session.GetString("UserId") == null)
            {
                throw new UserException(UserException.Type.NotSiginIn);
            }
            var data = HttpContext.Request.Form;
            InputProblemInfo inputProblemInfo = new InputProblemInfo();
            inputProblemInfo.Content = data.Files.GetFile("content");
            inputProblemInfo.RightResult = data.Files.GetFile("right_result");
            inputProblemInfo.TestCase = data.Files.GetFile("test_case");
            foreach(var item in data.Files)
            {
                if (item.ContentType != "text/plain")
                {
                    throw new UserException(UserException.Type.InputError);
                }
            }
            try
            {
                inputProblemInfo.SimpleInfo = new Problem
                {
                    MaxMemory = int.Parse(data["max_memory"].ToString()),
                    MaxTime = int.Parse(data["max_time"].ToString()),
                    Title = data["title"].ToString()
                };
            }
            catch (Exception)
            {
                throw new UserException(UserException.Type.InputError);
            }
            if (ProblemServices.CheckTitle(inputProblemInfo.SimpleInfo.Title))
            {
                throw new UserException(UserException.Type.ProblemExists);
            }
            ProblemServices.AddProblem(inputProblemInfo);
            return new Result();
        }

        [HttpGet("GetProblems/{start_from}/{len}")]
        public List<Problem> GetProblems(int start_from,int len)
        {
            return ProblemServices.GetProblems(start_from, len);
        }

        [HttpGet("GetProblem/{title}")]
        public OutputProblemInfo GetProblem(string title)
        {
            return ProblemServices.GetProblemInfo(title);
        }
    }
}
