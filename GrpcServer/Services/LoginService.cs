using CsvHelper;
using Grpc.Core;
using GrpcProtocol;
using GrpcServer.DB;
using GrpcServer.Util;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace GrpcServer.Services
{
    public class LoginService : Login.LoginBase
    {
        // csv temp class
        public class Item
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public int Type { get; set; }
            public int Count { get; set; }
        }

        public override Task<LoginReply> LoginRpc(LoginRequest request, ServerCallContext context)
        {
            // 1.입력받은 인자값 유효성 확인

            // 2.서버 로직

            // 3.db process
            // mysql 예시 사용 
            // TODO: 클래스 화 시키기
            using (MySqlConnection connection = new MySqlConnection("Server=localhost;Port=3306;Database=coding32;Uid=root;Pwd=1111"))
            {
                string insertQuery = "INSERT INTO Co32table(idx,header,body) VALUES(3,'header1','body2')";
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(insertQuery, connection);

                    if (command.ExecuteNonQuery() == 1)
                    {
                        Console.WriteLine("인서트 성공");
                    }
                    else
                    {
                        Console.WriteLine("인서트 실패");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("실패");
                    Console.WriteLine(ex.ToString());

                    // error Log
                    nLogger.Logger.Error(ex, "Mysql Error!");
                }
            }

            // 3-1. redis
            // TODO: 서버 초기 실행시 Init 하는 것으로 이동
            // 레디스 셋팅 및 사용 예시 
            var uuidKey = Guid.NewGuid().ToString();
            Redis.SetString(uuidKey, "10000");
            

            // 4. logging
            nLogger.Logger.Debug("Debug log");
            nLogger.Logger.Info("Debug log");
            nLogger.Logger.Warn("Debug log");

            var logMsg = "aabbcc";
            nLogger.Logger.Warn($"Debug log {logMsg}");
            nLogger.Logger.Warn("Debug log {0}", logMsg);


            // 5.csv 
            // TODO: 임시 테이블 구조를 객체 형태로 파싱
            // 쿼리 수준으로 사용하기 좋은 클래스 제공 필요
            var tablePath = "../Table/test.csv";
            using (var reader = new StreamReader(tablePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Item>();
            }


            // 6.response
            var responseMessage = $"Welcome userid:{request.UserId} username:{request.UserName}";
            return Task.FromResult(new LoginReply
            {
                Error = ErrorType.Success,
                Message = responseMessage
            });
        }
    }
}