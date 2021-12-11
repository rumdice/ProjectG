namespace GrpcServer.Common
{
    // TODO: 
    // 클래스 래핑
    // 필요한 기능 추가해서 사용
    // NLog 자세한 사용은 메뉴얼 참조
    public class NLogger
    {
        public static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();
    }
}

