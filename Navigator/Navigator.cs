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
            _releaseList.ForEach(x =>
            {
                x.Native.ForEach(y => y.ReleaseVersion = x.Version);
                allNative.AddRange(x.Native);
            });

            List<Native> namedNative = allNative.Where(x => x.VerifyName(nativeName)).ToList();

            var resultList = namedNative.Where(x =>
                x.VerifyVersion(nativeVersion)
                && x.VerifyUser(uid, _groupList));
            var resultItem = resultList.OrderByDescending(x => x.Version).FirstOrDefault();
            if (resultItem != null)
            {
                resultItem = resultList.Where(x => x.Version == resultItem.Version)
                    .OrderByDescending(x => x.ReleaseVersion)
                    .FirstOrDefault();
            }

            return resultItem == null ? namedNative.FirstOrDefault() : resultItem;
        }
    }
}
