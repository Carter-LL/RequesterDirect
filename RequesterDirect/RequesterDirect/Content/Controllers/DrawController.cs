using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RequesterDirect.Content.Controls;
using RequesterDirect.Content.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            foreach (Frame frame in Globals.Frames)
            {
                //Only draw frames we can see. Massive FPS fix
                Rectangle rectangle = new()
                {
                    X = 0,
                    Y = 0,
                    Width = Globals.WindowSize.Width,
                    Height = Globals.WindowSize.Height
                };

                Rectangle frameBounds = frame.GetBounds();

                // Calculate the intersection between the frame's bounds and the rectangle
                Rectangle visibleBounds = Rectangle.Intersect(frameBounds, rectangle);

                if (!visibleBounds.IsEmpty)
                {
                    // Draw only the portion of the frame that is within the visible rectangle
                    frame.Draw(_spriteBatch);
                }
            }
        }



        public SpriteBatch GetSpriteBatch()
        {
            return _spriteBatch;
        }
    }
}
