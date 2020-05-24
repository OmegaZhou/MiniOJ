using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniOJ.Entity;
using MiniOJ.Services;

namespace MiniOJ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        [HttpPost("Judge")]
        public Result Judge([FromForm] JudgeInfo info)
        {
            var user_id = HttpContext.Session.GetInt32("UserId");
            if (user_id ==null)
            {
                throw new UserException(UserException.Type.NotSiginIn);
            }
            SubmissionServices.Judge(info.Code, user_id.Value, info.Title, info.Lang);
            return new Result();
        }

        [HttpGet("GetSubmissions/{start_from}/{len}")]
        public Result GetSubmissions(int start_from,int len)
        {
            var user_id = HttpContext.Session.GetInt32("UserId");
            var items = SubmissionServices.GetSubmissions(start_from, len);
            SubmissionServices.SubmissionsFilter(items, user_id);
            return new Result<List<Submission>> { Data = items };
        }


        [HttpGet("GetSubmissionsByMe/{start_from}/{len}/{nickname}")]
        public Result GetSubmissionsByMe(int start_from,int len,string nickname)
        {
            var user_id = HttpContext.Session.GetInt32("UserId");
            var items = SubmissionServices.GetSubmissions(nickname,start_from, len);
            SubmissionServices.SubmissionsFilter(items, user_id);
            return new Result<List<Submission>> { Data = items };
        }
    }
}
