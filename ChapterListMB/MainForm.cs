using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace ChapterListMB
{
    public partial class MainForm : Form
    {
        private Track Track { get; set; }
        private Chapter CurrentChapter { get; set; }

        private readonly BindingSource _chapterListBindingSource = new BindingSource();
        private readonly DataGridViewCellStyle _defaultCellStyleA;  // White row style
        private readonly DataGridViewCellStyle _defaultCellStyleB;  // Tan row style
        
        public delegate void UpdateTrack(Track track);
        public delegate void UpdateChapterList();
        public delegate void SetCurrentChapter(Chapter chapter);

        public UpdateTrack UpdateTrackDelegate;
        public UpdateChapterList UpdateChapterListDelegate;
        public SetCurrentChapter SetCurrentChapterDelegate;
        public MainForm()
        {
            InitializeComponent();

            chaptersDGV.DataSource = _chapterListBindingSource;
            chaptersDGV.AutoGenerateColumns = false;
            // Copy default cell styles
            _defaultCellStyleA = chaptersDGV.DefaultCellStyle;
            _defaultCellStyleB = chaptersDGV.AlternatingRowsDefaultCellStyle;

            comboBoxNewChapterName.SelectedIndex = 0;

            UpdateTrackDelegate = UpdateTrackMethod;
            UpdateChapterListDelegate = UpdateFirstColumn;
            SetCurrentChapterDelegate = SetCurrentChapterMethod;
        }
        
        public void UpdateTrackMethod(Track track)
        {
            Track = track;
            _chapterListBindingSource.DataSource = Track.ChapterList.Chapters;

            ClearFirstColumn();

           titleArtistStatusLabel.Text = $"{Track.NowPlayingTrackInfo.Artist} – {Track.NowPlayingTrackInfo.Title}";
            if(Track.ChapterList.NumChapters == 0)
                chaptersCountStatusLabel.Text = "No Chapters";
        }

        public void UpdateFirstColumn()
        {
            foreach (DataGridViewRow row in chaptersDGV.Rows)
            {
                if (row.Index == RepeatSection.A?.ChapterNumber - 1)
                {   // Repeat chapter A image
                    SetReplayImage("black", row);
                }
                else if (row.Index == RepeatSection.B?.ChapterNumber - 1)
                {   // Repeat chapter B image
                    SetReplayImage("gray", row);
                }
                else if (row.Index == CurrentChapter?.ChapterNumber - 1)
                {   // Current chapter playhead
                    SetCurrentChapterImage();
                }
                else
                {   // Empty image for blank cell
                    row.Cells[0].Value = new Bitmap(16,16);
                }
            }
        }

        public void ClearFirstColumn()
        {   // Blanks out first current chapter column
            for (int i = 0; i < chaptersDGV.RowCount; i++)
            {
                chaptersDGV.Rows[i].Cells[0].Value = new Bitmap(16, 16);
            }
        }

        public void SetCurrentChapterMethod(Chapter chapter)
        {
            CurrentChapter = chapter;
            chaptersCountStatusLabel.Text = $"{CurrentChapter.ChapterNumber}/{Track.ChapterList.NumChapters} – {CurrentChapter.Title}";

            SetRowColors();
            UpdateFirstColumn();
        }

        private void SetRowColors()
        {
            foreach (var dgvRow in chaptersDGV.Rows) // Reset row colors to defaults
            {
                DataGridViewRow row = (DataGridViewRow) dgvRow;
                row.DefaultCellStyle = (row.Index%2) == 0 ? _defaultCellStyleA : _defaultCellStyleB;
                row.Cells[2].Style = (row.Index%2) == 0 ? _defaultCellStyleA : _defaultCellStyleB;
            }
            var selectedStyle = new DataGridViewCellStyle
            {
                BackColor = Properties.Settings.Default.HighlightColor,
                SelectionBackColor = Properties.Settings.Default.HighlightBackgroundColor
            };
            chaptersDGV.Rows[CurrentChapter.ChapterNumber - 1].DefaultCellStyle = selectedStyle;

            var boldStyle = new DataGridViewCellStyle(selectedStyle);   // Bold the current chapter title text
            boldStyle.Font = new Font(FontFamily.GenericSansSerif, 8.25f, FontStyle.Bold);
            chaptersDGV.Rows[CurrentChapter.ChapterNumber - 1].Cells[2].Style = boldStyle;
        }

        private Chapter GetSelectedDGVChapter()
        {
            if (chaptersDGV.SelectedRows.Contains(chaptersDGV.Rows[0]))
            {
                MessageBox.Show("Cannot remove or change position of first chapter.");
                return null;
            }
            if (chaptersDGV.SelectedRows.Count == 0 || chaptersDGV.SelectedRows.Count > 1)
            {
                MessageBox.Show("Invalid chapter selection.");
                return null;
            }
            return (Chapter)chaptersDGV.SelectedRows[0].DataBoundItem;
        }
        private void addChapterButton_Click(object sender, EventArgs e)
        {
            string newChapterName = comboBoxNewChapterName.Text == "<Default>"
                ? string.Empty
                : comboBoxNewChapterName.Text;  // Uses text from center combo box for new chapter name
            OnAddChapterButtonClickedRouted(newChapterName);
        }
        
        private void removeChapterButton_Click(object sender, EventArgs e)
        {
            var chapterToRemove = GetSelectedDGVChapter();
            if (chapterToRemove != null)
                OnRemoveChapterButtonClickedRouted(chapterToRemove);
        }
        private void shiftPositionBackButton_Click(object sender, EventArgs e)
        {
            var chapterToShiftBack = GetSelectedDGVChapter();
            if (chapterToShiftBack == null)
            {
                return;
            }

            int? newPosition = chapterToShiftBack.Position -
                               (ModifierKeys == Keys.Shift
                                   ? (int) Properties.Settings.Default.ChapterPositionShiftValue.TotalMilliseconds*4
                                   : (int) Properties.Settings.Default.ChapterPositionShiftValue.TotalMilliseconds);
            if (newPosition < 1) newPosition = 1;
            OnChangeChapterRequested(chapterToShiftBack, null, newPosition);
            chaptersDGV.UpdateCellValue(1, chapterToShiftBack.ChapterNumber - 1);
        }
        private void shiftPositionFwdButton_Click(object sender, EventArgs e)
        {
            var chapterToShiftForwards = GetSelectedDGVChapter();
            if (chapterToShiftForwards == null)
            {
                return;
            }
            int? newPosition = chapterToShiftForwards.Position +
                               (ModifierKeys == Keys.Shift
                                   ? (int) Properties.Settings.Default.ChapterPositionShiftValue.TotalMilliseconds*4
                                   : (int) Properties.Settings.Default.ChapterPositionShiftValue.TotalMilliseconds);
            if (newPosition > Track.NowPlayingTrackInfo.Duration.TotalMilliseconds)
                newPosition = (int)Track.NowPlayingTrackInfo.Duration.TotalMilliseconds;
            OnChangeChapterRequested(chapterToShiftForwards, null, newPosition);
            chaptersDGV.UpdateCellValue(1, chapterToShiftForwards.ChapterNumber - 1);
        }
        private void chaptersDGV_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {   // Requests to set player position to chapter position.
            if (e.RowIndex >= 0 && e.RowIndex < Track.ChapterList.NumChapters)
            {
                Chapter chapt = ((List<Chapter>) _chapterListBindingSource.DataSource)[e.RowIndex];
                OnSelectedItemDoubleClickedRouted(chapt);
            }
        }
        private void chaptersDGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {   // Changes chapter title from editbox
            Track.ChapterList.SaveChaptersToFile();
        }
        
        public event EventHandler<Chapter> SelectedItemDoubleClickedRouted;
        protected virtual void OnSelectedItemDoubleClickedRouted(Chapter e)
        {   
            SelectedItemDoubleClickedRouted?.Invoke(this, e);
        }

        public event EventHandler<string> AddChapterButtonClickedRouted;
        protected virtual void OnAddChapterButtonClickedRouted(string newChapterName)
        {
            AddChapterButtonClickedRouted?.Invoke(this, newChapterName);
            _chapterListBindingSource.ResetBindings(false);
        }

        public event EventHandler<Chapter> RemoveChapterButtonClickedRouted;
        protected virtual void OnRemoveChapterButtonClickedRouted(Chapter e)
        {
            RemoveChapterButtonClickedRouted?.Invoke(this, e);
            _chapterListBindingSource.ResetBindings(false);
        }

        public event EventHandler<ChapterChangeEventArgs> ChangeChapterRequested;
        protected virtual void OnChangeChapterRequested(Chapter c, string newTitle, int?  newPosition)
        {
            var chapterChange = !newPosition.HasValue ? 
                new ChapterChangeEventArgs(c, newTitle, c.Position) : 
                new ChapterChangeEventArgs(c, c.Title, newPosition.Value);
            ChangeChapterRequested?.Invoke(this, chapterChange);
        }
        private void OnChaptersDgvCellClick(object sender, DataGridViewCellEventArgs e)
        {   // A-B Repeating
            if((e.ColumnIndex == 0) )
            {
                Chapter toRepeat = (Chapter) chaptersDGV.Rows[e.RowIndex].DataBoundItem;
                RepeatSection.ReceiveChapter(toRepeat, Track.NowPlayingTrackInfo.Duration);
                UpdateFirstColumn();
                SetCurrentChapterImage();
            }
        }

        private void OnChaptersDgvSelectionChanged(object sender, EventArgs e)
        {   
            
            if (chaptersDGV.Rows.Count > 1) // Don't call SCCPI until after current chapter has changed.
            {
                if (CurrentChapter == null || chaptersDGV.SelectedRows.Count < 1) return;
                DataGridViewRow selectedChapterRow = chaptersDGV.SelectedRows[0];
                if (selectedChapterRow.Index == CurrentChapter.ChapterNumber- 1)
                {
                    SetCurrentChapterImage();
                }
                else
                {
                    UpdateFirstColumn();
                }
            }
        }

        private void SetCurrentChapterImage()
        {
            DataGridViewRow selectedRow = chaptersDGV.SelectedRows[0];
            if (selectedRow.Index == CurrentChapter.ChapterNumber - 1)
            {   // Current chapter is selected
                if (selectedRow.Index == RepeatSection.A?.ChapterNumber - 1
                    || selectedRow.Index == RepeatSection.B?.ChapterNumber - 1)
                {
                    SetReplayImage("white", selectedRow);
                }
                else
                {
                    SetPlayheadImage("white");
                }
            }
            else
            {   // Other chapter is selected
                SetPlayheadImage("black");
            }
        }

        private void SetReplayImage(string color, DataGridViewRow row)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            Stream myPlayheadStream = asm.GetManifestResourceStream($"ChapterListMB.Resources.replaychapter-{color}.png");
            row.Cells[0].Value = Image.FromStream(myPlayheadStream);
        }

        private void SetPlayheadImage(string color)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            Stream myPlayheadStream = asm.GetManifestResourceStream($"ChapterListMB.Resources.activechapter-{color}.png");
            chaptersDGV.Rows[CurrentChapter.ChapterNumber - 1].Cells[0].Value = Image.FromStream(myPlayheadStream);
        }
    }
}
