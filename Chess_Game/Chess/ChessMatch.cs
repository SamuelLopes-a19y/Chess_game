using board;
using Chess_Game.Chess;

namespace chess
{
    internal class ChessMatch
    { 
        public Board board {  get; private set; }
        public int turn {  get;private set; }
        public Color CurrentPlayer;
        public bool finished { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> captured;
        public bool check { get; private set; }
        public Piece vulnerableEnPassant { get; private set; }

        public ChessMatch()
        {
            board = new Board(8, 8);
            turn = 1;
            CurrentPlayer = Color.White;
            finished = false;
            check = false;
            vulnerableEnPassant = null;
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            putPieces();
        }

        public Piece makeMoviment(Position origin,Position target)
        {
            Piece p = board.removePiece(origin);
            p.addMoviments();
            Piece capturedPiece = board.removePiece(target);
            board.putPiece(p, target);
            if(capturedPiece != null)
            {
                captured.Add(capturedPiece);
            }

            // #Special Play: Small Castling
            if (p is King && target.column == origin.column + 2)
            {
                Position originR = new Position(origin.line, origin.column + 3);
                Position targetR = new Position(origin.line, origin.column + 1);
                Piece R = board.removePiece(originR);
                R.addMoviments();
                board.putPiece(R, targetR);
            }
            // #Special Play: Big Castling
            if (p is King && target.column == origin.column - 2)
            {
                Position originR = new Position(origin.line, origin.column - 4);
                Position targetR = new Position(origin.line, origin.column - 1);
                Piece R = board.removePiece(originR);
                R.addMoviments();
                board.putPiece(R, targetR);
            }

            //# Special Play: En Passant
            if(p is Pawn)
            {
                if(origin.column != target.column && capturedPiece == null)
                {
                    Position posP;
                    if(p.color == Color.White)
                    {
                        posP = new Position(target.line + 1, target.column);
                    }
                    else
                    {
                        posP = new Position(target.line - 1, target.column);
                    }
                    capturedPiece = board.removePiece(posP);
                    captured.Add(capturedPiece);
                }
            }
            return capturedPiece;  
        }

        public void undoMoviment(Position origin, Position target, Piece capturedPiece)
        {
            Piece p = board.removePiece(target);
            p.drecreaseMoviments();
            if(capturedPiece != null)
            {
                board.putPiece(capturedPiece, target);
                captured.Remove(capturedPiece);
            }
            board.putPiece(p, origin);

            // #Special Play: Small Castling
            if (p is King && target.column == origin.column + 2)
            {
                Position originR = new Position(origin.line, origin.column + 3);
                Position targetR = new Position(origin.line, origin.column + 1);
                Piece R = board.removePiece(targetR);
                R.drecreaseMoviments();
                board.putPiece(R, originR);
            }
            // #Special Play: Big Castling
            if (p is King && target.column == origin.column - 2)
            {
                Position originR = new Position(origin.line, origin.column - 4);
                Position targetR = new Position(origin.line, origin.column - 1);
                Piece R = board.removePiece(targetR);
                R.drecreaseMoviments();
                board.putPiece(R, originR);
            }

            //# Special Play: En Passant
            if(p is Pawn)
            {
                if(origin.column != target.column && capturedPiece == vulnerableEnPassant)
                {
                    Piece pawm = board.removePiece(target);
                    Position posP;
                    if (p.color == Color.White)
                    {
                        posP = new Position(3, target.column);
                    }
                    else
                    {
                        posP = new Position(4, target.column);
                    }
                    board.putPiece(pawm, posP);
                }
            }
        }

        public void play(Position origin, Position target)
        {
            Piece capturedPiece = makeMoviment(origin,target);

            if (inCheck(CurrentPlayer))
            {
                undoMoviment(origin, target, capturedPiece);
                throw new BoardException("You can't put yourself in check!");
            }

            Piece p = board.piece(target);

            //# Special Play: Promotion
            if (p is Pawn)
            {
                if((p.color == Color.White && target.line == 0) || (p.color == Color.Black && target.line == 7))
                {
                    p = board.removePiece(target);
                    pieces.Remove(p);
                    //User can choose which piece it wants -  implement after
                    Piece queen = new Queen(board, p.color);
                    board.putPiece(queen, target);
                    pieces.Add(queen);
                } 
            }
                
            if (inCheck(adversary(CurrentPlayer)))
            {
                check = true;
            }
            else
            {
                check = false;
            }
            if (checkMateTest(adversary(CurrentPlayer)))
            {
                finished = true;
            }
            else
            {
                turn++;
                changePlayer();
            }

            

            //# Special Play: En Passant
            if (p is Pawn && (target.line == origin.line - 2 || target.line == origin.line + 2))
            {
                vulnerableEnPassant = p;
            }
            else
            {
                vulnerableEnPassant = null;
            }
        }

        public void validateOriginPosition(Position pos)
        {
            if (board.piece(pos) == null)
            {
                throw new BoardException("Theres's no pience in this origin Position!");
            }
            if (CurrentPlayer != board.piece(pos).color)
            {
                throw new BoardException("The chosen piece isn't yours!");
            }
            if (!board.piece(pos).thereIsPossibleMovimets())
            {
                throw new BoardException("There's no allowed positions for this piece!");
            }
        }

        public void validateTargetPosition(Position origin, Position target)
        {
            if (!board.piece(origin).canMoveTo(target))
                throw new BoardException("invalid destiny position");
        }

        private void changePlayer()
        {
            if(CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }
            else
            {
                CurrentPlayer = Color.White;
            }          
        }

        public HashSet<Piece> capturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach(Piece p in captured)
            {
                if(p.color == color)
                    aux.Add(p);
            }
            return aux;

        }

