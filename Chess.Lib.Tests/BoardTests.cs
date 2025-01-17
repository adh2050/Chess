﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Chess.Base.PGN;

namespace Chess.Lib.Tests
{
	[TestFixture]
	public class BoardTests
	{
		[Test]
		public unsafe void TestBoardXY()
		{
			BoardStruct* b = (BoardStruct*)Board.Create();
			Assert.AreEqual(5, Board.X(5));
			Assert.AreEqual(5, Board.X(5+8));
			Assert.AreEqual(5, Board.X(5+16));
			Assert.AreEqual(3, Board.X(8*7 + 3));

			Assert.AreEqual(7, Board.Y(7 * 8 + 3));
			Assert.AreEqual(1, Board.Y(1 * 8 + 4));
			Assert.AreEqual(3, Board.Y(3 * 8 + 6));
			Assert.AreEqual(4, Board.Y(4 * 8 + 0));
			Assert.AreEqual(7, Board.Y(7 * 8 + 7));
		}

		[Test]
		public unsafe void TestBoardColorWhite()
		{
			BoardStruct* b = (BoardStruct*)Board.Create();
			b->Boards[Board.BOARD_PAWNS] = Bitboard.Set((b->Boards[Board.BOARD_PAWNS]), 5);
			b->Boards[Board.BOARD_PAWNS] = Bitboard.Set((b->Boards[Board.BOARD_PAWNS]), 7);
			b->Boards[Board.BOARD_PAWNS] = Bitboard.Set((b->Boards[Board.BOARD_PAWNS]), 47);

			b->Boards[Board.BOARD_WHITE] = Bitboard.Set((b->Boards[Board.BOARD_WHITE]), 5);
			b->Boards[Board.BOARD_WHITE] = Bitboard.Set((b->Boards[Board.BOARD_WHITE]), 7);
			b->Boards[Board.BOARD_WHITE] = Bitboard.Set((b->Boards[Board.BOARD_WHITE]), 47);
			Board.GenerateTileMap(b);


			Assert.AreEqual((int)Chess.Base.Color.White, Board.Color(b, 5));
			Assert.AreEqual((int)Chess.Base.Color.White, Board.Color(b, 7));
			Assert.AreEqual((int)Chess.Base.Color.White, Board.Color(b, 47));

			Assert.AreEqual(0, Board.Color(b, 3));
			Assert.AreEqual(0, Board.Color(b, 8));
			Assert.AreEqual(0, Board.Color(b, 46));
		}

		[Test]
		public unsafe void TestBoardColorBlack()
		{
			BoardStruct* b = (BoardStruct*)Board.Create();
			b->Boards[Board.BOARD_PAWNS] = Bitboard.Set((b->Boards[Board.BOARD_PAWNS]), 5);
			b->Boards[Board.BOARD_PAWNS] = Bitboard.Set((b->Boards[Board.BOARD_PAWNS]), 7);
			b->Boards[Board.BOARD_PAWNS] = Bitboard.Set((b->Boards[Board.BOARD_PAWNS]), 47);

			b->Boards[Board.BOARD_BLACK] = Bitboard.Set((b->Boards[Board.BOARD_BLACK]), 5);
			b->Boards[Board.BOARD_BLACK] = Bitboard.Set((b->Boards[Board.BOARD_BLACK]), 7);
			b->Boards[Board.BOARD_BLACK] = Bitboard.Set((b->Boards[Board.BOARD_BLACK]), 47);
			Board.GenerateTileMap(b);


			Assert.AreEqual((int)Chess.Base.Color.Black, Board.Color(b, 5));
			Assert.AreEqual((int)Chess.Base.Color.Black, Board.Color(b, 7));
			Assert.AreEqual((int)Chess.Base.Color.Black, Board.Color(b, 47));

			Assert.AreEqual(0, Board.Color(b, 3));
			Assert.AreEqual(0, Board.Color(b, 8));
			Assert.AreEqual(0, Board.Color(b, 46));
		}

