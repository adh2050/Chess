﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chess.Lib.MoveClasses;

namespace Chess.Lib.Tests
{
	[TestClass]
	public class KnightTest
	{
		[TestMethod]
		public unsafe void TestKnightMoves1()
		{
			BoardStruct* b = (BoardStruct*)Board.Board_Create();
			b->Boards[Board.BOARD_KNIGHTS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_KNIGHTS]), 27);
			b->Boards[Board.BOARD_WHITE] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_WHITE]), 27);

			ulong moves = Moves.Moves_GetMoves((IntPtr)b, 27);
			Assert.AreEqual((ulong)0x142200221400, moves);
		}

		[TestMethod]
		public unsafe void TestKnightMoves2()
		{
			BoardStruct* b = (BoardStruct*)Board.Board_Create();
			b->Boards[Board.BOARD_KNIGHTS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_KNIGHTS]), 14);
			b->Boards[Board.BOARD_WHITE] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_WHITE]), 14);

			ulong moves = Moves.Moves_GetMoves((IntPtr)b, 14);
			Assert.AreEqual((ulong)0xA0100010, moves);
		}

		[TestMethod]
		public unsafe void TestKnightMoves1WhiteBlocking()
		{
			BoardStruct* b = (BoardStruct*)Board.Board_Create();
			b->Boards[Board.BOARD_KNIGHTS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_KNIGHTS]), 27);
			b->Boards[Board.BOARD_WHITE] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_WHITE]), 27);

			// set 2 white pawns tha block moves
			b->Boards[Board.BOARD_PAWNS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_PAWNS]), 17);
			b->Boards[Board.BOARD_WHITE] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_WHITE]), 17);
			b->Boards[Board.BOARD_PAWNS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_PAWNS]), 44);
			b->Boards[Board.BOARD_WHITE] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_WHITE]), 44);

			ulong moves = Moves.Moves_GetMoves((IntPtr)b, 27);
			Assert.AreEqual((ulong)0x42200201400, moves);

			// white can still "attack" those squares
			ulong attacks = Moves.Moves_GetAttacks((IntPtr)b, 27);
			Assert.AreEqual((ulong)0x142200221400, attacks);
		}

		[TestMethod]
		public unsafe void TestKnightMoves2BWhiteBlocking()
		{
			BoardStruct* b = (BoardStruct*)Board.Board_Create();
			b->Boards[Board.BOARD_KNIGHTS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_KNIGHTS]), 14);
			b->Boards[Board.BOARD_WHITE] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_WHITE]), 14);

			// set 2 white pawns tha block moves
			b->Boards[Board.BOARD_PAWNS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_PAWNS]), 20);
			b->Boards[Board.BOARD_WHITE] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_WHITE]), 20);
			b->Boards[Board.BOARD_PAWNS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_PAWNS]), 29);
			b->Boards[Board.BOARD_WHITE] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_WHITE]), 29);

			ulong moves = Moves.Moves_GetMoves((IntPtr)b, 14);
			Assert.AreEqual((ulong)0x80000010, moves);

			ulong attacks = Moves.Moves_GetAttacks((IntPtr)b, 14);
			Assert.AreEqual((ulong)0xA0100010, attacks);
		}

		[TestMethod]
		public unsafe void TestKnightMoves1BlockOpponent()
		{
			BoardStruct* b = (BoardStruct*)Board.Board_Create();
			b->Boards[Board.BOARD_KNIGHTS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_KNIGHTS]), 27);
			b->Boards[Board.BOARD_WHITE] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_WHITE]), 27);

			// set 2 black pawns, they don't block moves
			b->Boards[Board.BOARD_PAWNS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_PAWNS]), 17);
			b->Boards[Board.BOARD_BLACK] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_BLACK]), 17);
			b->Boards[Board.BOARD_PAWNS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_PAWNS]), 44);
			b->Boards[Board.BOARD_BLACK] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_BLACK]), 44);

			// attacks and moves should be equal
			ulong moves = Moves.Moves_GetMoves((IntPtr)b, 27);
			Assert.AreEqual((ulong)0x142200221400, moves);

			// white can still "attack" those squares
			ulong attacks = Moves.Moves_GetAttacks((IntPtr)b, 27);
			Assert.AreEqual((ulong)0x142200221400, attacks);
		}

		[TestMethod]
		public unsafe void TestKnightMoves2BlockOpponent()
		{
			BoardStruct* b = (BoardStruct*)Board.Board_Create();
			b->Boards[Board.BOARD_KNIGHTS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_KNIGHTS]), 14);
			b->Boards[Board.BOARD_WHITE] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_WHITE]), 14);

			// set 2 black pawns, they don't block moves
			b->Boards[Board.BOARD_PAWNS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_PAWNS]), 20);
			b->Boards[Board.BOARD_BLACK] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_BLACK]), 20);
			b->Boards[Board.BOARD_PAWNS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_PAWNS]), 29);
			b->Boards[Board.BOARD_BLACK] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_BLACK]), 29);

			// attacks and moves should be equal
			ulong moves = Moves.Moves_GetMoves((IntPtr)b, 14);
			Assert.AreEqual((ulong)0xA0100010, moves);

			ulong attacks = Moves.Moves_GetAttacks((IntPtr)b, 14);
			Assert.AreEqual((ulong)0xA0100010, attacks);
		}

		[TestMethod]
		public unsafe void TestKnightBlack1()
		{
			BoardStruct* b = (BoardStruct*)Board.Board_Create();
			b->Boards[Board.BOARD_KNIGHTS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_KNIGHTS]), 41);
			b->Boards[Board.BOARD_BLACK] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_BLACK]), 41);

			// set 1 black pawn, 1 white pawn
			b->Boards[Board.BOARD_PAWNS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_PAWNS]), 51);
			b->Boards[Board.BOARD_BLACK] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_BLACK]), 51);
			b->Boards[Board.BOARD_PAWNS] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_PAWNS]), 35);
			b->Boards[Board.BOARD_WHITE] = Bitboard.Bitboard_Set((b->Boards[Board.BOARD_WHITE]), 35);

			ulong moves = Moves.Moves_GetMoves((IntPtr)b, 41);
			Assert.AreEqual((ulong)0x500000805000000, moves);

			ulong attacks = Moves.Moves_GetAttacks((IntPtr)b, 41);
			Assert.AreEqual((ulong)0x508000805000000, attacks);
		}

	}
}