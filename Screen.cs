using board;
using chess;

namespace Chess_Game
{
    internal class Screen
    {
        public static void printMatch(ChessMatch match)
        {
            printBoard(match.board);
            Console.WriteLine();
            printCapturedPieces(match);
            Console.WriteLine();
            Console.WriteLine("\nTurn: " + match.turn);
            if (!match.finished)
            {
                Console.WriteLine("Waiting for move: " + match.CurrentPlayer);
                if (match.check)
                {
                    Console.WriteLine("IN CHECK!");
                }
            }
            else 
            {
                Console.WriteLine("CHECKMATE!");
                Console.WriteLine("WINNER: " + match.CurrentPlayer);
            }
        }

        public static void printCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("Captured Pieces: ");
            Console.Write("White: ");
            printSet(match.capturedPieces(Color.White));
            Console.Write("\nBlack: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            printSet(match.capturedPieces(Color.Black));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void printSet(HashSet<Piece> set)
        {
            Console.Write("[");
                foreach(Piece p in set)
                {
                Console.Write(p + " ");
                }
                Console.Write("]");
        }

        public static void printBoard(Board board)
        {
            for (int i = 0; i < board.line; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.column; j++)
                {
                    printPiece(board.piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h ");
        }

        public static void printBoard(Board board, bool[,] possiblePosition)
        {
            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor changedBackground = ConsoleColor.DarkGray;

            for (int i = 0; i < board.line; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.column; j++)
                {
                    if (possiblePosition[i, j])
                    {
                        Console.BackgroundColor = changedBackground;
                    }
                    else
                    {
                        Console.BackgroundColor = originalBackground;
                    }
                    printPiece(board.piece(i, j));
                    Console.BackgroundColor = originalBackground;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h ");
            Console.BackgroundColor = originalBackground;
        }

        public static ChessPosition readPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int line = int.Parse(s[1] + "");
            return new ChessPosition(column, line);
        }

        public static void printPiece(Piece piece, bool[,] possiblePositions)
        {

            if (piece == null)
                Console.Write("- ");
            else
            {
                if (piece.color == Color.White)
                    Console.Write(piece);
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }

        public static void printPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }else{
                if (piece.color == Color.White) 
                    Console.Write(piece);
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }


    }
}