		[Test]
		public unsafe void TestBoardInit()
		{
			BoardStruct* b = (BoardStruct*)Board.Create();
			Board.Init(b, 0);

			byte allCastle = Board.CASTLE_BK | Board.CASTLE_BQ | Board.CASTLE_WK | Board.CASTLE_WQ;

			Assert.AreEqual((ulong)0, b->Boards[Board.BOARD_WHITE]);
			Assert.AreEqual((ulong)0, b->Boards[Board.BOARD_BLACK]);
			Assert.AreEqual(allCastle, b->Castle);
		}

		[Test]
		public unsafe void TestBoardInitPieces()
		{
			BoardStruct* b = (BoardStruct*)Board.Create();
			Board.Init(b, 1);

			byte allCastle = Board.CASTLE_BK | Board.CASTLE_BQ | Board.CASTLE_WK | Board.CASTLE_WQ;

			Assert.AreEqual((ulong)0xFFFF, b->Boards[Board.BOARD_WHITE]);
			Assert.AreEqual((ulong)0xFFFF000000000000, b->Boards[Board.BOARD_BLACK]);
			Assert.AreEqual((ulong)0xFF00000000FF00, b->Boards[Board.BOARD_PAWNS]);
			Assert.AreEqual((ulong)0x1000000000000010, b->Boards[Board.BOARD_KINGS]);
			Assert.AreEqual(allCastle, b->Castle);
		}

		[Test]
		public unsafe void TestBoardAttacks()
		{
			BoardStruct* b = (BoardStruct*)Board.Create();
			Board.SetPiece(b, 13, Board.PIECE_KING, Board.COLOR_WHITE);
			Board.SetPiece(b, 50, Board.PIECE_KING, Board.COLOR_BLACK);

			Board.SetPiece(b, 0, Board.PIECE_KNIGHT, Board.COLOR_WHITE);
			Board.SetPiece(b, 8, Board.PIECE_PAWN, Board.COLOR_WHITE);

			Board.SetPiece(b, 46, Board.PIECE_PAWN, Board.COLOR_BLACK);

			new Moves(); // load moves

			var whiteMap = Board.AttackMap(b, Board.COLOR_WHITE);
			var blackMap = Board.AttackMap(b, Board.COLOR_BLACK);
			
			Assert.AreEqual((ulong)0x725470, whiteMap);
			Assert.AreEqual((ulong)0xE0A0EA000000000, blackMap);
		}

		[Test]
		public unsafe void TestGetCastlingAll()
		{
			BoardStruct* b = (BoardStruct*)Board.Create();

			Board.SetPiece(b, 4, Board.PIECE_KING, Board.COLOR_WHITE);
			Board.SetPiece(b, 60, Board.PIECE_KING, Board.COLOR_BLACK);

			Board.SetPiece(b, 0, Board.PIECE_ROOK, Board.COLOR_WHITE);
			Board.SetPiece(b, 7, Board.PIECE_ROOK, Board.COLOR_WHITE);
			Board.SetPiece(b, 56, Board.PIECE_ROOK, Board.COLOR_BLACK);
			Board.SetPiece(b, 63, Board.PIECE_ROOK, Board.COLOR_BLACK);

			var castling = Board.GetCastling(b);
			Assert.AreEqual(Board.CASTLE_BK | Board.CASTLE_BQ | Board.CASTLE_WK | Board.CASTLE_WQ, castling);
		}

		[Test]
		public unsafe void TestGetCastlingNone()
		{
			BoardStruct* b = (BoardStruct*)Board.Create();

			Board.SetPiece(b, 4, Board.PIECE_KING, Board.COLOR_WHITE);
			Board.SetPiece(b, 60, Board.PIECE_KING, Board.COLOR_BLACK);

			var castling = Board.GetCastling(b);
			Assert.AreEqual(0, castling);
		}

