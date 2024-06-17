using board;

namespace chess
{
    internal class ChessPosition
    {
        public char column { get; set; }
        public int line {  get; set; }
        
        public ChessPosition(char column, int line)
        {
            this.line = line;
            this.column = column;
        }

        public Position toPosition()
        {
            /*A matriz é iniciada de forma contrária ao tabuleiro de 0 a 7
              Para a linha temos (8 - linha) e para coluna temos (char - 'a')
              'a' é equivalente a 97 em inteiro na tabela ASCII*/
            return new Position(8 -  line, column - 'a'); //(1,2)
        }

        public override string ToString()
        {
            return "" + column + line;
        }

    }
}
