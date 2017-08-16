using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterListMB
{
    public class SkinElementColors
    {
        public string ElementName { get; set; }
        public Color ForegroundColor { get; set; }
        public Color BackgroundColor { get; set; }
        public Color BackgroundColor2 { get; set; }
        public Color BorderColor { get; set; }

        public SkinElementColors(string name, string fg, string bg, string bg2, string bdr)
        {
            ElementName = name;
            ForegroundColor = ParseToColor(fg);
            BackgroundColor = ParseToColor(bg);
            BackgroundColor2 = ParseToColor(bg2);
            BorderColor = ParseToColor(bdr);
        }

        private Color ParseToColor(string rgb)
        {
            if (string.IsNullOrEmpty(rgb)) return Color.Empty;
            string[] split = rgb.Split(',');
            return Color.FromArgb(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]));
        }
    }
}
