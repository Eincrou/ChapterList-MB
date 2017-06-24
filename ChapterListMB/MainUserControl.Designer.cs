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
            this.chapterListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.timeCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.titleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chaptersBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chapterListBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.timeCodeDataGridViewTextBoxColumn,
            this.titleDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.chaptersBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(240, 150);
            this.dataGridView1.TabIndex = 0;
            // 
            // chaptersBindingSource
            // 
            this.chaptersBindingSource.DataMember = "Chapters";
            this.chaptersBindingSource.DataSource = this.chapterListBindingSource;
            // 
            // chapterListBindingSource
            // 
            this.chapterListBindingSource.DataSource = typeof(ChapterListMB.ChapterList);
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
            // MainUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Name = "MainUserControl";
            this.Size = new System.Drawing.Size(297, 496);
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
    }
}
