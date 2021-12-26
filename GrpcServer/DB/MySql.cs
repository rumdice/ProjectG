using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace GrpcServer.DB
{

    public class MySqlConfig
    {
        public string host;
        public int port;
        public string uid;
        public string pwd;
        public string database;
    }

    public class MySql
    {
        private static MySqlConnection connection;
        private static MySqlConfig config;
        private string connectString;

        public MySql()
        {
            LoadConfig("MysqlLocalConfig.json");

            this.connectString = @$"
                server = {config.host};
                port = {config.port};
                uid = {config.uid};
                pwd = {config.pwd};
                database = {config.database};
                Pooling = true;
                MIN Pool Size = 5;
                Max Pool Size = 10;
                Connection Timeout = 60;
                allow user variables = true;
                ";
        }

        // TODO: 리팩대상. 코드가 중복, mysql 연결 설정 뭐 여러가지 더 있음.
        private static void LoadConfig(string configFileName)
        {
            var dir = Directory.GetCurrentDirectory();
            var redisConfigPath = Path.Join(dir, $"Config/{configFileName}");
            var file = File.ReadAllText(redisConfigPath);
            if (file is not null)
                config = JsonConvert.DeserializeObject<MySqlConfig>(file);

            // TODO: 예외 사항 처리?
        }


        public bool Connect()
        {
            try
            {
                connection = new MySqlConnection(connectString);
                connection.Open();
                Console.WriteLine($"Mysql Connect! {config.host}:{config.port} db:{config.database}");
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
