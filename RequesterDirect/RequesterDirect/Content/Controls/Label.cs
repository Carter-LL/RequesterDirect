using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RequesterDirect.Content.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Color = Microsoft.Xna.Framework.Color;

namespace RequesterDirect.Content.Controls
{
    public class Label : Frame, FrameInterface
    {
        private string Text { get; set; } = "New Label";
        public Label(string name) : base(name)
        {

        }

        public override void Update()
        {
            base.Update();
            if (!isViewable()) { return; }

            Vector2 textSize = Globals.Fonts["Arial Bold"].MeasureString(Text);
            base.SetSize(new Size(base.GetSize().Width, Convert.ToInt32(textSize.Y)));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if(!isViewable()) { return; }

            Drawing.String(spriteBatch, Globals.Fonts["Arial Bold"], new Vector2(base.GetLocation().X + 3, base.GetLocation().Y), Color.Red, Text);
        }
    }
}
