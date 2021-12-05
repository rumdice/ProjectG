using StackExchange.Redis;

namespace GrpcServer.DB
{
    public class Redis
    {
        private ConnectionMultiplexer redisConnection;
        private IDatabase db;

        public Redis() { }

        public bool Init(string host, int port)
        {
            try
            {
                this.redisConnection = ConnectionMultiplexer.Connect(host + ":" + port);
                if (redisConnection.IsConnected)
                {
                    this.db = redisConnection.GetDatabase();
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

        public string GetString(string key)
        {
            return this.db.StringGet(key);
        }

        public bool SetString(string key, string val)
        {
            return this.db.StringSet(key, val);
        }
    }
}
