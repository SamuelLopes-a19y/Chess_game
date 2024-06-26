﻿using System;
using System.Collections.Generic;
using System.Linq;
using board;


namespace Chess_Game.Chess
{
    internal class Queen : Piece
    {
        public Queen(Board board, Color color) : base(board, color)
        {
            this.board = board;
        }

        public override string ToString()
        {
            return "Q";
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

            //UP
            pos.defineValues(position.line - 1, position.column);
            if (board.validPosition(pos) && canMove(pos))
            {
                while (board.validPosition(pos) && canMove(pos))
                {
                    mat[pos.line, pos.column] = true;
                    if (board.piece(pos) != null && board.piece(pos).color != color)
                        break;
                    pos.line--;
                }
            }
            //RIGHT
            pos.defineValues(position.line, position.column + 1);
            if (board.validPosition(pos) && canMove(pos))
            {

                while (board.validPosition(pos) && canMove(pos))
                {
                    mat[pos.line, pos.column] = true;
                    if (board.piece(pos) != null && board.piece(pos).color != color)
                        break;
                    pos.column++;
                }
            }
            //DOWN
            pos.defineValues(position.line + 1, position.column);
            if (board.validPosition(pos) && canMove(pos))
            {
                while (board.validPosition(pos) && canMove(pos))
                {
                    mat[pos.line, pos.column] = true;
                    if (board.piece(pos) != null && board.piece(pos).color != color)
                        break;
                    pos.line++;
                }
            }
            //LEFT
            pos.defineValues(position.line, position.column - 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                while (board.validPosition(pos) && canMove(pos))
                {
                    mat[pos.line, pos.column] = true;
                    if (board.piece(pos) != null && board.piece(pos).color != color)
                        break;
                    pos.column--;
                }
            }
            //NO
            pos.defineValues(position.line - 1, position.column - 1);
            while (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                    break;
                pos.defineValues(pos.line - 1, pos.column - 1);
            }
            //NE
            pos.defineValues(position.line - 1, position.column + 1);
            while (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                    break;
                pos.defineValues(pos.line - 1, pos.column + 1);
            }
            //SE
            pos.defineValues(position.line + 1, position.column + 1);
            while (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                    break;
                pos.defineValues(pos.line + 1, pos.column + 1);
            }
            //SO
            pos.defineValues(position.line + 1, position.column - 1);
            while (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                    break;
                pos.defineValues(pos.line + 1, pos.column - 1);
            }
            return mat;
        }
    }
}
