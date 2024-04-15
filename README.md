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

 - **Use case:**  Simple APP that needs access to ecarup for one user
![image](https://github.com/eCarUp/ecarup-api-examples/assets/46069751/d04d469a-bcf2-4b18-b9bd-b47449d05d88)



### ecarupGrpcWebExample
ASP.net Blazor Example with Code-First gRPC between Client (Webassembly) and Server). Uses the oauth Confidential flow. 

 - **Use case:**  Web Apps that need access to ecarup for multiple users. 

![image](https://github.com/eCarUp/ecarup-api-examples/assets/46069751/9845e2e8-3312-4a81-a46f-900ec76ab62d)


#### Get started with ecarupGrpcWebExample - Configure Authenication
1. Go to www.smart-me.com
2. Login with your account that you want to use as account for your application
3. Click on the username -> settings -> API & Access
4. Create a new "Confidential" OAuth Application
5. Enter the Redirect URL of your application. E.g. for the local development it will be https://localhost:7118/authorization-code/callback
6. Click on create
7. Copy the client id and the client secret into the appsettings.json in ecarupApiExamples\ecarupGrpcWebExample\Server\ in the oauth section.

![image](https://github.com/eCarUp/ecarup-api-examples/assets/46069751/add50ec4-5472-4f6d-ad41-0fd92db523c0)
