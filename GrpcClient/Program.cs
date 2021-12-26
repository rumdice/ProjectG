using Grpc.Core;
using Grpc.Net.Client;
using GrpcProtocol;
using System;

using var channel = GrpcChannel.ForAddress("https://localhost:5001");

var loginClient = new Login.LoginClient(channel);
var loginReply = await loginClient.LoginRpcAsync(
    new LoginRequest
    {
        UserId = 11001,
        UserName = "UserName1"
    });
Console.WriteLine(@$"loginReply 
ErrorCode:{loginReply.Error}
Message:{loginReply.Message}");


var itemClient = new Item.ItemClient(channel);
var itemUseReply = await itemClient.ItemUseRpcAsync(
    new ItemUseRequest
    {
        UserId = 11002,
        UserName = "UserName2",
        ItemId = 1001990,
        ItemCount = 1,
    });
Console.WriteLine(@$"itemUseReply 
ErrorCode:{itemUseReply.Error}
RemainItems:{itemUseReply.RemainItems}");


var testClient = new Test.TestClient(channel);

//// 단항 호출 (클라가 보내야 서버가 응답을 함)
//var testResponse = await testClient.UnaryCallAsync(
//    new TestRequest
//    {
//        UserId = 11111
//    });
//Console.WriteLine(@$"testResponse 
//ErrorCode:{testResponse.Error}
//Message:{testResponse.Message}");



//// 서버 스트리밍 호출 (클라가 보내야 서버가 응답을 함)
//using var call = testClient.StreamingFromServer(
//    new TestRequest
//    {
//        UserId = 11111
//    });
//await foreach (var res in call.ResponseStream.ReadAllAsync())
//{
//    Console.WriteLine(@$"TestResponseServerStream 
//    ErrorCode:{res.Error}
//    Message:{res.Message}");
//}


////// 클라 스트리밍 호출 (클라는 가만히 있어도 서버가 보낸걸 받음)
//using var call2 = testClient.StreamingFromClient();
//for (var i = 0; i < 3; i++)
//{
//    await call2.RequestStream.WriteAsync(new TestRequest { UserId = 111 });
//}
//await call2.RequestStream.CompleteAsync();

//var response2 = await call2;
//Console.WriteLine(@$"TestResponseClientStream 
//    ErrorCode:{response2.Error}
//    Message:{response2.Message}");



//// 양방향 스트리밍
using var call3 = testClient.StreamingBothWay();
Console.WriteLine("Starting background task to receive messages");
var readTask = Task.Run(async () =>
{
    await foreach (var response in call3.ResponseStream.ReadAllAsync())
    {
        // Echo messages sent to the service

        Console.WriteLine(@$"TestResponseStreamBoth
            ErrorCode:{response.Error}
            Message:{response.Message}");
    }
});

Console.WriteLine("Starting to send messages");
Console.WriteLine("Type a message to echo then press enter.");
while (true)
{
    var result = Console.ReadLine();
    if (string.IsNullOrEmpty(result))
    {
        break;
    }

    await call3.RequestStream.WriteAsync(new TestRequest { UserId = 111 });
}

Console.WriteLine("Disconnecting");
await call3.RequestStream.CompleteAsync();
await readTask;



Console.WriteLine("Press any key to exit...");
Console.ReadKey();