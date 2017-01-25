using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MusicBeePlugin;

namespace ChapterListMB
{

    class Track
    {

        public ChapterList ChapterList { get; private set; }
        public Uri FilePathUri { get; set; }
        public TimeSpan Duration { get; set; }

        public Track(Uri trackFilepath, TimeSpan trackDuration)
        {
            ChapterList = new ChapterList();
            FilePathUri = trackFilepath;
            Duration = trackDuration;
            CreateChapterList(FilePathUri);
        }

        private void CreateChapterList(Uri xmlPath)
        {
            try
            {
                var chaptersListDoc = XDocument.Load(xmlPath + ".xml");
                var importedChaptersList = from n in chaptersListDoc.Descendants("Chapter")
                                           select n;
                foreach (var xElement in importedChaptersList)
                {
                    ChapterList.CreateNewChapter(xElement.Attribute("name").Value, int.Parse(xElement.Attribute("pos").Value));
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public void CreateNewChapter(string chapterName, int chapterPosition)
        {
            ChapterList.CreateNewChapter(chapterName, chapterPosition);
        }

        public void RemoveChapter(Chapter chapterToRemove)
        {
            throw new NotImplementedException();
        }

    }
}
