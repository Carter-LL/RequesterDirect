using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RequesterDirect.Content.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequesterDirect.Content.Controllers
{
    public class DrawController
    {
        private SpriteBatch _spriteBatch;

        public DrawController(SpriteBatch spriteBatch) 
        { 
            this._spriteBatch = spriteBatch;
        }

        public void Draw()
        {
            foreach(Frame frame in Globals.Frames)
            {
                frame.Draw(_spriteBatch);
            }
        }

        public SpriteBatch GetSpriteBatch()
        {
            return _spriteBatch;
        }
    }
}
