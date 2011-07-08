using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    public class Board
    {
        private Piece currentPiece;
        public Piece CurrentPiece
        {
            get { return currentPiece; }
            set { currentPiece = value; }
        }

        public void AddNewPiece()
        {
            currentPiece = new Piece();
        }

        public void Advance()
        {
            currentPiece.Height = 1;
        }
    }

    public class Piece
    {
        int height;
        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }
    }
}
