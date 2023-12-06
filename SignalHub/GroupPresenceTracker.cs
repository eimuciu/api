namespace api.SignalHub
{
    public class GroupPresenceTracker
    {
        Dictionary<string, List<string>> _groupsOnline = new Dictionary<string, List<string>>();

        public List<string> AddToGroup(string groupname, string nickname)
        {
            if (_groupsOnline.ContainsKey(groupname))
            {
                _groupsOnline[groupname].Add(nickname);
            }
            else
            {
                _groupsOnline.Add(groupname, new List<string> { nickname });
            }
            return _groupsOnline[groupname];
        }

        public void RemoveFromGroup(string groupname, string nickname)
        {
            _groupsOnline[groupname].Remove(nickname);
        }
    }
}