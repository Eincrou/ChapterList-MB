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
            this.chaptersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.chapterListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chaptersBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chapterListBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chaptersBindingSource
            // 
            this.chaptersBindingSource.DataSource = this.chapterListBindingSource;
            // 
            // chapterListBindingSource
            // 
            this.chapterListBindingSource.DataSource = typeof(ChapterListMB.ChapterList);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(342, 324);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Image = global::ChapterListMB.Properties.Resources.ChapterAdd;
            this.button1.Location = new System.Drawing.Point(3, 283);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(38, 38);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // MainUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "MainUserControl";
            this.Size = new System.Drawing.Size(349, 331);
            ((System.ComponentModel.ISupportInitialize)(this.chaptersBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chapterListBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource chapterListBindingSource;
        private System.Windows.Forms.BindingSource chaptersBindingSource;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
    }
}
