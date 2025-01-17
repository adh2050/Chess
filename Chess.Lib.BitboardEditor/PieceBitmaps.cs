﻿using Chess.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Chess.Lib.BitboardEditor
{
	public class PieceBitmaps
	{
		public static Bitmap WhiteBishop;
		public static Bitmap WhiteKing;
		public static Bitmap WhiteKnight;
		public static Bitmap WhitePawn;
		public static Bitmap WhiteQueen;
		public static Bitmap WhiteRook;

		public static Bitmap BlackBishop;
		public static Bitmap BlackKing;
		public static Bitmap BlackKnight;
		public static Bitmap BlackPawn;
		public static Bitmap BlackQueen;
		public static Bitmap BlackRook;

		// dir = @"C:\Src\_Tree\Applications\Chess\Pieces\";
		static string _dir;
		public static string Dir
		{
			get { return _dir; }
			set
			{
				_dir = value;
				LoadBitmaps();
			}
		}

		static void LoadBitmaps()
		{
			WhiteBishop = new Bitmap(Dir + "white\\bishop.png");
			WhiteKing = new Bitmap(Dir + "white\\king.png");
			WhiteKnight = new Bitmap(Dir + "white\\knight.png");
			WhitePawn = new Bitmap(Dir + "white\\pawn.png");
			WhiteQueen = new Bitmap(Dir + "white\\queen.png");
			WhiteRook = new Bitmap(Dir + "white\\rook.png");

			BlackBishop = new Bitmap(Dir + "black\\bishop.png");
			BlackKing = new Bitmap(Dir + "black\\king.png");
			BlackKnight = new Bitmap(Dir + "black\\knight.png");
			BlackPawn = new Bitmap(Dir + "black\\pawn.png");
			BlackQueen = new Bitmap(Dir + "black\\queen.png");
			BlackRook = new Bitmap(Dir + "black\\rook.png");
		}

		public static Bitmap GetBitmap(int piece)
		{
			Chess.Base.Color color = Colors.Get(piece);
			Piece type = Pieces.Get(piece);

			if (color == Chess.Base.Color.White)
			{
				switch (type)
				{
					case Piece.Bishop:
						return WhiteBishop;
					case Piece.King:
						return WhiteKing;
					case Piece.Knight:
						return WhiteKnight;
					case Piece.Pawn:
						return WhitePawn;
					case Piece.Queen:
						return WhiteQueen;
					case Piece.Rook:
						return WhiteRook;
				}
			}
			if (color == Chess.Base.Color.Black)
			{
				switch (type)
				{
					case Piece.Bishop:
						return BlackBishop;
					case Piece.King:
						return BlackKing;
					case Piece.Knight:
						return BlackKnight;
					case Piece.Pawn:
						return BlackPawn;
					case Piece.Queen:
						return BlackQueen;
					case Piece.Rook:
						return BlackRook;
				}
			}

			return null;
		}
	}
}
