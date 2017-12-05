namespace View
{
    public class RenderSize
    {
        public RenderSize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int width;
        public int height;

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }
    }
}
