using StackExchange.Redis;

namespace GrpcServer.DB
{
    public class Redis
    {
        private static ConnectionMultiplexer redisConnection;
        private static IDatabase redis;
        private string host;
        private int port;
        private int db;

        public Redis() 
        {
            this.host = "localhost";
            this.port = 16379;
        }


        public bool Connect()
        {
            try
            {
                redisConnection = ConnectionMultiplexer.Connect(this.host + ":" + this.port);
                if (redisConnection.IsConnected)
                {
                    redis = redisConnection.GetDatabase();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                // TODO: 에러 메시지 처리 방안 마련
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public static string GetString(string key)
        {
            return redis.StringGet(key);
        }

        public static bool SetString(string key, string val)
        {
            return redis.StringSet(key, val);
        }
    }
}
