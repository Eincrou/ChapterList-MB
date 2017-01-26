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

        public ChapterList()
        {
            Chapters = new List<Chapter>();
        }
        public void CreateNewChapter(string name, int position)
        {
            Chapters.Add(new Chapter(position, name));
            ReorderChapters();

        }

        public void RemoveChapter(Chapter chapterToRemove)
        {
            if (!Chapters.Contains(chapterToRemove)) return;
            Chapters.Remove(chapterToRemove);
            ReorderChapters();
        }

        private void ReorderChapters()
        {
            var reorderedChapters = from chapter in Chapters
                                    orderby chapter.Position ascending
                                    select chapter;
            Chapters = reorderedChapters.ToList();
        }
    }
}
