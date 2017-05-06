using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterListMB
{
    public class ChapterList : IEnumerable<Chapter>
    {
        /// <summary>
        /// Gets the list of all chapters in this ChapterList.
        /// </summary>
        public List<Chapter> Chapters { get; } = new List<Chapter>();

        public Chapter this[int index]
        {
            get { return Chapters[index]; }
            set
            {
                Chapters.Insert(index, value);
            }
        }
        /// <summary>
        /// Gets the total number of chapters in this ChapterList.
        /// </summary>
        public int NumChapters => Chapters.Count;

        public ChapterList()
        {
            XmlOperations.ReadChapterListFromXml(this);
        }
        public ChapterList(List<Chapter> chapters)
        {
            Chapters = chapters;
        }
        /// <summary>
        /// Creates and adds a new chapter to the list, based on specified parameters.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="position"></param>
        public void CreateNewChapter(string name, int position)
        {
            Chapters.Add(new Chapter(position, name));
            SortChapters();
            OnChapterListUpdated();
        }
        /// <summary>
        /// Creates and adds a new chapter to the list based on position, with a default chapter name.
        /// </summary>
        /// <param name="position"></param>
        public void CreateNewChapter(int position)
        {
            if((Chapters.Count == 0) || (Chapters.First().Position != 0))
                Chapters.Add(new Chapter(0, "Start of first chapter"));
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

        public void SaveChaptersToFile()
        {
            XmlOperations.SaveChapterListToXml(this);
        }
        private void SortChapters()
        {
            //var reorderedChapters = from chapter in Chapters
            //                        orderby chapter.Position ascending
            //                        select chapter;
            Chapters.Sort();
            var chapterNumber = 1;
            foreach (var chapter in Chapters)
            {
                chapter.SetChapterNumber(chapterNumber);
                chapterNumber++;
            }
            //Chapters = reorderedChapters.ToList();

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

        public Chapter GetCurrentChapterFromPosition(int position)
        {
            for (int i = 1; i < Chapters.Count; i++)
            {
                if (position <= Chapters[i].Position)
                    return Chapters[i-1];
            }
            return Chapters[Chapters.Count - 1];
        }

        private bool FindChapter(Chapter chapter, int position)
        {
            var currChapIndex = Chapters.IndexOf(chapter);
            return Chapters[currChapIndex + 1].Position - position > chapter.Position;
        }


        public event EventHandler ChapterListUpdated;

        protected virtual void OnChapterListUpdated()
        {
            ChapterListUpdated?.Invoke(this, EventArgs.Empty);
        }

        public IEnumerator<Chapter> GetEnumerator()
        {
            return Chapters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Chapters.GetEnumerator();
        }
    }
}
