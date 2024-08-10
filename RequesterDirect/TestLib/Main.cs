using RequesterDirect.Content.Controls;
using RequesterDirect.Content;
using System.Drawing;
using Point = Microsoft.Xna.Framework.Point;
using RequesterDirect.Content.Interfaces;

namespace TestLib
{
    public class Main : IAddon
    {
        public void LibraryLoad()
        {
            Console.WriteLine("TestLib : Load");
        }

        public Window LibraryWindow()
        {
            Window window = new("TestLibAddonWindow");
            window.SetLocation(new Point(250, 250));

            Button button = new("buttonc");

            button.SetText("Fullscreen");
            button.SetSize(new Size(window.GetSize().Width - 2, 20));
            button.SetLocation(new Point(button.GetLocation().X + 1, button.GetLocation().Y + 20));
            button.Follow(window);
            button.Click += Button_Click;
            Globals.Frames.Add(button);

            return window;
        }

        private void Button_Click(object? sender, Button e)
        {
            Window w = (Window)Globals.Frames.Find(x => x.GetName().Equals("TestLibAddonWindow"));
            w.FullScreen();
        }

        public void LoadContent()
        {
            Console.WriteLine("TestLib : Content");
        }

        public void Update()
        {
            //Console.WriteLine("TestLib : Update");
        }

        public void Draw()
        {
            //Console.WriteLine("TestLib : Draw");
        }
    }
}
