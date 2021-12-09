using StackExchange.Redis;

namespace GrpcServer.DB
{
    public class Redis
    {
        private static ConnectionMultiplexer redisConnection;
        private static IDatabase redis;

        public Redis() { }


        public static bool Init(string host, int port)
        {
            try
            {
                redisConnection = ConnectionMultiplexer.Connect(host + ":" + port);
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
