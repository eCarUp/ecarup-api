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

        [Authorize]
        public async Task<ActiveChargingDto> GetActiveChargingForConnector(
            GetActiveChargingRequestDto request, 
            CallContext context = default)
        {
            var httpContext = context.ServerCallContext?.GetHttpContext();

            var accessToken = await httpContext?.GetTokenAsync("access_token");

            var ecarupApi = new EcarupApiGrpcClient();
            var activeChargingResponse = await ecarupApi.GetActiveChargingForConnector(
                accessToken,
                request.StationId,
                request.ConnectorId);

            return new ActiveChargingDto()
            {
                Id = activeChargingResponse.Id,
                StationId = request.StationId,
                ConnectorId = request.ConnectorId,
                Status = activeChargingResponse.Status.ToString(),
                DriverIdentifier = activeChargingResponse.DriverIdentifier,
                Duration = DateTime.UtcNow - FromGoogleTimestamp(activeChargingResponse.StartTime),
            };
        }

        private DateTime FromGoogleTimestamp(Google.Protobuf.WellKnownTypes.Timestamp timestamp)
        {
            var epochTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epochTime.AddTicks(timestamp.Nanos / 100);
        }
    }
}
