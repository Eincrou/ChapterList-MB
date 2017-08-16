using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChapterListMB.Properties;
using MusicBeePlugin;

namespace ChapterListMB
{
    internal partial class PanelManager
    {
        private readonly Control _panel;
        private readonly Manager _manager;
        
        private readonly BindingSource _chapterListBindingSource = new BindingSource();

        private DataGridView chaptersDgv;
        private DataGridViewImageColumn dgvChapterStatus;
        private DataGridViewTextBoxColumn dgvPositionCol;
        private DataGridViewTextBoxColumn dgvTitleCol;
        private DataGridViewCellStyle dataGridViewCellStyle1;
        private DataGridViewCellStyle dataGridViewCellStyle2;

        private Button btnAddChapter;
        private Button btnRemoveChapter;
        private Button btnShiftBackwards;
        private Button btnShiftForward;

        private ComboBox comboChapterTitles;
        private ToolTip toolTip1;

        public int CurrentChapterIndex { get; set; }
        
        /// <summary>
        /// Sets up and controls a MusicBee dockable panel
        /// </summary>
        /// <param name="panel">The panel control provided by MusicBee</param>
        /// <param name="manager">ChapterList MB manager</param>
        public PanelManager(Control panel, Manager manager)
        {
            _panel = panel;
            _manager = manager;

            toolTip1 = new ToolTip();

            SetUpDataGridView();
            SetUpButtons();
            SetUpOther();
            AddControls();

            _manager.CurrentChapterChanged += ManagerOnCurrentChapterChanged;
            _manager.TrackChanged += ManagerOnTrackChanged;

            panel.Invoke(new Action(() =>
            {
                panel.MinimumSize = new Size(300, 260);
            }));
        }
        
        private void SetUpDataGridView()
        {
            dataGridViewCellStyle1 = new DataGridViewCellStyle
            {
                BackColor = _manager.GetElementColor(Plugin.SkinElement.SkinTrackAndArtistPanel, Plugin.ElementState.ElementStateDefault, Plugin.ElementComponent.ComponentBackground),
                SelectionBackColor = _manager.GetElementColor(Plugin.SkinElement.SkinInputControl, Plugin.ElementState.ElementStateModified, Plugin.ElementComponent.ComponentForeground),
                SelectionForeColor = _manager.GetElementColor(Plugin.SkinElement.SkinInputControl, Plugin.ElementState.ElementStateDefault, Plugin.ElementComponent.ComponentBackground),
            };
            dataGridViewCellStyle2 = new DataGridViewCellStyle
            {
                BackColor = _manager.GetElementColor(Plugin.SkinElement.SkinInputPanelLabel, Plugin.ElementState.ElementStateDefault, Plugin.ElementComponent.ComponentBackground),
            };
            chaptersDgv = new DataGridView
            {
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeColumns = false,
                AllowUserToResizeRows = false,
                DefaultCellStyle = dataGridViewCellStyle1,
                AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2,
                AutoGenerateColumns = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.None,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                DataSource = _chapterListBindingSource,
                Location = new Point(12, 12),
                MinimumSize = new Size(200, 200),
                MultiSelect = false,
                Name = "chaptersDGV",
                RowHeadersVisible = false,
                RowHeadersWidth = 30,
                RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Size = new Size(_panel.Width - 16, _panel.Height - 62),
                TabIndex = 0,
                //CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.chaptersDGV_CellEndEdit),
                };
            chaptersDgv.BackgroundColor = _manager.GetElementColor(Plugin.SkinElement.SkinTrackAndArtistPanel,
                Plugin.ElementState.ElementStateDefault, Plugin.ElementComponent.ComponentBackground);
            dgvChapterStatus = new DataGridViewImageColumn
            {
                HeaderText = "",
                Name = "ChapterStatus",
                ReadOnly = true,
                Resizable = DataGridViewTriState.False,
                Width = 20,
            };
            dgvPositionCol = new DataGridViewTextBoxColumn
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader,
                DataPropertyName = "TimeCode",
                HeaderText = "Position",
                Name = "positionCol",
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                Width = 50
            };
            dgvTitleCol = new DataGridViewTextBoxColumn
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = "Title",
                HeaderText = "Chapter Title",
                Name = "titleCol",
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            chaptersDgv.Columns.AddRange(dgvChapterStatus, dgvPositionCol, dgvTitleCol);
            chaptersDgv.DataSource = _chapterListBindingSource;

