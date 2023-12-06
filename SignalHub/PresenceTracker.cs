namespace api.SignalHub
{
    public class PresenceTracker
    {
        // private readonly List<string> _onlineUsers = new List<string>();

        private readonly Dictionary<string, List<string>> _onlineUsers = new Dictionary<string, List<string>>();

        public void AddUser(string nickname, string connectionId)
        {
            if (_onlineUsers.ContainsKey(nickname))
            {
                _onlineUsers[nickname].Add(connectionId);
            }
            else
            {
                _onlineUsers.Add(nickname, new List<string> { connectionId });
            }
        }

        public void RemoveUser(string nickname, string connectionId)
        {
            _onlineUsers[nickname].Remove(connectionId);
        }

        public bool NicknameOnline(string nickname)
        {
            bool isOnline = false;

            if (_onlineUsers.ContainsKey(nickname) && _onlineUsers[nickname].Any())
            {
                isOnline = true;
            }

            return isOnline;
        }
    }
}