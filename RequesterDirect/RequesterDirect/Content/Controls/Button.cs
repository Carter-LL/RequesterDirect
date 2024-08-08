using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RequesterDirect.Content.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = Microsoft.Xna.Framework.Color;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace RequesterDirect.Content.Controls
{
    public class Button : Frame, FrameInterface
    {
        private Color TextColor { get; set; } = Color.White;
        private string Text { get; set; } = "Button";

        public Button(ContentManager content) : base(content)
        {
            base.SetBackColor(Color.FromNonPremultiplied(94, 88, 84, 255));
            base.SetSize(new Size(80, 30));
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            Vector2 textPosition = DrawingUtils.CenterTextInBounds(base.GetBounds(), Globals.Fonts["Arial"], Text);
            Drawing.String(spriteBatch, Globals.Fonts["Arial"], textPosition, TextColor, Text);

            //Main frame border
            Drawing.OutlinedRectangle(spriteBatch, base.GetBounds(), Color.Black, 1);
        }
    }
}
