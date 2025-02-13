using Grpc.Core;
using Grpc.Net.Client;
using PublicApi.ActiveChargings;
using PublicApi.ChargingHistory;
using PublicApi.Stations;
using System.Net.Http.Headers;

namespace ecarupGrpcWebExample.Server.ecarupApi
{
    public class EcarupApiGrpcClient
    {
        const string BaseUrl = "https://public-api.ecarup.com";

        public async Task<StationsReply> GetAllStations(string accessToken)
        {
            var customHttpClient = new HttpClient();
            customHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var options = new GrpcChannelOptions()
            {
                HttpClient = customHttpClient
            };

            using var channel = GrpcChannel.ForAddress(BaseUrl, options);
            var client = new StationProtoService.StationProtoServiceClient(channel);

            var reuqest = new GetAllStationsRequest()
            {
                Filter = StationsFilter.Ownedmanagedandviewed,
            };

            return await client.GetAllAsync(reuqest);
        }

        public async Task<ActiveChargingResponse> GetActiveChargingForConnector(
            string accessToken,
            string stationId,
            string connectorId)
        {
            var customHttpClient = new HttpClient();
            customHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var options = new GrpcChannelOptions()
            {
                HttpClient = customHttpClient
            };

            using var channel = GrpcChannel.ForAddress(BaseUrl, options);
            var client = new ActiveChargingProtoService.ActiveChargingProtoServiceClient(channel);

            var reuqest = new GetForConnectorRequest()
            {
               StationId = stationId,
               ConnectorId = connectorId
            };

            return await client.GetForConnectorAsync(reuqest);
        }
    }
}
