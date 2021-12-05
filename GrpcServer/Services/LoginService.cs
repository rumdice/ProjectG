﻿using Grpc.Core;
using GrpcProtocol;
using GrpcServer.DB;
using MySql.Data.MySqlClient;

namespace GrpcServer.Services
{
    public class LoginService : Login.LoginBase
    {

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
                }
            }

            // 3-1 redis
            // TODO: 서버 초기 실행시 Init 하는 것으로 이동
            // 레디스 셋팅 및 사용 예시 
            var redis = new Redis();
            redis.Init("localhost", 6379);
            
            var uuidKey = Guid.NewGuid().ToString();
            redis.SetString(uuidKey, "100");
           


            // 4.response
            var responseMessage = $"Welcome userid:{request.UserId} username:{request.UserName}";
            return Task.FromResult(new LoginReply
            {
                ErrorCode = 0, // success 
                Message = responseMessage
            });;
        }
    }
}