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
            SortChapters();
            OnChapterListUpdated();
        }
        public void CreateNewChapter(int position)
        {
            Chapters.Add(new Chapter(position));
            SortChapters();
            OnChapterListUpdated();
        }
        public void RemoveChapter(Chapter chapterToRemove)
        {
            if (!Chapters.Contains(chapterToRemove)) return;
            Chapters.Remove(chapterToRemove);
            SortChapters();
            OnChapterListUpdated();
        }

        private void SortChapters()
        {
            var reorderedChapters = from chapter in Chapters
                                    orderby chapter.Position ascending
                                    select chapter;
            Chapters = reorderedChapters.ToList();
            OnChapterListUpdated();
        }

        public void ChangeChapter(Chapter chapterToChange, string newTitle)
        {
            Chapters.Find(e=>e == chapterToChange).Title = newTitle;
            OnChapterListUpdated();
        }

        internal void ChangeChapter(Chapter chapterToChange, int newPosition)
        {
            Chapters.Find(e => e == chapterToChange).Position = newPosition;
            OnChapterListUpdated();
        }
        internal void ChangeChapter(Chapter chapterToChange, string newTitle, int newPosition)
        {
            var chapt = Chapters.Find(e => e == chapterToChange);
            chapt.Title = newTitle;
            chapt.Position = newPosition;
            OnChapterListUpdated();
        }

        internal void ChangeChapter(Chapter chapterToChange, Chapter newChapter)
        {
            var oldChapt = Chapters.Find(e => e == chapterToChange);
            oldChapt.Title = newChapter.Title;
            oldChapt.Position = newChapter.Position;
            OnChapterListUpdated();
        }
        public event EventHandler ChapterListUpdated;

        protected virtual void OnChapterListUpdated()
        {
            ChapterListUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
