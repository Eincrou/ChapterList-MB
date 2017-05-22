using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using ChapterListMB;
using Timer = System.Timers.Timer;

namespace MusicBeePlugin
{
    public partial class Plugin
    {
        private MusicBeeApiInterface mbApiInterface;
        private PluginInfo _about = new PluginInfo();
        private MainForm _mainForm;
        private Track _track;
        private Timer _timer;
        private Chapter _currentChapter;

        public PluginInfo Initialise(IntPtr apiInterfacePtr)
        {
            mbApiInterface = new MusicBeeApiInterface();
            mbApiInterface.Initialise(apiInterfacePtr);
            _about.PluginInfoVersion = PluginInfoVersion;
            _about.Name = "Chapter List | MB";
            _about.Description = "Creates chapters to jump to a position in a track.";
            _about.Author = "Eincrou";
            _about.TargetApplication = "";   // current only applies to artwork, lyrics or instant messenger name that appears in the provider drop down selector or target Instant Messenger
            _about.Type = PluginType.General;
            _about.VersionMajor = 1;  // your plugin version
            _about.VersionMinor = 0;
            _about.Revision = 1;
            _about.MinInterfaceVersion = MinInterfaceVersion;
            _about.MinApiRevision = MinApiRevision;
            _about.ReceiveNotifications = (ReceiveNotificationFlags.PlayerEvents);
            _about.ConfigurationPanelHeight = 0;   // height in pixels that musicbee should reserve in a panel for config settings. When set, a handle to an empty panel will be passed to the Configure function

            CreateMenuItem();
            return _about;
        }

        public bool Configure(IntPtr panelHandle)
        {
            // save any persistent settings in a sub-folder of this path
            string dataPath = mbApiInterface.Setting_GetPersistentStoragePath();
            // panelHandle will only be set if you set about.ConfigurationPanelHeight to a non-zero value
            // keep in mind the panel width is scaled according to the font the user has selected
            // if about.ConfigurationPanelHeight is set to 0, you can display your own popup window
            if (panelHandle != IntPtr.Zero)
            {
                Panel configPanel = (Panel)Panel.FromHandle(panelHandle);
                Label prompt = new Label();
                prompt.AutoSize = true;
                prompt.Location = new Point(0, 0);
                prompt.Text = "prompt:";
                TextBox textBox = new TextBox();
                textBox.Bounds = new Rectangle(60, 0, 100, textBox.Height);
                configPanel.Controls.AddRange(new Control[] { prompt, textBox });
            }
            return false;
        }
       
        // called by MusicBee when the user clicks Apply or Save in the MusicBee Preferences screen.
        // its up to you to figure out whether anything has changed and needs updating
        public void SaveSettings()
        {
            // save any persistent settings in a sub-folder of this path
            string dataPath = mbApiInterface.Setting_GetPersistentStoragePath();
        }

        // MusicBee is closing the plugin (plugin is being disabled by user or MusicBee is shutting down)
        public void Close(PluginCloseReason reason)
        {
        }

        // uninstall this plugin - clean up any persisted files
        public void Uninstall()
        {
        }

        // receive event notifications from MusicBee
        // you need to set about.ReceiveNotificationFlags = PlayerEvents to receive all notifications, and not just the startup event
        public void ReceiveNotification(string sourceFileUrl, NotificationType type)
        {
            // perform some action depending on the notification type
            switch (type)
            {
                case NotificationType.PluginStartup:
                    // perform startup initialisation
                    //if (_mainForm != null) return;
                    _timer = new Timer(100);
                    _timer.Elapsed += _timer_Elapsed;
                    switch (mbApiInterface.Player_GetPlayState())
                    {
                        case PlayState.Playing:
                            _timer.Start();
                            break;
                    }
                    break;
                case NotificationType.TrackChanged:
                    if (_mainForm == null) return;
                    RepeatSection.Clear();
                    _track = GetTrack();
                    _mainForm.Invoke(_mainForm.UpdateTrackDelegate, _track);
                    break;
                case NotificationType.TrackChanging:
                    if (!_timer.Enabled) _timer.Stop();
                    break;
                case NotificationType.PlayStateChanged:
                    if (_track == null) return;
                    switch (mbApiInterface.Player_GetPlayState())
                    {
                        case PlayState.Playing:
                            if (!_timer.Enabled) _timer.Start();
                            break;
                        case PlayState.Paused:
                            if (_timer.Enabled) _timer.Stop();
                            break;
                        case PlayState.Stopped:
                            if (_timer.Enabled) _timer.Stop();
                            break;
                        case PlayState.Undefined:
                            if (_timer.Enabled) _timer.Stop();
                            break;
                    }
                    break;
            }
        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_track.ChapterList.NumChapters == 0) return;
            int playerPosition = mbApiInterface.Player_GetPosition();
            Chapter currentChapter = _track.ChapterList.GetCurrentChapterFromPosition(playerPosition);
            if (!currentChapter.Equals(_currentChapter))
            {
                _mainForm.Invoke(_mainForm.SetCurrentChapterDelegate, currentChapter);
                _currentChapter = currentChapter;
            }
            // Repeat section
            if (RepeatSection.RepeatCheck(playerPosition))
            {
                mbApiInterface.Player_SetPosition(RepeatSection.A.Position);
            }
        }

