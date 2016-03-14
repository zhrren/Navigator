using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Mark.Navigator
{
    public class Native
    {
        private System.Version _version;

        public string Name { get; set; }
        public string Version { get; set; }
        public string User { get; set; }
        public string Group { get; set; }
        public string Url { get; set; }

        public bool VerifyName(string name)
        {
            if (string.IsNullOrWhiteSpace(Name) || Name == "*")
                return true;
            else
                return string.Equals(Name, name, StringComparison.OrdinalIgnoreCase);
        }

        public bool VerifyUser(string user, List<Group> groupList)
        {
            if (!string.IsNullOrWhiteSpace(user))
            {
                if (!string.IsNullOrWhiteSpace(User))
                {
                    if (User == "*") return true;
                    if (this.User.Split(',').Any(x => x.Equals(user, StringComparison.OrdinalIgnoreCase)))
                        return true;
                }

                if (!string.IsNullOrWhiteSpace(Group) && groupList != null && groupList.Count > 0)
                {
                    if (Group == "*" && groupList.Any(x => x.User.Any(u => user.Equals(u, StringComparison.OrdinalIgnoreCase))))
                        return true;

                    var group = groupList.FirstOrDefault(x => Group.Equals(x.Name, StringComparison.OrdinalIgnoreCase));
                    if (group != null && group.User.Any(x => user.Equals(x, StringComparison.OrdinalIgnoreCase)))
                        return true;
                }
            }

            return false;
        }

        public bool VerifyVersion(string version)
        {
            Regex reg = new Regex(@"([0-9\.]*)");
            var match = reg.Match(version);
            var validVersion = match.Groups[1].Value;

            var ver = new Version(validVersion);
            return ver >= this.VersionObject;
        }

        private Version VersionObject
        {
            get
            {
                if (_version == null)
                    _version = new Version(Version);
                return _version;
            }
        }
    }
}
