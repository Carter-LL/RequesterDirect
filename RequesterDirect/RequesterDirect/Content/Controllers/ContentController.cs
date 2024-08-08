﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RequesterDirect.Content.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


            Window frame = new Window(content);
            frame.SetBackColor(Color.FromNonPremultiplied(45, 45, 45, 255));
            frame.SetActiveTitlebarColor(Color.FromNonPremultiplied(94, 88, 84, 255));
            frame.SetTitleColor(Color.DarkGray);

            Globals.Frames.Add(frame);
        }
    }
}
