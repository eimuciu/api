namespace api.SignalHub
{
    public class PresenceTracker
    {
        private readonly List<string> _onlineUsers = new List<string>();

        public void AddUser(string nickname)
        {
            _onlineUsers.Add(nickname);
        }

        public bool NicknameOnline(string nickname)
        {
            bool isOnline = false;

            if (_onlineUsers.Any(x => x.ToLower() == nickname.ToLower()))
            {
                isOnline = true;
            }

            return isOnline;
        }
    }
}