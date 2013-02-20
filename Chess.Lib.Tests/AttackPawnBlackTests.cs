﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chess.Lib.Tests
{
	[TestClass]
	public class AttackPawnBlackTests
	{
		// --------------- Simple moves, single piece on board ---------------

		[TestMethod]
		public unsafe void TestBlackPawnSingle1()
		{
			BoardStruct* b = (BoardStruct*)Board.Board_Create();
			b->Boards[Board.BOARD_PAWNS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_PAWNS]), 53);
			b->Boards[Board.BOARD_BLACK] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_BLACK]), 53);

			ulong moves = Moves.Moves_GetAttacks((IntPtr)b, 53);

			Assert.AreEqual((ulong)0x500000000000, moves);
		}

		[TestMethod]
		public unsafe void TestBlackPawnSingle2()
		{
			BoardStruct* b = (BoardStruct*)Board.Board_Create();
			b->Boards[Board.BOARD_PAWNS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_PAWNS]), 35);
			b->Boards[Board.BOARD_BLACK] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_BLACK]), 35);

			ulong moves = Moves.Moves_GetAttacks((IntPtr)b, 35);

			Assert.AreEqual((ulong)0x14000000, moves);
		}

		[TestMethod]
		public unsafe void TestBlackPawnSingle3()
		{
			BoardStruct* b = (BoardStruct*)Board.Board_Create();
			b->Boards[Board.BOARD_PAWNS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_PAWNS]), 8);
			b->Boards[Board.BOARD_BLACK] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_BLACK]), 8);

			ulong moves = Moves.Moves_GetAttacks((IntPtr)b, 8);

			Assert.AreEqual((ulong)0x2, moves);
		}

		// --------------- capture moves, two pieces on board ---------------

		[TestMethod]
		public unsafe void TestBlackPawnCapture1()
		{
			BoardStruct* b = (BoardStruct*)Board.Board_Create();
			b->Boards[Board.BOARD_PAWNS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_PAWNS]), 55);
			b->Boards[Board.BOARD_BLACK] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_BLACK]), 55);

			b->Boards[Board.BOARD_PAWNS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_PAWNS]), 46);
			b->Boards[Board.BOARD_WHITE] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_WHITE]), 46);

			ulong moves = Moves.Moves_GetAttacks((IntPtr)b, 55);

			Assert.AreEqual((ulong)0x400000000000, moves);
		}

		[TestMethod]
		public unsafe void TestBlackPawnCapture2()
		{
			BoardStruct* b = (BoardStruct*)Board.Board_Create();
			b->Boards[Board.BOARD_PAWNS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_PAWNS]), 35);
			b->Boards[Board.BOARD_BLACK] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_BLACK]), 35);

			b->Boards[Board.BOARD_PAWNS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_PAWNS]), 35 - 7);
			b->Boards[Board.BOARD_WHITE] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_WHITE]), 35 - 7);

			b->Boards[Board.BOARD_PAWNS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_PAWNS]), 35 - 9);
			b->Boards[Board.BOARD_WHITE] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_WHITE]), 35 - 9);

			ulong moves = Moves.Moves_GetAttacks((IntPtr)b, 35);

			Assert.AreEqual((ulong)0x14000000, moves);
		}

		[TestMethod]
		public unsafe void TestBlackPawnCapture3()
		{
			BoardStruct* b = (BoardStruct*)Board.Board_Create();
			b->Boards[Board.BOARD_PAWNS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_PAWNS]), 35);
			b->Boards[Board.BOARD_BLACK] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_BLACK]), 35);

			b->Boards[Board.BOARD_PAWNS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_PAWNS]), 35 - 7);
			b->Boards[Board.BOARD_WHITE] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_WHITE]), 35 - 7);

			ulong moves = Moves.Moves_GetAttacks((IntPtr)b, 35);

			Assert.AreEqual((ulong)0x14000000, moves);
		}

		[TestMethod]
		public unsafe void TestBlackPawnCapture4()
		{
			BoardStruct* b = (BoardStruct*)Board.Board_Create();
			b->Boards[Board.BOARD_PAWNS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_PAWNS]), 15);
			b->Boards[Board.BOARD_BLACK] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_BLACK]), 15);

			b->Boards[Board.BOARD_PAWNS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_PAWNS]), 6);
			b->Boards[Board.BOARD_WHITE] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_WHITE]), 6);

			b->Boards[Board.BOARD_PAWNS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_PAWNS]), 7);
			b->Boards[Board.BOARD_WHITE] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_WHITE]), 7);

			ulong moves = Moves.Moves_GetAttacks((IntPtr)b, 15);

			Assert.AreEqual((ulong)0x40, moves);
		}

		// --------------- non-capture moves, same color ---------------

		[TestMethod]
		public unsafe void TestBlackPawnSame1()
		{
			BoardStruct* b = (BoardStruct*)Board.Board_Create();
			b->Boards[Board.BOARD_PAWNS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_PAWNS]), 48);
			b->Boards[Board.BOARD_BLACK] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_BLACK]), 48);

			b->Boards[Board.BOARD_PAWNS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_PAWNS]), 41);
			b->Boards[Board.BOARD_BLACK] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_BLACK]), 41);

			ulong moves = Moves.Moves_GetAttacks((IntPtr)b, 48);

			Assert.AreEqual((ulong)0x20000000000, moves);
		}

		[TestMethod]
		public unsafe void TestBlackPawnSame2()
		{
			BoardStruct* b = (BoardStruct*)Board.Board_Create();
			b->Boards[Board.BOARD_PAWNS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_PAWNS]), 35);
			b->Boards[Board.BOARD_BLACK] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_BLACK]), 35);

			b->Boards[Board.BOARD_PAWNS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_PAWNS]), 35 - 7);
			b->Boards[Board.BOARD_BLACK] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_BLACK]), 35 - 7);

			b->Boards[Board.BOARD_PAWNS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_PAWNS]), 35 - 9);
			b->Boards[Board.BOARD_BLACK] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_BLACK]), 35 - 9);

			ulong moves = Moves.Moves_GetAttacks((IntPtr)b, 35);

			Assert.AreEqual((ulong)0x14000000, moves);
		}

		[TestMethod]
		public unsafe void TestBlackPawnSame3()
		{
			BoardStruct* b = (BoardStruct*)Board.Board_Create();
			b->Boards[Board.BOARD_PAWNS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_PAWNS]), 35);
			b->Boards[Board.BOARD_BLACK] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_BLACK]), 35);

			b->Boards[Board.BOARD_PAWNS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_PAWNS]), 35 - 7);
			b->Boards[Board.BOARD_BLACK] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_BLACK]), 35 - 7);

			b->Boards[Board.BOARD_PAWNS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_PAWNS]), 35 - 9);
			b->Boards[Board.BOARD_WHITE] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_WHITE]), 35 - 9);

			ulong moves = Moves.Moves_GetAttacks((IntPtr)b, 35);

			Assert.AreEqual((ulong)0x14000000, moves);
		}

		[TestMethod]
		public unsafe void TestBlackPawnSame4()
		{
			BoardStruct* b = (BoardStruct*)Board.Board_Create();
			b->Boards[Board.BOARD_PAWNS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_PAWNS]), 15);
			b->Boards[Board.BOARD_BLACK] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_BLACK]), 15);

			b->Boards[Board.BOARD_PAWNS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_PAWNS]), 6);
			b->Boards[Board.BOARD_BLACK] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_BLACK]), 6);

			ulong moves = Moves.Moves_GetAttacks((IntPtr)b, 15);

			Assert.AreEqual((ulong)0x40, moves);
		}

		[TestMethod]
		public unsafe void TestBlackEnPassantAttack()
		{
			BoardStruct* b = (BoardStruct*)Board.Board_Create();
			Board.Board_SetPiece(b, 28, Board.PIECE_PAWN, Board.COLOR_WHITE);
			Board.Board_SetPiece(b, 29, Board.PIECE_PAWN, Board.COLOR_BLACK);
			b->EnPassantTile = 20;

			ulong attacks = Moves.Moves_GetAttacks(b, 29);
			Assert.AreEqual((ulong)0x500000, attacks);

			ulong moves = Moves.Moves_GetMoves(b, 29);
			Assert.AreEqual((ulong)0x300000, moves);
		}
	}
}