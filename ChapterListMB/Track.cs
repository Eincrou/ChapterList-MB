using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicBeePlugin;

namespace ChapterListMB
{

    class Track
    {
        private Plugin.MusicBeeApiInterface _mbApi;
        public ChapterList ChapterList { get; }
        public string FileUrl { get; set; }
        public int Duration { get; set; }

        public Track(Plugin.MusicBeeApiInterface mbApiInterface)
        {
            _mbApi = mbApiInterface;
            GetTrackInformation();
        }

        private void GetTrackInformation()
        {
            FileUrl = _mbApi.NowPlaying_GetFileUrl();
            Duration = _mbApi.NowPlaying_GetDuration();
        }

        public int GetTrackPosition()
        {
            return _mbApi.Player_GetPosition();
        }

        public void SetTrackPosition(int newPosition)
        {
            if (newPosition < Duration)
                _mbApi.Player_SetPosition(newPosition);
        }
    }
}
