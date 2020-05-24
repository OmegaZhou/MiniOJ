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
        public Result SignUp([FromForm] SignInInfo signInInfo)
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
                SaveSignInStatus(user);
                return new Result<User> { IsSuccess = true, Data = user };
            }
        }

        [HttpPost("SignIn")]
        public Result SignIn([FromForm]SignInInfo signInInfo)
        {
            var name = HttpContext.Session.GetString("UserName");
            if (name != null)
            {
                throw new UserException(UserException.Type.AlreadySignIn);
            }
            var user = UserServices.SignIn(signInInfo.Nickname, signInInfo.Password);
            if (user == null)
            {
                return new Result { IsSuccess = true, Message = "Nickname or password wrong" };
            }
            else
            {
                SaveSignInStatus(user);
                return new Result<User> { IsSuccess = true, Data = user };
            }
        }

        [HttpGet("GetUsers/{start_from}/{length}")]
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

        [HttpGet("CheckSignInStatus")]
        public Result CheckSiginInStatus()
        {
            return new Result<string>(HttpContext.Session.GetString("UserName"));
        }

        [HttpPost("Logout")]
        public Result Logout()
        {
            HttpContext.Session.Clear();
            return new Result();
        }
        private void SaveSignInStatus(User user)
        {
            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("UserName", user.Nickname);
        }
    }
}
