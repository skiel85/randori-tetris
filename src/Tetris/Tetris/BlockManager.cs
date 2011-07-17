using System;
using System.Collections.Generic;
using System.Linq;

namespace Tetris
{
    public class BlockManager
    {
        private readonly List<Block> _blocks = new List<Block>();

        public void Add(Block block)
        {
            _blocks.Add(block);
        }

        public IEnumerable<Block> GetBlocksAtRow(int currentRow)
        {
            return _blocks.Where(IsAtRow(currentRow));
        }

        public void RemoveLine(int currentRow)
        {
            _blocks.Where(IsAtRow(currentRow)).ToArray().Each(RemoveBlock);
        }

        public void MoveUpperRowsDown(int currentRow)
        {
            _blocks.Where(IsUpperTo(currentRow)).Each(MoveBlockDown());
        }

        public int CountBlocksAtRow(int currentRow)
        {
            return GetBlocksAtRow(currentRow).Count();
        }

        public void RemoveBlock(Block b)
        {
            _blocks.Remove(b);
        }

        public IEnumerable<Block> GetBlocks()
        {
            return _blocks.AsEnumerable();
        }

        public bool Contains(Block block)
        {
            return GetBlocks().Any(block.Equals);
        }


        public Block AddBlock(int x, int y)
        {
            var block = new Block(x, y);
            _blocks.Add(block);
            return block;
        }

        private static Func<Block, bool> IsAtRow(int currentRow)
        {
            return eachBlock => eachBlock.Y == currentRow;
        }

        private static Action<Block> MoveBlockDown()
        {
            return b => b.Y++;
        }

        private static Func<Block, bool> IsUpperTo(int currentRow)
        {
            return eachBlock => eachBlock.Y < currentRow;
        }

        public bool Collides(Piece currentPiece)
        {
            //return
            //    _blocks.Where(
            //        eachBlock => IsInmediatelyAboveOf(currentPiece, eachBlock) && IsHorizontalAligned(currentPiece, eachBlock)).
            //        Count() > 0;
            foreach (var boardBlock in _blocks)
            {
                foreach (var pieceBlock in currentPiece.GetBlocks())
                {
                    if (IsHorizontalAligned(boardBlock, pieceBlock) && IsAboveOf(boardBlock, pieceBlock))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool IsAboveOf(Block boardBlock, Block pieceBlock)
        {
            return pieceBlock.Y + 1 == boardBlock.Y;
        }

        //private static bool IsHorizontalAligned(Piece currentPiece, Block eachBlock)
        //{
        //    Block first = currentPiece.GetBlocks().First();
        //    return IsHorizontalAligned(eachBlock, first);
        //}

        private static bool IsHorizontalAligned(Block one, Block other)
        {
            return one.X == other.X;
        }

        //private static bool IsInmediatelyAboveOf(Piece currentPiece, Block eachBlock)
        //{
        //    Block first = currentPiece.GetBlocks().First();
        //    return IsInmediatelyAboveOf(eachBlock, first);
        //}

        private static bool IsInmediatelyAboveOf(Block one, Block aboveOf)
        {
            return aboveOf.Y == one.Y + 1;
        }
    }
}