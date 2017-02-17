namespace ChapterListMB
{
    public class RepeatChaptersEventArgs
    {
        public Chapter ChapterA;
        public Chapter ChapterB;
        public RepeatChaptersEventArgs(Chapter a, Chapter b)
        {
            ChapterA = a;
            ChapterB = b;
        }
    }
}