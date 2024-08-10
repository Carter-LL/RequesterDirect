using RequesterDirect.Content.Controls;
using RequesterDirect.Content.Models;
using SharpFont;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Button = RequesterDirect.Content.Controls.Button;
using Point = Microsoft.Xna.Framework.Point;

namespace RequesterDirect.Content.UI
{
    public class MainToolbarUI
    {
        public MainToolbarUI()
        {
            CreateToolbar();
            CreateAddonWindow();
        }

        private void CreateToolbar()
        {
            //Background Frame
            Frame frame = new Frame("ToolbarFrame");
            frame.SetLocation(new Point(0, 0));
            frame.SetSize(new Size(Globals.WindowSize.Width, 30));
            Globals.Frames.Add(frame);

            //Addons button
            Button button = new Button("AddonButton");
            button.SetTopLevel(-1);
            button.SetText("Addons");
            button.Follow(frame);
            button.Click += AddonButton_Click;
            Globals.Frames.Add(button);
        }

        private void CreateAddonWindow()
        {
            //Addon Window
            Window addon_window = new Window("AddonWindow");
            addon_window.SetLocation(new Point(50, 300));
            addon_window.SetSize(new Size(addon_window.GetSize().Width, 600));
            addon_window.SetVisible(false);
            addon_window.SetTitle("Addons");
            Globals.Frames.Add(addon_window);

            //Label
            /*
            Button label = new Button(Guid.NewGuid().ToString());
            label.SetLocation(new Point(label.GetLocation().X + 1, label.GetLocation().Y + 20));
            label.SetSize(new Size(test_window.GetSize().Width - 2, label.GetSize().Height));
            label.Follow(test_window);
            Globals.Frames.Add(label);*/

            FrameCollection buttons = new FrameCollection();
            
            foreach (LibraryModel library in Globals.LoadedAssemblies)
            {
                Button button = new Button(Guid.NewGuid().ToString());
                button.SetTopLevel(1);
                button.AddObjetToRuntimeData("library", library.GetHashCode().ToString());
                button.SetText(library.Name);
                button.SetSize(new Size(addon_window.GetSize().Width - 2, 20));
                button.Click += Addon_LoadClicked;
                buttons.Add(button);
            }

            int yspacing = buttons.CalculateTotalHeight() / buttons.Count;
            addon_window.SetSize(new Size(addon_window.GetSize().Width, buttons.CalculateTotalHeight() + (buttons.CalculateTotalHeight() / buttons.Count) + 1));
            int index = 0;
            foreach (Button button in buttons)
            {
                button.SetLocation(new Point(button.GetLocation().X + 1, button.GetLocation().Y + 20 + (yspacing * index)));
                button.Follow(addon_window);
                Globals.Frames.Add(button);
                index++;
            }
        }

        private void Addon_LoadClicked(object sender, Button button)
        {
            try
            {
                LibraryModel library = Globals.LoadedAssemblies.Find(x => x.GetHashCode().ToString().Equals(button.GetObjectFromRuntimeData("library")));
                MethodInfo method = library.type.GetMethod("LibraryWindow");
                Window result = (Window)method.Invoke(library.instance, null);
                result.SetTitle(library.Name);

                Frame frame = Globals.Frames.Find(x => x.GetName().Equals(result.GetName()));

                if(frame != null)
                {
                    Globals.Frames.RemoveAll(frame);
                }

                Globals.Frames.Add(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to execute LibraryWindow function in Library " + ex.StackTrace);
            }
        }

        private void AddonButton_Click(object sender, Button e)
        {
            var frame = Util.GetFrameByName("AddonWindow");
            frame.SetVisible(!frame.GetVisible());
        }
    }
}
