using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using RequesterDirect.Content.Controls;
using SharpDX.Direct3D9;
using Frame = RequesterDirect.Content.Controls.Frame;

namespace RequesterDirect.Content.Models
{
    public class FrameCollection : Collection<Frame>
    {
        private bool _isSorting;

        public FrameCollection() { }

        // Override InsertItem to perform actions when an item is added
        protected override void InsertItem(int index, Frame item)
        {
            base.InsertItem(index, item);
            OnFrameAdded(item);

            if (!_isSorting)
            {
                _isSorting = true;
                Sort((f1, f2) => f2.GetTopLevel().CompareTo(f1.GetTopLevel()));
                _isSorting = false;
            }
        }

        public void UpdateSort()
        {
            Sort((f1, f2) => f2.GetTopLevel().CompareTo(f1.GetTopLevel()));
        }

        // Override RemoveItem to perform actions when an item is removed
        protected override void RemoveItem(int index)
        {
            Frame item = this[index];
            base.RemoveItem(index);
            OnFrameRemoved(item);
        }

        // Define your custom method to handle frame additions
        private void OnFrameAdded(Frame frame)
        {
        }

        // Define your custom method to handle frame removals
        private void OnFrameRemoved(Frame frame)
        {
        }

        // Calculate the total height of all frames in the list
        public int CalculateTotalHeight()
        {
            return this.Sum(frame => frame.GetSize().Height);
        }

        // Implement TrueForAll to check if all elements satisfy a condition
        public bool TrueForAll(Func<Frame, bool> predicate)
        {
            return this.All(predicate);
        }

        // Implement Find to search for an element that matches the condition
        public Frame Find(Func<Frame, bool> predicate)
        {
            return this.FirstOrDefault(predicate);
        }

        public List<Frame> FindAll(Func<Frame, bool> predicate)
        {
            return this.Where(predicate).ToList();
        }

        // Implement Sort to sort the elements based on a comparison function
        public void Sort(Comparison<Frame> comparison)
        {
            List<Frame> sortedList = this.ToList();
            sortedList.Sort(comparison);
            // Clear the current list and add sorted items
            this.Clear();
            foreach (var frame in sortedList)
            {
                this.Add(frame);
            }
        }
    }
}
