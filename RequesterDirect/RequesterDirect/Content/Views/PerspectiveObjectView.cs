using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Point = Microsoft.Xna.Framework.Point;
using RequesterDirect.Content.Controls;

namespace RequesterDirect.Content.Views
{
    public class PerspectiveObjectView
    {
        private bool _isDragging = false;
        private Point _dragOffset;
        public PerspectiveObjectView() { }

        public void Update()
        {
            MouseState mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (!Globals.Frames.TrueForAll(x => x.GetActive()))
                {

                    Point mouseLocation = mouseState.Position;
                    foreach(Frame frame in Globals.Frames)
                    {
                        if(frame.GetType() == typeof(Frame)) { return; }
                        if (!_isDragging)
                        {
                            // Start dragging
                            _isDragging = true;

                            // Calculate offset between mouse position and the top-left corner of the frame
                            _dragOffset = new Point(mouseLocation.X - frame.GetLocation().X, mouseLocation.Y - frame.GetLocation().Y);
                        }

                        // Update the location to follow the mouse, maintaining the offset
                        frame.SetLocation(new Point(mouseLocation.X - _dragOffset.X, mouseLocation.Y - _dragOffset.Y));
                        //_baseRectangle = new Rectangle(Location.X, Location.Y, Size.Width, Size.Height);
                    }
                } else
                {
                    _isDragging = false;
                }
            } else
            {
                _isDragging = false;
            }
        }
    }
}
