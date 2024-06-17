using board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess
{
    internal class King : Piece
    {
        public King(Board board, Color color) : base(board, color) { 
            this.board = board;
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
            return mat;
        }
    }
}
