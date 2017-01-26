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
        }

        private void OnChapterDoubleClick(object sender, MouseEventArgs e)
        {
            OnSelectedItemDoubleClickedRouted(((ListBox) sender).SelectedItem as Chapter);
        }

        public void UpdateFormMethod(List<Chapter> chapters)
        {
            //chaptersListBox.DisplayMember = "Title";
            chaptersListBox.DataSource = chapters;
           // chaptersListBox.
            
            //UpdateChaptersListBox(chapters);
        }
        public event EventHandler<Chapter> SelectedItemDoubleClickedRouted;
        protected virtual void OnSelectedItemDoubleClickedRouted(Chapter e)
        {
            SelectedItemDoubleClickedRouted?.Invoke(this, e);
        }
    }
}
