using ProtoBuf;

namespace ecarupGrpcWebExample.Contract.Authentication
{
    [ProtoContract]
    public class AuthIdentity
    {
        [ProtoMember(1)]
        public required bool IsAuthenticated { get; set; }

        [ProtoMember(2)]
        public string? Username { get; set; }
    }
}