		[Test]
		public unsafe void TestGetCastlingNone2()
		{
			BoardStruct* b = (BoardStruct*)Board.Create();

			Board.SetPiece(b, 4, Board.PIECE_KING, Board.COLOR_WHITE);
			Board.SetPiece(b, 60, Board.PIECE_KING, Board.COLOR_BLACK);

			Board.SetPiece(b, 0, Board.PIECE_ROOK, Board.COLOR_WHITE);
			Board.SetPiece(b, 7, Board.PIECE_ROOK, Board.COLOR_WHITE);
			Board.SetPiece(b, 56, Board.PIECE_ROOK, Board.COLOR_BLACK);
			Board.SetPiece(b, 63, Board.PIECE_ROOK, Board.COLOR_BLACK);

			b->Castle = 0;

			var castling = Board.GetCastling(b);
			Assert.AreEqual(0, castling);
		}

		[Test]
		public unsafe void TestGetCastlingSingleRooks()
		{
			BoardStruct* b = (BoardStruct*)Board.Create();

			Board.SetPiece(b, 4, Board.PIECE_KING, Board.COLOR_WHITE);
			Board.SetPiece(b, 60, Board.PIECE_KING, Board.COLOR_BLACK);


			Board.SetPiece(b, 0, Board.PIECE_ROOK, Board.COLOR_WHITE);
			var castling = Board.GetCastling(b);
			Assert.AreEqual(Board.CASTLE_WQ, castling);
			Board.ClearPiece(b, 0);

			Board.SetPiece(b, 7, Board.PIECE_ROOK, Board.COLOR_WHITE);
			castling = Board.GetCastling(b);
			Assert.AreEqual(Board.CASTLE_WK, castling);
			Board.ClearPiece(b, 7);

			Board.SetPiece(b, 56, Board.PIECE_ROOK, Board.COLOR_BLACK);
			castling = Board.GetCastling(b);
			Assert.AreEqual(Board.CASTLE_BQ, castling);
			Board.ClearPiece(b, 56);

			Board.SetPiece(b, 63, Board.PIECE_ROOK, Board.COLOR_BLACK);
			castling = Board.GetCastling(b);
			Assert.AreEqual(Board.CASTLE_BK, castling);
			Board.ClearPiece(b, 63);
		}

		[Test]
		public unsafe void TestGetCastlingSingleAllowances()
		{
			BoardStruct* b = (BoardStruct*)Board.Create();

			Board.SetPiece(b, 4, Board.PIECE_KING, Board.COLOR_WHITE);
			Board.SetPiece(b, 60, Board.PIECE_KING, Board.COLOR_BLACK);

			Board.SetPiece(b, 0, Board.PIECE_ROOK, Board.COLOR_WHITE);
			Board.SetPiece(b, 7, Board.PIECE_ROOK, Board.COLOR_WHITE);
			Board.SetPiece(b, 56, Board.PIECE_ROOK, Board.COLOR_BLACK);
			Board.SetPiece(b, 63, Board.PIECE_ROOK, Board.COLOR_BLACK);

			b->Castle = Board.CASTLE_WQ;
			var castling = Board.GetCastling(b);
			Assert.AreEqual(Board.CASTLE_WQ, castling);

			b->Castle = Board.CASTLE_WK;
			castling = Board.GetCastling(b);
			Assert.AreEqual(Board.CASTLE_WK, castling);

			b->Castle = Board.CASTLE_BQ;
			castling = Board.GetCastling(b);
			Assert.AreEqual(Board.CASTLE_BQ, castling);

			b->Castle = Board.CASTLE_BK;
			castling = Board.GetCastling(b);
			Assert.AreEqual(Board.CASTLE_BK, castling);
		}

