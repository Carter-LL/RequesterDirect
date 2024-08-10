using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using RequesterDirect.Content.Controls;

namespace RequesterDirect.Content.Views
{
    public class PerspectiveObjectView
    {
        private bool _isDragging = false;
        private Point _dragOffset;

        private int _previousScrollWheelValue;

        public PerspectiveObjectView()
        {
            MouseState mouseState = Mouse.GetState();
            _previousScrollWheelValue = mouseState.ScrollWheelValue;
        }

        public void Update()
        {
            UpdateDebugLabel();
            MouseState mouseState = Mouse.GetState();
            Point mouseLocation = mouseState.Position;

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                HandleMousePress(mouseLocation);
            }
            else
            {
                EndDraggingForAllFrames();
            }

            // Scroll handling
            if (mouseState.ScrollWheelValue != _previousScrollWheelValue)
            {
                int scrollDelta = mouseState.ScrollWheelValue - _previousScrollWheelValue;
                HandleScroll(scrollDelta);
                _previousScrollWheelValue = mouseState.ScrollWheelValue;
            }
        }

        private void UpdateDebugLabel()
        {
            if (Globals.DebugLabels.ContainsKey("perspective"))
            {
                Globals.DebugLabels["perspective"] = $"Perspective Dragging: {_isDragging}";
            }
            else
            {
                Globals.DebugLabels.Add("perspective", $"Perspective Dragging: {_isDragging}");
            }
        }

        private void HandleMousePress(Point mouseLocation)
        {
            if (IsMouseOverFrames(mouseLocation))
            {
                return;
            }

            if (!Globals.Frames.TrueForAll(x => x.GetActive()))
            {
                StartDragging(mouseLocation);
            }
            else
            {
                EndDraggingForAllFrames();
            }
        }

        private bool IsMouseOverFrames(Point mouseLocation)
        {
            return Globals.Frames.Any(frame => frame.GetBounds().Contains(mouseLocation));
        }

        private bool AnyFramesActive()
        {
            return Globals.Frames.OfType<Window>().Any(frame => (bool?)frame.GetObjectFromRuntimeData("isdragging") == true);
        }

        private void StartDragging(Point mouseLocation)
        {
            foreach (Frame frame in Globals.Frames.OfType<Window>())
            {
                if (!IsDragging(frame))
                {
                    BeginDragging(frame, mouseLocation);
                    _isDragging = true;
                }
                UpdateFramePosition(frame, mouseLocation);
            }
        }

        private bool IsDragging(Frame frame)
        {
            return (bool?)frame.GetObjectFromRuntimeData("isdragging") == true;
        }

        private void BeginDragging(Frame frame, Point mouseLocation)
        {
            frame.SetObjectInRuntimeData("isdragging", true);
            Point offset = new Point(mouseLocation.X - frame.GetLocation().X, mouseLocation.Y - frame.GetLocation().Y);
            frame.SetObjectInRuntimeData("dragoffset", offset);
        }

        private void UpdateFramePosition(Frame frame, Point mouseLocation)
        {
            if (frame.GetObjectFromRuntimeData("dragoffset") is Point dragOffset)
            {
                frame.SetLocation(new Point(mouseLocation.X - dragOffset.X, mouseLocation.Y - dragOffset.Y));
            }
        }

        private void EndDraggingForAllFrames()
        {
            foreach (Frame frame in Globals.Frames.OfType<Window>())
            {
                frame.SetObjectInRuntimeData("isdragging", false);
            }
            _isDragging = false;
        }

        private void HandleScroll(int scrollDelta)
        {
            // Move frames based on the scroll direction
            // Positive scrollDelta means scroll up, negative means scroll down
            foreach (Frame frame in Globals.Frames.OfType<Window>())
            {
                MoveFrame(frame, scrollDelta);
            }
        }

        private void MoveFrame(Frame frame, int scrollDelta)
        {
            // Define the movement speed
            int movementSpeed = 40; // Adjust this value as needed

            // Determine movement direction
            int deltaX = 0;
            int deltaY = 0;

            if (scrollDelta < 0)
            {
                // Scroll up - move up
                deltaY = -movementSpeed;
            }
            else if (scrollDelta > 0)
            {
                // Scroll down - move down
                deltaY = movementSpeed;
            }

            // Update the frame position based on the movement direction
            Point currentPosition = frame.GetLocation();
            Point newPosition = new Point(currentPosition.X + deltaX, currentPosition.Y + deltaY);
            frame.SetLocation(newPosition);
        }
    }
}
