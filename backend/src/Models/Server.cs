namespace Masterlist.DB;

public class Server
{
  public string Id { get; set; }
  public string Name { get; set; }
  public string ConnectionAddress { get; set; }
  public Dictionary<string, string> CustomData { get; set; }
}

// TODO Use an actual cache DB.
public class ServerDb
{
  public Dictionary<string, Server> servers = new();

  public bool Exists(string id) => servers.ContainsKey(id);

  public Server GetServer(string id)
  {
    if (id is null) return null;
    if (servers.ContainsKey(id))
    {
      return servers[id];
    }
    else
    {
      return null;
    }
  }

  public Server[] GetServers()
  {
    return servers.Values.ToArray();
  }

  public Server AddServer(Server server)
  {
    if (server.Id is null)
    {
      server.Id = Guid.NewGuid().ToString();
    }
    servers.Add(server.Id, server);
    return server;
  }

  public Server UpdateServer(Server server)
  {
    if (servers.ContainsKey(server.Id))
    {
      servers[server.Id] = server;
      return server;
    }
    else
    {
      return null;
    }
  }
}