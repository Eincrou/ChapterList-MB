using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterListMB
{
    public class Chapter
    {
        public string Name { get; private set; }
        public int Position { get; private set; }

        public Chapter(string name, int position)
        {
            Name = name;
            Position = position;
        }
    }
}
