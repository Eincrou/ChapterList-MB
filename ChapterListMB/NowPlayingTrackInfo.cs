using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterListMB
{
    public class NowPlayingTrackInfo
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public TimeSpan Duration { get; set; }
        public Uri FilePath { get; set; }
        /// <summary>
        /// Creates a new NowPlayingTrackInfo object.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="artist"></param>
        /// <param name="album"></param>
        /// <param name="duration"></param>
        /// <param name="filepath"></param>
        public NowPlayingTrackInfo(string title, string artist, string album, TimeSpan duration, Uri filepath)
        {
            Title = title;
            Artist = artist;
            Album = album;
            Duration = duration;
            FilePath = filepath;
        }
    }
}
