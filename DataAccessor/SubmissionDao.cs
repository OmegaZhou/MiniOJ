using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessor
{
    public class Submission
    {
        public int SubmissionId { get; set; }
        public int UserId { get; set; }
        public int ProblemId { get; set; }
        public string Uuid { get; set; }
        public string Code { get; set; }
        public string Lang { get; set; }
        public string Status { get; set; }
        public string CreateTime { get; set; }
        public int Memory { get; set; }
        public int Time { get; set; }
        public string NickName { get; set; }
    }
    public class SubmissionDao
    {

        static public bool AddSubmission(Submission submission)
        {
            using(var connection = ConnectionGetter.GetConnection())
            {
                var sql = "insert into submission(UserId,ProblemId,Status,Code,Lang,Uuid) values(@UserId,@ProblemId,@Status,@Code,@Lang,@Uuid)";
                return connection.Execute(sql, submission)==1;
            }
        }

        static public bool ChangeSubmissionStatus(Submission submission)
        {
            using (var connection = ConnectionGetter.GetConnection())
            {
                var sql = "update Submission set Status=@Status where Uuid=@Uuid";
                return connection.Execute(sql, submission) == 1;
            }
        }
        static public bool SetResult(Submission submission)
        {
            using (var connection = ConnectionGetter.GetConnection())
            {
                var sql = "update Submission set Status=@Status,Time=@Time,Memory=@Memory where Uuid=@Uuid";
                return connection.Execute(sql, submission) == 1;
            }
        }

        static public List<Submission> GetSubmissions(int start_from,int length)
        {
            using (var connection = ConnectionGetter.GetConnection())
            {
                var sql = "select * from submission natrual join (select UserId,Nickname from User) as tmp " +
                    " order by CreateTime DESC limit @start_from,@length";
                return connection.Query<Submission>(sql,new { start_from, length }).ToList();
            }
        }
        
    }
}
