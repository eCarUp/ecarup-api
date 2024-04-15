using ecarupGrpcApi;
using Grpc.Core;

var example = new EcarupApiExample();

await example.ExampleRun();

Console.WriteLine(string.Empty);
Console.WriteLine("Done");
Console.WriteLine("Press any key to close");
Console.ReadKey();