            chaptersDgv.CellMouseClick += ChaptersDgvOnCellMouseClick;
            chaptersDgv.CellMouseDoubleClick += ChaptersDgvOnCellMouseDoubleClick;
            chaptersDgv.SelectionChanged += ChaptersDgvOnSelectionChanged;
            chaptersDgv.CellEndEdit += ChaptersDgvOnCellEndEdit;
        }



        private void SetUpButtons()
        {
            btnAddChapter = new Button
            {
                Image = Resources.ChapterAdd,
                Size = new Size(38, 38),
                Location = new Point(8, _panel.Height - 35 - 8),
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom,
                TabIndex = 1
            };
            btnAddChapter.Click += BtnAddChapterOnClick;
            toolTip1.SetToolTip(btnAddChapter, Resources.TooltipAddChapter);
            btnRemoveChapter = new Button
            {
                Image = Resources.ChapterRemove,
                Size = new Size(38, 38),
                Location = new Point(btnAddChapter.Location.X + btnAddChapter.Width + 2, _panel.Height - 35 - 8),
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom,
                TabIndex = 2
            };
            btnRemoveChapter.Click += BtnRemoveChapterOnClick;
            toolTip1.SetToolTip(btnRemoveChapter, Resources.TooltipRemoveChapter);
            btnShiftForward = new Button
            {
                Image = Resources.Shiftforward,
                Size = new Size(38, 38),
                Location = new Point(_panel.Width - 38 - 8, _panel.Height - 35 - 8),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                TabIndex = 5,
            };
            btnShiftForward.Click += BtnShiftForwardOnClick;
            toolTip1.SetToolTip(btnShiftForward, Resources.TooltipShiftFwd);
            btnShiftBackwards = new Button
            {
                Image = Resources.ShiftBack,
                Size = new Size(38, 38),
                Location = new Point(btnShiftForward.Location.X - btnShiftForward.Width - 2, _panel.Height - 35 - 8),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                TabIndex = 4
            };
            btnShiftBackwards.Click += BtnShiftBackwardsOnClick;
            toolTip1.SetToolTip(btnShiftBackwards, Resources.TooltipShiftBackward);
        }
        

