
namespace board
{
    internal abstract class Piece
    {
        public Position position {get;set; }
        public Color color { get;set; }
        public int moviments {get; protected set; }
        public Board board { get; protected set; }

        public Piece(Board board, Color color)
        {
            this.position = null;
            this.color = color;
            this.board = board;
            this.moviments = 0;
        } 

        public void addMoviments()
        {
            moviments++;
        }

        public void drecreaseMoviments()
        {
            moviments--;
        }

        public bool thereIsPossibleMovimets()
        {
            bool[,] mat = allowedMoves();
            for(int i = 0; i < board.line; i++)
            {
                for(int j = 0; j < board.column; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool canMoveTo(Position pos)
        {
            return allowedMoves()[pos.line, pos.column];
        }

        //Abstract classes can only be extends, can be alterated just in other classes 
        public abstract bool[,] allowedMoves();
    
    }
}
