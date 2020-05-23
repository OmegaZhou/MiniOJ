using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessor
{
    public class Problem
    {
        public int ProblemId { get; set; }
        public string Title { get; set; } = null;
        public int MaxTime { get; set; }
        public int MaxMemory { get; set; }
    }
    public class ProblemDao
    {
        static public bool IfProblemExists(string title)
        {
            using (var connection = ConnectionGetter.GetConnection())
            {
                var sql = "select count(ProblemId) from problem where Title=@title";
                return connection.QueryFirst<int>(sql, new { title }) == 1;
            }
        }
        static public bool AddProblem(Problem problem)
        {
            using(var connection = ConnectionGetter.GetConnection())
            {
                var sql = "insert into problem(Title,MaxTime,MaxMemory) values(@Title,@MaxTime,@MaxMemory)";
                return connection.Execute(sql, problem) == 1;
            }
        }
        static public List<Problem> GetProblems(int start_from,int length)
        {
            using (var connection = ConnectionGetter.GetConnection())
            {
                var sql = "select * from problem limit @start_from,@length";
                return connection.Query<Problem>(sql, new { start_from,length }).ToList();
            }
        }

        static public Problem GetProblem(string title)
        {
            using (var connection = ConnectionGetter.GetConnection())
            {
                var sql = "select * from problem where title=@title";
                return connection.QueryFirstOrDefault<Problem>(sql, new { title });
            }
        }
    }
}
