using GrpcServer.Common;
using GrpcServer.DB;
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

            // Init (mysql, redis, config table...)
            // TODO: 
            Redis.Init("127.0.0.1", 6379);
            TablePaser.Load();
            
            app.MapGrpcService<GreeterService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}