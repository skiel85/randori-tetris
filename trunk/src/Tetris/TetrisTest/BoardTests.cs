﻿using System;
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
         * Al apoyarse la pieza, el tablero comprueba si hay línea y en ese caso borra los bloques de la misma
         * No se puede agregar una pieza si hay una pieza actual
         * El tablero pierde al llegar a la cima
         * El tablero gana con el alcance de un objetivo
         * 
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
            Assert.AreEqual(0, board.CurrentPiece.Height);
        }

        [TestMethod]
        public void WhenBoardFirstAdvancesCurrentPieceGoesDown()
        {
            var board = new Board();
            board.AddNewPiece();
            board.Advance();
            Assert.AreEqual(1, board.CurrentPiece.Height);
        }

        [TestMethod]
        public void WhenBoardAdvancesCurrentPieceGoesDown()
        {
            var board = new Board(30);
            board.AddNewPiece();
            for (int i = 0; i <= 20; i++)
            {
                Assert.AreEqual(i, board.CurrentPiece.Height);
                board.Advance();
            }
            Assert.AreEqual(21, board.CurrentPiece.Height);
        }

        [TestMethod]
        public void WhenDoesNotExistsCurrentPieceAndBoardAdvancesNothingOcurrs()
        {
            var board = new Board();
            board.Advance();
        }

        [TestMethod]
        public void CurrentPieceAdvancesUntilSomeLimit()
        {
            var board = new Board(3);
            board.AddNewPiece();
            board.Advance(); //1
            board.Advance(); //2
            Assert.AreEqual(2, board.CurrentPiece.Height);
            board.Advance(); //3
            Assert.AreEqual(3, board.CurrentPiece.Height);
            board.Advance(); //3
            Assert.IsNull(board.CurrentPiece);
        }

        [TestMethod]
        public void DefaultConstructorCreatesABoardWithAHeightOf10()
        {
            var board = new Board();
            Assert.AreEqual(10, board.Height);
        }


        [TestMethod]
        public void WhenPieceReachesLimitItsBlocksBecamePartOfTheBoard()
        {
            var board = new Board(3);
            board.AddNewPiece();
            var block = board.CurrentPiece.GetBlocks().First();
            block.Y = 3;

            board.Advance();
            board.Advance();
            board.Advance();
            board.Advance();

            Assert.IsTrue(board.Contains(block));
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

