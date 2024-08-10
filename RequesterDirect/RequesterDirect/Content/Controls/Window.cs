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
using Vector2 = Microsoft.Xna.Framework.Vector2;
using System.Drawing;
using Microsoft.Xna.Framework.Input;

namespace RequesterDirect.Content.Controls
{
    public class Window : Frame, FrameInterface
    {
        private Color TitlebarColor { get; set; } = Color.White;
        private Color TitleColor { get; set; } = Color.DarkGray;
        private Color ActiveTitlebarColor { get; set; } = Color.FromNonPremultiplied(94, 88, 84, 255);
        private string Title { get; set; } = "New Window";

        private Rectangle _titlebarRectangle;
        private int _titlebarHeight = 20;

        public Window(string name) : base(name)
        {
            base.SetBackColor(Color.FromNonPremultiplied(45, 45, 45, 255));
            base.SetSize(new Size(200, 250));
            base.SetDraggable(false);
            base.SetTopLevel(1);
        }

        public override void Update()
        {
            base.Update();
            if (!isViewable()) { return; }

            #region Mouse Dragging
            _titlebarRectangle = new Rectangle(base.GetLocation().X, base.GetLocation().Y, base.GetSize().Width, _titlebarHeight);

            MouseState mouseState = Mouse.GetState();
            Point mouseLocation = mouseState.Position;

            if (_titlebarRectangle.Contains(mouseLocation))
            {
                base.InvokeDrag(true);
            } else
            {
                base.InvokeDrag(false);
            }
            #endregion
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (!isViewable()) { return; }

            //Toolbar
            if (base.GetActive())
            {
                Drawing.Rectangle(spriteBatch, _titlebarRectangle, ActiveTitlebarColor);
            } 
            else
            {
                Drawing.Rectangle(spriteBatch, _titlebarRectangle, TitlebarColor);
            }
            Drawing.OutlinedRectangle(spriteBatch, _titlebarRectangle, Color.Black, 1);

            //Title
            Drawing.String(spriteBatch, Globals.Fonts["Arial Bold"], new Vector2(_titlebarRectangle.X + 5, _titlebarRectangle.Top + 2), TitleColor, Title);

            //Main frame border
            Drawing.OutlinedRectangle(spriteBatch, base.GetBounds(), Color.Black, 1);
        }

        public void SetActiveTitlebarColor(Color color)
        {
            ActiveTitlebarColor = color;
        }

        public void SetTitlebarColor(Color color)
        {
            TitlebarColor = color;
        }

        public void SetTitle(string title)
        {
            Title = title;
        }

        public void SetTitleColor(Color color)
        {
            TitleColor = color;
        }

        public Color GetActiveTitlebarColor()
        {
            return ActiveTitlebarColor;
        }

        public Color GetTitlebarColor()
        {
            return TitlebarColor;
        }

        public string GetTitle()
        {
            return Title;
        }

        public Color GetTitleColor()
        {
            return TitleColor;
        }

        public override void Follow(Frame frame)
        {
            base.Follow(frame);
        }

        public void FullScreen()
        {
            SetSize(Globals.WindowSize);
            SetTopLevel(-100);
            SetLocation(new Point(0, 0));
            List<Frame> frames = Globals.Frames.FindAll(x => x.GetFollow() != null);
            foreach (Frame frame in frames)
            {
                if (frame.GetFollow().GetHashCode().Equals(this.GetHashCode()))
                {
                    frame.SetTopLevel(-101);
                }
            }
            Globals.Frames.UpdateSort();
        }
    }
}
