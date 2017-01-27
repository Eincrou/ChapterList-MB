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
        public delegate void UpdateForm(List<Chapter> chapters);
        
        public UpdateForm UpdateFormDelegate;

        private List<Chapter> _chapters;
        public MainForm()
        {
            InitializeComponent();
            UpdateFormDelegate = UpdateFormMethod;
            chaptersListBox.MouseDoubleClick += OnChapterDoubleClick;
            addChapterButton.Click += OnAddChapterButtonClicked;
            removeChapterButton.Click += OnRemoveChapterButtonClicked;
        }

        private void OnChapterDoubleClick(object sender, MouseEventArgs e)
        {
            OnSelectedItemDoubleClickedRouted(((ListBox) sender).SelectedItem as Chapter);
        }
        private void OnAddChapterButtonClicked(object sender, EventArgs e)
        {
            OnAddChapterButtonClickedRouted(chapterTitleTextBox.Text);
        }
        private void OnRemoveChapterButtonClicked(object sender, EventArgs e)
        {
            OnRemoveChapterButtonClickedRouted((Chapter)chaptersListBox.SelectedItem);
        }
        public void UpdateFormMethod(List<Chapter> chapters)
        {
            //chaptersListBox.DisplayMember = "Title";
            chaptersListBox.DataSource = chapters;
           // chaptersListBox.
            
            //UpdateChaptersListBox(chapters);
        }


        private void addChapterButton_Click(object sender, EventArgs e)
        {
            

        }
        
        private void removeChapterButton_Click(object sender, EventArgs e)
        {
            if (chaptersListBox.SelectedIndex < 0) MessageBox.Show("Please select a chapter to remove.");

        }

        public event EventHandler<Chapter> SelectedItemDoubleClickedRouted;
        protected virtual void OnSelectedItemDoubleClickedRouted(Chapter e)
        {
            SelectedItemDoubleClickedRouted?.Invoke(this, e);
        }

        public event EventHandler<string> AddChapterButtonClickedRouted;

        protected virtual void OnAddChapterButtonClickedRouted(string chapterTitle)
        {
            AddChapterButtonClickedRouted?.Invoke(this, chapterTitle);
        }
        public event EventHandler<Chapter> RemoveChapterButtonClickedRouted;
        protected virtual void OnRemoveChapterButtonClickedRouted(Chapter e)
        {
            RemoveChapterButtonClickedRouted?.Invoke(this, e);
        }
    }
}
