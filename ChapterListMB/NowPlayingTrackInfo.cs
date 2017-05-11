using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterListMB
{
    public struct NowPlayingTrackInfo : IEquatable<NowPlayingTrackInfo>
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

        public bool Equals(NowPlayingTrackInfo other)
        {
            return this.ToString().Equals(other.ToString());
        }

        public override string ToString()
        {
            return $"{Artist} - {Album} - {Title} ({Duration:g}) | {FilePath}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is NowPlayingTrackInfo)) return false;
            return ToString().Equals(obj?.ToString());
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
