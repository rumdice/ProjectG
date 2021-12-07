using CsvHelper;
using Grpc.Core;
using GrpcProtocol;
using GrpcServer.DB;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace GrpcServer.Services
{
    public class LoginService : Login.LoginBase
    {

        // 로거 인스턴스 생성
        // TODO: 전역에서 단 한번만 선언하고 여러곳에서 사용하게끔
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

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
                    Logger.Error(ex, "Mysql Error!");
                }
            }

            // 3-1. redis
            // TODO: 서버 초기 실행시 Init 하는 것으로 이동
            // 레디스 셋팅 및 사용 예시 
            var redis = new Redis();
            redis.Init("localhost", 6379);

            var uuidKey = Guid.NewGuid().ToString();
            redis.SetString(uuidKey, "100");


            // 4. logging
            Logger.Debug("Debug log");
            Logger.Info("Debug log");
            Logger.Warn("Debug log");

            var logMsg = "aabbcc";
            Logger.Warn($"Debug log {logMsg}");
            Logger.Warn("Debug log {0}", logMsg);


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
            }); ;
        }
    }
}