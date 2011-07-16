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

        public IEnumerable<Block> GetBlocks()
        {
            return _blocks;
        }

        public void MoveDown()
        {
            foreach (var block in _blocks)
            {
                block.Y++;
            }
        }
    }
}
