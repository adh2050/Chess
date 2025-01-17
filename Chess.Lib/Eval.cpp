
#include <string.h>
#include "Eval.h"
#include "Moves.h"
#include "Moves/King.h"
#include "Search.h"

int Eval_PieceValues[8];
int Eval_MobilityBonus[8];
int Eval_UndefendedPiecePenalty[8];
int* Eval_Positions[8];

// NOTE: These arrays are not in the correct order
// they are written so that they are easy to read by humans
// Eval_ReverseArray() is used to flip each line so that they are correct
int Eval_PositionPawn[64] = {
	  0,   0,   0,   0,   0,   0,   0,   0,
	 25,  25,  30,  35,  35,  30,  25,  25,
	  4,   8,  15,  20,  20,  15,   8,   4,
	  0,   6,   8,  15,  15,   8,   6,   0,
	 -3,   4,   5,  10,  10,   5,   4,  -3,
	 -5,   2,   2, -10, -10,   2,   2,  -5,
	  5,   5,   5, -30, -30,   5,   5,   5,
	  0,   0,   0,   0,   0,   0,   0,   0
};

int Eval_PositionKnight[64] = {
	-10, -10, -10, -10, -10, -10, -10, -10,
	-10,   0,   0,   0,   0,   0,   0, -10,
	-10,   0,   5,   5,   5,   5,   0, -10,
	-10,   0,   5,  15,  15,   5,   0, -10,
	-10,   0,   8,  15,  15,   8,   0, -10,
	-10,   0,  15,   5,   5,  15,   0, -10,
	-10,   0,   0,   0,   0,   0,   0, -10,
	-10, -30, -10, -10, -10, -10, -30, -10
};

int Eval_PositionBishop[64] = {
	-10, -10, -10, -10, -10, -10, -10, -10,
	-10,   0,   0,   0,   0,   0,   0, -10,
	-10,   0,   3,   5,   5,   3,   0, -10,
	-10,   0,   5,  12,  12,   5,   0, -10,
	-10,   0,   5,  15,  15,   5,   0, -10,
	-10,   0,   3,   5,   5,   3,   0, -10,
	-10,   0,   0,   0,   0,   0,   0, -10,
	-10, -10, -20, -10, -10, -20, -10, -10
};

int Eval_PositionRook[64] = {
	  0,   0,   0,   0,   0,   0,   0,   0,
	  0,   0,   0,   0,   0,   0,   0,   0,
	  0,   0,   0,   0,   0,   0,   0,   0,
	  0,   0,   0,   0,   0,   0,   0,   0,
	  0,   0,   0,   0,   0,   0,   0,   0,
	  0,   0,   0,   0,   0,   0,   0,   0,
	  0,   0,   0,   0,   0,   0,   0,   0,
	-10,  -5,   0,  10,  10,   0,  -5, -10
};

int Eval_PositionQueen[64] = {
	  0,   0,   0,   0,   0,   0,   0,   0,
	  0,   0,   0,   0,   0,   0,   0,   0,
	  0,   0,   0,   0,   0,   0,   0,   0,
	  0,   0,   0,   0,   0,   0,   0,   0,
	  0,   0,   0,   0,   0,   0,   0,   0,
	  0,   0,   0,   0,   0,   0,   0,   0,
	  0,   0,   0,   0,   0,   0,   0,   0,
	  0,   0,   0,   0,   0,   0,   0,   0
};

int Eval_PositionKing[64] = {
	-10, -10, -10, -10, -10, -10, -10, -10,
	-10, -10, -10, -10, -10, -10, -10, -10,
	-10, -10, -10, -10, -10, -10, -10, -10,
	-10, -10, -10, -10, -10, -10, -10, -10,
	-10, -10, -10, -10, -10, -10, -10, -10,
	-10, -10, -10, -10, -10, -10, -10, -10,
	 10,  10, -10, -10, -10, -10,  10,  10,
	 15,  10,  30, -10,   0, -10,  30,  15
};

int Eval_PositionKingEndgame[64] = {
	-90, -70, -30, -10, -10, -30, -70, -90,
	-70, -25,   0,   0,   0,   0, -25, -70,
	-30,   0,   8,   5,   5,   8,   0, -30,
	-10,   0,   5,  10,  10,   5,   0, -10,
	-10,   0,   5,  10,  10,   5,   0, -10,
	-30,   0,   8,   5,   5,   8,   0, -30,
	-70, -25,   0,   0,   0,   0, -25, -70,
	-90, -70, -30, -10, -10, -30, -70, -90,
};

int Eval_Flip[64] = {
	 56,  57,  58,  59,  60,  61,  62,  63,
	 48,  49,  50,  51,  52,  53,  54,  55,
	 40,  41,  42,  43,  44,  45,  46,  47,
	 32,  33,  34,  35,  36,  37,  38,  39,
	 24,  25,  26,  27,  28,  29,  30,  31,
	 16,  17,  18,  19,  20,  21,  22,  23,
	  8,   9,  10,  11,  12,  13,  14,  15,
	  0,   1,   2,   3,   4,   5,   6,   7
};

