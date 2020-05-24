using DataAccessor;
using Microsoft.AspNetCore.Razor.Language;
using MiniOJ.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace MiniOJ.Services
{
    public class SubmissionServices
    {
        private static Dictionary<string, JudgeCore.Language> lang_map_ = new Dictionary<string, JudgeCore.Language>
        {
            { "CPP", JudgeCore.Language.CPP },
            {"C",JudgeCore.Language.C },
            {"JAVA",JudgeCore.Language.JAVA }
        };
        private static JudgeCore.Language GetLanguage(string str)
        {
            if (lang_map_.ContainsKey(str))
            {
                return lang_map_[str];
            }
            else
            {
                return JudgeCore.Language.CPP;
            }
        }
        public static void Judge(string code, int user_id, string problem_title, string lang)
        {
            OutputProblemInfo info = ProblemServices.GetProblemInfoInner(problem_title);
            var uuid = Guid.NewGuid().ToString("N");
            var l = GetLanguage(lang);
            Submission submission = new Submission
            {
                Code = code,
                ProblemId = info.SimpleInfo.ProblemId,
                Uuid = uuid,
                UserId = user_id,
                Lang = l.ToString()
            };
            SubmissionDao.AddSubmission(submission);
            Task task = new Task(() =>
              {
                  submission.Status = JudgeCore.JudgeStatus.Running.ToString();
                  SubmissionDao.ChangeSubmissionStatus(submission);
                  var simple_info = info.SimpleInfo;
                  JudgeCore.ProblemInfo judge_info = new JudgeCore.ProblemInfo()
                  {
                      code = submission.Code,
                      max_mem = simple_info.MaxMemory,
                      max_time = simple_info.MaxTime,
                      uuid = uuid,
                      right_result = info.RightResult,
                      test_case = info.TestCase,
                      language = l
                  };
                  var result = JudgeCore.Judger.judge(judge_info);
                  submission.Status = result.status.ToString();
                  submission.Time = result.time;
                  submission.Memory = result.memory;
                  SubmissionDao.SetResult(submission);
              });
            task.Start();
        }

        public static List<Submission> GetSubmissions(int start_from,int len)
        {
            return SubmissionDao.GetSubmissions(start_from, len);
        }

        public static List<Submission> GetSubmissions(string nickname,int start_from,int len)
        {
            return SubmissionDao.GetSubmissionsByOne(nickname, start_from, len);
        }
        public static void SubmissionsFilter(List<Submission> submissions,int? user_id)
        {
            foreach(var item in submissions)
            {
                if (item.UserId != user_id)
                {
                    item.Code = null;
                }
            }
        }
    }
}
