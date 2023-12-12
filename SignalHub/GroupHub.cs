using api.Data;
using api.DTOs;
using api.Entities;
using Microsoft.AspNetCore.SignalR;

namespace api.SignalHub
{
    public class GroupHub : Hub
    {
        GroupPresenceTracker _groupTracker;
        MessageRepository _messageRepository;
        UserRepository _userRepository;
        public GroupHub(GroupPresenceTracker groupTracker, MessageRepository messageRepository, UserRepository userRepository)
        {
            _groupTracker = groupTracker;
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }
        public async Task ConnectUserToGroup(string nickname, string groupname, string previousGroup)
        {

            await Groups.AddToGroupAsync(Context.ConnectionId, groupname);

            List<string> usersInGroup = _groupTracker.AddUserToGroup(groupname, nickname);

            List<Message> groupMessages = await _messageRepository.GetGroupMessages(groupname);

            User presentUser = await _userRepository.GetUser(nickname);

            if (!string.IsNullOrEmpty(previousGroup))
            {
                _groupTracker.RemoveUserFromGroup(previousGroup, nickname);

            }

            // var httpContext = Context.GetHttpContext();
            // string nickname = httpContext.Request.Query["nick"];
            // string groupname = httpContext.Request.Query["groupname"];


            await Clients.Caller.SendAsync("OnUserConnectionToGroup", new { usersInGroup = usersInGroup, groupMessages = groupMessages });
            await Clients.OthersInGroup(groupname).SendAsync("OnUserJoinGroup", presentUser);
        }

        public async Task ChangeUserGroup(string previousGroup)
        {
            List<string> remainingUsers = _groupTracker.GetGroupUsers(previousGroup);
            await Clients.OthersInGroup(previousGroup).SendAsync("OnUserChangingGroup", remainingUsers);
        }

        public async Task SendGroupMessage(MessageDto message)
        {
            Message newMsg = await _messageRepository.AddNewMessage(message);
            await Clients.Group(message.GroupName).SendAsync("NewMessage", newMsg);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var httpContext = Context.GetHttpContext();
            string nickname = httpContext.Request.Query["nick"];
            string groupname = httpContext.Request.Query["groupname"];

            _groupTracker.RemoveUserFromGroup(groupname, nickname);

            await Clients.Group(groupname).SendAsync("OnUserDisconnecting", nickname);
            await base.OnDisconnectedAsync(exception);
        }
    }
}