// Reverse the array for the evaluation tables into the correct format
void Eval_ReverseArray(int* array64)
{
	int temp[64];
	memcpy(temp, array64, 64 * sizeof(int));

	for(int i = 0; i < 64; i++)
	{
		int line = i / 8;
		int fromLine = 7 - line;
		int col = i % 8;

		array64[i] = temp[fromLine * 8 + col];
	}
}

void Eval_Init()
{
	Eval_PieceValues[0]            = 0;
	Eval_PieceValues[1]            = 0;
	Eval_PieceValues[PIECE_PAWN]   = 100;
	Eval_PieceValues[PIECE_KNIGHT] = 320;
	Eval_PieceValues[PIECE_BISHOP] = 330;
	Eval_PieceValues[PIECE_ROOK]   = 500;
	Eval_PieceValues[PIECE_QUEEN]  = 900;
	Eval_PieceValues[PIECE_KING]   = 100000;

	Eval_MobilityBonus[PIECE_PAWN]   = 7;
	Eval_MobilityBonus[PIECE_KNIGHT] = 7;
	Eval_MobilityBonus[PIECE_BISHOP] = 5;
	Eval_MobilityBonus[PIECE_ROOK]   = 5;
	Eval_MobilityBonus[PIECE_QUEEN]  = 5;
	Eval_MobilityBonus[PIECE_KING]   = 10;

	Eval_UndefendedPiecePenalty[PIECE_PAWN]   = 0;
	Eval_UndefendedPiecePenalty[PIECE_KNIGHT] = 10;
	Eval_UndefendedPiecePenalty[PIECE_BISHOP] = 10;
	Eval_UndefendedPiecePenalty[PIECE_ROOK]   = 20;
	Eval_UndefendedPiecePenalty[PIECE_QUEEN]  = 20;
	Eval_UndefendedPiecePenalty[PIECE_KING]   = 20;

	Eval_ReverseArray(Eval_PositionPawn);
	Eval_ReverseArray(Eval_PositionKnight);
	Eval_ReverseArray(Eval_PositionBishop);
	Eval_ReverseArray(Eval_PositionKing);
	Eval_ReverseArray(Eval_PositionKingEndgame);

	Eval_Positions[PIECE_PAWN] = Eval_PositionPawn;
	Eval_Positions[PIECE_KNIGHT] = Eval_PositionKnight;
	Eval_Positions[PIECE_BISHOP] = Eval_PositionBishop;
	Eval_Positions[PIECE_ROOK] = Eval_PositionRook;
	Eval_Positions[PIECE_QUEEN] = Eval_PositionQueen;
	Eval_Positions[PIECE_KING] = Eval_PositionKing;
}

#ifdef STATS_EVAL
EvalStats EStats[33];
#endif

typedef struct
{
	uint64_t Hash;
	int Score;
	
} EvalEntry;

const int Eval_EntryCount = 10000000;
const int Eval_NoEntry = 1000000000;
EvalEntry Eval_Entries[Eval_EntryCount];

int Eval_ReadEntry(uint64_t hash)
{
	int index = hash % Eval_EntryCount;
	EvalEntry* entry = &Eval_Entries[index];
	return (entry->Hash == hash) ? entry->Score : 1000000000;
}

void Eval_WriteEntry(uint64_t hash, int score)
{
	int index = hash % Eval_EntryCount;
	EvalEntry* entry = &Eval_Entries[index];
	entry->Hash = hash;
	entry->Score = score;
}

