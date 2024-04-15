using ProtoBuf;

namespace ecarupGrpcWebExample.Contract.ecarup
{
    [ProtoContract]
    public class ConnectorDto
    {
        [ProtoMember(1)]
        public required string StationId { get; set; }

        [ProtoMember(2)]
        public required string ConnectorId { get; set; }

        [ProtoMember(3)]
        public required int ConnectorNumber { get; set; }

        [ProtoMember(4)]
        public required string StationName { get; set; }

        [ProtoMember(5)]
        public required string ConnectorName { get; set; }

    }
}
