using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using MusicBeePlugin;

namespace ChapterListMB
{
    /// <summary>
    /// Controls chapter repeating functionality.
    /// </summary>
    internal static class RepeatSection
    {
        public static bool LoopingEnabled;
        public static Chapter A;
        public static Chapter B;
        public static TimeSpan TrackDuration;

        public static void ReceiveChapter(Chapter chapter, TimeSpan trackDuration)
        {
            TrackDuration = trackDuration;
            if (A == null)
            {
                A = chapter;
            }
            else if (A.Equals(chapter))
            {   // Resets both chapters if A is clicked
                A = null;
                B = null;
            }
            else if (chapter.Position > A.Position)
            {
                B = B == null ? chapter : null;
            }
            LoopingEnabled = A != null;
        }

        public static bool RepeatCheck(int currentPosition)
        {
            return LoopingEnabled && (currentPosition > B?.Position ||  // Loop at chapter set as B
                currentPosition > TrackDuration.TotalMilliseconds - 200);   //  Loop at (almost) end of track.
        }

        public static void Clear()
        {
            LoopingEnabled = false;
            A = null;
            B = null;
            TrackDuration = TimeSpan.Zero;
        }
    }
}
