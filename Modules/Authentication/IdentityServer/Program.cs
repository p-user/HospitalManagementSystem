using Duende.IdentityServer.Validation;
using IdentityServer;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.


builder.Services.AddIdentityServer()
           .AddInMemoryClients(Config.Clients)
           .AddInMemoryApiScopes(Config.ApiScopes)
           .AddDeveloperSigningCredential();


var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();
app.UseIdentityServer();



app.Run();

