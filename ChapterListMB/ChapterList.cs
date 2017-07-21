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
        /// <summary>
        /// Removes the provided chapter from the ChapterList
        /// </summary>
        /// <param name="chapterToRemove">Chapter to remove</param>
        public void RemoveChapter(Chapter chapterToRemove)
        {
            if (!Items.Contains(chapterToRemove)) return;
            Items.Remove(chapterToRemove);
            SortChapters();
            OnChapterListUpdated();
        }
        /// <summary>
        /// Saves ChapterList to XML file
        /// </summary>
        public void SaveChaptersToFile()
        {
            XmlOperations.SaveChapterListToXml(this);
        }
        private void SortChapters()
        {
            List<Chapter> itemsSaved = Items.ToList();
            Items.Clear();
            var ordereditems = itemsSaved.OrderBy(c => c.Position);
            int chapterNumber = 1;
            foreach (Chapter chapter in ordereditems)
            {
                chapter.SetChapterNumber(chapterNumber);
                chapterNumber++;
                Items.Add(chapter);
            }
            OnChapterListUpdated();
        }
        /// <summary>
        /// Changes the title of a given chapter
        /// </summary>
        /// <param name="chapterToChange"></param>
        /// <param name="newTitle"></param>
        public void ChangeChapter(Chapter chapterToChange, string newTitle)
        {
            int index = Items.IndexOf(chapterToChange);
            Items[index].Title = newTitle;
            OnChapterListUpdated();
        }
        /// <summary>
        /// Changes the position of a given chapter
        /// </summary>
        /// <param name="chapterToChange"></param>
        /// <param name="newPosition"></param>
        public void ChangeChapter(Chapter chapterToChange, int newPosition)
        {
            int index = Items.IndexOf(chapterToChange);
            Items[index].Position = newPosition;
            OnChapterListUpdated();
        }
        /// <summary>
        /// Changes the title and position of a given chapter
        /// </summary>
        /// <param name="chapterToChange"></param>
        /// <param name="newTitle"></param>
        /// <param name="newPosition"></param>
        public void ChangeChapter(Chapter chapterToChange, string newTitle, int newPosition)
        {
            int index = Items.IndexOf(chapterToChange);
            Items[index].Title = newTitle;
            Items[index].Position = newPosition;
            OnChapterListUpdated();
        }
        /// <summary>
        /// Changes a give
        /// </summary>
        /// <param name="chapterToChange"></param>
        /// <param name="newChapter"></param>
        public void ChangeChapter(Chapter chapterToChange, Chapter newChapter)
        {
            int index = Items.IndexOf(chapterToChange);
            Items[index].Title = newChapter.Title;
            Items[index].Position = newChapter.Position;
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

        public event EventHandler ChapterListUpdated;

        protected virtual void OnChapterListUpdated()
        {
            ChapterListUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
