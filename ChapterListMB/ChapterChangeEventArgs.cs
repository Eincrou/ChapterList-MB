using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterListMB
{
    public class ChapterChangeEventArgs : EventArgs
    {
        public Chapter ChapterToChange { get; set; }
        public string Title { get; set; }
        public int Position { get; set; }

        public ChapterChangeEventArgs(Chapter chapterToChange, string title, int position)
        {
            ChapterToChange = chapterToChange;
            Title = title;
            Position = position;
        }
    }
}
