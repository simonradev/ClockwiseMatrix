namespace ClockwiseMatrix
{
    public class Position
    {
        private static readonly Position[] ClockwiseDirections = new Position[]
        {
            new Position(-1, 0),  //up
            new Position(-1, +1), //up and right diagonal
            new Position(0, +1),  //right
            new Position(+1, +1), //down and right diagonal
            new Position(+1, 0),  //down
            new Position(+1, -1), //down and left diagonal
            new Position(0, -1),  //left
            new Position(-1, -1)  //up and left diagonal
        };
        
        private int row;
        private int col;

        public Position(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

        public int Row
        {
            get { return this.row; }
            set { this.row = value; }
        }

        public int Col
        {
            get { return this.col; }
            set { this.col = value; }
        }

        public static Position GetNewPosition(int index)
        {
            Position toReturn = new Position(ClockwiseDirections[index].row, ClockwiseDirections[index].col);

            return toReturn;
        }

        public static Position GetNewPosition(ClockwiseDirection direcion)
        {
            return ClockwiseDirections[(int)direcion];
        }
    }
}
