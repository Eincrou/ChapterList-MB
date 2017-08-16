using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChapterListMB
{
    internal partial class PanelManager
    {
        private void ChaptersDgvOnCellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                _manager.ChangePlayerPosition(e.RowIndex);
            }
        }

        private void ChaptersDgvOnCellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // A-B Repeating
            if (e.ColumnIndex == 0)
            {
                _manager.SetChapterRepeat(e.RowIndex);
                UpdateFirstColumn();
                SetCurrentChapterImage();
            }
        }

        private void ChaptersDgvOnSelectionChanged(object sender, EventArgs eventArgs)
        {
            if (_manager.CurrentChapter == null || chaptersDgv.SelectedRows.Count < 1) return;
            DataGridViewRow selectedChapterRow = chaptersDgv.SelectedRows[0];
            if (selectedChapterRow.Index == _manager.CurrentChapter.ChapterNumber - 1)
            {
                SetCurrentChapterImage();
            }
            else
            {
                UpdateFirstColumn();
            }
            SetButtonsEnabledState();
        }
        private void ChaptersDgvOnCellEndEdit(object sender, DataGridViewCellEventArgs dataGridViewCellEventArgs)
        {
            _manager.Track.ChapterList.SaveChaptersToFile();
        }

        private void BtnAddChapterOnClick(object sender, EventArgs eventArgs)
        {
            _manager.AddChapter(comboChapterTitles.Text);
            _chapterListBindingSource.ResetBindings(false);
        }

        private void BtnRemoveChapterOnClick(object sender, EventArgs eventArgs)
        {
            _manager.RemoveChapter(chaptersDgv.SelectedRows[0].Index);
            _chapterListBindingSource.ResetBindings(false);
        }

        private void BtnShiftForwardOnClick(object sender, EventArgs eventArgs)
        {
            int selectedIndex = chaptersDgv.SelectedRows[0].Index;
            _manager.ChangeChapterPosition(selectedIndex, ShiftChapterPositionType.Forwards,
                GetPositionShiftModifier());
            chaptersDgv.UpdateCellValue(1, selectedIndex);
        }

        private void BtnShiftBackwardsOnClick(object sender, EventArgs eventArgs)
        {
            int selectedIndex = chaptersDgv.SelectedRows[0].Index;
            _manager.ChangeChapterPosition(selectedIndex, ShiftChapterPositionType.Backwards,
                GetPositionShiftModifier());
            chaptersDgv.UpdateCellValue(1, selectedIndex);
        }

        private double GetPositionShiftModifier()
        {
            double modifier = 1;
            switch (Control.ModifierKeys)
            {
                case Keys.Shift:
                    modifier = 4;
                    break;
                case Keys.Control:
                    modifier = 0.5;
                    break;
                case Keys.Alt:
                    break;
            }
            return modifier;
        }
    }
}
