using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MusicBeePlugin;

namespace ChapterListMB
{
    public class Track
    {

        public ChapterList ChapterList { get; private set; }
        public NowPlayingTrackInfo NowPlayingTrackInfo { get; set; }
        private readonly Uri _xmlPath;
        public Track(NowPlayingTrackInfo trackInfo)
        {
            ChapterList = new ChapterList();
            NowPlayingTrackInfo = trackInfo;
            _xmlPath = new Uri($"{trackInfo.FilePath}.xml", UriKind.Absolute);
            CreateChapterList();
            ChapterList.ChapterListUpdated += ChapterList_ChapterListUpdated;
        }
        
        private void CreateChapterList()
        {
            try
            {
                if (!System.IO.File.Exists(_xmlPath.LocalPath)) { return; }
                var chaptersListDoc = XDocument.Load(_xmlPath.LocalPath);
                if (chaptersListDoc.Root.Attribute("version").Value == "1.0")   // 1.0 is original chapterlist XML format
                {
                    foreach (var xElement in chaptersListDoc.Descendants("Chapter"))
                    {
                        ChapterList.CreateNewChapter(xElement.Attribute("name").Value,
                            int.Parse(xElement.Attribute("pos").Value));
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public void SaveChapterList()
        {
            var xmlDoc = new XDocument(
                new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement("Chapterlist", new XAttribute("version", "1.0")
                )
            );
            foreach (var chapter in ChapterList.Chapters)
            {
                var newElement = new XElement("Chapter",
                    new XAttribute("pos", chapter.Position),
                    new XAttribute("name", chapter.Title)
                );
                xmlDoc.Root.Add(newElement);
            }
            xmlDoc.Save(_xmlPath.LocalPath);
        }

        private void ChapterList_ChapterListUpdated(object sender, EventArgs e)
        {
            SaveChapterList();
        }
    }
}
