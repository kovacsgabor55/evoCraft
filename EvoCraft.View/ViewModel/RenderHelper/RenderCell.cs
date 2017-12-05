namespace View
{
    public class RenderCell
    {
        public RenderCell(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int row;
        public int column;

        public int Row
        {
            get { return row; }
            set
            {
                row = value;
            }
        }

        public int Column
        {
            get { return column; }
            set
            {
                column = value;
            }
        }
    }
}
