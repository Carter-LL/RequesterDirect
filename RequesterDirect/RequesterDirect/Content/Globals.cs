﻿using Microsoft.Xna.Framework.Graphics;
using RequesterDirect.Content.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequesterDirect.Content
{
    public class Globals
    {
        public static List<Frame> Frames = new();
        public static Dictionary<string, SpriteFont> Fonts = new();
        public static Size WindowSize {  get; set; }
    }
}
