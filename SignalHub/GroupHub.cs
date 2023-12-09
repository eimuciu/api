using api.Data;
using api.Entities;
using Microsoft.AspNetCore.SignalR;

namespace api.SignalHub
{
    public class GroupHub : Hub
    {
        GroupPresenceTracker _groupTracker;
        MessageRepository _messageRepository;
        public GroupHub(GroupPresenceTracker groupTracker, MessageRepository messageRepository)
        {
            _groupTracker = groupTracker;
            _messageRepository = messageRepository;
        }
        public async void ConnectUserToGroup(string nickname, string groupname, string previousGroup)
        {
            Console.WriteLine(previousGroup);
            if (!string.IsNullOrEmpty(previousGroup))
            {
                _groupTracker.RemoveUserFromGroup(previousGroup, nickname);
            }

            // var httpContext = Context.GetHttpContext();
            // string nickname = httpContext.Request.Query["nick"];
            // string groupname = httpContext.Request.Query["groupname"];

            await Groups.AddToGroupAsync(Context.ConnectionId, groupname);

            List<string> usersInGroup = _groupTracker.AddUserToGroup(groupname, nickname);

            List<Message> groupMessages = await _messageRepository.GetGroupMessages(groupname);

            await Clients.Caller.SendAsync("OnUserConnectionToGroup", new { usersInGroup = usersInGroup, groupMessages = groupMessages });

        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var httpContext = Context.GetHttpContext();
            string nickname = httpContext.Request.Query["nick"];
            string groupname = httpContext.Request.Query["groupname"];

            Console.WriteLine("User disconnecting from group");
            Console.WriteLine(nickname);

            _groupTracker.RemoveUserFromGroup(groupname, nickname);
            await base.OnDisconnectedAsync(exception);
        }
    }
}