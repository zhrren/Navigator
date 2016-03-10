using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mark.Navigator
{
    public class Group
    {
        public Group(string name, string[] user)
        {
            Name = name;
            User = user;
        }

        public string Name { get; set; }
        
        public string[] User { get; set; }
    }
}
