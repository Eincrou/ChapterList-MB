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
            this.addChapterButton = new System.Windows.Forms.Button();
            this.chaptersListBox = new System.Windows.Forms.ListBox();
            this.removeChapterButton = new System.Windows.Forms.Button();
            this.chapterTitleTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addChapterButton
            // 
            this.addChapterButton.Location = new System.Drawing.Point(12, 406);
            this.addChapterButton.Name = "addChapterButton";
            this.addChapterButton.Size = new System.Drawing.Size(145, 76);
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
            this.chaptersListBox.Location = new System.Drawing.Point(13, 13);
            this.chaptersListBox.Name = "chaptersListBox";
            this.chaptersListBox.Size = new System.Drawing.Size(392, 340);
            this.chaptersListBox.TabIndex = 1;
            // 
            // removeChapterButton
            // 
            this.removeChapterButton.Location = new System.Drawing.Point(163, 406);
            this.removeChapterButton.Name = "removeChapterButton";
            this.removeChapterButton.Size = new System.Drawing.Size(145, 76);
            this.removeChapterButton.TabIndex = 2;
            this.removeChapterButton.Text = "Remove Chapter";
            this.removeChapterButton.UseVisualStyleBackColor = true;
            this.removeChapterButton.Click += new System.EventHandler(this.removeChapterButton_Click);
            // 
            // chapterTitleTextBox
            // 
            this.chapterTitleTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chapterTitleTextBox.Location = new System.Drawing.Point(13, 360);
            this.chapterTitleTextBox.Name = "chapterTitleTextBox";
            this.chapterTitleTextBox.Size = new System.Drawing.Size(392, 26);
            this.chapterTitleTextBox.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 494);
            this.Controls.Add(this.chapterTitleTextBox);
            this.Controls.Add(this.removeChapterButton);
            this.Controls.Add(this.chaptersListBox);
            this.Controls.Add(this.addChapterButton);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addChapterButton;
        private System.Windows.Forms.ListBox chaptersListBox;
        private System.Windows.Forms.Button removeChapterButton;
        private System.Windows.Forms.TextBox chapterTitleTextBox;
    }
}