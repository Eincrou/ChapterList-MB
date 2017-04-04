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
    internal class RepeatSection
    {
        public bool LoopingEnabled;
        public Chapter A;
        public Chapter B;

        private Plugin.MusicBeeApiInterface _mbapi;
        private MainForm _mainForm;
        private Timer _timer;
        public RepeatSection(Plugin.MusicBeeApiInterface mbapi, MainForm mainForm, Timer timer)
        {
            _mbapi = mbapi;
            _mainForm = mainForm;
            _timer = timer;
        }

        public void ReceiveChapter(Chapter chapter)
        {
            
            if (A == null)
            {
                A = chapter;
            }
            else if (A != null)
            {
                B = (B == chapter) ? null : chapter;
            }
            else
            {
                A = null;
            }
            LoopingEnabled = A != null && B != null;
        }

        public void Loop()
        {
            var loopPosition = B.Position;
            if (A.ChapterNumber == _mainForm.Track.ChapterList.Chapters.Count)  // If A is last chapter, loops from the end of the track.
            {
                loopPosition = _mbapi.NowPlaying_GetDuration()-200; // -200ms so timer can catch it before track ends.
                LoopingEnabled = true;
            }
            if (LoopingEnabled && _mbapi.Player_GetPosition() >= loopPosition)
            {
                _mbapi.Player_SetPosition(A.Position);
            }
        }
    }
}
