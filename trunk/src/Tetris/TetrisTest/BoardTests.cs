using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
         * > probar que luego de hacer lineas bajan los blques
         * > probar que una pieza se pueda apoyar, no solo en el fondo, sino tambien sobre un bloque
         * > piezas de mas de un bloque
         * > Se puede obtener el bloque que ocupa la celda actual del tablero.
         * Se puede cargar el tablero a partir de un string.
         * Se puede guardar el tablero en un string.
         * No se puede agregar una pieza si hay una pieza actual
         * El tablero pierde al llegar a la cima
         * El tablero gana con el alcance de un objetivo en cantidad de líneas
         * El tablero tiene en cuenta las colisiones contra bloques al moverse lateralmente
         * El tablero tiene en cuenta las colisiones contra bloques al rotar.
         * Las piezas se pueden rotar en 4 orientaciones.
         * Al hacer una línea aumenta el contador de líneas (puntaje).
         * Las piezas se pueden mover lateralmente.
         * Las piezas se pueden mover hacia abajo sin necesidad de avanzar el tablero.
         * Las piezas se pueden soltar para que caigan hasta el límite.
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
            var board = new Board(2, 3);
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
            var board = new Board(2, 3);
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
            var board = new Board(2, 3);
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
            var board = new Board(2, 3);
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
            var board = new Board(2, 3);
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
            var board = new Board(2, 4);
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
            var board = new Board(2, 4);
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


        [TestMethod]
        public void PiecesAreLeanedAlsoOnIsolatedBlocks()
        {
            var board = new Board(2, 4);
            //
            //
            //B
            //
            var isolated = board.AddBlock(0, 2);
            board.AddNewPiece();
            var pieceBlock = board.CurrentPiece.GetBlocks().Single();
            board.Advance();
            board.Advance();
            Assert.AreEqual(2, board.GetBlocks().Count());
            Assert.IsTrue(board.GetBlocks().Contains(isolated));
            Assert.IsTrue(board.GetBlocks().Contains(pieceBlock));

            Assert.AreEqual(2, isolated.Y);
            Assert.AreEqual(0, isolated.X);

            Assert.AreEqual(1, pieceBlock.Y);
            Assert.AreEqual(0, pieceBlock.X);
        }




        [TestMethod]
        public void WhenAdvancesPieceBlocksAlsoAdvanceEvenForComplexPieces()
        {
            var board = new Board(4, 5);
            var block1 = new Block(2, 0);
            var block2 = new Block(3, 1);
            board.AddNewPiece(new[] { block1, block2 });
            board.Advance();
            Assert.AreEqual(2, block1.X);
            Assert.AreEqual(1, block1.Y);
            Assert.AreEqual(3, block2.X);
            Assert.AreEqual(2, block2.Y);
        }


        [TestMethod]
        public void WhenCurrentPieceReachesBottomVerifyesIfThereIsLineEvenForComplexPieces()
        {
            var board = new Board(3, 5);
            var block1 = new Block(0, 0);
            var block2 = new Block(2, 0);
            board.AddNewPiece(new[] { block1, block2 });
            board.AddBlock(1, 4);
            // P P
            //
            //
            //
            //  X
            Assert.AreEqual(1, board.GetBlocks().Count());
            board.Advance(); //1
            board.Advance();
            board.Advance();
            board.Advance(); //4
            board.Advance();

            Assert.IsNull(board.CurrentPiece);
            Assert.AreEqual(0, board.GetBlocks().Count( ));
        }


        [TestMethod]
        public void WhenCurrentPieceColidesWithIsolatedBlocksEvenForComplexPieces()
        {
            var board = new Board(3, 3);
            var block1 = new Block(0, 0);
            var block2 = new Block(2, 0);
            board.AddNewPiece(new[] { block1, block2 });
            board.AddBlock(2, 2);
            board.AddBlock(1, 0);
            // PXP
            //  
            //   X
            Assert.AreEqual(2, board.GetBlocks().Count());
            board.Advance(); //1
            Assert.AreEqual(2, board.GetBlocks().Count());
            Assert.IsNotNull(board.CurrentPiece);
            board.Advance(); //2
            Assert.IsNull(board.CurrentPiece);
            Assert.AreEqual(4, board.GetBlocks().Count());
        }

        [TestMethod]
        public void CanGetCellBlock()
        {
            var board = new Board(3, 3);
            board.AddBlock(2, 2);
            board.AddBlock(1, 0);
            Assert.IsNotNull(board.GetBlock(2, 2));
            Assert.IsNotNull(board.GetBlock(1, 0));
            Assert.IsNull(board.GetBlock(2, 1));
        } 
    }
}

