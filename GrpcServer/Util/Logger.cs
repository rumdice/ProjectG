using NLog;
using System;

namespace GrpcServer.Util
{
    //public class SingleTon<T> where T : class, new()
    //{
    //    //private SingleTon() { }

    //    // private static readonly Lazy<T> _instance = new Lazy<T>(() => new T());
    //    // 위의 코드 new 단순화
    //    private static readonly Lazy<T> _instance = new(() => new T());

    //    public static T Instance { get { return _instance.Value; } }
    //}

    // TODO: 
    // 제네릭
    // thread safe lazy 초기화
    public class nLogger
    {
        public static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        //public NLog.Logger Instance() { return Logger; } 
    }
}