int Eval_Evaluate(Board* board)
{
	#ifdef STATS_SEARCH
	SStats.EvalTotal++;
	#endif

	int stored = Eval_ReadEntry(board->Hash);
	if(stored != Eval_NoEntry)
	{
		#ifdef STATS_SEARCH
		SStats.EvalHits++;
		#endif

		return stored;
	}

	#ifdef STATS_EVAL
	EStats[COLOR_WHITE].DoublePawnPenalty = 0;
	EStats[COLOR_WHITE].IsolatedPawnPenalty = 0;
	EStats[COLOR_WHITE].Material = 0;
	EStats[COLOR_WHITE].MobilityBonus = 0;
	EStats[COLOR_WHITE].PositionalBonus = 0;
	EStats[COLOR_WHITE].TempoBonus = 0;
	EStats[COLOR_WHITE].UndefendedPenalty = 0;
	EStats[COLOR_BLACK].DoublePawnPenalty = 0;
	EStats[COLOR_BLACK].IsolatedPawnPenalty = 0;
	EStats[COLOR_BLACK].Material = 0;
	EStats[COLOR_BLACK].MobilityBonus = 0;
	EStats[COLOR_BLACK].PositionalBonus = 0;
	EStats[COLOR_BLACK].TempoBonus = 0;
	EStats[COLOR_BLACK].UndefendedPenalty = 0;
	#endif

	uint64_t attacksWhite = Board_AttackMap(board, COLOR_WHITE);
	uint64_t attacksBlack = Board_AttackMap(board, COLOR_BLACK);

	int whiteValue = 0;
	int blackValue = 0;

	uint8_t* tiles = board->Tiles;

	for(int i = 0; i < 64; i++)
	{
		if(tiles[i] == 0)
			continue;

		int pValue = 0;
		int square = i;
		int x = Board_X(square);
		//int y = Board_Y(square);
		int piece = Board_Piece(board, square);
		int color = Board_Color(board, square);

		// index to read position tables
		int index = square;
		if(color == COLOR_BLACK)
			index = Eval_Flip[index];
		
		pValue += Eval_PieceValues[piece];
		pValue += Eval_Positions[piece][index];
//		pValue += Eval_MobilityBonus[piece] * moveCount;

		#ifdef STATS_EVAL
		EStats[color].Material += Eval_PieceValues[piece];
		EStats[color].PositionalBonus += Eval_Positions[piece][index];
//		EStats[color].MobilityBonus += Eval_MobilityBonus[piece] * moveCount;
		#endif

		if(color == COLOR_WHITE)
		{
			if(Bitboard_GetRef(&attacksWhite, square) == 0)
				pValue -= Eval_UndefendedPiecePenalty[piece];

			#ifdef STATS_EVAL
			EStats[color].UndefendedPenalty -= Eval_UndefendedPiecePenalty[piece];
			#endif

			if(piece == PIECE_PAWN)
			{
				uint64_t whitePawns = board->Boards[BOARD_WHITE] & board->Boards[BOARD_PAWNS];
				uint64_t pawnsOnFile = Bitboard_Files[x] & whitePawns;
				if(Bitboard_PopCount(pawnsOnFile) > 1)
				{
					pValue -= Eval_Penalty_DoublePawn;
					#ifdef STATS_EVAL
					EStats[color].DoublePawnPenalty -= Eval_Penalty_DoublePawn;
					#endif
				}

				// get all the squares around the pawn, I just use the King_Move table for that
				uint64_t pawnAreaBoard = King_Read(square);
				if((whitePawns & pawnAreaBoard) == 0)
				{
					pValue -= Eval_Penalty_IsolatedPawn;
					#ifdef STATS_EVAL
					EStats[color].IsolatedPawnPenalty -= Eval_Penalty_IsolatedPawn;
					#endif
				}
			}
		}
		else
		{
			if(Bitboard_GetRef(&attacksBlack, square) == 0)
				pValue -= Eval_UndefendedPiecePenalty[piece];

			#ifdef STATS_EVAL
			EStats[color].UndefendedPenalty -= Eval_UndefendedPiecePenalty[piece];
			#endif

			if(piece == PIECE_PAWN)
			{
				uint64_t blackPawns = board->Boards[BOARD_BLACK] & board->Boards[BOARD_PAWNS];
				uint64_t pawnsOnFile = Bitboard_Files[x] & blackPawns;
				if(Bitboard_PopCount(pawnsOnFile) > 1)
				{
					pValue -= Eval_Penalty_DoublePawn;
					#ifdef STATS_EVAL
					EStats[color].DoublePawnPenalty -= Eval_Penalty_DoublePawn;
					#endif
				}

				// get all the squares around the pawn, I just use the King_Move table for that
				uint64_t pawnAreaBoard = King_Read(square);
				if((blackPawns & pawnAreaBoard) == 0)
				{
					pValue -= Eval_Penalty_IsolatedPawn;
					#ifdef STATS_EVAL
					EStats[color].IsolatedPawnPenalty -= Eval_Penalty_IsolatedPawn;
					#endif
				}
			}
		}

		if(color == COLOR_WHITE)
			whiteValue += pValue;
		else
			blackValue += pValue;
	}

	int whiteAttackCount = Bitboard_PopCount(attacksWhite);
	int blackAttackCount = Bitboard_PopCount(attacksBlack);

	whiteValue += whiteAttackCount * 3;
	blackValue += blackAttackCount * 3;

	#ifdef STATS_EVAL
	EStats[COLOR_WHITE].MobilityBonus = whiteAttackCount * 3;
	EStats[COLOR_BLACK].MobilityBonus = blackAttackCount * 3;
	#endif

	// slight bonus for current player
	if(board->PlayerTurn == COLOR_WHITE)
		whiteValue += Eval_Bonus_CurrentPlayer;
	else
		blackValue += Eval_Bonus_CurrentPlayer;

	#ifdef STATS_EVAL
	EStats[board->PlayerTurn].TempoBonus += Eval_Bonus_CurrentPlayer;
	#endif

	int output = whiteValue - blackValue;

	if(board->PlayerTurn == COLOR_BLACK)
		output = -output;

	Eval_WriteEntry(board->Hash, output);
	return output;
}

EvalStats* Eval_GetEvalStats(int color)
{
#ifdef STATS_EVAL
	return &EStats[color];
#else
	return 0;
#endif
}