using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RequesterDirect.Content.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace RequesterDirect.Content.Controls
{
    public class Button : Frame, FrameInterface
    {
        public event EventHandler<Point> Click;

        private Color TextColor { get; set; } = Color.White;
        private Color HoverColor { get; set; } = Color.FromNonPremultiplied(45, 45, 45, 255);
        private string Text { get; set; } = "Button";

        private bool _hovering = false;
        private bool _mouseDown = false;

        public Button()
        {
            base.SetBackColor(Color.FromNonPremultiplied(94, 88, 84, 255));
            base.SetSize(new Size(80, 30));
        }

        public override void Update()
        {
            base.Update();

            #region Mouse click & hover
            MouseState mouseState = Mouse.GetState();
            Point mouseLocation = mouseState.Position;

            if (base.GetBounds().Contains(mouseLocation))
            {
                if(mouseState.LeftButton == ButtonState.Released && _mouseDown)
                {
                    Click?.Invoke(this, mouseLocation);
                    _mouseDown = false;
                    _hovering = true;
                }

                if(mouseState.LeftButton == ButtonState.Pressed)
                {
                    _mouseDown = true;
                    _hovering = false;
                } 
                else
                {
                    _hovering = true;
                }
            } else { _hovering = false; }
            #endregion
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (_hovering)
            {
                Drawing.Rectangle(spriteBatch, base.GetBounds(), HoverColor);
            }

            //Center Text
            Vector2 textPosition = DrawingUtils.CenterTextInBounds(base.GetBounds(), Globals.Fonts["Arial"], Text);
            Drawing.String(spriteBatch, Globals.Fonts["Arial"], textPosition, TextColor, Text);

            //Main frame border
            Drawing.OutlinedRectangle(spriteBatch, base.GetBounds(), Color.Black, 1);
        }

        public void SetHoverColor(Color color)
        {
            HoverColor = color;
        }

        public void SetTextColor(Color color)
        {
            TextColor = color;
        }

        public void SetText(string text)
        {
            Text = text;
        }

        public bool GetHovering()
        {
            return _hovering;
        }

        public Color GetHoverColor()
        {
            return HoverColor;
        }

        public Color GetTextColor()
        {
            return TextColor;
        }

        public string GetText()
        {
            return Text;
        }
    }
}
