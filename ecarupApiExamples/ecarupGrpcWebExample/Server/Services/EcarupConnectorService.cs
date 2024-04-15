using ecarupGrpcWebExample.Contract.ecarup;
using ecarupGrpcWebExample.Server.ecarupApi;
using Grpc.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using ProtoBuf.Grpc;

namespace ecarupGrpcWebExample.Server.Services
{
    public class EcarupConnectorService : IConnectorService
    {
        [Authorize]
        public async Task<IEnumerable<ConnectorDto>> GetAll(CallContext context = default)
        {
            var httpContext = context.ServerCallContext?.GetHttpContext();

            var accessToken = await httpContext?.GetTokenAsync("access_token");

            var ecarupApi = new EcarupApiGrpcClient();
            var stationReply = await ecarupApi.GetAllStations(accessToken);

            var connectorDtos = new List<ConnectorDto>();

            foreach(var station in stationReply.Stations)
            { 
               foreach(var connector in station.Connectors)
                {
                    connectorDtos.Add(new ConnectorDto
                    {
                        StationId = station.Id,
                        ConnectorId = connector.Id,
                        ConnectorNumber = connector.Number,
                        StationName = station.Name,
                        ConnectorName = connector.Name
                    });
                }
            }

            return connectorDtos;
        }
    }
}
