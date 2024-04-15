using ecarupGrpcWebExample.Contract.Authentication;
using ecarupGrpcWebExample.Server;
using ecarupGrpcWebExample.Server.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Configuration;
using ProtoBuf.Grpc.Server;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddCodeFirstGrpc(config =>
{
    config.ResponseCompressionLevel = System.IO.Compression.CompressionLevel.Optimal;
    config.EnableDetailedErrors = true;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = AuthConstants.AuthenticationTheme;
})
    
 .AddCookie()
 .AddOAuth(AuthConstants.AuthenticationTheme, options =>
 {
     var oauthConfig = builder.Configuration.GetSection("oauth");

     options.ClientId = oauthConfig["client_id"]!;
     options.ClientSecret = oauthConfig["client_secret"]!;
     options.UsePkce = true;
     options.CallbackPath = new PathString("/signin-smartme");
     options.AccessDeniedPath = new PathString("/OAuthAccessDenied");

     options.AuthorizationEndpoint = AuthConstants.AuthorizationEndpoint;
     options.TokenEndpoint = AuthConstants.TokenEndpoint;
     options.UserInformationEndpoint = AuthConstants.UserInformationEndpoint;
     options.Events = new OAuthEvents
     {
         OnCreatingTicket = async context =>
         {
             var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
             request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
             request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

             var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);

             var user = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;

             context.RunClaimActions(user);
         }
     };

     options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "Id");
     options.ClaimActions.MapJsonKey(ClaimTypes.Name, "Email");

     // Save the access token in the cookie. Otherwise we can not use it in the gRPC service.
     options.SaveTokens = true;

     // options.Scope.Add("offline_access");
     options.Scope.Add("user.read");
 });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseGrpcWeb(new GrpcWebOptions() { DefaultEnabled = true });

#region Authentication

app.MapGet(SharedConstants.LoginUrl, async context =>
{
    await context.ChallengeAsync(AuthConstants.AuthenticationTheme, new AuthenticationProperties()
    {
        RedirectUri = "/"
    });
});
app.MapGet(SharedConstants.LogoutUrl, async context =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    context.Response.Redirect("/");
});


#endregion

app.MapGrpcService<AuthService>();
app.MapGrpcService<EcarupConnectorService>();

app.MapRazorPages();
app.MapFallbackToFile("index.html");

app.Run();
