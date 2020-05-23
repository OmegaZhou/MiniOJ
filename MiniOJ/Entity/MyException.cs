using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MiniOJ.Entity
{
    public class MyException: Exception
    {
        public Result Result { get; protected set; }
    }
    public class MySytemException: MyException
    {
        public MySytemException()
        {
            Result = new Result { IsSucess = false, Message = "System Error" };
        }
    }

    public class UserException : MyException
    {
        public enum Type
        {
            NickExists,AlreadySignIn,NotSiginIn,ProblemExists,InputError
        }
        public UserException(Type type)
        {
            Result = new Result { IsSucess = true, Message = GetMessageFromType(type) };
        }
        private string GetMessageFromType(Type type)
        {
            switch (type)
            {
                case Type.NickExists:
                    return "Nickname already exists";
                case Type.AlreadySignIn:
                    return "Already sign in";
                case Type.NotSiginIn:
                    return "Please sigin in";
                case Type.ProblemExists:
                    return "The problem title is used";
                case Type.InputError:
                    return "The input is wrong";
                default:
                    return "Unknow exception";
            }
        }
    }
}
