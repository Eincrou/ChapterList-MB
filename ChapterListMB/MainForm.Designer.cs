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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.addChapterButton = new System.Windows.Forms.Button();
            this.chaptersListBox = new System.Windows.Forms.ListBox();
            this.removeChapterButton = new System.Windows.Forms.Button();
            this.chapterTitleTextBox = new System.Windows.Forms.TextBox();
            this.artistTitleLabel = new System.Windows.Forms.Label();
            this.shiftPositionBackButton = new System.Windows.Forms.Button();
            this.shiftPositionFwdButton = new System.Windows.Forms.Button();
            this.chaptersDGV = new System.Windows.Forms.DataGridView();
            this.Icon = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.positionCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.titleCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.chaptersDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // addChapterButton
            // 
            this.addChapterButton.Location = new System.Drawing.Point(15, 460);
            this.addChapterButton.Name = "addChapterButton";
            this.addChapterButton.Size = new System.Drawing.Size(91, 34);
            this.addChapterButton.TabIndex = 0;
            this.addChapterButton.Text = "Add Chapter";
            this.addChapterButton.UseVisualStyleBackColor = true;
            this.addChapterButton.Click += new System.EventHandler(this.addChapterButton_Click);
            // 
            // chaptersListBox
            // 
            this.chaptersListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chaptersListBox.FormattingEnabled = true;
            this.chaptersListBox.ItemHeight = 16;
            this.chaptersListBox.Items.AddRange(new object[] {
            "test1",
            "test2",
            "test3"});
            this.chaptersListBox.Location = new System.Drawing.Point(15, 80);
            this.chaptersListBox.Name = "chaptersListBox";
            this.chaptersListBox.Size = new System.Drawing.Size(324, 340);
            this.chaptersListBox.TabIndex = 1;
            // 
            // removeChapterButton
            // 
            this.removeChapterButton.Location = new System.Drawing.Point(112, 460);
            this.removeChapterButton.Name = "removeChapterButton";
            this.removeChapterButton.Size = new System.Drawing.Size(104, 34);
            this.removeChapterButton.TabIndex = 2;
            this.removeChapterButton.Text = "Remove Chapter";
            this.removeChapterButton.UseVisualStyleBackColor = true;
            this.removeChapterButton.Click += new System.EventHandler(this.removeChapterButton_Click);
            // 
            // chapterTitleTextBox
            // 
            this.chapterTitleTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chapterTitleTextBox.Location = new System.Drawing.Point(15, 427);
            this.chapterTitleTextBox.Name = "chapterTitleTextBox";
            this.chapterTitleTextBox.Size = new System.Drawing.Size(324, 26);
            this.chapterTitleTextBox.TabIndex = 3;
            this.chapterTitleTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chapterTitleTextBox_KeyDown);
            // 
            // artistTitleLabel
            // 
            this.artistTitleLabel.AutoSize = true;
            this.artistTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.artistTitleLabel.Location = new System.Drawing.Point(12, 29);
            this.artistTitleLabel.Name = "artistTitleLabel";
            this.artistTitleLabel.Size = new System.Drawing.Size(51, 20);
            this.artistTitleLabel.TabIndex = 4;
            this.artistTitleLabel.Text = "label1";
            // 
            // shiftPositionBackButton
            // 
            this.shiftPositionBackButton.Location = new System.Drawing.Point(253, 459);
            this.shiftPositionBackButton.Name = "shiftPositionBackButton";
            this.shiftPositionBackButton.Size = new System.Drawing.Size(40, 34);
            this.shiftPositionBackButton.TabIndex = 5;
            this.shiftPositionBackButton.Text = "<<";
            this.shiftPositionBackButton.UseVisualStyleBackColor = true;
            this.shiftPositionBackButton.Click += new System.EventHandler(this.shiftPositionBackButton_Click);
            // 
            // shiftPositionFwdButton
            // 
            this.shiftPositionFwdButton.Location = new System.Drawing.Point(299, 460);
            this.shiftPositionFwdButton.Name = "shiftPositionFwdButton";
            this.shiftPositionFwdButton.Size = new System.Drawing.Size(40, 34);
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
            this.chaptersDGV.BackgroundColor = System.Drawing.SystemColors.Window;
            this.chaptersDGV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.chaptersDGV.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.chaptersDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.chaptersDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Icon,
            this.positionCol,
            this.titleCol});
            this.chaptersDGV.Location = new System.Drawing.Point(346, 80);
            this.chaptersDGV.MultiSelect = false;
            this.chaptersDGV.Name = "chaptersDGV";
            this.chaptersDGV.RowHeadersVisible = false;
            this.chaptersDGV.RowHeadersWidth = 30;
            this.chaptersDGV.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.chaptersDGV.Size = new System.Drawing.Size(365, 340);
            this.chaptersDGV.TabIndex = 7;
            this.chaptersDGV.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.chaptersDGV_CellMouseDoubleClick);
            // 
            // Icon
            // 
            this.Icon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Icon.HeaderText = "";
            this.Icon.Name = "Icon";
            this.Icon.ReadOnly = true;
            this.Icon.Width = 30;
            // 
            // positionCol
            // 
            this.positionCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.positionCol.HeaderText = "Position";
            this.positionCol.Name = "positionCol";
            this.positionCol.ReadOnly = true;
            this.positionCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.positionCol.Width = 50;
            // 
            // titleCol
            // 
            this.titleCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.titleCol.HeaderText = "Chapter Title";
            this.titleCol.Name = "titleCol";
            this.titleCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 510);
            this.Controls.Add(this.chaptersDGV);
            this.Controls.Add(this.shiftPositionFwdButton);
            this.Controls.Add(this.shiftPositionBackButton);
            this.Controls.Add(this.artistTitleLabel);
            this.Controls.Add(this.chapterTitleTextBox);
            this.Controls.Add(this.removeChapterButton);
            this.Controls.Add(this.chaptersListBox);
            this.Controls.Add(this.addChapterButton);
            this.Name = "MainForm";
            this.Text = "Chapter List | MB";
            ((System.ComponentModel.ISupportInitialize)(this.chaptersDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addChapterButton;
        private System.Windows.Forms.ListBox chaptersListBox;
        private System.Windows.Forms.Button removeChapterButton;
        private System.Windows.Forms.TextBox chapterTitleTextBox;
        private System.Windows.Forms.Label artistTitleLabel;
        private System.Windows.Forms.Button shiftPositionBackButton;
        private System.Windows.Forms.Button shiftPositionFwdButton;
        private System.Windows.Forms.DataGridView chaptersDGV;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Icon;
        private System.Windows.Forms.DataGridViewTextBoxColumn positionCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn titleCol;
    }
}