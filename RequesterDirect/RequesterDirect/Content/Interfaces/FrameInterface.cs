using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace RequesterDirect.Content.Interfaces
{
    public interface FrameInterface
    {
        public void Update();
        public void Draw(SpriteBatch spriteBatch);
    }
}
