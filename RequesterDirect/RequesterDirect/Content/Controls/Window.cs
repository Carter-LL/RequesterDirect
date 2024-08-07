using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RequesterDirect.Content.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Point = Microsoft.Xna.Framework.Point;
using Color = Microsoft.Xna.Framework.Color;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using System.Drawing;
using Microsoft.Xna.Framework.Input;

namespace RequesterDirect.Content.Controls
{
    public class Window : Frame, FrameInterface
    {
        private Color ToolbarColor { get; set; } = Color.White;
        private Color ActiveToolbarColor { get; set; } = Color.Black;

        private Rectangle _toolbarRectangle;
        private int _toolbarHeight = 20;

        private bool _isActive = false;

        public Window(ContentManager content) : base(content)
        {
            base.SetSize(new Size(200, 250));
            base.SetDraggable(false);
        }

        public override void Update()
        {
            base.Update();

            #region Mouse Dragging
            _toolbarRectangle = new Rectangle(base.GetLocation().X, base.GetLocation().Y, base.GetSize().Width, _toolbarHeight);

            MouseState mouseState = Mouse.GetState();
            Point mouseLocation = mouseState.Position;

            if (_toolbarRectangle.Contains(mouseLocation))
            {
                base.ToggleDrag(true);
                if(mouseState.LeftButton == ButtonState.Pressed)
                {
                    _isActive = true;
                }
            } else
            {
                base.ToggleDrag(false);
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (!base.GetBounds().Contains(mouseLocation))
                    {
                        _isActive = false;
                    }
                }
            }
            #endregion
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            //Toolbar
            if (_isActive)
            {
                Drawing.Rectangle(spriteBatch, _toolbarRectangle, ActiveToolbarColor);
            } 
            else
            {
                Drawing.Rectangle(spriteBatch, _toolbarRectangle, ToolbarColor);
            }
            Drawing.OutlinedRectangle(spriteBatch, _toolbarRectangle, Color.Black, 1);

            //Main frame border
            Drawing.OutlinedRectangle(spriteBatch, base.GetBounds(), Color.Black, 1);
        }

        public bool GetActive()
        {
            return _isActive;
        }

        public void SetActiveToolbarColor(Color color)
        {
            ActiveToolbarColor = color;
        }
    }
}
