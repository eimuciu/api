using Microsoft.AspNetCore.SignalR;

namespace api.SignalHub
{
    public class GroupHub : Hub
    {
        public async void ConnectUserToGroup()
        {
            var httpContext = Context.GetHttpContext();
            string nick = httpContext.Request.Query["nick"];
            string groupname = httpContext.Request.Query["groupname"];

            await Groups.AddToGroupAsync(Context.ConnectionId, groupname);
        }
    }
}