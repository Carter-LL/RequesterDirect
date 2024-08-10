using Microsoft.Xna.Framework;
using RequesterDirect.Content.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequesterDirect.Content.Controllers
{
    public class UpdateController
    {
        public UpdateController() 
        { 
        
        }

        public void Update()
        {
            var framesCopy = new List<Frame>(Globals.Frames); // Create a copy of the collection
            foreach (Frame frame in framesCopy)
            {
                frame.Update();
            }
        }
    }
}
