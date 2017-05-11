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
        private Track Track { get; set; }
        private Chapter CurrentChapter { get; set; }
        private const int ShiftAmount = 250;
        private TimeSpan _shiftMillseconds;
        private readonly BindingSource _chapterListBindingSource = new BindingSource();
        private readonly DataGridViewCellStyle _defaultCellStyle;
        //public DataGridView DataGridView => chaptersDGV;

        public delegate void UpdateTrack(Track track);
        public delegate void UpdateChapterList();
        public delegate void SetCurrentChapter(Chapter chapter);

        public UpdateTrack UpdateTrackDelegate;
        public UpdateChapterList UpdateChapterListDelegate;
        public SetCurrentChapter SetCurrentChapterDelegate;
        public MainForm()
        {
            InitializeComponent();
            _shiftMillseconds = new TimeSpan(0, 0, 0, 0, ShiftAmount);

            chaptersDGV.DataSource = _chapterListBindingSource;
            //chaptersDGV.Columns[1].DataPropertyName = "TimeCode";
            //chaptersDGV.Columns[2].DataPropertyName = "Title";
            chaptersDGV.AutoGenerateColumns = false;
            _defaultCellStyle = chaptersDGV.DefaultCellStyle;
            
            UpdateTrackDelegate = UpdateTrackMethod;
            UpdateChapterListDelegate = UpdateFirstColumn;
            SetCurrentChapterDelegate = SetCurrentChapterMethod;
        }
        
        public void UpdateTrackMethod(Track track)
        {
            Track = track;
            _chapterListBindingSource.DataSource = Track.ChapterList.Chapters;
           
           titleArtistStatusLabel.Text = $"{Track.NowPlayingTrackInfo.Artist} – {Track.NowPlayingTrackInfo.Title}";
            if(Track.ChapterList.NumChapters == 0)
                chaptersCountStatusLabel.Text = "No Chapters";
        }

        public void UpdateFirstColumn()
        {
            for (int i = 0; i < chaptersDGV.RowCount; i++)
            {
                if (i == CurrentChapter.ChapterNumber - 1)
                {
                    StringBuilder sb = new StringBuilder(">>");
                    if (i == RepeatSection.A?.ChapterNumber - 1)
                        sb.Insert(0, "A");
                    else if (i == RepeatSection.B?.ChapterNumber - 1)
                        sb.Insert(0, "B");
                    chaptersDGV.Rows[CurrentChapter.ChapterNumber - 1].Cells[0].Value = sb.ToString();
                }
                else if (i == RepeatSection.A?.ChapterNumber - 1)
                    chaptersDGV.Rows[i].Cells[0].Value = "A";
                else if (i == RepeatSection.B?.ChapterNumber - 1)
                    chaptersDGV.Rows[i].Cells[0].Value = "B";
                else
                    chaptersDGV.Rows[i].Cells[0].Value = string.Empty;
            }
        }

        public void SetCurrentChapterMethod(Chapter chapter)
        {
            CurrentChapter = chapter;
            chaptersCountStatusLabel.Text = $"{CurrentChapter.ChapterNumber}/{Track.ChapterList.NumChapters} – {CurrentChapter.Title}";
            foreach (var dgvRow in chaptersDGV.Rows)
            {
                DataGridViewRow row = (DataGridViewRow) dgvRow;
                row.DefaultCellStyle = _defaultCellStyle;
            }
            chaptersDGV.Rows[CurrentChapter.ChapterNumber - 1].DefaultCellStyle = new DataGridViewCellStyle {BackColor = Color.PaleGreen, SelectionBackColor = Color.Green};
            UpdateFirstColumn();
        }
        private Chapter GetSelectedDGVChapter()
        {
            var firstRow = chaptersDGV.Rows[0];
            if (chaptersDGV.SelectedRows.Contains(firstRow))
            {
                MessageBox.Show("Cannot remove first chapter.");
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
            OnAddChapterButtonClickedRouted(e);
        }
        
        private void removeChapterButton_Click(object sender, EventArgs e)
        {
            var chapterToRemove = GetSelectedDGVChapter();
            if (chapterToRemove != null)
                //_chapterListBindingSource.RemoveCurrent();
                OnRemoveChapterButtonClickedRouted(chapterToRemove);
        }
        private void shiftPositionBackButton_Click(object sender, EventArgs e)
        {
            var chapterToShiftBack = GetSelectedDGVChapter();
            if (chapterToShiftBack == null)
            {
                return;
            }
            int? newPosition = chapterToShiftBack.Position - _shiftMillseconds.Milliseconds;
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
            int? newPosition = chapterToShiftForwards.Position + _shiftMillseconds.Milliseconds;
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
                //var chapt = ((sender as DataGridView).DataSource as List<Chapter>)[e.RowIndex];
                OnSelectedItemDoubleClickedRouted(chapt);
            }
        }
        private void chaptersDGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {   // Changes chapter title
            Track.ChapterList.SaveChaptersToFile();
            //var chapterToChangeTitle = (Chapter)chaptersDGV.Rows[e.RowIndex].DataBoundItem;
            //var newChapterTitle = (string) chaptersDGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            //if (chapterToChangeTitle.Title != newChapterTitle)
            //{
            //    OnChangeChapterRequested(chapterToChangeTitle, newChapterTitle, null);
            //}
        }

        
        public event EventHandler<Chapter> SelectedItemDoubleClickedRouted;
        protected virtual void OnSelectedItemDoubleClickedRouted(Chapter e)
        {
            SelectedItemDoubleClickedRouted?.Invoke(this, e);
        }

        public event EventHandler AddChapterButtonClickedRouted;
        protected virtual void OnAddChapterButtonClickedRouted(EventArgs e)
        {
            AddChapterButtonClickedRouted?.Invoke(this, e);
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
        private void chaptersDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {   // A-B Repeating
            if((e.ColumnIndex == 0) )
            {
                RepeatSection.ReceiveChapter((Chapter)chaptersDGV.Rows[e.RowIndex].DataBoundItem);
                UpdateFirstColumn();
            }
        }
    }
}
