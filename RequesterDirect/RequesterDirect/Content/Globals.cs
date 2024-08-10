using Microsoft.Xna.Framework.Graphics;
using RequesterDirect.Content.Controls;
using RequesterDirect.Content.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RequesterDirect.Content
{
    public class Globals
    {
        public static FrameCollection Frames = new();
        public static Dictionary<string, SpriteFont> Fonts = new();
        public static Size WindowSize {  get; set; }
        public static Dictionary<string, string> DebugLabels = new();
        public static List<LibraryModel> LoadedAssemblies = new();
    }
}