		[Test]
		public unsafe void TestCheckStateNone()
		{
			BoardStruct* b = (BoardStruct*)Board.Create();
			Board.SetPiece(b, 4, Board.PIECE_KING, Board.COLOR_WHITE);
			Board.SetPiece(b, 60, Board.PIECE_KING, Board.COLOR_BLACK);

			Board.SetPiece(b, 0, Board.PIECE_ROOK, Board.COLOR_WHITE);
			Board.SetPiece(b, 63, Board.PIECE_ROOK, Board.COLOR_BLACK);

			//b->AttacksBlack = Board.AttackMap(b, Board.COLOR_BLACK);
			//b->AttacksWhite = Board.AttackMap(b, Board.COLOR_WHITE);

			var check = Board.IsChecked(b, Board.COLOR_WHITE);
			Assert.AreEqual(0, check);

			check = Board.IsChecked(b, Board.COLOR_BLACK);
			Assert.AreEqual(0, check);
		}

		[Test]
		public unsafe void TestCheckStateWhite()
		{
			BoardStruct* b = (BoardStruct*)Board.Create();
			Board.SetPiece(b, 4, Board.PIECE_KING, Board.COLOR_WHITE);
			Board.SetPiece(b, 60, Board.PIECE_KING, Board.COLOR_BLACK);

			Board.SetPiece(b, 0, Board.PIECE_ROOK, Board.COLOR_BLACK);

			//b->AttacksBlack = Board.AttackMap(b, Board.COLOR_BLACK);
			//b->AttacksWhite = Board.AttackMap(b, Board.COLOR_WHITE);

			var check = Board.IsChecked(b, Board.COLOR_WHITE);
			Assert.AreEqual(1, check);
		}

		[Test]
		public unsafe void TestCheckStateBlack()
		{
			BoardStruct* b = (BoardStruct*)Board.Create();
			Board.SetPiece(b, 4, Board.PIECE_KING, Board.COLOR_WHITE);
			Board.SetPiece(b, 60, Board.PIECE_KING, Board.COLOR_BLACK);

			Board.SetPiece(b, 63, Board.PIECE_ROOK, Board.COLOR_WHITE);

			//b->AttacksBlack = Board.AttackMap(b, Board.COLOR_BLACK);
			//b->AttacksWhite = Board.AttackMap(b, Board.COLOR_WHITE);

			var check = Board.IsChecked(b, Board.COLOR_BLACK);
			Assert.AreEqual(1, check);
		}

		[Test]
		public unsafe void TestIsCheckedNone()
		{
			BoardStruct* b = (BoardStruct*)Board.Create();
			Board.SetPiece(b, 4, Board.PIECE_KING, Board.COLOR_WHITE);
			Board.SetPiece(b, 60, Board.PIECE_KING, Board.COLOR_BLACK);

			Board.SetPiece(b, 0, Board.PIECE_ROOK, Board.COLOR_WHITE);
			Board.SetPiece(b, 63, Board.PIECE_ROOK, Board.COLOR_BLACK);

			//b->AttacksBlack = Board.AttackMap(b, Board.COLOR_BLACK);
			//b->AttacksWhite = Board.AttackMap(b, Board.COLOR_WHITE);

			var check = Board.IsChecked(b, Board.COLOR_WHITE);
			Assert.AreEqual(0, check);
			check = Board.IsChecked(b, Board.COLOR_BLACK);
			Assert.AreEqual(0, check);
		}

		[Test]
		public unsafe void TestIsCheckedWhite()
		{
			BoardStruct* b = (BoardStruct*)Board.Create();
			Board.SetPiece(b, 4, Board.PIECE_KING, Board.COLOR_WHITE);
			Board.SetPiece(b, 60, Board.PIECE_KING, Board.COLOR_BLACK);

			Board.SetPiece(b, 0, Board.PIECE_ROOK, Board.COLOR_BLACK);

			//b->AttacksBlack = Board.AttackMap(b, Board.COLOR_BLACK);
			//b->AttacksWhite = Board.AttackMap(b, Board.COLOR_WHITE);

			var check = Board.IsChecked(b, Board.COLOR_WHITE);
			Assert.AreEqual(1, check);
			check = Board.IsChecked(b, Board.COLOR_BLACK);
			Assert.AreEqual(0, check);
		}

