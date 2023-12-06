using Microsoft.AspNetCore.SignalR;

namespace api.SignalHub
{
    public class GroupHub : Hub
    {
        GroupPresenceTracker _groupTracker;
        public GroupHub(GroupPresenceTracker groupTracker)
        {
            _groupTracker = groupTracker;
        }
        public async void ConnectUserToGroup()
        {
            var httpContext = Context.GetHttpContext();
            string nickname = httpContext.Request.Query["nick"];
            string groupname = httpContext.Request.Query["groupname"];

            await Groups.AddToGroupAsync(Context.ConnectionId, groupname);

            List<string> usersInGroup = _groupTracker.AddUserToGroup(groupname, nickname);

            // Here I need to get all messages for a group


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