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
         * Al principio el tablero no tiene pieza actual
         * El tablero permite poner una pieza
         * El tablero da informacion sobre la posicion de la pieza actual
         * Al hacer progresar el tablero baja la pieza actual
         * Al apoyarse la pieza, cambia la pieza actual
         * Al apoyarse la pieza, el tablero comprueba si hay línea y en ese caso borra los bloques de la misma
         * Si se avanza el tablero y no existe pieza actual deberia dar error
         * No se puede agregar una pieza si hay una pieza actual
         * El tablero pierde al llegar a la cima.
         * El tablero gana con el alcance de un objetivo.
         */

        [TestMethod]
        public void BoardExists()
        {
            var board = new Board();
        }

        [TestMethod]
        public void BoardHasNotCurrentPiece()
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
            Assert.AreEqual(0, board.CurrentPiece.Height);
        }

        [TestMethod]
        public void WhenBoardAdvancesCurrentPieceGoesDown()
        {
            var board = new Board();
            board.AddNewPiece();
            board.Advance();
            Assert.AreEqual(1, board.CurrentPiece.Height);
        }
    }
}
