using Grpc.Net.Client;
using System.Net.Http.Headers;

namespace ecarupGrpcApi
{
    public class EcarupGrpcClient
    {
        private GrpcChannel channel;
        public StationProtoService.StationProtoServiceClient StationProtoClient { get; init; }
        public ChargingHistoryProtoService.ChargingHistoryProtoServiceClient ChargingHistoryProtoClient { get; init; }

        public EcarupGrpcClient(
            string serverUrl, 
            string accessToken)
        {
            var customHttpClient = new HttpClient();
            customHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var options = new GrpcChannelOptions()
            {
                HttpClient = customHttpClient
            };

            channel = GrpcChannel.ForAddress(serverUrl, options);

            // Create all GRPC Clients
            StationProtoClient = new StationProtoService.StationProtoServiceClient(channel);
            ChargingHistoryProtoClient = new ChargingHistoryProtoService.ChargingHistoryProtoServiceClient(channel);
        }
    }
}
