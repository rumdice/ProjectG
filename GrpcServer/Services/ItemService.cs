using Grpc.Core;
using GrpcProtocol;

namespace GrpcServer.Services
{
    public class ItemService : Item.ItemBase
    {
        public override Task<ItemUseReply> ItemUseRpc(ItemUseRequest request, ServerCallContext context)
        {
            // check request

            // 대략적인 설계 (여기선 함수 콜하고 결과 받고 땡)
            // call logic
            // - process db, redis
            // - logging
            // - 에러 발생시 한 곳에서 처리
            // call result out respose

            // response
            return Task.FromResult(new ItemUseReply
            {
                Error = ErrorType.Success,
                RemainItems = { },
            });
        }
    }
}