using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniOJ.Entity;
using DataAccessor;
using MiniOJ.Services;
using System.Runtime.CompilerServices;

namespace MiniOJ.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost("SignUp")]
        public Result SignUp([FromBody] SignInInfo signInInfo)
        {
            if (UserServices.CheckNickname(signInInfo.Nickname))
            {
                throw new UserException(UserException.Type.NickExists);
            }
            var user = Services.UserServices.SignUp(signInInfo.Nickname, signInInfo.Password);
            if (user== null)
            {
                throw new MySytemException();
            }
            else
            {
                HttpContext.Session.SetString("UserId", user.UserId.ToString());
                HttpContext.Session.SetString("UserName", user.Nickname);
                return new Result<User> { IsSucess = true, Data = user };
            }
        }

        [HttpPost("SignIn")]
        public Result SignIn([FromBody] SignInInfo signInInfo)
        {
            var name = HttpContext.Session.GetString("UserName");
            if (name != null)
            {
                throw new UserException(UserException.Type.AlreadySignIn);
            }
            var user = UserServices.SignIn(signInInfo.Nickname, signInInfo.Password);
            if (user == null)
            {
                return new Result { IsSucess = true, Message = "Nickname or password wrong" };
            }
            else
            {
                HttpContext.Session.SetString("UserId", user.UserId.ToString());
                HttpContext.Session.SetString("UserName", user.Nickname);
                return new Result<User> { IsSucess = true, Data = user };
            }
        }

        [HttpGet("GetUsers/{start_from}/{lenght}")]
        public Result<List<User>> GetUsers(int start_from,int length)
        {
            return new Result<List<User>> { Data = UserServices.GetUsers(start_from, length) };
        }
        [HttpGet("GetUser/{nickname}")]
        public Result<User> GetUser(string nickname)
        {
            return new Result<User> { Data = UserServices.GetUser(nickname) };
        }

        [HttpGet("GetMe")]
        public Result<User> GetMe()
        {
            var name = HttpContext.Session.GetString("UserName");
            if (name == null)
            {
                throw new UserException(UserException.Type.NotSiginIn);
            }
            return new Result<User> { Data = UserServices.GetUser(name) };
        }
    }
}
