using Grpc.Core;
using GrpcProtocol;

namespace GrpcServer.Services
{
    public class TestService : Test.TestBase
    {
        // 단항
        public override Task<TestResponse> UnaryCall(TestRequest request, ServerCallContext context)
        {
            return Task.FromResult(new TestResponse
            {
                Error = ErrorType.Success,
                Message = "server test UnaryCall response"
            });
        }


        // 서버 스트리밍
        public override async Task StreamingFromServer(TestRequest request, IServerStreamWriter<TestResponse> responseStream, ServerCallContext context)
        {
            //for (var i = 0; i < 5; i++)
            //{
            //    await responseStream.WriteAsync(new TestResponse());
            //    await Task.Delay(TimeSpan.FromSeconds(1));
            //}

            while (!context.CancellationToken.IsCancellationRequested)
            {
                var resp = new TestResponse
                {
                    Error = ErrorType.Success,
                    Message = "server test StreamServer response"
                };

                //await responseStream.WriteAsync(new TestResponse());
                await responseStream.WriteAsync(resp);
                await Task.Delay(TimeSpan.FromSeconds(1), context.CancellationToken);
            }
        }


        // 클라 스트리밍
        public override async Task<TestResponse> StreamingFromClient(
            IAsyncStreamReader<TestRequest> requestStream,
            ServerCallContext context)
        {
            //while (await requestStream.MoveNext())
            //{
            //    var message = requestStream.Current;
            //    // ...
            //}

            await foreach (var message in requestStream.ReadAllAsync())
            {
                // .. 
            }

            var resp = new TestResponse
            {
                Error = ErrorType.Success,
                Message = "server test StreamClient response"
            };
            return resp;

            // return new TestResponse();
        }


        // 양방향 스트리밍
        public override async Task StreamingBothWay(
            IAsyncStreamReader<TestRequest> requestStream,
            IServerStreamWriter<TestResponse> responseStream,
            ServerCallContext context)
        {
            var readTask = Task.Run(async () =>
            {
                await foreach (var message in requestStream.ReadAllAsync())
                {
                    // Process request.
                }
            });

            while (!readTask.IsCompleted)
            {

                var resp = new TestResponse
                {
                    Error = ErrorType.Success,
                    Message = "server test StreamBoth response"
                };
                await responseStream.WriteAsync(resp);
                await Task.Delay(TimeSpan.FromSeconds(1), context.CancellationToken);
            }
        }
    }
}