﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Chess.Base.Tests
{
	[TestFixture]
	public class TestNotation
	{
		[Test]
		public void TestTextToTile()
		{
			string a = "E1";
			string b = "d5";
			string c = "h8";

			string d = "a1";
			string e = "b5";
			string f = "c7";
			string g = "f5";
			string h = "g6";

			string X = "A0";
			string XX = "A9";
			string XXX = "A12";
			string XXXX = "K4";

			int tile = 0;

			tile = Notation.TextToTile(a);
			Assert.AreEqual(4, tile);

			tile = Notation.TextToTile(b);
			Assert.AreEqual(35, tile);

			tile = Notation.TextToTile(c);
			Assert.AreEqual(63, tile);

			tile = Notation.TextToTile(d);
			Assert.AreEqual(0, tile);

			tile = Notation.TextToTile(e);
			Assert.AreEqual(33, tile);

			tile = Notation.TextToTile(f);
			Assert.AreEqual(50, tile);

			tile = Notation.TextToTile(g);
			Assert.AreEqual(37, tile);

			tile = Notation.TextToTile(h);
			Assert.AreEqual(46, tile);


			bool exception = false;
			try
			{
				tile = Notation.TextToTile(X);
			}
			catch (Exception)
			{
				exception = true;
			}
			Assert.IsTrue(exception);

			try
			{
				tile = Notation.TextToTile(XX);
			}
			catch (Exception)
			{
				exception = true;
			}
			Assert.IsTrue(exception);

			try
			{
				tile = Notation.TextToTile(XXX);
			}
			catch (Exception)
			{
				exception = true;
			}
			Assert.IsTrue(exception);

			try
			{
				tile = Notation.TextToTile(XXXX);
			}
			catch (Exception)
			{
				exception = true;
			}
			Assert.IsTrue(exception);
		}

		[Test]
		public void TestTileToText()
		{
			Assert.AreEqual("e1", Notation.TileToText(4));
			Assert.AreEqual("d5", Notation.TileToText(35));
			Assert.AreEqual("h8", Notation.TileToText(63));

			Assert.AreEqual("", Notation.TileToText(-1));
			Assert.AreEqual("", Notation.TileToText(64));
			Assert.AreEqual("", Notation.TileToText(1235));
		}

		[Test]
		public void TestFEN1()
		{ 
			var b1 = new Board(true);

			var str = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq";
			var b2 = Notation.ReadFEN(str);
			
			bool equal = b1.State.SequenceEqual(b2.State);
			Assert.IsTrue(equal);
		}

		[Test]
		public void TestFEN2()
		{
			var b1 = new Board(true);
			b1.Move(12, 12 + 16);
			b1.Move(50, 50 - 16);
			b1.Move(12+16, 12 + 16 + 8);

			var str = "rnbqkbnr/pp1ppppp/8/2p1P3/8/8/PPPP1PPP/RNBQKBNR b KQkq";
			var b2 = Notation.ReadFEN(str);

			bool equal = b1.State.SequenceEqual(b2.State);
			Assert.IsTrue(equal);
		}

		[Test]
		public void TestFENEnPassantBlank()
		{
			var str = "rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq -";
			var b2 = Notation.ReadFEN(str);
			Assert.AreEqual(0, b2.EnPassantTile);
		}

		[Test]
		public void TestFENEnPassantWhite()
		{
			var str = "rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3";
			var b2 = Notation.ReadFEN(str);

			Assert.AreEqual(Notation.TextToTile("e3"), b2.EnPassantTile);

			// compare state
			var b1 = new Board(true);
			b1.Move(Notation.TextToTile("e2"), Notation.TextToTile("e4"));
			bool equal = b1.State.SequenceEqual(b2.State);
			Assert.IsTrue(equal);
		}

		[Test]
		public void TestFENEnPassantBlack()
		{
			var str = "rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c6";
			var b2 = Notation.ReadFEN(str);

			Assert.AreEqual(Notation.TextToTile("c6"), b2.EnPassantTile);

			// compare state
			var b1 = new Board(true);
			b1.Move(Notation.TextToTile("e2"), Notation.TextToTile("e4"));
			b1.Move(Notation.TextToTile("c7"), Notation.TextToTile("c5"));
			bool equal = b1.State.SequenceEqual(b2.State);
			Assert.IsTrue(equal);
		}

		[Test]
		public void TestFENEnPassantWrongTurnWhite()
		{
			var str = "rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR w KQkq e3";

			bool ex = false;
			try
			{
				var b2 = Notation.ReadFEN(str);
			}
			catch (Exception e)
			{
				Assert.IsTrue(e.Message.Contains("white moved last"));
				ex = true;
			}

			Assert.IsTrue(ex);
		}

		[Test]
		public void TestFENEnPassantWrongTurnBlack()
		{
			var str = "rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR b KQkq c6";

			bool ex = false;
			try
			{
				var b2 = Notation.ReadFEN(str);
			}
			catch (Exception e)
			{
				Assert.IsTrue(e.Message.Contains("black moved last"));
				ex = true;
			}

			Assert.IsTrue(ex);
		}

		[Test]
		public void TestFENEnPassantMissingPieceWhite()
		{
			var str = "rnbqkbnr/pppppppp/8/8/8/8/PPPP1PPP/RNBQKBNR b KQkq e3";

			bool ex = false;
			try
			{
				var b2 = Notation.ReadFEN(str);
			}
			catch (Exception e)
			{
				Assert.IsTrue(e.Message.Contains("expected white pawn"));
				ex = true;
			}

			Assert.IsTrue(ex);
		}

		[Test]
		public void TestFENEnPassantMissingPieceBlack()
		{
			var str = "rnbqkbnr/pp1ppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c6";

			bool ex = false;
			try
			{
				var b2 = Notation.ReadFEN(str);
			}
			catch (Exception e)
			{
				Assert.IsTrue(e.Message.Contains("expected black pawn"));
				ex = true;
			}

			Assert.IsTrue(ex);
		}

		[Test]
		public void TestFENCastle1()
		{
			var b1 = new Board(true);

			var str = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq";
			var b2 = Notation.ReadFEN(str);

			Assert.IsTrue(b2.CanCastleQWhite);
			Assert.IsTrue(b2.CanCastleKWhite);

			Assert.IsTrue(b2.CanCastleQBlack);
			Assert.IsTrue(b2.CanCastleKBlack);
		}

		[Test]
		public void TestFENCastle2()
		{
			var b1 = new Board(true);

			var str = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w Kq";
			var b2 = Notation.ReadFEN(str);

			Assert.IsFalse(b2.CanCastleQWhite);
			Assert.IsTrue(b2.CanCastleKWhite);

			Assert.IsTrue(b2.CanCastleQBlack);
			Assert.IsFalse(b2.CanCastleKBlack);
		}

		[Test]
		public void TestFENCastleNone()
		{
			var b1 = new Board(true);

			var str = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w -";
			var b2 = Notation.ReadFEN(str);

			Assert.IsFalse(b2.CanCastleQWhite);
			Assert.IsFalse(b2.CanCastleKWhite);

			Assert.IsFalse(b2.CanCastleQBlack);
			Assert.IsFalse(b2.CanCastleKBlack);
		}

		[Test]
		public void TestFENTurnWhite()
		{
			var b1 = new Board(true);

			var str = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR W Kq";
			var b2 = Notation.ReadFEN(str);

			Assert.AreEqual(Color.White, b2.PlayerTurn);
		}

		[Test]
		public void TestFENTurnBlack()
		{
			var b1 = new Board(true);

			var str = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b Kq";
			var b2 = Notation.ReadFEN(str);

			Assert.AreEqual(Color.Black, b2.PlayerTurn);
		}

		[Test]
		public void TestFENHalfmovesAndRound()
		{
			var b1 = new Board(true);

			var str = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b Kq - 12 18";
			var b2 = Notation.ReadFEN(str);

			Assert.AreEqual(12, b2.FiftyMoveRulePlies);
			Assert.AreEqual(18, b2.MoveCount);
		}

		[Test]
		public void TestFromToFEN()
		{
			var strings = new List<string>();
			strings.Add("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq");
			strings.Add("rnbqkbnr/pp1ppppp/8/2p1P3/8/8/PPPP1PPP/RNBQKBNR b KQkq");
			strings.Add("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq -");
			strings.Add("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3");
			strings.Add("rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c6");
			strings.Add("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq");
			strings.Add("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w Kq");
			strings.Add("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w -");
			strings.Add("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w Kq");
			strings.Add("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b Kq");
			strings.Add("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b Kq - 12 18");

			foreach(var str in strings)
			{
				var b = Notation.ReadFEN(str);
				var fen = Notation.BoardToFEN(b);

				// use StartsWith because the BoardToFEN may add extra default data
				Assert.IsTrue(fen.StartsWith(str));
			}
		}
	}
}
