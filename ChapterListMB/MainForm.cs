using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChapterListMB
{
    public partial class MainForm : Form
    {
        public Track Track { get; set; }
        private int ShiftAmount { get; } = 250;
        private TimeSpan _shiftMillseconds;

        public delegate void UpdateTrack(Track track);
        public delegate void UpdateChapterList();

        public UpdateTrack UpdateTrackDelegate;
        public UpdateChapterList UpdateChapterListDelegate;
        public MainForm()
        {
            InitializeComponent();
            _shiftMillseconds = new TimeSpan(0, 0, 0, 0, ShiftAmount);
            chaptersDGV.AutoGenerateColumns = false;

            UpdateTrackDelegate = UpdateTrackMethod;
            UpdateChapterListDelegate = UpdateChapterListMethod;
            chaptersListBox.SelectedIndexChanged += OnChaptersListBoxSelectedIndexChanged;
            chaptersListBox.MouseDoubleClick += OnChapterDoubleClick;
        }
        
        public void UpdateTrackMethod(Track track)
        {
            Track = track;
            //chaptersListBox.DisplayMember = "Title";
            chaptersListBox.DataSource = Track.ChapterList.Chapters;
            artistTitleLabel.Text = $"{Track.NowPlayingTrackInfo.Artist} - \"{Track.NowPlayingTrackInfo.Title}\"";

            chaptersDGV.DataSource = Track.ChapterList.Chapters;
            chaptersDGV.Columns[0].DataPropertyName = "TimeCode";
            chaptersDGV.Columns[1].DataPropertyName = "Title";
        }

        public void UpdateChapterListMethod()
        {
            //chaptersListBox.DataSource = Track.ChapterList.Chapters;
        }
        private void OnChaptersListBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            if (chaptersListBox.SelectedIndex < 0) return;
            var lb = (ListBox)sender;
            var chapter = (lb.SelectedItem as Chapter);
            if (chapter != null) chapterTitleTextBox.Text = chapter.Title;
        }
        private void OnChapterDoubleClick(object sender, MouseEventArgs e)
        {
            OnSelectedItemDoubleClickedRouted(((ListBox)sender).SelectedItem as Chapter);
        }
        private void addChapterButton_Click(object sender, EventArgs e)
        {
            OnAddChapterButtonClickedRouted(chapterTitleTextBox.Text);
        }
        
        private void removeChapterButton_Click(object sender, EventArgs e)
        {
            if (chaptersListBox.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a chapter to remove.");
                return;
            }
            OnRemoveChapterButtonClickedRouted((Chapter)chaptersListBox.SelectedItem);

        }
        private void shiftPositionBackButton_Click(object sender, EventArgs e)
        {
            if (chaptersListBox.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a chapter.");
                return;
            }
            var chapt = (Chapter) chaptersListBox.SelectedItem;
            int? newPosition = chapt.Position - ShiftAmount;
            if (newPosition < 0) newPosition = 0;
            OnChangeChapterRequested((Chapter) chaptersListBox.SelectedItem, null, newPosition);
        }
        private void shiftPositionFwdButton_Click(object sender, EventArgs e)
        {
            if (chaptersListBox.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a chapter.");
                return;
            }
            var chapt = (Chapter)chaptersListBox.SelectedItem;
            int? newPosition = chapt.Position + ShiftAmount;
            if (newPosition > Track.NowPlayingTrackInfo.Duration.TotalMilliseconds)
                newPosition = (int)Track.NowPlayingTrackInfo.Duration.TotalMilliseconds;
            OnChangeChapterRequested((Chapter)chaptersListBox.SelectedItem, null, newPosition);
        }
        private void chapterTitleTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || chaptersListBox.SelectedIndex < 0) return;
            var chapt = (Chapter)chaptersListBox.SelectedItem;
            if (chapt.Title == chapterTitleTextBox.Text) return;
            OnChangeChapterRequested(chapt, chapterTitleTextBox.Text, null);
        }
        private void chaptersDGV_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < Track.ChapterList.Chapters.Count)
            {
                var chapt = ((sender as DataGridView).DataSource as List<Chapter>)[e.RowIndex];
                OnSelectedItemDoubleClickedRouted(chapt);
            }
        }

        public event EventHandler<Chapter> SelectedItemDoubleClickedRouted;
        protected virtual void OnSelectedItemDoubleClickedRouted(Chapter e)
        {
            SelectedItemDoubleClickedRouted?.Invoke(this, e);
        }

        public event EventHandler<string> AddChapterButtonClickedRouted;
        protected virtual void OnAddChapterButtonClickedRouted(string chapterTitle)
        {
            AddChapterButtonClickedRouted?.Invoke(this, chapterTitle);
        }

        public event EventHandler<Chapter> RemoveChapterButtonClickedRouted;
        protected virtual void OnRemoveChapterButtonClickedRouted(Chapter e)
        {
            RemoveChapterButtonClickedRouted?.Invoke(this, e);
        }

        public event EventHandler<ChapterChangeEventArgs> ChangeChapterRequested;
        protected virtual void OnChangeChapterRequested(Chapter c, string newTitle, int?  newPosition)
        {
            ChapterChangeEventArgs chapterChange;
            chapterChange = !newPosition.HasValue ? 
                new ChapterChangeEventArgs(c, newTitle, c.Position) : 
                new ChapterChangeEventArgs(c, c.Title, newPosition.Value);
            ChangeChapterRequested?.Invoke(this, chapterChange);
        }


    }
}
