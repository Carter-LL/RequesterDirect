using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RequesterDirect.Content.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;

namespace RequesterDirect.Content.Controllers
{
    public class ContentController
    {
        public ContentController(GraphicsDevice graphicsDevice, ContentManager content) 
        {
            // Create a 1x1 pixel texture
            Textures.whitePixel = new Texture2D(graphicsDevice, 1, 1);
            Color[] data = new Color[1];
            data[0] = Color.White; // or any color you prefer
            Textures.whitePixel.SetData(data);

            //Make fonts
            Globals.Fonts.Add("Arial Bold", content.Load<SpriteFont>("Arial Bold")); // Exclude the .xnb extension
            Globals.Fonts.Add("Arial", content.Load<SpriteFont>("Arial")); // Exclude the .xnb extension


            //Test object
            Window test_window = new Window();
            test_window.SetLocation(new Point(50, 300));
            Globals.Frames.Add(test_window);

            Label label = new Label();
            label.SetLocation(new Point(label.GetLocation().X + 1, label.GetLocation().Y + 20));
            label.SetSize(new Size(test_window.GetSize().Width - 2, label.GetSize().Height));
            label.Follow(test_window);
            Globals.Frames.Add(label);

            Frame frame = new Frame();
            frame.SetLocation(new Point(0, 0));
            frame.SetSize(new Size(Globals.WindowSize.Width, 30));
            Globals.Frames.Add(frame);

            Button button = new Button();
            button.Follow(frame);
            Globals.Frames.Add(button);
        }
    }
}
