using board;
using chess;
using Chess_Game;

namespace Chess_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessMatch match = new ChessMatch();
                while(!match.finished)
                {
                    try
                    {
                        Console.Clear();
                        Screen.printMatch(match);

                        Console.Write("\nOrigin: ");
                        Position origin = Screen.readPosition().toPosition();
                        match.validateOriginPosition(origin);

                        bool[,] possiblePositions = match.board.piece(origin).allowedMoves();

                        Console.Clear();
                        Screen.printBoard(match.board, possiblePositions);

                        Console.Write("\nDestiny: ");
                        Position target = Screen.readPosition().toPosition();
                        match.validateTargetPosition(origin, target);

                        match.play(origin, target);
                    }catch (BoardException e){
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Screen.printMatch(match);

            }
            catch (BoardException e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}

