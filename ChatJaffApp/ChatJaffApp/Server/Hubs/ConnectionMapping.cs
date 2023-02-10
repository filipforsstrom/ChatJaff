using System.Collections.Generic;
using System.Linq;

namespace ChatJaffApp.Server.Hubs;

public class ConnectionMapping<T>
{
    private readonly Dictionary<T, Connections> _connections =
        new();

    public int Count
    {
        get
        {
            return _connections.Count;
        }
    }

    public class Connections
    {
        public HashSet<string> ConnectionId { get; set; }
        public List<Guid> ChatRoomIds { get; set; }
    }

    public bool VerifyGroup(T key, Guid chatRoomId)
    {
        if (_connections.TryGetValue(key, out Connections connections))
        {
            return connections.ChatRoomIds.Contains(chatRoomId);
        }

        return false;
    }


    public void Add(T key, string connectionId, List<Guid> chatRoomIds)
    {
        lock (_connections)
        {
            if (!_connections.TryGetValue(key, out Connections connections))
            {
                connections = new Connections();
                connections.ConnectionId = new HashSet<string>();
                connections.ChatRoomIds = new List<Guid>();
                _connections.Add(key, connections);
            }

            lock (connections)
            {
                connections.ConnectionId.Add(connectionId);
                connections.ChatRoomIds.AddRange(chatRoomIds);
            }
        }
    }

    public IEnumerable<string> GetConnections(T key)
    {
        if (_connections.TryGetValue(key, out Connections connections))
        {
            return connections.ConnectionId;
        }

        return Enumerable.Empty<string>();
    }

    public void Remove(T key, string connectionId)
    {
        lock (_connections)
        {
            if (!_connections.TryGetValue(key, out Connections connections))
            {
                return;
            }

            lock (connections)
            {
                connections.ConnectionId.Remove(connectionId);

                if (connections.ConnectionId.Count == 0)
                {
                    _connections.Remove(key);
                }
            }
        }
    }
}
