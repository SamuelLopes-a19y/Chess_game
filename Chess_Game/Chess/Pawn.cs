using board;
using chess;

namespace Chess_Game.Chess
{
    internal class Pawn : Piece
    {
        private ChessMatch match;

        public Pawn(Board board, Color color, ChessMatch match) : base(board, color)
        {
            this.board = board;
            this.match = match;
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
                Position p2 = new Position(position.line - 1, position.column);
                if (board.validPosition(p2) && free(p2) && board.validPosition(pos) && free(p2) && moviments == 0)
                {
                    mat[pos.line, pos.column] = true;
                }              
                  
                pos.defineValues(position.line - 1, position.column - 1);
                if (board.validPosition(pos) && theresEnemy(pos))
                    mat[pos.line, pos.column] = true;

                pos.defineValues(position.line - 1, position.column + 1);
                if (board.validPosition(pos) && theresEnemy(pos))
                    mat[pos.line, pos.column] = true;

                //# Special Play: En Passant
                if (position.line == 3)
                {
                    //LEFT
                    Position left = new Position(position.line, position.column - 1 );
                    if(board.validPosition(left) && theresEnemy(left) && board.piece(left) == match.vulnerableEnPassant) { 
                        mat[left.line - 1, left.column] = true;
                    }
                    //Right
                    Position right = new Position(position.line, position.column + 1);
                    if (board.validPosition(right) && theresEnemy(right) && board.piece(right) == match.vulnerableEnPassant)
                    {
                        mat[right.line - 1, right.column] = true;
                    }
                }
            }
            else
            {
                pos.defineValues(position.line + 1, position.column);
                if (board.validPosition(pos) && free(pos))
                    mat[pos.line, pos.column] = true;

                pos.defineValues(position.line + 2, position.column);
                Position p2 = new Position(position.line + 1, pos.column);
                if (board.validPosition(p2) && free(p2) && board.validPosition(pos) && free(p2) && moviments == 0)
                {
                    mat[pos.line, pos.column] = true;
                }

                pos.defineValues(position.line + 1, position.column - 1);
                if (board.validPosition(pos) && theresEnemy(pos))
                    mat[pos.line, pos.column] = true;

                pos.defineValues(position.line + 1, position.column + 1);
                if (board.validPosition(pos) && theresEnemy(pos))
                    mat[pos.line, pos.column] = true;

                //# Special Play: En Passant
                if (position.line == 4)
                {
                    //LEFT
                    Position left = new Position(position.line, position.column - 1);
                    if (board.validPosition(left) && theresEnemy(left) && board.piece(left) == match.vulnerableEnPassant)
                    {
                        mat[left.line + 1, left.column] = true;
                    }
                    //Right
                    Position right = new Position(position.line, position.column + 1);
                    if (board.validPosition(right) && theresEnemy(right) && board.piece(right) == match.vulnerableEnPassant)
                    {
                        mat[right.line + 1, right.column] = true;
                    }
                }
            }
            return mat;
        }
    }
}
