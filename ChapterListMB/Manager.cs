using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using ChapterListMB.Properties;
using MusicBeePlugin;

namespace ChapterListMB
{
    public class Manager
    {
        /// <summary>
        /// The manager's current track
        /// </summary>
        public Track Track { get; private set; }
        /// <summary>
        /// The currently playing chapter
        /// </summary>
        public Chapter CurrentChapter { get; private set; }
        /// <summary>
        /// Dictionary of parsed skin colors, if possible.
        /// </summary>
        public Dictionary<string, SkinElementColors> SkinElementColors { get; private set; }
        /// <summary>
        /// The MusicBee plugin API
        /// </summary>
        private Plugin.MusicBeeApiInterface _api;

        private readonly Timer _timer;

        /// <summary>
        /// Create a new instance of ChapterList MB manager class
        /// </summary>
        /// <param name="mbapi">Plugin API from Music Bee</param>
        public Manager(Plugin.MusicBeeApiInterface mbapi)
        {
            _api = mbapi;
            _timer = new Timer(100);
            //SkinElementColors = XmlOperations.GetSkinColors(_api.Setting_GetSkin());
            
            _timer.Elapsed += TimerOnElapsed;
        }

        /// <summary>
        /// Change the track in use by the manager
        /// </summary>
        /// <param name="track">New track to for the manager</param>
        public void UpdateTrack(Track track)
        {
            Track = track;
            CurrentChapter = track.ChapterList.Count > 0 ? Track.ChapterList[0] : null;
            OnTrackChanged(Track);
            SetCurrentChapter();
        }
        
        /// <summary>
        /// Starts the timer
        /// </summary>
        public void StartTimer()
        {
            if (Track.ChapterList.Count > 0 && !_timer.Enabled) _timer.Start();
        }
        /// <summary>
        /// Stops the timer
        /// </summary>
        public void StopTimer()
        {
            if (_timer.Enabled) _timer.Stop();
        }
        /// <summary>
        /// Adds a chapter to the chapter list
        /// </summary>
        /// <param name="title">New chapter's title</param>
        public void AddChapter(string title)
        {
            StopTimer();
            string newChapterName = title == "<Default>"
                ? string.Empty
                : title;
            Track.ChapterList.CreateNewChapter(newChapterName, _api.Player_GetPosition());
            StartTimer();
        }
        /// <summary>
        /// Removes a chapter from the chapter list
        /// </summary>
        /// <param name="index">Index of the chapter to remove</param>
        public void RemoveChapter(int index)
        {
            StopTimer();
            if (index < 0 || index > Track.ChapterList.Count - 1) return;
            Track.ChapterList.RemoveChapter(Track.ChapterList[index]);
            StartTimer();
        }

        /// <summary>
        /// Changes the position of a Chapter by the ChapterPositionShiftValue
        /// </summary>
        /// <param name="index">Index of the Chapter to shift</param>
        /// <param name="shiftType">Whether to shift the position backwards or forwards</param>
        /// <param name="modifier"></param>
        public void ChangeChapterPosition(int index, ShiftChapterPositionType shiftType, double modifier)
        {
            if (index < 0 || index > Track.ChapterList.Count - 1) return;
            Chapter chapterToChange = Track.ChapterList[index];
            int shiftAmount = 0;
            switch (shiftType)
            {
                case ShiftChapterPositionType.Forwards:
                    shiftAmount = Convert.ToInt32(Settings.Default.ChapterPositionShiftValue.TotalMilliseconds*modifier);
                    break;
                case ShiftChapterPositionType.Backwards:
                    shiftAmount =
                        -Convert.ToInt32(Settings.Default.ChapterPositionShiftValue.TotalMilliseconds*modifier);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(shiftType), shiftType, null);
            }
            Track.ChapterList.ChangeChapter(chapterToChange, chapterToChange.Position + shiftAmount);

            _api.Player_SetPosition(chapterToChange.Position);
        }
        /// <summary>
        /// Submit a chapter for section repeating
        /// </summary>
        /// <param name="index"></param>
        public void SetChapterRepeat(int index)
        {
            if (index < 0 || index > Track.ChapterList.Count - 1) return;
            RepeatSection.ReceiveChapter(Track.ChapterList[index], Track.NowPlayingTrackInfo.Duration);
        }
        /// <summary>
        /// Changes MusicBee's player position to a Chapter's position
        /// </summary>
        /// <param name="index">Index of the chapter to change the player position to</param>
        public void ChangePlayerPosition(int index)
        {
            if (index < 0 || index > Track.ChapterList.Count - 1) return;
            _api.Player_SetPosition(Track.ChapterList[index].Position);
        }
        /// <summary>
        /// Gets the color from a skin element component.
        /// </summary>
        /// <param name="skinElement"></param>
        /// <param name="elementState"></param>
        /// <param name="elementComponent"></param>
        /// <returns></returns>
        public Color GetElementColor(Plugin.SkinElement skinElement, Plugin.ElementState elementState, Plugin.ElementComponent elementComponent)
        {
            int colorValue = _api.Setting_GetSkinElementColour(skinElement, elementState, elementComponent);
            return Color.FromArgb(colorValue);
        }
        /// <summary>
        /// Closes the manager
        /// </summary>
        public void Close()
        {
            _timer.Close();
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            SetCurrentChapter();
            if (RepeatSection.RepeatCheck(_api.Player_GetPosition()))
            {
                _api.Player_SetPosition(RepeatSection.A.Position);
            }
        }

        private void SetCurrentChapter()
        {
            if (Track.ChapterList.Count == 0)
            {
                CurrentChapter = null;
                return;
            }
            int playerPosition = _api.Player_GetPosition();
            Chapter current = Track.ChapterList.GetCurrentChapterFromPosition(playerPosition);
            if (!current.Equals(CurrentChapter))
            {
                CurrentChapter = current;
                OnCurrentChapterChanged(CurrentChapter);
            }
        }

        #region Events

        /// <summary>
        /// Occurs when the manager's track changes
        /// </summary>
        public event EventHandler<Track> TrackChanged;
        protected virtual void OnTrackChanged(Track t)
        {
            TrackChanged?.Invoke(this, t);
        }
        /// <summary>
        /// Occurs when the current chapter changes
        /// </summary>
        public event EventHandler<Chapter> CurrentChapterChanged;
        protected virtual void OnCurrentChapterChanged(Chapter c)
        {
            CurrentChapterChanged?.Invoke(this, c);
        }

        #endregion
    }
    /// <summary>
    /// Whether to shift a chapter position forward or backwards
    /// </summary>
    public enum ShiftChapterPositionType
    {
        Forwards, Backwards
    }
}
