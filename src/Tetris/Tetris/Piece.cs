using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Tetris
{
    public class Piece
    {
        public int Y
        {
            get
            {
                return _blocks.First().Y;
            }
        }

        private readonly IList<Block> _blocks;

        public Piece()
        {
            _blocks = new List<Block> { new Block() };
        }

        public Piece(IEnumerable<Block> blocks)
        {
            _blocks = new List<Block>(blocks);
        }

        public IEnumerable<Block> GetBlocks()
        {
            return _blocks;
        }

        public void MoveDown()
        {
            _blocks.Each(MoveBlockDown);
        }

        private static void MoveBlockDown(Block b)
        {
            b.Y++;
        }
    }
}
