using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
            _spriteBatch.Begin();

            // Draw a rectangle at position (100, 100) with width 200 and height 150
            Drawing.DrawRectangle(_spriteBatch, new Rectangle(100, 100, 200, 150), Color.Red);

            _spriteBatch.End();
        }
    }
}
