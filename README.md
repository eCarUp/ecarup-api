# ecarup Public API

Example projects how to use the ecarup public API.

The ecarup API supports REST and GRPC.  For new project we recommend to use gRPC


## REST API
Docs: https://public-api.ecarup.com/swagger/index.html

## gRPC
gRPC is a high performance RPC framework. The protobuffer files needed you can find here: https://github.com/eCarUp/ecarup-api-examples/tree/main/ecarupApiExamples/ecarupGrpcApi/Protos

## Authentication
ecarup uses the smart-me / ecarup oauth Identity server. Before you can use the API you need to get an access token for the oauth server. 
An example how to do that you can e.g. find here: https://github.com/eCarUp/ecarup-api-examples/blob/main/ecarupApiExamples/ecarupGrpcApi/SmartMeEcarupOauthClient.cs

To use oauth you need to get an client_id and maybe a client_secret first. A guid how to do it you can find here: https://sites.google.com/smart-me.com/wiki-english/3rd-party-systems/ecarup-api#h.2jf3qbh1r69y


## Examples

### ecarupGrpcApi
Simple console (.net C#) example how to use the ecarup gRPC API. The example uses the oauth client credentials flow to get an access token. 
