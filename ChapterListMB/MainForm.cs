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
        private int _repeatIndexA = 0;
        private int _repeatIndexB = 0;

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
            chaptersDGV.AutoGenerateColumns = false;

            UpdateTrackDelegate = UpdateTrackMethod;
            UpdateChapterListDelegate = UpdateChapterListMethod;
            SetCurrentChapterDelegate = SetCurrentChapterMethod;
        }
        
        public void UpdateTrackMethod(Track track)
        {
            Track = track;

            chaptersDGV.DataSource = Track.ChapterList.Chapters;
            chaptersDGV.Columns[1].DataPropertyName = "TimeCode";
            chaptersDGV.Columns[2].DataPropertyName = "Title";

           // statusBar1.Text =$"{Track.NowPlayingTrackInfo.Artist} - {Track.NowPlayingTrackInfo.Title}";
            titleArtistStatusLabel.Text = $"{Track.NowPlayingTrackInfo.Artist} – {Track.NowPlayingTrackInfo.Title}";
        }

        public void UpdateChapterListMethod()
        {

        }

        public void SetCurrentChapterMethod(Chapter chapter)
        {
            for (int i = 0; i < chaptersDGV.RowCount; i++)
            {
                if (i == chapter.ChapterNumber)
                    chaptersDGV.Rows[chapter.ChapterNumber - 1].Cells[0].Value = ">>";
                else if (i == _repeatIndexA)
                    chaptersDGV.Rows[i].Cells[0].Value = "A";
                else if (i == _repeatIndexB)
                    chaptersDGV.Rows[i].Cells[0].Value = "B";
                else
                    chaptersDGV.Rows[i].Cells[0].Value = string.Empty;
            }
            
            chaptersCountStatusLabel.Text = $"{chapter.ChapterNumber}/{Track.ChapterList.NumChapters} – {chapter.Title}";
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
                OnRemoveChapterButtonClickedRouted(chapterToRemove);
        }
        private void shiftPositionBackButton_Click(object sender, EventArgs e)
        {
            var chapterToShiftBack = GetSelectedDGVChapter();
            if (chapterToShiftBack == null)
            {
                return;
            }
            int? newPosition = chapterToShiftBack.Position - ShiftAmount;
            if (newPosition < 1) newPosition = 1;
            OnChangeChapterRequested(chapterToShiftBack, null, newPosition);
        }
        private void shiftPositionFwdButton_Click(object sender, EventArgs e)
        {
            var chapterToShiftForwards = GetSelectedDGVChapter();
            if (chapterToShiftForwards == null)
            {
                return;
            }
            int? newPosition = chapterToShiftForwards.Position + ShiftAmount;
            if (newPosition > Track.NowPlayingTrackInfo.Duration.TotalMilliseconds)
                newPosition = (int)Track.NowPlayingTrackInfo.Duration.TotalMilliseconds;
            OnChangeChapterRequested(chapterToShiftForwards, null, newPosition);
        }
        private void chaptersDGV_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {   // Requests to set player position to chapter position.
            if (e.RowIndex >= 0 && e.RowIndex < Track.ChapterList.Chapters.Count)
            {
                var chapt = ((sender as DataGridView).DataSource as List<Chapter>)[e.RowIndex];
                OnSelectedItemDoubleClickedRouted(chapt);
            }
        }
        private void chaptersDGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {   // Changes chapter title
            var chapterToChangeTitle = (Chapter)chaptersDGV.Rows[e.RowIndex].DataBoundItem;
            var newChapterTitle = (string) chaptersDGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            if (chapterToChangeTitle.Title != newChapterTitle)
            {
                OnChangeChapterRequested(chapterToChangeTitle, newChapterTitle, null);
            }
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
        }

        public event EventHandler<Chapter> RemoveChapterButtonClickedRouted;
        protected virtual void OnRemoveChapterButtonClickedRouted(Chapter e)
        {
            RemoveChapterButtonClickedRouted?.Invoke(this, e);
        }

        public event EventHandler<ChapterChangeEventArgs> ChangeChapterRequested;
        protected virtual void OnChangeChapterRequested(Chapter c, string newTitle, int?  newPosition)
        {
            var chapterChange = !newPosition.HasValue ? 
                new ChapterChangeEventArgs(c, newTitle, c.Position) : 
                new ChapterChangeEventArgs(c, c.Title, newPosition.Value);
            ChangeChapterRequested?.Invoke(this, chapterChange);
        }
        public event EventHandler<RepeatChaptersEventArgs> RepeatChaptersRequested;
        protected virtual void OnRepeatChaptersRequested(Chapter a, Chapter b)
        {
            var chaptersToRepeat = new RepeatChaptersEventArgs(a, b);
            RepeatChaptersRequested?.Invoke(this, chaptersToRepeat);
        }

        private void chaptersDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if((e.ColumnIndex == 0) )
            {
                if((_repeatIndexA == 0) && (_repeatIndexB == 0) && (e.RowIndex != chaptersDGV.RowCount) ) // Can't set A to last chapter
                {
                    if(_repeatIndexA > 0 && _repeatIndexB == 0)
                    {
                        _repeatIndexA = 0;
                        chaptersDGV.Rows[e.RowIndex].Cells[0].Value = string.Empty;
                    }
                    else
                    {
                        _repeatIndexA = e.RowIndex;
                        chaptersDGV.Rows[e.RowIndex].Cells[0].Value = "A";
                    }                    
                }
                else if ((_repeatIndexB == 0) && (e.RowIndex > _repeatIndexA))
                {
                    if (_repeatIndexB > 0)
                    {
                        _repeatIndexB = 0;
                        chaptersDGV.Rows[e.RowIndex].Cells[0].Value = string.Empty;
                    }
                    else
                    {
                        _repeatIndexB = e.RowIndex;
                        chaptersDGV.Rows[e.RowIndex].Cells[0].Value = "B";
                    }
                    
                }
                if(_repeatIndexA > 0 && _repeatIndexB < 1)
                {
                    var chapterA = Track.ChapterList.Chapters[_repeatIndexA];
                    var chapterB = Track.ChapterList.Chapters[_repeatIndexA+1];   // Use next chapter if no 'B' has been set.
                    OnRepeatChaptersRequested(chapterA, chapterB);
                }
                else if (_repeatIndexA > 0 && _repeatIndexB > 0)
                {
                    var chapterA = Track.ChapterList.Chapters[_repeatIndexA];
                    var chapterB = Track.ChapterList.Chapters[_repeatIndexB];
                    OnRepeatChaptersRequested(chapterA, chapterB);
                }                              
            }
        }


    }
}
