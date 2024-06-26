﻿using board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess
{
    internal class Rook : Piece
    {
        public Rook(Board board, Color color) : base(board, color) { }

        public override string ToString()
        {
            return "R";
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
            return mat;
        }
    }
}
