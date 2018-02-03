using EvoCraft.Core;

namespace View
{
    public class RenderHelper
    {
        public static RenderHelper instance;
        public static RenderCell leftTopCorner;
        public static RenderSize size;

        /// <summary>
        /// 1.Parameter is the top left corner of the render 
        /// 2.parameter is the render size
        /// </summary>
        /// <param name="renderLeftTopCorner"></param>
        /// <param name="renderSize"></param>
        public RenderHelper(RenderCell renderLeftTopCorner, RenderSize renderSize)
        {
            leftTopCorner = renderLeftTopCorner;
            size = renderSize;
        }

        public static RenderHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RenderHelper(new RenderCell(0, 0), new RenderSize(30, 20));
                }
                return instance;
            }
        }


        public RenderCell LeftTopCorner
        {
            get
            {
                if (leftTopCorner == null)
                {
                    leftTopCorner = new RenderCell(0, 0);
                }
                return leftTopCorner;
            }
            set
            {
                if ((leftTopCorner.Row + value.Row >= 0) && (leftTopCorner.Column + value.Column >= 0) && ( leftTopCorner.Column + value.Column + Size.Width <= Engine.Map.Width) && ( leftTopCorner.Row + value.Row + Size.Height <= Engine.Map.Height))
                {
                    leftTopCorner.Column += value.Column;
                    leftTopCorner.Row += value.Row;
                }
            }
        }

        public RenderSize Size
        {
            get
            {
                if (size == null)
                {
                    size = new RenderSize(30, 20);
                }
                return size;
            }
            set
            {
                if ((value.Width < Engine.Map.Width) && (value.Height < Engine.Map.Height))
                {
                    size = value;
                }
            }
        }

        public void MoveUp()
        {
            LeftTopCorner = new RenderCell(-1, 0);
        }

        public void MoveDown()
        {
            LeftTopCorner = new RenderCell(1, 0);
        }

        public void MoveLeft()
        {
            LeftTopCorner = new RenderCell(0, -1);
        }

        public void MoveRight()
        {
            LeftTopCorner = new RenderCell(0, 1);
        }
    }
}