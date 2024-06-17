
namespace board
{
    internal class Board
    {
        public int line { get; set; }
        public int column { get; set; }
        private Piece[,] pieces;

        public Board(int line, int column)
        {
            this.line = line;
            this.column = column;
            pieces = new Piece[line, column]; //cordinate
        }

        public Piece piece(int line, int column)
        {
            return pieces[line, column];
        }

        public Piece piece(Position pos)
         {
            return pieces[pos.line, pos.column];
        }

        public bool pieceExists(Position pos)
        {
            validatingPiece(pos);
            return piece(pos) != null;
        }

        public void putPiece(Piece p, Position pos)
        {
            if (pieceExists(pos))
            {
                throw new BoardException("There's already a piece in that position");
            }
            pieces[pos.line, pos.column] = p;
            p.position = pos;
        }

        public Piece removePiece(Position pos)
        {
            if (piece(pos) == null)
                return null;
            Piece aux = piece(pos);
            aux.position = null;
            pieces[pos.line, pos.column] = null;
            return aux;
        }

        public bool validPosition(Position pos)
        {
            if (pos.line < 0 || pos.line >= line || pos.column < 0 || pos.column >= column)
                return false;
            return true;
        }

        public void validatingPiece(Position pos)
        {
            if (!validPosition(pos))
                throw new BoardException("Invalid Position!");
        }
    }
}
