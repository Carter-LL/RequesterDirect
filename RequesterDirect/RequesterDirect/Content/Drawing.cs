using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequesterDirect.Content
{
    public static class Drawing
    {
        public static void Rectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color)
        {
            spriteBatch.Draw(Textures.whitePixel, rectangle, color);
        }

        public static void String(SpriteBatch spriteBatch, SpriteFont spriteFont, Vector2 location, Color color, string text)
        {
            spriteBatch.DrawString(spriteFont, text, location, color);
        }

        public static void OutlinedRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color borderColor, int borderWidth)
        {
            // Draw the borders
            // Top border
            Rectangle topBorder = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, borderWidth);
            Rectangle(spriteBatch, topBorder, borderColor);

            // Bottom border
            Rectangle bottomBorder = new Rectangle(rectangle.X, rectangle.Bottom - borderWidth, rectangle.Width, borderWidth);
            Rectangle(spriteBatch, bottomBorder, borderColor);

            // Left border
            Rectangle leftBorder = new Rectangle(rectangle.X, rectangle.Y + borderWidth, borderWidth, rectangle.Height - 2 * borderWidth);
            Rectangle(spriteBatch, leftBorder, borderColor);

            // Right border
            Rectangle rightBorder = new Rectangle(rectangle.Right - borderWidth, rectangle.Y + borderWidth, borderWidth, rectangle.Height - 2 * borderWidth);
            Rectangle(spriteBatch, rightBorder, borderColor);
        }
    }
}
