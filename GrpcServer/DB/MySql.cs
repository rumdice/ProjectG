using MySql.Data.MySqlClient;

namespace GrpcServer.DB
{
    public class MySql
    {
        private string host;
        private int port;
        private string database;
        private static MySqlConnection connection;

        private string connectString;

        public MySql()
        {
            this.host = "localhost";
            this.port = 3306;
            this.database = "rumdice-master";

            this.connectString =
                $"server = {this.host};" +
                $"port = {this.port};" +
                $"uid = admin;" +
                "pwd = pass;" +
                $"database = {this.database};" +
                "Pooling = true;" +
                "MIN Pool Size = 5;" +
                "Max Pool Size = 10;" +
                "Connection Timeout = 60;" +
                "allow user variables = true;" +
                "";
        }



        public bool Connect()
        {
            try
            {
                connection = new MySqlConnection(connectString);
                connection.Open();

                return true;
            }
            catch (Exception e)
            {
                // TODO: 에러 메시지 처리 방안 마련
                Console.WriteLine(e.Message);
                return false;
            }
        }

    }
}
