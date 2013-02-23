
#include "Moves.h"
#include "Bitboard.h"
#include "Board.h"
#include "Moves/Pawn.h"
#include "Moves/Knight.h"
#include "Moves/Bishop.h"
#include "Moves/Rook.h"
#include "Moves/King.h"
#include "Moves/Queen.h"

// converts castling types to bitboard og possible king moves
// the key is castling type
uint64_t CastlingTableTypesToBitboard[16];

void Moves_Init()
{
	CastlingTableTypesToBitboard[CASTLE_WK]             = 0x40;
	CastlingTableTypesToBitboard[CASTLE_WQ]             = 0x4;
	CastlingTableTypesToBitboard[CASTLE_WK | CASTLE_WQ] = 0x44;

	CastlingTableTypesToBitboard[CASTLE_BK]             = 0x4000000000000000;
	CastlingTableTypesToBitboard[CASTLE_BQ]             = 0x400000000000000;
	CastlingTableTypesToBitboard[CASTLE_BK | CASTLE_BQ] = 0x4400000000000000;
}

uint64_t Moves_GetMoves(Board* board, int tile)
{
	int piece = Board_Piece(board, tile);
	int color = Board_Color(board, tile);
	uint64_t value = 0;
	uint64_t occupancy = board->Boards[BOARD_WHITE] | board->Boards[BOARD_BLACK];

	if(piece == 0 || color == 0)
		return 0;

	if(piece == PIECE_PAWN)
	{
		if(color == COLOR_WHITE)
		{
			uint64_t moves = Pawn_ReadWhiteMove(tile);
			uint64_t attacks = Pawn_ReadWhiteAttack(tile);
			uint64_t enPassant = Moves_GetEnPassantMove(board, tile);

			moves = moves & ~occupancy;
			attacks = attacks & board->Boards[BOARD_BLACK];
			return moves | attacks | enPassant;
		}
		else
		{
			uint64_t moves = Pawn_ReadBlackMove(tile);
			uint64_t attacks = Pawn_ReadBlackAttack(tile);
			uint64_t enPassant = Moves_GetEnPassantMove(board, tile);

			moves = moves & ~occupancy;
			attacks = attacks & board->Boards[BOARD_WHITE];
			return moves | attacks | enPassant;
		}
	}

	switch(piece)
	{
	case(PIECE_KNIGHT):
		value = Knight_Read(tile);
		break;
	case(PIECE_BISHOP):
		value = Bishop_Read(tile, occupancy);
		break;
	case(PIECE_ROOK):
		value = Rook_Read(tile, occupancy);
		break;
	case(PIECE_QUEEN):
		value = Queen_Read(tile, occupancy);
		break;
	case(PIECE_KING):
		value = King_Read(tile);
		value |= (tile == 4 || tile == 60) ? Moves_GetCastlingMoves(board, color) : 0;
		break;
	}

	if(color == COLOR_WHITE)
		value = value & ~(board->Boards[BOARD_WHITE]);
	else
		value = value & ~(board->Boards[BOARD_BLACK]);

	return value;
}

uint64_t Moves_GetAttacks(Board* board, int tile)
{
	int piece = Board_Piece(board, tile);
	int color = Board_Color(board, tile);
	uint64_t value = 0;
	uint64_t occupancy = board->Boards[BOARD_WHITE] | board->Boards[BOARD_BLACK];

	if(piece == 0 || color == 0)
		return 0;

	if(piece == PIECE_PAWN)
	{
		uint64_t enPassant = Moves_GetEnPassantMove(board, tile);

		if(color == COLOR_WHITE)
		{
			uint64_t attacks = Pawn_ReadWhiteAttack(tile);
			return attacks | enPassant;
		}
		else
		{
			uint64_t attacks = Pawn_ReadBlackAttack(tile);
			return attacks | enPassant;
		}
	}

	switch(piece)
	{
	case(PIECE_KNIGHT):
		value = Knight_Read(tile);
		break;
	case(PIECE_BISHOP):
		value = Bishop_Read(tile, occupancy);
		break;
	case(PIECE_ROOK):
		value = Rook_Read(tile, occupancy);
		break;
	case(PIECE_QUEEN):
		value = Queen_Read(tile, occupancy);
		break;
	case(PIECE_KING):
		value = King_Read(tile);
		break;
	}

	return value;
}

uint8_t Moves_GetCastlingType(Board* board, int from, int to)
{
	if(from == 4)
	{
		uint64_t whiteKing = board->Boards[BOARD_WHITE] & board->Boards[BOARD_KINGS];
		int isKing = Bitboard_GetRef(&whiteKing, 4);
		return isKing * (((to == 2) * CASTLE_WQ) | ((to == 6) * CASTLE_WK));
	}
	else if(from == 60)
	{
		uint64_t blackKing = board->Boards[BOARD_BLACK] & board->Boards[BOARD_KINGS];
		int isKing = Bitboard_GetRef(&blackKing, 60);

		return isKing * (((to == 58) * CASTLE_BQ) | ((to == 62) * CASTLE_BK));
	}

	return 0;
}