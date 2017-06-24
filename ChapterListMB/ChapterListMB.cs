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
        private bool _launchOnStartup;

        public PluginInfo Initialise(IntPtr apiInterfacePtr)
        {
            mbApiInterface = new MusicBeeApiInterface();
            mbApiInterface.Initialise(apiInterfacePtr);
            _about.PluginInfoVersion = PluginInfoVersion;
            _about.Name = "Chapter List | MB";
            _about.Description = "Creates chapters to jump to a position in a track.";
            _about.Author = "Eincrou";
            _about.TargetApplication = "Chapter List | MB";   // current only applies to artwork, lyrics or instant messenger name that appears in the provider drop down selector or target Instant Messenger
            _about.Type = PluginType.General;
            _about.VersionMajor = 2;  // your plugin version
            _about.VersionMinor = 0;
            _about.Revision = 0;
            _about.MinInterfaceVersion = MinInterfaceVersion;
            _about.MinApiRevision = MinApiRevision;
            _about.ReceiveNotifications = (ReceiveNotificationFlags.PlayerEvents);
            _about.ConfigurationPanelHeight = 40;   // height in pixels that musicbee should reserve in a panel for config settings. When set, a handle to an empty panel will be passed to the Configure function

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
                Label lblLaunchStartup = new Label();
                lblLaunchStartup.AutoSize = true;
                lblLaunchStartup.Location = new Point(0, 0);
                lblLaunchStartup.Text = "launch on startup:";
                CheckBox cbLaunchStartup = new CheckBox();
                cbLaunchStartup.AutoSize = true;
                cbLaunchStartup.Location = new Point(0, 0);
                cbLaunchStartup.Text = "Launch on Startup";
                //TextBox textBox = new TextBox();
                //textBox.Bounds = new Rectangle(60, 0, 100, textBox.Height);
                configPanel.Controls.AddRange(new Control[] { cbLaunchStartup });
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
                    _timer = new Timer(100);
                    _timer.Elapsed += _timer_Elapsed;
                    switch (mbApiInterface.Player_GetPlayState())
                    {
                        case PlayState.Playing:
                            _timer.Start();
                            break;
                    }
                    //if (ChapterListMB.Properties.Settings.Default.StartWithMusicBee)
                    //    OnMenuClicked(null, null);
                    break;
                case NotificationType.TrackChanged:
                    if (_mainForm == null) return;
                    RepeatSection.Clear();
                    _currentChapter = null;
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
            if (_mainForm != null) return;
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

        #region AddMusicBeePanel
        
        //  presence of this function indicates to MusicBee that this plugin has a dockable panel. MusicBee will create the control and pass it as the panel parameter
        //  you can add your own controls to the panel if needed
        //  you can control the scrollable area of the panel using the mbApiInterface.MB_SetPanelScrollableArea function
        //  to set a MusicBee header for the panel, set about.TargetApplication in the Initialise function above to the panel header text
        public int OnDockablePanelCreated(Control panel)
        {
            //    return the height of the panel and perform any initialisation here
            //    MusicBee will call panel.Dispose() when the user removes this panel from the layout configuration
            //    < 0 indicates to MusicBee this control is resizable and should be sized to fill the panel it is docked to in MusicBee
            //    = 0 indicates to MusicBee this control resizeable
            //    > 0 indicates to MusicBee the fixed height for the control.Note it is recommended you scale the height for high DPI screens(create a graphics object and get the DpiY value)

            Label lbl = new Label
            {
                Text = "Sup, fools!",
                AutoSize = true,
                Location = new Point(0, 0)
            };

            Button btn = new Button
            {
                Text = "Button Test  Text",
                AutoSize = true,
                Location = new Point(0, 20)
            };

            DataGridView dgv = new DataGridView();
            //dgv.AutoSize = true;
            dgv.Location = new Point(0,60);

            ListBox lb = new ListBox
            {
                DataSource = new List<string>() {"test1", "test2", "test3"},
                Location = new Point(0, 120)
            };

            panel.Invoke(new Action(() =>
            {
                //panel.Controls.Add(lbl);
                panel.Controls.AddRange(new Control[] {lbl, btn, lb});
            }));

            float dpiScaling = 0;
            using (Graphics g = panel.CreateGraphics())
            {
                dpiScaling = g.DpiY / 96f;
            }
            panel.Paint += panel_Paint;
            //return Convert.ToInt32(100 * dpiScaling);
            return 0;
        }

        // presence of this function indicates to MusicBee that the dockable panel created above will show menu items when the panel header is clicked
        // return the list of ToolStripMenuItems that will be displayed
        public List<ToolStripItem> GetHeaderMenuItems()
        {
            ToolStripItem tsiTest1 = new ToolStripMenuItem();
            tsiTest1.Name = "tsiTest1";
            tsiTest1.Text = "Super testalicious!";
            
            List<ToolStripItem> list = new List<ToolStripItem>();
            list.Add(tsiTest1);
            return list;
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.Clear(Color.Red);
            TextRenderer.DrawText(e.Graphics, "hello", SystemFonts.CaptionFont, new Point(10, 10), Color.Blue);
        }

        #endregion


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
        private void MainFormOnAddChapterButtonClickedRouted(object sender, string newChapterName)
        {
            var currentPosition = mbApiInterface.Player_GetPosition();
            _track.ChapterList.CreateNewChapter(newChapterName, currentPosition);
            if (!_timer.Enabled)
                _timer.Enabled = true;
        }
        private void MainFormOnRemoveChapterButtonClickedRouted(object sender, Chapter e)
        {
            _track.ChapterList.RemoveChapter(e);
            _mainForm.Invoke(_mainForm.UpdateTrackDelegate, _track);
        }
        private void MainFormOnChangeChapterRequested(object sender, ChapterChangeEventArgs e)
        {
            if (e.ChapterToChange.Position != e.Position)
            {
                mbApiInterface.Player_SetPosition(e.Position);
            }
            _track.ChapterList.ChangeChapter(e.ChapterToChange, new Chapter(e.Position, e.Title));
        }
        
    }
}