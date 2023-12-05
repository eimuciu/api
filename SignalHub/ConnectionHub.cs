using Microsoft.AspNetCore.SignalR;

namespace api.SignalHub
{
    public class ConnectionHub : Hub
    {
        PresenceTracker _tracker;

        public ConnectionHub(PresenceTracker tracker)
        {
            _tracker = tracker;
        }
        // public override async Task OnConnectedAsync()
        // {
        // }

        public async Task RegisterUser(string nickname)
        {
            if (_tracker.NicknameOnline(nickname))
            {
                await Clients.Caller.SendAsync("UserRegistration", new { Connected = false, Message = "Pick another nickname" });
            }
            else
            {
                _tracker.AddUser(nickname);
                await Clients.Caller.SendAsync("UserRegistration", new { Connected = true, Message = "Connected" });
            }

        }
    }
}