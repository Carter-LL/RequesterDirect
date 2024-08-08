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
            Window test_frame = new Window(content);
            test_frame.SetLocation(new Point(50, 300));
            Globals.Frames.Add(test_frame);

            Frame frame = new Frame(content);
            frame.SetLocation(new Point(0, 0));
            frame.SetSize(new Size(Globals.WindowSize.Width, 30));
            Globals.Frames.Add(frame);

            Button button = new Button(content);
            button.Follow(frame);
            Globals.Frames.Add(button);
        }
    }
}
