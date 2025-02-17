using ProtoBuf;

namespace ecarupGrpcWebExample.Contract.ecarup
{
    [ProtoContract]
    public class GetActiveChargingRequestDto
    {
        [ProtoMember(1)]
        public required string StationId { get; set; }

        [ProtoMember(2)]
        public required string ConnectorId { get; set; }
    }
}
