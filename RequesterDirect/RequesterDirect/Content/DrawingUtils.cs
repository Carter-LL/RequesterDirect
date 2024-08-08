using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequesterDirect.Content
{
    public static class DrawingUtils
    {
        public static Vector2 CenterTextInBounds(Rectangle bounds, SpriteFont font, string text)
        {
            // Calculate the position to center the text
            Vector2 boundsPosition = new Vector2(bounds.X, bounds.Y);
            Vector2 boundsSize = new Vector2(bounds.Width, bounds.Height);
            Vector2 textSize = font.MeasureString(text);

            return boundsPosition + (boundsSize / 2) - (textSize / 2);
        }
    }
}
