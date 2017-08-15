namespace ChapterListMB
{
    partial class MainUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.chaptersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnAddChapter = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.timeCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.titleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chapterListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chaptersBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chapterListBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.timeCodeDataGridViewTextBoxColumn,
            this.titleDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.chaptersBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(291, 235);
            this.dataGridView1.TabIndex = 0;
            // 
            // chaptersBindingSource
            // 
            this.chaptersBindingSource.DataSource = this.chapterListBindingSource;
            // 
            // btnAddChapter
            // 
            this.btnAddChapter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddChapter.Location = new System.Drawing.Point(4, 244);
            this.btnAddChapter.Name = "btnAddChapter";
            this.btnAddChapter.Size = new System.Drawing.Size(30, 30);
            this.btnAddChapter.TabIndex = 1;
            this.btnAddChapter.Text = "+";
            this.btnAddChapter.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(40, 244);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(30, 30);
            this.button1.TabIndex = 1;
            this.button1.Text = "-";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // timeCodeDataGridViewTextBoxColumn
            // 
            this.timeCodeDataGridViewTextBoxColumn.DataPropertyName = "TimeCode";
            this.timeCodeDataGridViewTextBoxColumn.HeaderText = "Time";
            this.timeCodeDataGridViewTextBoxColumn.Name = "timeCodeDataGridViewTextBoxColumn";
            this.timeCodeDataGridViewTextBoxColumn.ReadOnly = true;
            this.timeCodeDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.timeCodeDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.timeCodeDataGridViewTextBoxColumn.Width = 60;
            // 
            // titleDataGridViewTextBoxColumn
            // 
            this.titleDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.titleDataGridViewTextBoxColumn.DataPropertyName = "Title";
            this.titleDataGridViewTextBoxColumn.HeaderText = "Chapter Title";
            this.titleDataGridViewTextBoxColumn.Name = "titleDataGridViewTextBoxColumn";
            // 
            // chapterListBindingSource
            // 
            this.chapterListBindingSource.DataSource = typeof(ChapterListMB.ChapterList);
            // 
            // MainUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnAddChapter);
            this.Controls.Add(this.dataGridView1);
            this.Name = "MainUserControl";
            this.Size = new System.Drawing.Size(297, 279);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chaptersBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chapterListBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource chapterListBindingSource;
        private System.Windows.Forms.BindingSource chaptersBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn titleDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnAddChapter;
        private System.Windows.Forms.Button button1;
    }
}