		[Test]
		public unsafe void TestIsCheckedBlack()
		{
			BoardStruct* b = (BoardStruct*)Board.Create();
			Board.SetPiece(b, 4, Board.PIECE_KING, Board.COLOR_WHITE);
			Board.SetPiece(b, 60, Board.PIECE_KING, Board.COLOR_BLACK);

			Board.SetPiece(b, 63, Board.PIECE_ROOK, Board.COLOR_WHITE);

			//b->AttacksBlack = Board.AttackMap(b, Board.COLOR_BLACK);
			//b->AttacksWhite = Board.AttackMap(b, Board.COLOR_WHITE);

			var check = Board.IsChecked(b, Board.COLOR_WHITE);
			Assert.AreEqual(0, check);
			check = Board.IsChecked(b, Board.COLOR_BLACK);
			Assert.AreEqual(1, check);
		}

		[Test]
		public unsafe void TestMakePawn()
		{
			BoardStruct* b = (BoardStruct*)Board.Create();
			Board.Init(b, 1);
			Board.Make(b, 12, 28);

			Assert.AreEqual(20, b->EnPassantTile);
			Assert.AreEqual(true, Bitboard.Get(b->Boards[Board.BOARD_PAWNS], 28));
			Assert.AreEqual(false, Bitboard.Get(b->Boards[Board.BOARD_PAWNS], 12));
		}

		[Test]
		public unsafe void TestMakeRooks()
		{
			BoardStruct* b = (BoardStruct*)Board.Create();
			Board.Init(b, 1);
			Board.Make(b, 6, 21);

			Assert.AreEqual(1, b->CurrentMove);
			Assert.AreEqual(0, b->EnPassantTile);
			Assert.AreEqual(true, Bitboard.Get(b->Boards[Board.BOARD_KNIGHTS], 21));
			Assert.AreEqual(false, Bitboard.Get(b->Boards[Board.BOARD_WHITE], 6));
			Assert.AreEqual(Board.COLOR_BLACK, b->PlayerTurn);
			Board.Make(b, 62, 45);

			Assert.AreEqual(2, b->CurrentMove);
			Assert.AreEqual(2, b->FiftyMoveRulePlies);
			Assert.AreEqual(Board.COLOR_WHITE, b->PlayerTurn);
			Assert.AreEqual(true, Bitboard.Get(b->Boards[Board.BOARD_KNIGHTS], 45));
			Assert.AreEqual(false, Bitboard.Get(b->Boards[Board.BOARD_BLACK], 62));
		}

		[Test]
		public unsafe void TestMakeRooksUnmake()
		{
			BoardStruct* b = (BoardStruct*)Board.Create();
			Board.Init(b, 1);

			//b->AttacksWhite = Board.AttackMap(b, Board.COLOR_WHITE);
			//b->AttacksBlack = Board.AttackMap(b, Board.COLOR_BLACK);

			Board.Make(b, 6, 21);
			Board.Make(b, 62, 45);

			Assert.AreEqual(2, b->CurrentMove);

			Board.Unmake(b);
			Board.Unmake(b);

			Assert.AreEqual(Zobrist.Calculate(b), b->Hash);
			//Assert.AreEqual(Board.AttackMap(b, Board.COLOR_WHITE), b->AttacksWhite);
			//Assert.AreEqual(Board.AttackMap(b, Board.COLOR_BLACK), b->AttacksBlack);
			Assert.AreEqual(Board.CASTLE_BK | Board.CASTLE_BQ | Board.CASTLE_WK | Board.CASTLE_WQ, b->Castle);
			Assert.AreEqual(Board.COLOR_WHITE, b->PlayerTurn);
			Assert.AreEqual(0, b->CurrentMove);
		}
		
