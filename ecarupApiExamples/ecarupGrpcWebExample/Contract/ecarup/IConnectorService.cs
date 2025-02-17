using ProtoBuf.Grpc.Configuration;
using ProtoBuf.Grpc;

namespace ecarupGrpcWebExample.Contract.ecarup
{
    [Service]
    public interface IConnectorService
    {
        [Operation]
        Task<IEnumerable<ConnectorDto>> GetAll(CallContext context = default);

        [Operation]
        Task<ActiveChargingDto> GetActiveChargingForConnector(GetActiveChargingRequestDto request, CallContext context = default);
    }
}