using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecarupGrpcApi
{
    public class EcarupApiExample
    {
        // To get a client ID and client Secret login into your smart-me account. Go to Account -> Settings -> Oauth 
        // In this example the "Client credentials" flow is used to get the access token. Other flows are also possible.
        // More Information:
        // https://sites.google.com/smart-me.com/ecarup-wiki/drittsysteme/ecarup-api#h.hfxankz252pr
        const string client_id = "{your_client_id}";
        const string client_secret = "{your_client_secret}";

        const string EcarupSmartMeIdentityProvider = "https://api.smart-me.com";

        const string EcarupApiBaseUrl = "https://public-api.ecarup.com";


        public async Task ExampleRun()
        {
            Console.WriteLine("ecarup Public GRPC API");
            Console.WriteLine("Getting access token...");

            var accessToken = await SmartMeEcarupOauthClient.GetAccessToken(
                EcarupSmartMeIdentityProvider,
                client_id,
                client_secret
                );

            if (string.IsNullOrEmpty(accessToken))
            {
                Console.WriteLine("Error getting access token. ");
                Console.WriteLine("Please check your client_id and client_secret in the EcarupApiExample class");
                Console.WriteLine("Exiting...");
                return;
            }

            Console.WriteLine("Access token ok");


            var ecarupGrpcClient = new EcarupGrpcClient(
                EcarupApiBaseUrl,
                accessToken);

            // Get all stations
            await GetAllStations(ecarupGrpcClient);

            // Get single station
            await GetSingleStation(ecarupGrpcClient, "b18510f0-fbd2-207f-e1b6-0a55899c22ca");

            // Get the last charging histories for this user as driver
            await GetLastDriverHistories(ecarupGrpcClient);

            // Get all charging histories for this user as driver (with paging)
            await GetAllDriverHistoriesPaged(ecarupGrpcClient);

            // Get all charging histories for this user as station owner
            await GetLastStationHistories(ecarupGrpcClient, "b18510f0-fbd2-207f-e1b6-0a55899c22ca");
        }

        private async Task GetAllStations(EcarupGrpcClient ecarupGrpcClient)
        {
            try
            {
                // Get all stations
                Console.WriteLine("Getting all stations of this user...");

                var request = new GetAllStationsRequest()
                {
                    Filter = StationsFilter.Ownedmanagedandviewed
                };

                var stations = await ecarupGrpcClient.StationProtoClient.GetAllAsync(request);

                Console.WriteLine($"Got {stations.Stations.Count} stations");
            }
            catch (RpcException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        private async Task GetSingleStation(
            EcarupGrpcClient ecarupGrpcClient,
            string id)
        {
            try
            {
                var singleStationRequest = new GetStationRequest()
                {
                    Id = id
                };
                var singleStation = await ecarupGrpcClient.StationProtoClient.GetAsync(singleStationRequest);
                Console.WriteLine($"Got single station: {singleStation.Name}");
            }
            catch (RpcException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task GetLastDriverHistories(EcarupGrpcClient ecarupGrpcClient)
        {
            try
            {
                Console.WriteLine("Getting the last charging histories of this user...");

                var historyForDriverRequest = new GetChargingHistoryForDriverRequest();
                var chargingHistoriesForDriver = await ecarupGrpcClient.ChargingHistoryProtoClient.GetForDriverAsync(historyForDriverRequest);

                Console.WriteLine($"Got {chargingHistoriesForDriver.Histories.Count} driver charging histories");
            }
            catch (RpcException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task GetAllDriverHistoriesPaged(EcarupGrpcClient ecarupGrpcClient)
        {
            try
            {
                Console.WriteLine("Getting all charging histories of this user (with paging)...");

                var paginationCursor = string.Empty;

                while (true)
                {
                    var historyForDriverRequest = new GetChargingHistoryForDriverRequest()
                    {
                        PaginationCursor = paginationCursor
                    };

                    var page = await ecarupGrpcClient.ChargingHistoryProtoClient.GetForDriverAsync(historyForDriverRequest);

                    Console.WriteLine($"Got {page.Histories.Count} driver charging histories");

                    if (!string.IsNullOrEmpty(page.PaginationCursor))
                    {
                        paginationCursor = page.PaginationCursor;
                    }
                    else
                    {
                        // We have reached the end
                        break;
                    }                    
                }
            }
            catch (RpcException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task GetLastStationHistories(
            EcarupGrpcClient ecarupGrpcClient,
            string stationId)
        {
            try
            {
                Console.WriteLine("Getting all charging histories of this user as station owner...");

                var historyForStationOwnerRequest = new GetChargingHistoryForStationRequest()
                {
                    Id = stationId
                };
                var chargingHistoriesForStationOwner = await ecarupGrpcClient.ChargingHistoryProtoClient.GetForStationAsync(historyForStationOwnerRequest);
               
                Console.WriteLine($"Got {chargingHistoriesForStationOwner.Histories.Count} station charging histories");
            }
            catch (RpcException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