		[Test]
		public unsafe void TestMakeUnmakePGNFischer()
		{
			string data = System.IO.File.ReadAllText("..\\..\\..\\TestData\\BobbyFischer.pgn");
			string finalState = "1Q6/5pk1/2p3p1/1p2N2p/1b5P/1bn5/2r3P1/2K5 w - - 16 42";
			var games = new PGNParser().ParsePGN(data);
			var game = games.Games[0];
			var moves = game.GetMainVariation();
			GenericGameTest(moves, finalState);
		}

		[Test]
		public unsafe void TestMakeUnmakePGNKasparovDeepBlue()
		{
			string data = System.IO.File.ReadAllText("..\\..\\..\\TestData\\HumansVsComputers.pgn");
			string finalState = "4r3/6P1/2p2P1k/1p6/pP2p1R1/P1B5/2P2K2/3r4 b - - 0 45";
			var games = new PGNParser().ParsePGN(data);
			var game = games.Games[0];
			var moves = game.GetMainVariation();
			GenericGameTest(moves, finalState);
		}

		[Test]
		public unsafe void TestMakeUnmakePGNKasparovX3DFritz()
		{
			string data = System.IO.File.ReadAllText("..\\..\\..\\TestData\\HumansVsComputers.pgn");
			string finalState = "r6k/1r1qnppp/NPp2n1b/2Pp4/3Pp3/1RB1P1PP/R1Q2P2/K4B2 b - - 4 45";
			var games = new PGNParser().ParsePGN(data).Games;
			var game = games[games.Count - 2];
			var moves = game.GetMainVariation();
			GenericGameTest(moves, finalState);
		}

		[Test]
		public unsafe void TestMakeUnmakePGNKasparovX3D()
		{
			string data = System.IO.File.ReadAllText("..\\..\\..\\TestData\\HumansVsComputers.pgn");
			string finalState = "3Q4/6pk/7p/8/8/8/r4qPP/3R3K w - - 2 28";
			var games = new PGNParser().ParsePGN(data).Games;
			var game = games.Last();
			var moves = game.GetMainVariation();
			GenericGameTest(moves, finalState);
		}

		[Test]
		public unsafe void TestMakeUnmakePromotion()
		{
			string data = "1. e4 d5 2. exd5 Nf6 3. Bb5+ c6 4. dxc6 Qb6 5. Bf1 Qb4 6. cxb7 Kd7 7. bxc8=Q+ Kd6 8. Qh3 Qb5 9. Qxh7 Qb6 10. Qxh8 ";
			string finalState = "rn3b1Q/p3ppp1/1q1k1n2/8/8/8/PPPP1PPP/RNBQKBNR b KQ - 0 10";
			var game = new PGNParser().ParseGame(data);
			var moves = game.GetMainVariation();
			GenericGameTest(moves, finalState);
		}

