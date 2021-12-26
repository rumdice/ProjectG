using Newtonsoft.Json;
using StackExchange.Redis;

namespace GrpcServer.DB
{

    public class RedisConfig
    {
        public string host;
        public int port;
        public int db;
    }

    public class Redis
    {
        private static ConnectionMultiplexer redisConnection;
        private static IDatabase redis;
        private static RedisConfig config;

        public Redis()
        {
            LoadConfig("RedisLocalConfig.json");
        }

        // TODO config 읽기 범용성 있게.
        private static void LoadConfig(string configFileName)
        {
            var dir = Directory.GetCurrentDirectory();
            var redisConfigPath = Path.Join(dir, $"Config/{configFileName}");
            var file = File.ReadAllText(redisConfigPath);
            if (file is not null)
                config = JsonConvert.DeserializeObject<RedisConfig>(file);

            // TODO: 예외 사항 처리?
        }

        public bool Connect()
        {
            try
            {
                redisConnection = ConnectionMultiplexer.Connect(config.host + ":" + config.port);
                if (redisConnection.IsConnected)
                {
                    redis = redisConnection.GetDatabase();
                    Console.WriteLine($"Redis Connect! {config.host}:{config.port} db:{config.db}");
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
