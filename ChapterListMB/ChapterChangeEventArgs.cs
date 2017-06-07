using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterListMB
{
    public class ChapterChangeEventArgs : EventArgs
    {
        /// <summary>
        /// The chapter whose position and/or title will be changed
        /// </summary>
        public Chapter ChapterToChange { get; set; }
        /// <summary>
        /// Title of the chapter to change
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Position of the chapter to change, in milliseconds
        /// </summary>
        public int Position { get; set; }
        /// <summary>
        /// Creates a new instance of ChapterChangeEventArgs
        /// </summary>
        /// <param name="chapterToChange"></param>
        /// <param name="title"></param>
        /// <param name="position"></param>
        public ChapterChangeEventArgs(Chapter chapterToChange, string title, int position)
        {
            ChapterToChange = chapterToChange;
            Title = title;
            Position = position;
        }
    }
}
