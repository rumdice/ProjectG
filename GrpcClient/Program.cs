using Grpc.Core;
using Grpc.Net.Client;
using GrpcGreeter;
using System;




using var channel = GrpcChannel.ForAddress("https://localhost:7293");
var client = new Greeter.GreeterClient(channel);
var reply = await client.SayHelloAsync(
                  new HelloRequest { Name = "GreeterClient" });
Console.WriteLine("Greeting: " + reply.Message);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();