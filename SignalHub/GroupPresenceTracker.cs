namespace api.SignalHub
{
    public class GroupPresenceTracker
    {
        Dictionary<string, List<string>> _groupsOnline = new Dictionary<string, List<string>>();

        public List<string> AddUserToGroup(string groupname, string nickname)
        {

            if (_groupsOnline.ContainsKey(groupname) && _groupsOnline[groupname].Contains(nickname)) { return _groupsOnline[groupname]; }

            if (_groupsOnline.ContainsKey(groupname) && !_groupsOnline[groupname].Contains(nickname))
            {
                _groupsOnline[groupname].Add(nickname);
                return _groupsOnline[groupname];
            }
            _groupsOnline.Add(groupname, new List<string> { nickname });
            return _groupsOnline[groupname];
        }

        public void RemoveUserFromGroup(string groupname, string nickname)
        {
            if (!_groupsOnline.ContainsKey(groupname)) return;
            _groupsOnline[groupname].Remove(nickname);
        }

        public List<string> GetGroupUsers(string groupname)
        {
            return _groupsOnline[groupname];
        }
    }
}