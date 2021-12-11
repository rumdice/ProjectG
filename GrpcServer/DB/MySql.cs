using GrpcServer.Common;
using MySql.Data.MySqlClient;

namespace GrpcServer.DB
{
    public class MySql
    {
        public MySql() { }

        public static bool Init()
        {
            // TODO: DB 연결만 시켜놓고 쿼리를 수행하는것은 로직부분으로 이동
            using (MySqlConnection connection = new MySqlConnection("Server=localhost;Port=3306;Database=coding32;Uid=root;Pwd=1111"))
            {
                string insertQuery = "INSERT INTO Co32table(idx,header,body) VALUES(3,'header1','body2')";
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(insertQuery, connection);

                    if (command.ExecuteNonQuery() == 1)
                    {
                        Console.WriteLine("인서트 성공");
                    }
                    else
                    {
                        Console.WriteLine("인서트 실패");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("실패");
                    Console.WriteLine(ex.ToString());

                    // error Log
                    NLogger.Log.Error(ex, "Mysql Error!");
                    return false;
                }
            }

            return true;
        }
    }
}
