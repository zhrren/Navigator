using Mark.Navigator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navigator.Tests.Base
{
    public class Apps
    {
        public static Settings Settings { get; set; }
    }

    public class Settings
    {
        public List<Release> Release { get; set; }
    }
}
