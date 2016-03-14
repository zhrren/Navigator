using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mark.Navigator
{
    public class Navigator
    {
        private List<Release> _releaseList;
        private List<Group> _groupList;

        public Navigator(List<Release> releases, List<Group> groups)
        {
            _releaseList = releases;
            _groupList = groups;
        }

        public Native Match(string nativeName, string nativeVersion, string uid)
        {
            List<Native> allNative = new List<Native>();
            _releaseList.ForEach(x => allNative.AddRange(x.Native));

            List<Native> namedNative = allNative.Where(x => x.VerifyName(nativeName)).ToList();

            Native verifyNative = namedNative.Where(x =>
                x.VerifyVersion(nativeVersion)
                && x.VerifyUser(uid, _groupList))
            .OrderByDescending(x => x.Version)
            .FirstOrDefault();

            return verifyNative == null ? namedNative.FirstOrDefault() : verifyNative;
        }
    }
}
