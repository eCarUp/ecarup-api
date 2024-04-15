using Grpc.Core;
using Grpc.Net.Client;
using PublicApi.ChargingHistory;
using PublicApi.Stations;
using System.Net.Http.Headers;

namespace ecarupGrpcWebExample.Server.ecarupApi
{
    public class EcarupApiGrpcClient
    {
        public async Task<StationsReply> GetAllStations(string accessToken)
        {
            var customHttpClient = new HttpClient();
            customHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var options = new GrpcChannelOptions()
            {
                HttpClient = customHttpClient
            };

            using var channel = GrpcChannel.ForAddress("https://public-api.ecarup.com", options);
            var client = new StationProtoService.StationProtoServiceClient(channel);

            var reuqest = new GetAllStationsRequest()
            {
                Filter = StationsFilter.Ownedmanagedandviewed,
            };

            return await client.GetAllAsync(reuqest);
        }
    }
}
