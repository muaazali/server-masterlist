using Microsoft.OpenApi.Models;
using Masterlist.DB;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v0.1", new OpenApiInfo { Title = "Masterlist API", Description = "", Version = "v.1" });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
  c.SwaggerEndpoint("/swagger/v0.1/swagger.json", "Masterlist API V1");
});
var serverDb = new ServerDb();

app.MapGet("/masterlist", async () =>
{
  return serverDb.GetServers();
});

app.MapPut("/server", async ([FromBody] Server server) =>
{
  var existingServer = serverDb.GetServer(server.Id);
  if (existingServer is not null)
  {
    existingServer.Name = server.Name;
    existingServer.ConnectionAddress = server.ConnectionAddress;
    existingServer.CustomData = server.CustomData;
    return existingServer;
  }
  else
  {
    serverDb.AddServer(server);
    return server;
  }
});

app.MapDelete("/server", async ([FromBody] Server server) =>
{
  var existingServer = serverDb.GetServer(server.Id);
  if (existingServer is not null)
  {
    serverDb.servers.Remove(server.Id);
    return existingServer;
  }
  else
  {
    return null;
  }
});

app.Run();
