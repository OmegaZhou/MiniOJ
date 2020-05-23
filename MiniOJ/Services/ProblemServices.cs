using DataAccessor;
using MiniOJ.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MiniOJ.Services
{
    public class ProblemServices
    {
        private static readonly string problem_path = "./problem/";
        private static readonly string content_path = "/problem.txt";
        private static readonly string test_case_path = "/test_case.txt";
        private static readonly string right_result_path = "/right_result.txt";
        public static void AddProblem(InputProblemInfo info)
        {
            var dir = problem_path + info.SimpleInfo.Title;
            try
            {
                Directory.CreateDirectory(dir);
                using (FileStream fs = new FileStream(dir + content_path, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    info.Content.CopyTo(fs);
                }
                using (FileStream fs = new FileStream(dir + test_case_path, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    info.TestCase.CopyTo(fs);
                }
                using (FileStream fs = new FileStream(dir + right_result_path, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    info.RightResult.CopyTo(fs);
                }
                ProblemDao.AddProblem(info.SimpleInfo);
            }
            catch (Exception)
            {
                Directory.Delete(dir, true);
                throw new MySytemException();
            }
        }

        public static OutputProblemInfo GetProblemInfoInner(string title)
        {
            var info = ProblemDao.GetProblem(title);
            if (info.Title == null)
            {
                throw new MySytemException();
            }
            var dir = problem_path + title;
            var result = new OutputProblemInfo { SimpleInfo = info };
            using (FileStream fs = new FileStream(dir + content_path, FileMode.Open, FileAccess.Read))
            {
                StreamReader reader = new StreamReader(fs);
                result.Content = reader.ReadToEnd();
            }
            using (FileStream fs = new FileStream(dir + test_case_path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                StreamReader reader = new StreamReader(fs);
                result.TestCase = reader.ReadToEnd();
            }
            using (FileStream fs = new FileStream(dir + right_result_path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                StreamReader reader = new StreamReader(fs);
                result.RightResult = reader.ReadToEnd();
            }
            return result;
        }

        public static bool CheckTitle(string title)
        {
            return ProblemDao.IfProblemExists(title);
        }

        public static List<Problem> GetProblems(int start_from, int len)
        {
            return ProblemDao.GetProblems(start_from, len);
        }

        public static OutputProblemInfo GetProblemInfo(string title)
        {
            var info = ProblemDao.GetProblem(title);
            if (info.Title == null)
            {
                throw new MySytemException();
            }
            var dir = problem_path + title;
            var result = new OutputProblemInfo { SimpleInfo = info };
            using (FileStream fs = new FileStream(dir + content_path, FileMode.Open, FileAccess.Read))
            {
                StreamReader reader = new StreamReader(fs);
                result.Content = reader.ReadToEnd();
            }
            return result;
        }
    }
}
