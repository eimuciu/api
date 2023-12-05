using api.Data;
using api.Entities;
using Microsoft.AspNetCore.SignalR;

namespace api.SignalHub
{
    public class ConnectionHub : Hub
    {
        PresenceTracker _tracker;
        UserRepository _repository;

        public ConnectionHub(PresenceTracker tracker, UserRepository repository)
        {
            _tracker = tracker;
            _repository = repository;
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
                if (await _repository.UserExists(nickname))
                {
                    User userData = await _repository.GetUser(nickname);
                    await Clients.Caller.SendAsync("UserRegistration", new { Connected = true, Message = "Connected", user = userData });
                }
                else
                {
                    User userData = await _repository.AddNewUser(new User { Nickname = nickname, IsGuest = true });
                    await Clients.Caller.SendAsync("UserRegistration", new { Connected = true, Message = "Connected", user = userData });
                }
            }

        }
    }
}