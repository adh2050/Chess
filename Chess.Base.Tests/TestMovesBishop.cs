﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Chess.Base.Tests
{
	[TestFixture]
	public class TestMovesBishop
	{
		[Test]
		public void TestFree()
		{
			// test free space
			var b = new Board();
			int pos = 4 * 8 + 4;
			b.State[pos] = Colors.Val(Piece.Bishop, Color.White);
			var moves = Moves.GetMoves(b, pos);
			Assert.AreEqual(13, moves.Length);

			Assert.IsTrue(moves.Contains(pos + 9));
			Assert.IsTrue(moves.Contains(pos + 18));
			Assert.IsTrue(moves.Contains(pos + 27));
			Assert.IsTrue(moves.Contains(pos - 9));
			Assert.IsTrue(moves.Contains(pos - 18));
			Assert.IsTrue(moves.Contains(pos - 27));
			Assert.IsTrue(moves.Contains(pos - 36));

			Assert.IsTrue(moves.Contains(pos + 7));
			Assert.IsTrue(moves.Contains(pos + 14));
			Assert.IsTrue(moves.Contains(pos + 21));
			Assert.IsTrue(moves.Contains(pos - 7));
			Assert.IsTrue(moves.Contains(pos - 14));
			Assert.IsTrue(moves.Contains(pos - 21));
		}

		[Test]
		public void TestCorner()
		{
			// test free space
			var b = new Board();
			int pos = 7;
			b.State[pos] = Colors.Val(Piece.Bishop, Color.White);
			var moves = Moves.GetMoves(b, pos);
			
		}

		[Test]
		public void TestSameColor()
		{
			var b = new Board();
			int pos = 4 * 8 + 4;
			b.State[pos] = Colors.Val(Piece.Bishop, Color.White);
			b.State[pos + 18] = Colors.Val(Piece.Pawn, Color.White);
			b.State[pos - 18] = Colors.Val(Piece.Pawn, Color.White);
			b.State[pos + 14] = Colors.Val(Piece.Pawn, Color.White);
			b.State[pos - 14] = Colors.Val(Piece.Pawn, Color.White);

			var moves = Moves.GetMoves(b, pos);
			Assert.AreEqual(4, moves.Length);
			Assert.IsTrue(moves.Contains(pos + 9));
			Assert.IsTrue(moves.Contains(pos + 7));

			Assert.IsTrue(moves.Contains(pos - 9));
			Assert.IsTrue(moves.Contains(pos - 7));
		}

		[Test]
		public void TestOppositeColor()
		{
			var b = new Board();
			int pos = 4 * 8 + 4;
			b.State[pos] = Colors.Val(Piece.Bishop, Color.White);
			b.State[pos + 18] = Colors.Val(Piece.Pawn, Color.Black);
			b.State[pos - 18] = Colors.Val(Piece.Pawn, Color.Black);
			b.State[pos + 14] = Colors.Val(Piece.Pawn, Color.Black);
			b.State[pos - 14] = Colors.Val(Piece.Pawn, Color.Black);

			var moves = Moves.GetMoves(b, pos);
			Assert.AreEqual(8, moves.Length);
			Assert.IsTrue(moves.Contains(pos + 9));
			Assert.IsTrue(moves.Contains(pos + 7));
			Assert.IsTrue(moves.Contains(pos + 18));
			Assert.IsTrue(moves.Contains(pos + 14));

			Assert.IsTrue(moves.Contains(pos - 9));
			Assert.IsTrue(moves.Contains(pos - 7));
			Assert.IsTrue(moves.Contains(pos - 18));
			Assert.IsTrue(moves.Contains(pos - 14));
		}


	}
}
