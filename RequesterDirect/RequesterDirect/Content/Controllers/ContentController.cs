using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequesterDirect.Content.Controllers
{
    public class ContentController
    {
        public ContentController(GraphicsDevice graphicsDevice) 
        {
            // Create a 1x1 pixel texture
            Textures.whitePixel = new Texture2D(graphicsDevice, 1, 1);
            Color[] data = new Color[1];
            data[0] = Color.White; // or any color you prefer
            Textures.whitePixel.SetData(data);
        }
    }
}
