using Chatservices.Modal;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.SignalR;
using Chatservices.DataService;


namespace Chatservices.Hubs
{
	public class ChatHub : Hub
	{

		private readonly SharedDB _shared;

		public ChatHub(SharedDB shared) => _shared = shared;

		public async Task JoinChat(Userconnection conn)
		{
			await Clients.All.SendAsync("ReceiveMessage", "admin", $"{conn.Username} has joined");

		}
		public async Task JoinPrivateChat(Userconnection conn)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, conn.ChatRoom);

			_shared.connections[Context.ConnectionId] = conn;

			await Clients.Group(conn.ChatRoom).SendAsync("JoinPrivateChat", "admin", $"{conn.Username} has joined {conn.ChatRoom}");
		}

		public async Task SendMessage(string msg)
		{
			if (_shared.connections.TryGetValue(Context.ConnectionId, out Userconnection _conn))
			{
				await Clients.Group(_conn.ChatRoom).SendAsync("ReceiveMessage" , _conn.Username,msg);
			}
		}
	}
}
