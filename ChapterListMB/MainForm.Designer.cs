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
            this.button1 = new System.Windows.Forms.Button();
            this.chaptersListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(451, 335);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(145, 76);
            this.button1.TabIndex = 0;
            this.button1.Text = "Button 1";
            this.button1.UseVisualStyleBackColor = true;
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 423);
            this.Controls.Add(this.chaptersListBox);
            this.Controls.Add(this.button1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox chaptersListBox;
    }
}