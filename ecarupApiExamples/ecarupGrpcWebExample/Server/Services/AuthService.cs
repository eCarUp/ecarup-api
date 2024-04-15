using ecarupGrpcWebExample.Contract.Authentication;
using Grpc.Core;
using ProtoBuf.Grpc;

namespace ecarupGrpcWebExample.Server.Services
{
    public class AuthService : IAuthService
    {
        public async Task<AuthIdentity> GetIdentity(CallContext context = default)
        {
            var httpContext = context.ServerCallContext!.GetHttpContext();

            var identity = httpContext.User.Identity!;

            if (identity.IsAuthenticated)
            {
                return await Task.FromResult(new AuthIdentity
                {
                    IsAuthenticated = true,
                    Username = identity.Name!
                });
            }

            return new AuthIdentity
            {
                IsAuthenticated = false
            };
        }
    }
}
