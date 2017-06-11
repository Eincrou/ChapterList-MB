namespace ChapterListMB
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.addChapterButton = new System.Windows.Forms.Button();
            this.removeChapterButton = new System.Windows.Forms.Button();
            this.shiftPositionBackButton = new System.Windows.Forms.Button();
            this.shiftPositionFwdButton = new System.Windows.Forms.Button();
            this.chaptersDGV = new System.Windows.Forms.DataGridView();
            this.ChapterStatus = new System.Windows.Forms.DataGridViewImageColumn();
            this.positionCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.titleCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.titleArtistStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.chaptersCountStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.comboBoxNewChapterName = new System.Windows.Forms.ComboBox();
            this.lblNewChapt = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chaptersDGV)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // addChapterButton
            // 
            this.addChapterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addChapterButton.Location = new System.Drawing.Point(11, 294);
            this.addChapterButton.Name = "addChapterButton";
            this.addChapterButton.Size = new System.Drawing.Size(40, 35);
            this.addChapterButton.TabIndex = 0;
            this.addChapterButton.Text = "Add";
            this.toolTip1.SetToolTip(this.addChapterButton, "Create a new chapter at the current player position.");
            this.addChapterButton.UseVisualStyleBackColor = true;
            this.addChapterButton.Click += new System.EventHandler(this.addChapterButton_Click);
            // 
            // removeChapterButton
            // 
            this.removeChapterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.removeChapterButton.Location = new System.Drawing.Point(57, 294);
            this.removeChapterButton.Name = "removeChapterButton";
            this.removeChapterButton.Size = new System.Drawing.Size(57, 35);
            this.removeChapterButton.TabIndex = 2;
            this.removeChapterButton.Text = "Remove";
            this.toolTip1.SetToolTip(this.removeChapterButton, "Delete the selected chapter.");
            this.removeChapterButton.UseVisualStyleBackColor = true;
            this.removeChapterButton.Click += new System.EventHandler(this.removeChapterButton_Click);
            // 
            // shiftPositionBackButton
            // 
            this.shiftPositionBackButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.shiftPositionBackButton.Location = new System.Drawing.Point(211, 294);
            this.shiftPositionBackButton.Name = "shiftPositionBackButton";
            this.shiftPositionBackButton.Size = new System.Drawing.Size(40, 35);
            this.shiftPositionBackButton.TabIndex = 5;
            this.shiftPositionBackButton.Text = "<<";
            this.shiftPositionBackButton.UseVisualStyleBackColor = true;
            this.shiftPositionBackButton.Click += new System.EventHandler(this.shiftPositionBackButton_Click);
            // 
            // shiftPositionFwdButton
            // 
            this.shiftPositionFwdButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.shiftPositionFwdButton.Location = new System.Drawing.Point(257, 295);
            this.shiftPositionFwdButton.Name = "shiftPositionFwdButton";
            this.shiftPositionFwdButton.Size = new System.Drawing.Size(40, 35);
            this.shiftPositionFwdButton.TabIndex = 6;
            this.shiftPositionFwdButton.Text = ">>";
            this.shiftPositionFwdButton.UseVisualStyleBackColor = true;
            this.shiftPositionFwdButton.Click += new System.EventHandler(this.shiftPositionFwdButton_Click);
            // 
            // chaptersDGV
            // 
            this.chaptersDGV.AllowUserToAddRows = false;
            this.chaptersDGV.AllowUserToDeleteRows = false;
            this.chaptersDGV.AllowUserToResizeColumns = false;
            this.chaptersDGV.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            this.chaptersDGV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.chaptersDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chaptersDGV.BackgroundColor = System.Drawing.SystemColors.Window;
            this.chaptersDGV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.chaptersDGV.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.chaptersDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.chaptersDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ChapterStatus,
            this.positionCol,
            this.titleCol});
            this.chaptersDGV.Location = new System.Drawing.Point(12, 12);
            this.chaptersDGV.MinimumSize = new System.Drawing.Size(200, 200);
            this.chaptersDGV.MultiSelect = false;
            this.chaptersDGV.Name = "chaptersDGV";
            this.chaptersDGV.RowHeadersVisible = false;
            this.chaptersDGV.RowHeadersWidth = 30;
            this.chaptersDGV.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.chaptersDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.chaptersDGV.Size = new System.Drawing.Size(285, 268);
            this.chaptersDGV.TabIndex = 7;
            this.chaptersDGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnChaptersDgvCellClick);
            this.chaptersDGV.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.chaptersDGV_CellEndEdit);
            this.chaptersDGV.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.chaptersDGV_CellMouseDoubleClick);
            this.chaptersDGV.SelectionChanged += new System.EventHandler(this.OnChaptersDgvSelectionChanged);
            // 
            // ChapterStatus
            // 
            this.ChapterStatus.HeaderText = "";
            this.ChapterStatus.Name = "ChapterStatus";
            this.ChapterStatus.ReadOnly = true;
            this.ChapterStatus.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ChapterStatus.Width = 30;
            // 
            // positionCol
            // 
            this.positionCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.positionCol.DataPropertyName = "TimeCode";
            this.positionCol.HeaderText = "Position";
            this.positionCol.Name = "positionCol";
            this.positionCol.ReadOnly = true;
            this.positionCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.positionCol.Width = 50;
            // 
            // titleCol
            // 
            this.titleCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.titleCol.DataPropertyName = "Title";
            this.titleCol.HeaderText = "Chapter Title";
            this.titleCol.Name = "titleCol";
            this.titleCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.titleArtistStatusLabel,
            this.chaptersCountStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 337);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(309, 24);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // titleArtistStatusLabel
            // 
            this.titleArtistStatusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.titleArtistStatusLabel.Name = "titleArtistStatusLabel";
            this.titleArtistStatusLabel.Size = new System.Drawing.Size(55, 19);
            this.titleArtistStatusLabel.Text = "titleArtist";
            // 
            // chaptersCountStatusLabel
            // 
            this.chaptersCountStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.chaptersCountStatusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.chaptersCountStatusLabel.Name = "chaptersCountStatusLabel";
            this.chaptersCountStatusLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chaptersCountStatusLabel.Size = new System.Drawing.Size(56, 19);
            this.chaptersCountStatusLabel.Text = "chapters";
            // 
            // comboBoxNewChapterName
            // 
            this.comboBoxNewChapterName.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.comboBoxNewChapterName.FormattingEnabled = true;
            this.comboBoxNewChapterName.Items.AddRange(new object[] {
            "<Default>",
            "Intro",
            "Verse",
            "Prechorus",
            "Chorus",
            "Bridge",
            "Solo",
            "Outro"});
            this.comboBoxNewChapterName.Location = new System.Drawing.Point(120, 308);
            this.comboBoxNewChapterName.Name = "comboBoxNewChapterName";
            this.comboBoxNewChapterName.Size = new System.Drawing.Size(85, 21);
            this.comboBoxNewChapterName.TabIndex = 9;
            // 
            // lblNewChapt
            // 
            this.lblNewChapt.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblNewChapt.AutoSize = true;
            this.lblNewChapt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewChapt.Location = new System.Drawing.Point(116, 288);
            this.lblNewChapt.Name = "lblNewChapt";
            this.lblNewChapt.Size = new System.Drawing.Size(92, 13);
            this.lblNewChapt.TabIndex = 10;
            this.lblNewChapt.Text = "New Chapter Title";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 361);
            this.Controls.Add(this.lblNewChapt);
            this.Controls.Add(this.comboBoxNewChapterName);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.chaptersDGV);
            this.Controls.Add(this.shiftPositionFwdButton);
            this.Controls.Add(this.shiftPositionBackButton);
            this.Controls.Add(this.removeChapterButton);
            this.Controls.Add(this.addChapterButton);
            this.MinimumSize = new System.Drawing.Size(325, 350);
            this.Name = "MainForm";
            this.Text = "Chapter List | MB";
            ((System.ComponentModel.ISupportInitialize)(this.chaptersDGV)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addChapterButton;
        private System.Windows.Forms.Button removeChapterButton;
        private System.Windows.Forms.Button shiftPositionBackButton;
        private System.Windows.Forms.Button shiftPositionFwdButton;
        private System.Windows.Forms.DataGridView chaptersDGV;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel titleArtistStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel chaptersCountStatusLabel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox comboBoxNewChapterName;
        private System.Windows.Forms.DataGridViewImageColumn ChapterStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn positionCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn titleCol;
        private System.Windows.Forms.Label lblNewChapt;
    }
}