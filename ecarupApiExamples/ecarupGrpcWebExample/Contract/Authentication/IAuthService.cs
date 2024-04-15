using ProtoBuf.Grpc.Configuration;
using ProtoBuf.Grpc;

namespace ecarupGrpcWebExample.Contract.Authentication
{
    [Service]
    public interface IAuthService
    {
        [Operation]
        Task<AuthIdentity> GetIdentity(CallContext context = default);
    }
}
