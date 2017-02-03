using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterListMB
{
    public class Chapter : IEquatable<Chapter>
    {
        /// <summary>
        /// Title of this chapter.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Timecode of this chapter's position, in H:MM:SS
        /// </summary>
        public string TimeCode
        {
            get
            {
                var time = new TimeSpan(0, 0, 0, 0, Position);
                return $"{time.Hours}:{time.Minutes:00}:{time.Seconds:00}";
            }
        }
        /// <summary>
        /// Position of the chapter, in milliseconds.
        /// </summary>
        public int Position { get; set; }

        public int ChapterNumber { get; private set; }

        /// <summary>
        /// Creates a new Chapter object.
        /// </summary>
        /// <param name="position">Position of the chapter, in milliseconds.</param>
        /// <param name="title">Name of the chapter.</param>
        public Chapter(int position, string title = "New Chapter")
        {
            Title = title;
            Position = position;
        }

        public void SetChapterNumber(int num)
        {
            ChapterNumber = num;
        }

        public bool Equals(Chapter other)
        {
            if (other == null) return false;
            return (Title == other.Title) && (Position == other.Position);
        }

        public override string ToString()
        {
            return $"{TimeCode} - {Title}";
        }
    }
}