        private void SetUpOther()
        {
            comboChapterTitles = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDown,
                Items = {"<Default>", "Intro", "Verse", "Prechorus", "Chorus", "Bridge", "Solo", "Outro"},
                Anchor = AnchorStyles.Bottom,
                SelectedIndex = 0,
                TabIndex = 3,
            };
            comboChapterTitles.Location = new Point(_panel.Width/2 - comboChapterTitles.Width/2, _panel.Height - 35);
            toolTip1.SetToolTip(comboChapterTitles, Resources.TooltipChapterTitles);
        }

        private void AddControls()
        {
            _panel.Invoke(new Action(() =>
            {
                _panel.SuspendLayout();
                _panel.Controls.AddRange(new Control[]
                {
                    chaptersDgv, btnAddChapter,btnRemoveChapter, btnShiftForward, btnShiftBackwards, comboChapterTitles,
                    comboChapterTitles
                });
                _panel.ResumeLayout();
            }));
        }

        private void UpdateFirstColumn()
        {
            _panel.Invoke(new Action(() =>
            {
                chaptersDgv.SuspendLayout();
                foreach (DataGridViewRow row in chaptersDgv.Rows)
                {
                    if (row.Index == RepeatSection.A?.ChapterNumber - 1)
                    {   // Repeat chapter A image
                        SetReplayImage("A", row);
                    }
                    else if (row.Index == RepeatSection.B?.ChapterNumber - 1)
                    {   // Repeat chapter B image
                        SetReplayImage("B", row);
                    }
                    else if (row.Index == _manager.CurrentChapter.ChapterNumber - 1)
                    {   // Current chapter playhead
                        SetCurrentChapterImage();
                    }
                    else
                    {   // Empty image for blank cell
                        row.Cells[0].Value = new Bitmap(16, 16);
                    }
                }
                chaptersDgv.ResumeLayout();
            }));

        }
        
        private void SetCurrentChapterImage()
        {
            DataGridViewRow selectedRow = chaptersDgv.SelectedRows[0];
            if (selectedRow.Index == _manager.CurrentChapter.ChapterNumber - 1)
            {   // Current chapter is selected
                if (selectedRow.Index == RepeatSection.A?.ChapterNumber - 1
                    || selectedRow.Index == RepeatSection.B?.ChapterNumber - 1)
                {
                    SetReplayImage("white", selectedRow);
                }
                else
                {
                    chaptersDgv.Rows[_manager.CurrentChapter.ChapterNumber - 1].Cells[0].Value = Resources.ActiveChapterWhite;
                }
            }
            else
            {   // Other chapter is selected
                chaptersDgv.Rows[_manager.CurrentChapter.ChapterNumber - 1].Cells[0].Value = Resources.ActiveChapterBlack;
            }
        }

        private void SetDgvHighlightColors()
        {
            foreach (DataGridViewRow row in chaptersDgv.Rows)
            {
                row.DefaultCellStyle = (row.Index % 2) == 0 ? dataGridViewCellStyle1 : dataGridViewCellStyle2; ;
                row.Cells[2].Style = (row.Index % 2) == 0 ? dataGridViewCellStyle1 : dataGridViewCellStyle2; ;
            }
            var selectedStyle = new DataGridViewCellStyle
            {
                BackColor = Settings.Default.HighlightColor,
                SelectionBackColor = Settings.Default.HighlightBackgroundColor,
            };
            chaptersDgv.Rows[CurrentChapterIndex].DefaultCellStyle = selectedStyle;

            DataGridViewCellStyle boldStyle = new DataGridViewCellStyle(selectedStyle)
            {
                Font = new Font(FontFamily.GenericSansSerif, 8.25f, FontStyle.Bold)
            }; // Bold the current chapter title text
            
            chaptersDgv.Rows[CurrentChapterIndex].Cells[2].Style = boldStyle;
        }
        private void SetReplayImage(string repeatType, DataGridViewRow row)
        {
            switch (repeatType)
            {
                case "A":
                    Bitmap repeatA = new Bitmap(Resources.RepeatA, new Size(16, 16));
                    row.Cells[0].Value = repeatA;
                    break;
                case "B":
                    Bitmap repeatB = new Bitmap(Resources.RepeatB, new Size(16, 16));
                    row.Cells[0].Value = repeatB;
                    break;
            }
        }
        private void SetButtonsEnabledState()
        {
            if (_manager.Track.ChapterList.NumChapters == 0 || chaptersDgv.SelectedRows[0].Index == 0)
            {
                btnRemoveChapter.Enabled = false;
                btnShiftBackwards.Enabled = false;
                btnShiftForward.Enabled = false;
            }
            else
            {
                btnRemoveChapter.Enabled = true;
                btnShiftBackwards.Enabled = true;
                btnShiftForward.Enabled = true;
            }
        }

        private void ManagerOnTrackChanged(object sender, Track track)
        {
            _panel.Invoke(new Action(() =>
            {
                _chapterListBindingSource.DataSource = null;
                _chapterListBindingSource.DataSource = track.ChapterList;
                
                SetButtonsEnabledState();
            }));
            SetDgvHighlightColors();
            UpdateFirstColumn();
        }
        private void ManagerOnCurrentChapterChanged(object sender, Chapter chapter)
        {
            CurrentChapterIndex = chapter.ChapterNumber - 1;
            SetDgvHighlightColors();
            UpdateFirstColumn();
        }
    }
}
