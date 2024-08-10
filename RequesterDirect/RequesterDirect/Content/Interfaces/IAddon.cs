using RequesterDirect.Content.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequesterDirect.Content.Interfaces
{
    public interface IAddon
    {
        public void LibraryLoad();

        public Window LibraryWindow();

        public void LoadContent();

        public void Update();

        public void Draw();
    }
}
