using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterListMB
{
    public class ChapterList
    {
        public List<Chapter> Chapters { get; private set; }
        public int NumChapters { get { return Chapters.Count; } }

        public void CreateNewChapter(string name, int position)
        {
            Chapters.Add(new Chapter(name, position));
        }

        public void RemoveChapter(Chapter chapterToRemove)
        {
            if (Chapters.Contains(chapterToRemove))
            {
                Chapters.Remove(chapterToRemove);
            }
        }
    }
}