		private unsafe static void GenericGameTest(List<PGNMove> moves, string endState)
		{
			BoardStruct* b = (BoardStruct*)Board.Create();
			Board.Init(b, 1);

			var finalBoard = Chess.Base.Notation.ReadFEN(endState);
			var fBoard = Helpers.ManagedBoardToNative(finalBoard);
			Assert.AreEqual(Zobrist.Calculate(fBoard), fBoard->Hash);

			var stateStack = new IntPtr[150];
			int head = 0;

			for (int i = 0; i < moves.Count; i++)
			{
				stateStack[head] = Board.Copy(b);
				head++;
				var ok = Board.Make(b, moves[i].From, moves[i].To);
				if (moves[i].Promotion != 0)
				{
					bool promoteOk = Board.Promote(b, moves[i].To, (int)moves[i].Promotion);
					Assert.AreEqual(true, promoteOk);
				}

				Assert.AreEqual(true, ok);
				Assert.AreEqual(Zobrist.Calculate(b), b->Hash);
			}

			Assert.AreEqual(Zobrist.Calculate(fBoard), b->Hash);
			Assert.AreEqual(fBoard->Hash, b->Hash);
			//Assert.AreEqual(fBoard->AttacksWhite, b->AttacksWhite);
			//Assert.AreEqual(fBoard->AttacksBlack, b->AttacksBlack);
			Assert.AreEqual(fBoard->Castle, b->Castle);
			Assert.AreEqual(fBoard->PlayerTurn, b->PlayerTurn);
			Assert.AreEqual(fBoard->FiftyMoveRulePlies, b->FiftyMoveRulePlies);
			Assert.AreEqual(fBoard->CurrentMove, b->CurrentMove);

			while (head > 0)
			{
				head--;
				Board.Unmake(b);
				var bStack = (BoardStruct*)stateStack[head];

				Assert.AreEqual(Zobrist.Calculate(b), b->Hash);
				Assert.AreEqual(bStack->Hash, b->Hash);
				//Assert.AreEqual(bStack->AttacksWhite, b->AttacksWhite);
				//Assert.AreEqual(bStack->AttacksBlack, b->AttacksBlack);
				Assert.AreEqual(bStack->Castle, b->Castle);
				Assert.AreEqual(bStack->PlayerTurn, b->PlayerTurn);
				Assert.AreEqual(bStack->FiftyMoveRulePlies, b->FiftyMoveRulePlies);
				Assert.AreEqual(bStack->CurrentMove, b->CurrentMove);
			}
		}

		[Test]
		public unsafe void TestMoveCanSelfCheck1()
		{
			// Can self check because of coverage
			var b = Board.Create();
			Board.SetPiece(b, 36, Board.PIECE_KING, Board.COLOR_WHITE);
			Board.SetPiece(b, 34, Board.PIECE_PAWN, Board.COLOR_WHITE);
			var canSelfCheck = Board.MoveCanSelfCheck(b, 34, 42);
			Assert.IsTrue(canSelfCheck);
		}

		[Test]
		public unsafe void TestMoveCanSelfCheck2()
		{
			// Cannot self check because of coverage 2
			var b = Board.Create();
			Board.SetPiece(b, 36, Board.PIECE_KING, Board.COLOR_WHITE);
			Board.SetPiece(b, 32, Board.PIECE_PAWN, Board.COLOR_WHITE);
			Board.SetPiece(b, 33, Board.PIECE_PAWN, Board.COLOR_BLACK);
			var canSelfCheck = Board.MoveCanSelfCheck(b, 32, 40);
			Assert.IsFalse(canSelfCheck);
		}

		[Test]
		public unsafe void TestMoveCanSelfCheck3()
		{
			// Can always self check when moving the king
			var b = Board.Create();
			Board.SetPiece(b, 36, Board.PIECE_KING, Board.COLOR_WHITE);
			Board.SetPiece(b, 32, Board.PIECE_PAWN, Board.COLOR_WHITE);
			Board.SetPiece(b, 33, Board.PIECE_PAWN, Board.COLOR_BLACK);
			var canSelfCheck = Board.MoveCanSelfCheck(b, 36, 44);
			Assert.IsTrue(canSelfCheck);
		}

		[Test]
		public unsafe void TestMoveCanSelfCheck4()
		{
			// Can self check because of En passant
			var b = Board.Create();
			b->PlayerTurn = Board.COLOR_BLACK;
			Board.SetPiece(b, 0, Board.PIECE_KING, Board.COLOR_WHITE);
			Board.SetPiece(b, 63, Board.PIECE_KING, Board.COLOR_BLACK);

			Board.SetPiece(b, 49, Board.PIECE_PAWN, Board.COLOR_BLACK);
			Board.SetPiece(b, 34, Board.PIECE_PAWN, Board.COLOR_WHITE);

			var valid = Board.Make(b, 49, 33);

			Assert.AreEqual(true, valid);
			Assert.AreEqual(41, b->EnPassantTile);

			var canSelfCheck = Board.MoveCanSelfCheck(b, 34, 41);
			Assert.IsTrue(canSelfCheck);
		}

