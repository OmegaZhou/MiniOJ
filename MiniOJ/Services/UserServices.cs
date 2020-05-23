using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccessor;
namespace MiniOJ.Services
{
    public class UserServices
    {
        private static string MD5_(string text)
        {
            var md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(Encoding.Default.GetBytes(text));
            return Encoding.Default.GetString(result);
        }
        public static User SignUp(string nickname,string password)
        {
            UserDao.AddUser(nickname, MD5_(password));
            return UserDao.CheckPassword(nickname, MD5_(password));
        }

        public static User SignIn(string nickname,string password)
        {
            return UserDao.CheckPassword(nickname, MD5_(password));
            
        }

        public static bool CheckNickname(string nickname)
        {
            return UserDao.IfNicknameExist(nickname);
        }
        public static User GetUser(string nickname)
        {
            var result= UserDao.SearchUser(nickname);
            if (result == null || result.UserId == -1)
            {
                return null;
            }
            else
            {
                return result;
            }
        }
        public static List<User>GetUsers(int start_from,int length)
        {
            return UserDao.GetUsers(start_from, length);
        }
    }
}
