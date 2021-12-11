namespace GrpcServer.Common
{
    // TODO: 
    // 제네릭
    // thread safe lazy 초기화
    public abstract class Singleton<T> where T : class, new()
    {
        protected Singleton() { }
        
        // private static readonly Lazy<T> _instance = new Lazy<T>(() => new T());
        private static readonly Lazy<T> _instance = new(() => new T()); // 위의 코드 단순화

        public static T Instance { get { return _instance.Value; } }
    }
}