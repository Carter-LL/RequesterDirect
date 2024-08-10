using RequesterDirect.Content.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequesterDirect.Content
{
    public static class Util
    {
        public static Frame GetFrameByName(string name)
        {
            return Globals.Frames.Find(x => x.GetName().Equals(name));
        }
    }
}
