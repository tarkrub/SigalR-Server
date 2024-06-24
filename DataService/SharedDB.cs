using System.Collections.Concurrent;
using Chatservices.Modal;

namespace Chatservices.DataService
{
    public class SharedDB
    {
        private readonly ConcurrentDictionary<string, Userconnection> _connections = new ConcurrentDictionary<string, Userconnection>();
        public ConcurrentDictionary<string, Userconnection> connections => _connections;
    }
}
