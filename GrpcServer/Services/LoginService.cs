using Grpc.Core;
using GrpcProtocol;
using GrpcServer.DB;

namespace GrpcServer.Services
{
    public class LoginService : Login.LoginBase
    {
        public override Task<LoginReply> LoginRpc(LoginRequest request, ServerCallContext context)
        {
            // 1.입력받은 인자값 유효성 확인

            // 2.서버 로직
            // 로직 안에서 db 처리가 필요
            // 로직 안에서 db 처리가 필요
            // 로직 안에서 필요시 에러 로깅

            // 3.db process
            // mysql 예시 사용 
            // TODO: 클래스 화 시키기
            

            // 3-1. redis
            // TODO: 서버 초기 실행시 Init 하는 것으로 이동
            // 레디스 셋팅 및 사용 예시 
            var uuidKey = Guid.NewGuid().ToString();
            Redis.SetString(uuidKey, "10000");
            

            //// logging
            //NLogger.Log.Debug("Debug log");
            //NLogger.Log.Info("Debug log");
            //NLogger.Log.Warn("Debug log");

            //var logMsg = "aabbcc";
            //NLogger.Log.Warn($"Debug log {logMsg}");
            //NLogger.Log.Warn("Debug log {0}", logMsg);


            // response
            var responseMessage = $"Welcome userid:{request.UserId} username:{request.UserName}";
            return Task.FromResult(new LoginReply
            {
                Error = ErrorType.Success,
                Message = responseMessage
            });
        }
    }
}