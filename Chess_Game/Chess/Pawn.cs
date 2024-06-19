using board;

namespace Chess_Game.Chess
{
    internal class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(board, color)
        {
            this.board = board;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool theresEnemy(Position pos) 
        {   
            Piece p = board.piece(pos);
            return p != null && p.color != color;
        }

        private bool free(Position pos)
        { 
            return board.piece(pos) == null;
        }

        private bool canMove(Position pos)
        {
            Piece p = board.piece(pos);
            return p == null || p.color != color;
        }

        public override bool[,] allowedMoves()
        {
            bool[,] mat = new bool[board.line, board.column];

            Position pos = new Position(0, 0);

            if (color == Color.White)
            {
                pos.defineValues(position.line - 1, position.column);
                if (board.validPosition(pos) && free(pos))
                    mat[pos.line, pos.column] = true;

                pos.defineValues(position.line - 2, position.column);
                if (board.validPosition(pos) && free(pos) && moviments == 0)
                    mat[pos.line, pos.column] = true;

                pos.defineValues(position.line - 1, position.column - 1);
                if (board.validPosition(pos) && theresEnemy(pos))
                    mat[pos.line, pos.column] = true;

                pos.defineValues(position.line - 1, position.column + 1);
                if (board.validPosition(pos) && theresEnemy(pos))
                    mat[pos.line, pos.column] = true;
            }
            else
            {
                pos.defineValues(position.line + 1, position.column);
                if (board.validPosition(pos) && free(pos))
                    mat[pos.line, pos.column] = true;

                pos.defineValues(position.line + 2, position.column);
                if (board.validPosition(pos) && free(pos) && moviments == 0)
                    mat[pos.line, pos.column] = true;

                pos.defineValues(position.line + 1, position.column - 1);
                if (board.validPosition(pos) && theresEnemy(pos))
                    mat[pos.line, pos.column] = true;

                pos.defineValues(position.line + 1, position.column + 1);
                if (board.validPosition(pos) && theresEnemy(pos))
                    mat[pos.line, pos.column] = true;
            }
            return mat;
        }
    }
}