        public HashSet<Piece> piecesInGame(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece p in pieces)
            {
                if (p.color == color)
                    aux.Add(p);
            }
            aux.ExceptWith(capturedPieces(color));
            return aux;
        }

        private Color adversary(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        private Piece king(Color color)
        {
            foreach (Piece p in piecesInGame(color))
            {
                if(p is King)//To test if a variable of superclass type is a instance of subclass I use "is"
                {
                    return p;
                }
            }
            return null;
        }

        public bool inCheck(Color color)
        {
            Piece K = king(color);
            if (K == null)//Não tem rei
            {
                throw new BoardException("Theres's no " + color + " King on board");
            }
            foreach (Piece p in piecesInGame(adversary(color)))
            {
                bool[,] mat = p.allowedMoves(); 
                if(mat[K.position.line, K.position.column])
                {
                    return true;
                }                       
            }
            return false;
        }

        public bool checkMateTest(Color color)
        {
            if (!inCheck(color))
            {
                return false;
            }
            foreach (Piece p in piecesInGame(color))
            {
                bool[,] mat = p.allowedMoves();
                for(int i = 0; i < board.line; i++)
                {
                    for(int j = 0; j < board.column; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = p.position;
                            Position destiny =  new Position(i,j);
                            Piece capturedPiece = makeMoviment(p.position, destiny);
                            bool checkTest = inCheck(color);
                            undoMoviment(origin, destiny, capturedPiece);
                            if (!checkTest) {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void addNewPiece(char column, int line, Piece p)
        {
            board.putPiece(p, new ChessPosition(column, line).toPosition());
            pieces.Add(p);
        }

        private void putPieces() 
        {
            //WHITE
            addNewPiece('a', 1, new Rook(board, Color.White));
            addNewPiece('b', 1, new Knight(board, Color.White));
            addNewPiece('c', 1, new Bishop(board, Color.White));            
            addNewPiece('d', 1, new Queen(board, Color.White));
            addNewPiece('e', 1, new King(board, Color.White, this));
            addNewPiece('f', 1, new Bishop(board, Color.White));
            addNewPiece('g', 1, new Knight(board, Color.White));
            addNewPiece('h', 1, new Rook(board, Color.White));
            addNewPiece('a', 2, new Pawn(board, Color.White, this));
            addNewPiece('b', 2, new Pawn(board, Color.White, this));
            addNewPiece('c', 2, new Pawn(board, Color.White, this));
            addNewPiece('d', 2, new Pawn(board, Color.White, this));
            addNewPiece('e', 2, new Pawn(board, Color.White, this));
            addNewPiece('f', 2, new Pawn(board, Color.White, this));
            addNewPiece('g', 2, new Pawn(board, Color.White, this));
            addNewPiece('h', 2, new Pawn(board, Color.White, this));


            //BLACK
            addNewPiece('a', 8, new Rook(board, Color.Black));
            addNewPiece('b', 8, new Knight(board, Color.Black));
            addNewPiece('c', 8, new Bishop(board, Color.Black));
            addNewPiece('d', 8, new Queen(board, Color.Black));
            addNewPiece('e', 8, new King(board, Color.Black, this));
            addNewPiece('f', 8, new Bishop(board, Color.Black));
            addNewPiece('g', 8, new Knight(board, Color.Black));
            addNewPiece('h', 8, new Rook(board, Color.Black));
            addNewPiece('a', 7, new Pawn(board, Color.Black, this));
            addNewPiece('b', 7, new Pawn(board, Color.Black, this));
            addNewPiece('c', 7, new Pawn(board, Color.Black, this));
            addNewPiece('d', 7, new Pawn(board, Color.Black, this));
            addNewPiece('e', 7, new Pawn(board, Color.Black, this));
            addNewPiece('f', 7, new Pawn(board, Color.Black, this));
            addNewPiece('g', 7, new Pawn(board, Color.Black, this));
            addNewPiece('h', 7, new Pawn(board, Color.Black, this));
        }
    }
}
