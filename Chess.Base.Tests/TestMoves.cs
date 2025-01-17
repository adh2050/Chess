﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Chess.Base.Tests
{
	[TestFixture]
	public class TestMoves
	{
		[Test]
		public void TestIsCastlingMove()
		{
			var b = new Board();
			b.InitBoard();
			Assert.IsTrue(b.CanCastleKWhite);

			Assert.AreEqual(Castling.KingsideWhite, Moves.IsCastlingMove(b, 4, 6));
			Assert.AreEqual(Castling.QueensideWhite, Moves.IsCastlingMove(b, 4, 2));

			Assert.IsTrue(Moves.IsCastlingMove(b, 4, 5) == 0);
			Assert.IsTrue(Moves.IsCastlingMove(b, 4, 7) == 0);
			Assert.IsTrue(Moves.IsCastlingMove(b, 4, 3) == 0);
			Assert.IsTrue(Moves.IsCastlingMove(b, 4, 1) == 0);

			Assert.AreEqual(Castling.KingsideBlack, Moves.IsCastlingMove(b, 60, 62));
			Assert.AreEqual(Castling.QueensideBlack, Moves.IsCastlingMove(b, 60, 58));

			Assert.IsTrue(Moves.IsCastlingMove(b, 60, 61) == 0);
			Assert.IsTrue(Moves.IsCastlingMove(b, 60, 63) == 0);
			Assert.IsTrue(Moves.IsCastlingMove(b, 60, 69) == 0);
			Assert.IsTrue(Moves.IsCastlingMove(b, 60, 57) == 0);
		}

		[Test]
		public void TestIsEnPassantLeftWhite()
		{
			var b = new Board();
			b.PlayerTurn = Color.Black;

			int posWhite = Notation.TextToTile("e5");
			int posBlack = Notation.TextToTile("d7");

			b.State[posWhite] = Colors.Val(Piece.Pawn, Color.White);
			b.State[posBlack] = Colors.Val(Piece.Pawn, Color.Black);

			b.Move(posBlack, posBlack - 16);

			bool isEnPassant = Moves.IsEnPassantCapture(b, posWhite, posWhite + 7);
			Assert.IsTrue(isEnPassant);

			// test the other / wrong way - not allowed!
			isEnPassant = Moves.IsEnPassantCapture(b, posWhite, posWhite + 9);
			Assert.IsFalse(isEnPassant);
		}

		[Test]
		public void TestIsEnPassantRightWhite()
		{
			var b = new Board();
			b.PlayerTurn = Color.Black;

			int posWhite = Notation.TextToTile("e5");
			int posBlack = Notation.TextToTile("f7");

			b.State[posWhite] = Colors.Val(Piece.Pawn, Color.White);
			b.State[posBlack] = Colors.Val(Piece.Pawn, Color.Black);

			b.Move(posBlack, posBlack - 16);

			bool isEnPassant = Moves.IsEnPassantCapture(b, posWhite, posWhite + 9);
			Assert.IsTrue(isEnPassant);

			// test the other / wrong way - not allowed!
			isEnPassant = Moves.IsEnPassantCapture(b, posWhite, posWhite + 7);
			Assert.IsFalse(isEnPassant);
		}

		[Test]
		public void TestIsEnPassantLeftBlack()
		{
			var b = new Board();
			b.PlayerTurn = Color.White;

			int posBlack = Notation.TextToTile("e4");
			int posWhite = Notation.TextToTile("d2");

			b.State[posBlack] = Colors.Val(Piece.Pawn, Color.Black);
			b.State[posWhite] = Colors.Val(Piece.Pawn, Color.White);

			b.Move(posWhite, posWhite + 16);

			bool isEnPassant = Moves.IsEnPassantCapture(b, posBlack, posBlack - 9);
			Assert.IsTrue(isEnPassant);

			// test the other / wrong way - not allowed!
			isEnPassant = Moves.IsEnPassantCapture(b, posBlack, posBlack - 7);
			Assert.IsFalse(isEnPassant);
		}

		[Test]
		public void TestIsEnPassantRightBlack()
		{
			var b = new Board();
			b.PlayerTurn = Color.White;

			int posBlack = Notation.TextToTile("e4");
			int posWhite = Notation.TextToTile("f2");

			b.State[posBlack] = Colors.Val(Piece.Pawn, Color.Black);
			b.State[posWhite] = Colors.Val(Piece.Pawn, Color.White);

			b.Move(posWhite, posWhite + 16);

			bool isEnPassant = Moves.IsEnPassantCapture(b, posBlack, posBlack - 7);
			Assert.IsTrue(isEnPassant);

			// test the other / wrong way - not allowed!
			isEnPassant = Moves.IsEnPassantCapture(b, posBlack, posBlack - 9);
			Assert.IsFalse(isEnPassant);
		}

		[Test]
		public void TestEnPassantVictimWhiteRight()
		{
			var b = new Board();
			b.PlayerTurn = Color.Black;

			int posWhite = Notation.TextToTile("e5");
			int posBlack = Notation.TextToTile("f7");

			b.State[posWhite] = Colors.Val(Piece.Pawn, Color.White);
			b.State[posBlack] = Colors.Val(Piece.Pawn, Color.Black);

			b.Move(posBlack, posBlack - 16);

			bool isEnPassant = Moves.IsEnPassantCapture(b, posWhite, posWhite + 9);
			Assert.IsTrue(isEnPassant);

			int target = Moves.EnPassantVictim(b, posWhite, posWhite + 9);
			Assert.AreEqual(target, posBlack - 16);
		}

		[Test]
		public void TestEnPassantVictimBlackLeft()
		{
			var b = new Board();
			b.PlayerTurn = Color.White;

			int posBlack = Notation.TextToTile("e4");
			int posWhite = Notation.TextToTile("d2");

			b.State[posBlack] = Colors.Val(Piece.Pawn, Color.Black);
			b.State[posWhite] = Colors.Val(Piece.Pawn, Color.White);

			b.Move(posWhite, posWhite + 16);

			bool isEnPassant = Moves.IsEnPassantCapture(b, posBlack, posBlack - 9);
			Assert.IsTrue(isEnPassant);

			int target = Moves.EnPassantVictim(b, posBlack, posBlack - 9);
			Assert.AreEqual(target, posWhite + 16);
		}

		[Test]
		public void TestIsCaptureMoveEnPassant()
		{
			var b = new Board();
			b.PlayerTurn = Color.Black;

			int posWhite = Notation.TextToTile("e5");
			int posBlack = Notation.TextToTile("d7");

			b.State[posWhite] = Colors.Val(Piece.Pawn, Color.White);
			b.State[posBlack] = Colors.Val(Piece.Pawn, Color.Black);

			b.Move(posBlack, posBlack - 16);

			bool capture = Moves.IsCaptureMove(b, posWhite, posWhite + 7);
			Assert.IsTrue(capture);
		}

		[Test]
		public void TestIsCaptureMoveNormal()
		{
			var b = new Board();
			b.PlayerTurn = Color.Black;

			int posWhite = Notation.TextToTile("d2");
			int posBlack = Notation.TextToTile("d6");

			b.State[posWhite] = Colors.Val(Piece.Rook, Color.White);
			b.State[posBlack] = Colors.Val(Piece.Pawn, Color.Black);

			bool capture = Moves.IsCaptureMove(b, posWhite, posBlack);
			Assert.IsTrue(capture);

			capture = Moves.IsCaptureMove(b, posWhite, posBlack - 8);
			Assert.IsFalse(capture);
		}
	}
}
