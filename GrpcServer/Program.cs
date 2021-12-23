using GrpcServer.Common;
using GrpcServer.Services;
using GrpcServer.Tables;

namespace GrpcServer
{
    class Program
    {
        static void Main(string[] args)
        {
            NLogger.Log.Info("Server Start.");
            
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddGrpc();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            // TODO: 서버 시작전 초기 셋팅 
            // Db, Config, Table, ETC...

            var Redis = new GrpcServer.DB.Redis();
            Redis.Connect();

            var Mysql = new GrpcServer.DB.MySql();
            Mysql.Connect();

            TablePaser.Load();
            
            app.MapGrpcService<GreeterService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            // 서버 시작
            app.Run();
        }
    }
}