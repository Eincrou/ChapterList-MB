using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterListMB
{
    public class ChapterList : Collection<Chapter>
    {
        /// <summary>
        /// Gets the total number of chapters in this ChapterList.
        /// </summary>
        public int NumChapters => Items.Count;

        public ChapterList()
        {
            XmlOperations.ReadChapterListFromXml(this);
        }
        public ChapterList(List<Chapter> chapters)
        {
            foreach (Chapter c in chapters)
            {
                Items.Add(c);
            }
        }
        /// <summary>
        /// Creates and adds a new chapter to the list, based on specified parameters.
        /// </summary>
        /// <param name="name">Title of the new chapter</param>
        /// <param name="position">Position in milliseconds of the new chapter</param>
        public void CreateNewChapter(string name, int position)
        {
            Items.Add(string.IsNullOrWhiteSpace(name) ? new Chapter(position) : new Chapter(position, name));
            CheckForZeroPositionChapter();
            SortChapters();
            OnChapterListUpdated();
        }
        /// <summary>
        /// Creates and adds a new chapter to the list based on position, with a default chapter name.
        /// </summary>
        /// <param name="position"></param>
        public void CreateNewChapter(int position)
        {
            Items.Add(new Chapter(position));
            CheckForZeroPositionChapter();
            SortChapters();
            OnChapterListUpdated();
        }

        private void CheckForZeroPositionChapter()
        {
            if ((Items.Count == 0) || (Items[0].Position != 0))
                Items.Add(new Chapter(0, "Start of first chapter"));
        }
        public void RemoveChapter(Chapter chapterToRemove)
        {
            if (!Items.Contains(chapterToRemove)) return;
            Items.Remove(chapterToRemove);
            SortChapters();
            OnChapterListUpdated();
        }

        public void SaveChaptersToFile()
        {
            XmlOperations.SaveChapterListToXml(this);
        }
        private void SortChapters()
        {
            List<Chapter> lc = new List<Chapter>(Items);
            lc.Sort();
            var chapterNumber = 1;
            foreach (var chapter in Items)
            {
                chapter.SetChapterNumber(chapterNumber);
                chapterNumber++;
            }
            OnChapterListUpdated();
        }

        public void ChangeChapter(Chapter chapterToChange, string newTitle)
        {
            List<Chapter> lc = new List<Chapter>(Items);
            lc.Find(e=>e == chapterToChange).Title = newTitle;
            OnChapterListUpdated();
        }

        internal void ChangeChapter(Chapter chapterToChange, int newPosition)
        {
            List<Chapter> lc = new List<Chapter>(Items);
            lc.Find(e => e == chapterToChange).Position = newPosition;
            OnChapterListUpdated();
        }
        internal void ChangeChapter(Chapter chapterToChange, string newTitle, int newPosition)
        {
            List<Chapter> lc = new List<Chapter>(Items);
            var chapt = lc.Find(e => e == chapterToChange);
            chapt.Title = newTitle;
            chapt.Position = newPosition;
            OnChapterListUpdated();
        }

        internal void ChangeChapter(Chapter chapterToChange, Chapter newChapter)
        {
            List<Chapter> lc = new List<Chapter>(Items);
            var oldChapt = lc.Find(e => e == chapterToChange);
            oldChapt.Title = newChapter.Title;
            oldChapt.Position = newChapter.Position;
            OnChapterListUpdated();
        }

        public Chapter GetCurrentChapterFromPosition(int position)
        {
            for (int i = 1; i < Items.Count; i++)
            {
                if (position <= Items[i].Position)
                    return Items[i-1];
            }
            return Items[Items.Count - 1];
        }

        private bool FindChapter(Chapter chapter, int position)
        {
            var currChapIndex = Items.IndexOf(chapter);
            return Items[currChapIndex + 1].Position - position > chapter.Position;
        }


        public event EventHandler ChapterListUpdated;

        protected virtual void OnChapterListUpdated()
        {
            ChapterListUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
