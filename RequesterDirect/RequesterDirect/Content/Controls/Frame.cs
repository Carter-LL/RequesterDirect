using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Point = Microsoft.Xna.Framework.Point;
using Color = Microsoft.Xna.Framework.Color;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using RequesterDirect.Content.Interfaces;

namespace RequesterDirect.Content.Controls
{
    public class Frame : FrameInterface
    {
        private Point Location { get; set; } = new Point(0, 0);
        private Size Size { get; set; } = new Size(100, 50);
        private Color BackColor { get; set; } = Color.Gray;

        private bool _isDragging;
        private bool _draggable = true;
        private bool _invokeDrag = false;
        private MouseState _previousMouseState;
        private Rectangle _baseRectangle;
        private Point _dragOffset;

        private SpriteFont _fontArial;

        public Frame(ContentManager content) 
        {
            _fontArial = content.Load<SpriteFont>("Arial");
            _baseRectangle = new Rectangle(Location.X, Location.Y, Size.Width, Size.Height);
            _isDragging = false;
            _previousMouseState = Mouse.GetState();
        }

        public virtual void Update()
        {
            #region Mouse Dragging
            MouseState mouseState = Mouse.GetState();
            Point mouseLocation = mouseState.Position;

            if (_baseRectangle.Contains(mouseLocation) && _draggable || _invokeDrag)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (!_isDragging)
                    {
                        // Start dragging
                        _isDragging = true;

                        // Calculate offset between mouse position and the top-left corner of the frame
                        _dragOffset = new Point(mouseLocation.X - Location.X, mouseLocation.Y - Location.Y);
                    }

                    // Update the location to follow the mouse, maintaining the offset
                    Location = new Point(mouseLocation.X - _dragOffset.X, mouseLocation.Y - _dragOffset.Y);
                }
                else
                {
                    // End dragging
                    _isDragging = false;
                }
            }
            else
            {
                // End dragging if the mouse is no longer over the rectangle
                if (mouseState.LeftButton == ButtonState.Released)
                {
                    _isDragging = false;
                }
            }

            _baseRectangle = new Rectangle(Location.X, Location.Y, Size.Width, Size.Height);
            _previousMouseState = mouseState;
            #endregion
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Drawing.Rectangle(spriteBatch, _baseRectangle, BackColor);
        }

        public void SetLocation(Point location)
        {
            Location = location;
            _baseRectangle = new Rectangle(Location.X, Location.Y, Size.Width, Size.Height);
        }

        public Point GetLocation()
        {
            return Location;
        }

        public void SetSize(Size size)
        {
            Size = size;
            _baseRectangle = new Rectangle(Location.X, Location.Y, Size.Width, Size.Height);
        }

        public Size GetSize()
        {
            return Size;
        }

        public void SetBackColor(Color color) 
        {
            BackColor = color;
        }

        public Rectangle GetBounds()
        {
            return _baseRectangle;
        }

        public void ToggleDrag(bool status)
        {
            _invokeDrag = status;
        }

        public void SetDraggable(bool status)
        {
            _draggable = status;
        }
    }
}
