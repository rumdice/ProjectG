using Grpc.Core;
using Grpc.Net.Client;
using GrpcProtocol;
using System;




using var channel = GrpcChannel.ForAddress("https://localhost:7293");

var loginClient = new Login.LoginClient(channel);
var loginReply = await loginClient.LoginRpcAsync(
    new LoginRequest
    {
        UserId = 11001,
        UserName = "UserName1"
    });

Console.WriteLine("LoginReply: " + loginReply.Message);


var itemClient = new Item.ItemClient(channel);
var itemUseReply = await itemClient.ItemUseRpcAsync(
    new ItemUseRequest
    {
        UserId = 11002,
        UserName = "UserName2",
        ItemId = 1001990,
        ItemCount = 1,
    });

// ItemUseReply 패킷 형식
Console.WriteLine("ItemUseReply: " + itemUseReply.Error, ":" + itemUseReply.RemainItems);


Console.WriteLine("Press any key to exit...");
Console.ReadKey();