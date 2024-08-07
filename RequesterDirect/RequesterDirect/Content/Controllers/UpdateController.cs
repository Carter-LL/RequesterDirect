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
            foreach (Frame frame in Globals.Frames)
            {
                frame.Update();
            }
        }
    }
}
