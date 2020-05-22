using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySql;
namespace DataAccessor
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ProblemDao.AddProblem(new Problem { Title = "1", MaxMemory = 10, MaxTime = 1 });
                ProblemDao.AddProblem(new Problem { Title = "2", MaxMemory = 10, MaxTime = 1 });
                ProblemDao.AddProblem(new Problem { Title = "3", MaxMemory = 10, MaxTime = 1 });
                Console.WriteLine(ProblemDao.IfProblemExists("3"));
                Console.WriteLine(ProblemDao.IfProblemExists("4"));
                var i=ProblemDao.GetProblems(0, 2);
                UserDao.AddUser("u1", "aaaa");
                UserDao.AddUser("u2", "aaa");
                UserDao.AddUser("u3", "aaa");
                Console.WriteLine(UserDao.CheckPassword("u2", "aaa"));
                Console.WriteLine(UserDao.CheckPassword("u1", "aaa"));
                var i1=UserDao.SearchUser("u1");
                var i3=UserDao.IfNicknameExist("u1");

                SubmissionDao.AddSubmission(new Submission { UserId = 1, ProblemId = 1, Uuid = "ssss" });
                SubmissionDao.ChangeSubmissionStatus(new Submission { Status = "Accept", Uuid = "ssss" });
                SubmissionDao.AddSubmission(new Submission { UserId = 1, ProblemId = 1, Uuid = "sssss" });
                SubmissionDao.SetResult(new Submission { Status = "Accept", Uuid = "sssss",Memory=9,Time=1000 });
                SubmissionDao.AddSubmission(new Submission { UserId = 1, ProblemId = 2, Uuid = "sasss" });
                SubmissionDao.SetResult(new Submission { Status = "Accept", Uuid = "sasss", Memory = 9, Time = 1000 });
                SubmissionDao.AddSubmission(new Submission { UserId = 2, ProblemId = 2, Uuid = "saasss" });
                SubmissionDao.SetResult(new Submission { Status = "Accept", Uuid = "saasss", Memory = 9, Time = 1000 });

                var i4=SubmissionDao.GetSubmissions(0, 3);

                var i5=UserDao.GetUsers(0,2);
                //Console.WriteLine(UserDao.AddUser("ddd", "dasdasdas"));
                //Console.WriteLine(UserDao.IfNicknameExist("dddd"));
                //var i = UserDao.SearchUser("ddd");
                //Console.WriteLine(i.Nickname);
                //var items = UserDao.GetUsers(0, 10);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Utils.Logger.GetInstance().Error(e.Message);
            }
            
        }
    }
}
