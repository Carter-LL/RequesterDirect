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

        private bool _isDragging = false;
        private bool _isActive = false;
        private bool _draggable = false;
        private bool _invokeDrag = false;
        private Rectangle _baseRectangle;
        private Point _dragOffset;
        private Frame _followFrame;

        public Frame() 
        {
            _baseRectangle = new Rectangle(Location.X, Location.Y, Size.Width, Size.Height);
        }

        public virtual void Update()
        {
            if (!Globals.DebugLabels.ContainsKey(GetHashCode().ToString()))
            {
                Globals.DebugLabels.Add(GetHashCode().ToString(), $"{GetHashCode().ToString()} Frame Dragging: {_isDragging}");
            }
            else
            {
                Globals.DebugLabels[GetHashCode().ToString()] = $"{GetHashCode().ToString()} Frame Dragging: {_isDragging}";
            }

            MouseState mouseState = Mouse.GetState();
            Point mouseLocation = mouseState.Position;

            #region Check is active
            if (_baseRectangle.Contains(mouseLocation))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    _isActive = true;
                }
            } 
            else
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (!_baseRectangle.Contains(mouseLocation))
                    {
                        _isActive = false;
                    }
                }
            }
            #endregion

            #region Mouse Dragging

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

            #endregion

            if (_followFrame != null)
            {
                _baseRectangle = new Rectangle(_followFrame.GetLocation().X + Location.X, _followFrame.GetLocation().Y + Location.Y, Size.Width, Size.Height);
            }
            else
            {
                _baseRectangle = new Rectangle(Location.X, Location.Y, Size.Width, Size.Height);
            }
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

        public void SetBackColor(Color color)
        {
            BackColor = color;
        }

        public void SetSize(Size size)
        {
            Size = size;
            _baseRectangle = new Rectangle(_baseRectangle.X, _baseRectangle.Y, Size.Width, Size.Height);
        }

        public void SetDraggable(bool status)
        {
            _draggable = status;
        }

        public Point GetLocation()
        {
            return new Point(_baseRectangle.X, _baseRectangle.Y);
        }

        public Size GetSize()
        {
            return Size;
        }

        public Rectangle GetBounds()
        {
            return _baseRectangle;
        }

        public bool GetActive()
        {
            return _isActive;
        }

        public void InvokeDrag(bool status)
        {
            _invokeDrag = status;
        }

        public virtual void Follow(Frame frame)
        {
            _followFrame = frame;
        }
    }
}
