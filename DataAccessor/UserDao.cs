using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace DataAccessor
{
    public class User
    {
        public int UserId { get; set; }
        public string Nickname { get; set; }
        public int SolvedNumber { get; set; }
    }
    public class UserDao
    {
        static public User SearchUser(string nickname)
        {
            using (var connection = ConnectionGetter.GetConnection())
            {
                var sql = "select User.UserId, Nickname, count(distinct ProblemId) as SolvedNumber" +
                    " from User left join (select * from submission where Status='Accept') as tmp " +
                    " on User.UserId=tmp.UserId  where nickname=@nickname";
                return connection.QueryFirst<User>(sql, new { nickname });
            }
        }

        static public bool IfNicknameExist(string nickname)
        {
            using (var connection = ConnectionGetter.GetConnection())
            {
                return connection.QueryFirst<int>("Select count(UserId) from User where Nickname=@nickname", new { nickname }) == 1;
            }
        }

        static public bool AddUser(string nickname, string password)
        {

            using (var connection = ConnectionGetter.GetConnection())
            {
                string sql_str = "Insert into User(Nickname,Password) values(@nickname,@password)";
                return connection.Execute(sql_str, new { nickname, password }) == 1;
            }

        }

        static public List<User> GetUsers(int start_from, int length)
        {
            using (var connection = ConnectionGetter.GetConnection())
            {
                var sql = "select User.UserId, Nickname, count(distinct ProblemId) as SolvedNumber" +
                    " from User left join (select * from submission where Status='Accept') as tmp" +
                    " on User.UserId=tmp.UserId " +
                    " group by UserId order by SolvedNumber DESC limit @start_from,@length";
                return connection.Query<User>(sql, new { start_from, length }).ToList();
            }
        }

        static public bool CheckPassword(string nickname, string password)
        {
            using (var connection = ConnectionGetter.GetConnection())
            {
                var sql = "select count(*) from user where nickname=@nickname and password=@password";
                return connection.QueryFirst<int>(sql, new { nickname, password }) == 1;
            }
        }
    }
}
