using board;

namespace chess
{
    internal class King : Piece
    {
        private ChessMatch match;

        public King(Board board, Color color, ChessMatch match) : base(board, color) { 
            this.board = board;
            this.match = match;
        }

        public override string ToString()
        {
            return "K";
        }

        private bool canMove(Position pos)
        {
            Piece p = board.piece(pos);
            return p == null || p.color != color;
        }

        private bool testingRookForCastling(Position pos)
        {
            Piece p = board.piece(pos);
            return p != null && p is Rook && p.color == color && p.moviments == 0;
        }

        public override bool[,] allowedMoves()
        {
            bool[,] mat = new bool[board.line, board.column];

            Position pos = new Position(0,0);

            //UP
            pos.defineValues(position.line - 1, position.column);
            if (board.validPosition(pos) && canMove(pos))
                mat[pos.line, pos.column] = true;
            //NE
            pos.defineValues(position.line - 1, position.column + 1);
            if (board.validPosition(pos) && canMove(pos))
                mat[pos.line, pos.column] = true;
            //RIGHT
            pos.defineValues(position.line, position.column + 1);
            if (board.validPosition(pos) && canMove(pos))
                mat[pos.line, pos.column] = true;
            //SE
            pos.defineValues(position.line + 1, position.column + 1);
            if (board.validPosition(pos) && canMove(pos))
                mat[pos.line, pos.column] = true;
            //DOWN
            pos.defineValues(position.line + 1, position.column);
            if (board.validPosition(pos) && canMove(pos))
                mat[pos.line, pos.column] = true;
            //SO
            pos.defineValues(position.line + 1, position.column - 1);
            if (board.validPosition(pos) && canMove(pos))
                mat[pos.line, pos.column] = true;
            //LEFT
            pos.defineValues(position.line, position.column - 1);
            if (board.validPosition(pos) && canMove(pos))
                mat[pos.line, pos.column] = true;
            //NO
            pos.defineValues(position.line - 1, position.column - 1);
            if (board.validPosition(pos) && canMove(pos))
                mat[pos.line, pos.column] = true;

            // #Special Play: Small castling
            if(moviments == 0 && !match.check)
            {
                //Testing Rook
                Position posR1 =  new Position(position.line, position.column + 3);
                if (testingRookForCastling(posR1)) 
                {
                    Position p1 = new Position(position.line, position.column + 1);
                    Position p2 = new Position(position.line, position.column + 2);
                    if(board.piece(p1) == null && board.piece(p2) == null)
                    {
                        mat[position.line, position.column + 2] =  true;
                    }
                }
            }

            // #Special Play: Big castling
            if (moviments == 0 && !match.check)
            {
                //Testing Rook
                Position posR2 = new Position(position.line, position.column - 4);
                if (testingRookForCastling(posR2))
                {
                    Position p1 = new Position(position.line, position.column - 1);
                    Position p2 = new Position(position.line, position.column - 2);
                    Position p3 = new Position(position.line, position.column - 3);
                    if (board.piece(p1) == null && board.piece(p2) == null && board.piece(p3) == null)
                    {
                        mat[position.line, position.column - 2] = true;
                    }
                }
            }
            return mat;
        }
    }
}
