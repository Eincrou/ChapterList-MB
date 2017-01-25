using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterListMB
{
    public class Chapter
    {
        /// <summary>
        /// Title of this chapter.
        /// </summary>
        public string Title { get; private set; }
        /// <summary>
        /// Timecode of this chapter's position, in HH:MM:SS
        /// </summary>
        public string TimeCode
        {
            get
            {
                var time = new TimeSpan(0, 0, 0, 0, Position);
                return $"{time.Hours}:{time.Minutes}:{time.Seconds}";
            }
        }
        /// <summary>
        /// Position of the chapter, in milliseconds.
        /// </summary>
        public int Position { get; private set; }
        /// <summary>
        /// Creates a new Chapter object.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="position"></param>
        public Chapter(string title, int position)
        {
            Title = title;
            Position = position;
        }
    }
}