		[Test]
		public unsafe void TestFen1()
		{
			string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

			byte* str = stackalloc byte[100];
			var bb = Chess.Base.Notation.ReadFEN(fen);
			var b = Helpers.ManagedBoardToNative(bb);

			Board.ToFEN(b, str);
			string fen2 = Helpers.GetString(str);
			Assert.AreEqual(fen, fen2);
		}

		[Test]
		public unsafe void TestFen2()
		{
			string fen = "r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 1";

			byte* str = stackalloc byte[100];
			var bb = Chess.Base.Notation.ReadFEN(fen);
			var b = Helpers.ManagedBoardToNative(bb);

			Board.ToFEN(b, str);
			string fen2 = Helpers.GetString(str);
			Assert.AreEqual(fen, fen2);
		}

		[Test]
		public unsafe void TestFen3()
		{
			string fen = "8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 1";

			byte* str = stackalloc byte[100];
			var bb = Chess.Base.Notation.ReadFEN(fen);
			var b = Helpers.ManagedBoardToNative(bb);

			Board.ToFEN(b, str);
			string fen2 = Helpers.GetString(str);
			Assert.AreEqual(fen, fen2);
		}

		[Test]
		public unsafe void TestFen4()
		{
			string fen = "r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1";

			byte* str = stackalloc byte[100];
			var bb = Chess.Base.Notation.ReadFEN(fen);
			var b = Helpers.ManagedBoardToNative(bb);

			Board.ToFEN(b, str);
			string fen2 = Helpers.GetString(str);
			Assert.AreEqual(fen, fen2);
		}

		[Test]
		public unsafe void TestFen5()
		{
			string fen = "rnbqkb1r/pp1p1ppp/2p5/4P3/2B5/8/PPP1NnPP/RNBQK2R w KQkq - 0 6";

			byte* str = stackalloc byte[100];
			var bb = Chess.Base.Notation.ReadFEN(fen);
			var b = Helpers.ManagedBoardToNative(bb);

			Board.ToFEN(b, str);
			string fen2 = Helpers.GetString(str);
			Assert.AreEqual(fen, fen2);
		}

		[Test]
		public unsafe void TestSmallestAttacker()
		{
			string fen = @"2kq4/8/2p5/3b3R/8/4N3/8/2R3K1 w KQkq - 0 1";
			var bb = Chess.Base.Notation.ReadFEN(fen);
			var b = Helpers.ManagedBoardToNative(bb);

			var attacker = Board.GetSmallestAttacker(b, 35, Board.COLOR_BLACK, 0);
			Assert.AreEqual(42, attacker);
		}

		[Test]
		public unsafe void TestSmallestAttackerPinned()
		{
			string fen = @"2kq4/8/2p5/3b3R/8/4N3/8/2R3K1 w KQkq - 0 1";
			var bb = Chess.Base.Notation.ReadFEN(fen);
			var b = Helpers.ManagedBoardToNative(bb);

			var attacker = Board.GetSmallestAttacker(b, 35, Board.COLOR_BLACK, 0x40000000000);
			Assert.AreEqual(59, attacker);
		}

		[Test]
		public unsafe void TestMakeNullMove()
		{
			string fen = @"2kq4/8/2p5/3b3R/8/4N3/8/2R3K1 w KQkq - 0 1";
			var bb = Chess.Base.Notation.ReadFEN(fen);
			var b = Helpers.ManagedBoardToNative(bb);

			var hash = b->Hash;
			var player = b->PlayerTurn;
			Board.MakeNullMove(b);
			Assert.AreNotEqual(player, b->PlayerTurn);
			Board.Unmake(b);
			Assert.AreEqual(hash, b->Hash);
		}

	}
}