        private Track GetTrack()
        {
            var trackInfo = new NowPlayingTrackInfo(
                mbApiInterface.NowPlaying_GetFileTag(MetaDataType.TrackTitle),
                mbApiInterface.NowPlaying_GetFileTag(MetaDataType.Artist),
                mbApiInterface.NowPlaying_GetFileTag(MetaDataType.Album),
                new TimeSpan(0, 0, 0, 0,
                    mbApiInterface.NowPlaying_GetDuration()),
                new Uri(mbApiInterface.NowPlaying_GetFileProperty(
                    FilePropertyType.Url), UriKind.Absolute)
            );
            Track track = new Track(trackInfo);
            return track;
        }

        private void CreateMenuItem()
        {
            mbApiInterface.MB_AddMenuItem("mnuTools/" + @"Chapter List | MB", "Hotkey for CLMB", OnMenuClicked);
        }

        private void OnMenuClicked(object sender, EventArgs args)
        {
            _mainForm = new MainForm();
            //mbApiInterface.MB_AddPanel(_mainForm.DataGridView, PluginPanelDock.ApplicationWindow);
            _mainForm.Show();
            SubscribeToEvents();
            if (mbApiInterface.Player_GetPlayState() != PlayState.Undefined)
            {
                _track = GetTrack();
                _mainForm.Invoke(_mainForm.UpdateTrackDelegate, _track);
                if (_track.ChapterList.NumChapters == 0) return;
                if (mbApiInterface.Player_GetPlayState() == PlayState.Playing)
                {
                    _currentChapter = _track.ChapterList[0];
                    _mainForm.Invoke(_mainForm.SetCurrentChapterDelegate, _currentChapter);
                    _timer.Start();
                }
            }
        }


        /* * * * * *
         *  Events *
         * * * * * */
        private void SubscribeToEvents()
        {
            _mainForm.SelectedItemDoubleClickedRouted += MainFormOnSelectedItemDoubleClickedRouted;
            _mainForm.AddChapterButtonClickedRouted += MainFormOnAddChapterButtonClickedRouted;
            _mainForm.RemoveChapterButtonClickedRouted += MainFormOnRemoveChapterButtonClickedRouted;
            _mainForm.ChangeChapterRequested += MainFormOnChangeChapterRequested;
        }
        
        private void MainFormOnSelectedItemDoubleClickedRouted(object sender, Chapter chapter)
        {
            mbApiInterface.Player_SetPosition(chapter.Position);
        }
        private void MainFormOnAddChapterButtonClickedRouted(object sender, EventArgs e)
        {
            var currentPosition = mbApiInterface.Player_GetPosition();
            _track.ChapterList.CreateNewChapter(currentPosition);
        }
        private void MainFormOnRemoveChapterButtonClickedRouted(object sender, Chapter e)
        {
            _track.ChapterList.RemoveChapter(e);
            _mainForm.Invoke(_mainForm.UpdateTrackDelegate, _track);
        }
        private void MainFormOnChangeChapterRequested(object sender, ChapterChangeEventArgs e)
        {
            _track.ChapterList.ChangeChapter(e.ChapterToChange, new Chapter(e.Position, e.Title));
           //_mainForm.Invoke(_mainForm.UpdateTrackDelegate, _track);
        }
        
    }
}