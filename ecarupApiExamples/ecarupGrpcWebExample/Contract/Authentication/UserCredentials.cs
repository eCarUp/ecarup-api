using ProtoBuf;
using System.Runtime.Serialization;

namespace ecarupGrpcWebExample.Contract.Authentication
{
    [ProtoContract]
    public class UserCredentials
    {
        public UserCredentials()
        {
            Username = string.Empty;
            Password = string.Empty;
        }

        [ProtoMember(1)]
        public string Username { get; set; }

        [ProtoMember(2)]
        public string Password { get; set; }
    }
}
