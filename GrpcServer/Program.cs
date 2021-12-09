using GrpcServer.DB;
using GrpcServer.Services;
using GrpcServer.Util;
using NLog;

//private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.

// Init
//var log = new nLogger(); // 전역적인 로그. 좀 더 스마트한 방법...찾기
//log.Instance().Debug("aaa");
// nLogger.Logger.Debug("aaa");

Redis.Init("127.0.0.1", 6379);

app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();


//namespace GrpcServer
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
            
//        }
//    }
//}