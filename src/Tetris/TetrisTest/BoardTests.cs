using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Tetris.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class BoardTests
    {


        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion


        /* TODO:
         * > Al principio el tablero no tiene pieza actual
         * > El tablero permite poner una pieza
         * > El tablero da informacion sobre la posicion de la pieza actual
         * > Si se avanza el tablero y no existe pieza actual no tira error
         * > Al hacer progresar el tablero baja la pieza actual desde la posición 0 a la posición 1
         * > Al hacer progresar el tablero baja la pieza actual de a uno
         * > La pieza avanza hasta un limite establecido
         * > Al apoyarse la pieza, la pieza se separa en sus bloques y pasan a formar parte del tablero
         * ¡4 horas hasta acá!
         * > Cuando se avanza el tablero comprueba si hay línea y en ese caso borra los bloques de la misma
         * > Al mover la pieza se mueven los bloques que contiene
         * > el tablero permite 2 dimensiones
         * > probar que hace lineas multiples
         * probar que luego de hacer lineas bajan los blques
         * piezas de mas de un bloque
         * probar que una pieza se pueda apoyar, no solo en el fondo, sino tambien sobre un bloque
         * No se puede agregar una pieza si hay una pieza actual
         * El tablero pierde al llegar a la cima
         * El tablero gana con el alcance de un objetivo
         * El tablero tiene en cuenta las colisiones contra bloques al moverse lateralmente y al rotar.
         * Rotar?
         * Puntaje?
         */

        [TestMethod]
        public void BoardExists()
        {
            var board = new Board();
        }

        [TestMethod]
        public void AtFirstBoardHasNotCurrentPiece()
        {
            var board = new Board();
            Assert.IsNull(board.CurrentPiece);
        }


        [TestMethod]
        public void WhenAddingPieceCurrentPieceExists()
        {
            var board = new Board();
            board.AddNewPiece();
            Assert.IsNotNull(board.CurrentPiece);
        }

        [TestMethod]
        public void WhenPuttingAPieceItTheCurrentPiecePlacesAtZeroHeight()
        {
            var board = new Board();
            board.AddNewPiece();
            Assert.AreEqual(0, board.CurrentPiece.Y);
        }

        [TestMethod]
        public void WhenBoardFirstAdvancesCurrentPieceGoesDown()
        {
            var board = new Board();
            board.AddNewPiece();
            board.Advance();
            Assert.AreEqual(1, board.CurrentPiece.Y);
        }

        [TestMethod]
        public void WhenBoardAdvancesCurrentPieceGoesDown()
        {
            var board = new Board(30);
            board.AddNewPiece();
            for (int i = 0; i <= 20; i++)
            {
                Assert.AreEqual(i, board.CurrentPiece.Y);
                board.Advance();
            }
            Assert.AreEqual(21, board.CurrentPiece.Y);
        }

        [TestMethod]
        public void WhenDoesNotExistsCurrentPieceAndBoardAdvancesNothingOcurrs()
        {
            var board = new Board();
            board.Advance();
        }

        [TestMethod]
        public void CurrentPieceAdvancesUntilBottom()
        {
            var board = new Board(3);
            board.AddNewPiece(); //0
            board.Advance(); //1
            board.Advance(); //2
            Assert.AreEqual(2, board.CurrentPiece.Y);
            board.Advance();
            Assert.IsNull(board.CurrentPiece);
        }

        [TestMethod]
        public void DefaultConstructorCreatesABoardWithAHeightOf10()
        {
            var board = new Board();
            Assert.AreEqual(10, board.Height);
        }


        [TestMethod]
        public void WhenPieceReachesBottomItsBlocksBecamePartOfTheBoard()
        {
            var board = new Board(3,2);
            board.AddNewPiece();
            var block = board.CurrentPiece.GetBlocks().First();

            board.Advance();
            board.Advance();
            board.Advance();//no deberia hacer linea

            Assert.IsTrue(board.Contains(block));
        }

        [TestMethod]
        public void WhenAdvancesPieceBlocksAlsoAdvance()
        {
            var board = new Board();
            board.AddNewPiece();
            Assert.AreEqual(0, board.CurrentPiece.GetBlocks().First().Y);
            board.Advance();
            Assert.AreEqual(1, board.CurrentPiece.GetBlocks().First().Y);
        }

        //Al apoyarse la pieza, el tablero comprueba si hay línea y en ese caso borra los bloques de la misma
        [TestMethod]
        public void WhenCurrentPieceReachesBottomVerifyesIfThereIsLine()
        {
            var board = new Board(3, 2);
            board.AddBlock(1, 2);
            board.AddNewPiece(); //0
            board.Advance(); //1
            board.Advance(); //2
            board.Advance(); //La pieza se apoyó.
            Assert.AreEqual(0, board.GetBlocks().Count());
        }


        ////Al apoyarse la pieza, el tablero comprueba si hay línea y en ese caso borra los bloques de la misma
        [TestMethod]
        public void BoardDetectsMultipleLines()
        {
            var board = new Board(3, 2);
            board.AddBlock(0, 2);
            board.AddBlock(1, 2);
            board.AddBlock(1, 1);
            board.AddBlock(0, 1);
            //BB
            //BB
            Assert.AreEqual(4, board.GetBlocks().Count());
            board.Advance();
            Assert.AreEqual(0, board.GetBlocks().Count());
        }

        //Al apoyarse la pieza, el tablero comprueba si hay línea y en ese caso borra los bloques de la misma
        [TestMethod]
        public void BoardDetectsBottomLinesEvenIfNoCurrentPieceLeans()
        {
            var board = new Board(3, 2);
            board.AddNewPiece();
            board.AddBlock(1, 2);
            board.AddBlock(0, 2);
            //BB
            //BB
            Assert.AreEqual(2, board.GetBlocks().Count());
            board.Advance();
            Assert.AreEqual(0, board.GetBlocks().Count());
        }

        [TestMethod]
        public void BoardDetectsBottomLinesEvenIfNoCurrentPieceExists()
        {
            var board = new Board(3, 2);
            board.AddBlock(1, 2);
            board.AddBlock(0, 2);
            //BB
            //BB
            Assert.AreEqual(2, board.GetBlocks().Count());
            board.Advance();
            Assert.AreEqual(0, board.GetBlocks().Count());
        }


        [TestMethod]
        public void WhenFullLinesIsDetectedOnlyFullLineBlocksAreRemoved()
        {
            var board = new Board(4, 2);
            board.AddBlock(1, 1);
            board.AddBlock(0, 1);
            board.AddBlock(1, 2);
            board.AddBlock(1, 3);
            board.AddBlock(0, 3);
            //BB
            // B
            //BB
            Assert.AreEqual(5, board.GetBlocks().Count());
            board.Advance();
            //
            // B
            //
            Assert.AreEqual(1, board.GetBlocks().Count());
        }


        [TestMethod]
        public void WhenFullLinesIsDetectedBlocksAreMovedDown()
        {
            var board = new Board(4, 2);
            var block1 = board.AddBlock(0, 0);
            board.AddBlock(1, 1);
            board.AddBlock(0, 1);
            var block2 = board.AddBlock(1, 2);
            board.AddBlock(1, 3);
            board.AddBlock(0, 3);
            //B
            //BB
            // B
            //BB
            Assert.AreEqual(6, board.GetBlocks().Count());
            board.Advance();
            //
            //
            //B
            // B
            Assert.AreEqual(2, board.GetBlocks().Count());
            Assert.AreEqual(0, block1.X);
            Assert.AreEqual(2, block1.Y);
            Assert.AreEqual(1, block2.X);
            Assert.AreEqual(3, block2.Y);
        }

        /*
        [TestMethod]
        public void xx()
        {
            var a = new Block();
            var b = xxxx(a);
            Assert.IsTrue(a == b);
        }

        public Block xxxx (Block block)
        {
            return block;
        }
          */
    }
}

