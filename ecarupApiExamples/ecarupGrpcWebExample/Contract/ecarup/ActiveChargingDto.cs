using ProtoBuf;

namespace ecarupGrpcWebExample.Contract.ecarup
{
    [ProtoContract]
    public class ActiveChargingDto
    {
        [ProtoMember(1)]
        public required string Id { get; set; }

        [ProtoMember(2)]
        public required string StationId { get; set; }

        [ProtoMember(3)]
        public required string ConnectorId { get; set; }

        [ProtoMember(4)]
        public required string DriverIdentifier { get; set; }

        [ProtoMember(5)]
        public required string Status { get; set; }

        [ProtoMember(6)]
        public required TimeSpan Duration { get; set; }
    }
}
