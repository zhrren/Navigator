using System;
using System.Collections.Generic;
using System.Text;

namespace Mark.Navigator
{
    public class Release
    {
        public string Version { get; set; }

        public List<Native> Native { get; set; }
    }
}
