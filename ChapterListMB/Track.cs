using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MusicBeePlugin;

namespace ChapterListMB
{
    public class Track : IEquatable<Track>
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
        public override string ToString()
        {
            return $"{NowPlayingTrackInfo.Artist} - {NowPlayingTrackInfo.Album} - {NowPlayingTrackInfo.Title}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Track track  = obj as Track;
            if ((object) track == null)
                return false;
            return ChapterList.Equals(track.ChapterList) && 
                NowPlayingTrackInfo.Equals(track.NowPlayingTrackInfo);
        }
        public bool Equals(Track other)
        {
            if ((object) other == null) return false;
                return ChapterList.Equals(other.ChapterList) &&
                NowPlayingTrackInfo.Equals(other.NowPlayingTrackInfo);
        }
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
