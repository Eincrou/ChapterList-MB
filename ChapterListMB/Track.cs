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
        public static Uri XmlPath;
        public Track(NowPlayingTrackInfo trackInfo)
        {
            XmlPath = new Uri($"{trackInfo.FilePath}.xml", UriKind.Absolute);
            NowPlayingTrackInfo = trackInfo;
            ChapterList = new ChapterList();
            ChapterList.ChapterListUpdated += ChapterList_ChapterListUpdated;
        }
        
        private void ChapterList_ChapterListUpdated(object sender, EventArgs e)
        {
            ChapterList.SaveChaptersToFile();
        }
    }
}
