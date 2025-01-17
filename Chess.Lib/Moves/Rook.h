#ifndef ROOK
#define ROOK

#include "../Default.h"
#include "RookMagic.h"

extern "C"
{
	__dllexport void Rook_SetupTables();
	__dllexport int Rook_Load(int pos, uint64_t permutation, uint64_t moveBoard);
	__dllexport void Rook_LoadVector(int pos, uint64_t moveBoard);
	__dllexport uint64_t Rook_Read(int pos, uint64_t occupancy);

	extern uint64_t** Rook_Tables;

	// an array containing bitboard with the unmasked rook move vectors
	extern uint64_t Rook_Vectors[64];

	// an array containing the number of bits to shift the magic constant to get the index
	extern uint64_t Rook_Shift[64];

	inline int Rook_Index(int pos, uint64_t permutation)
	{
		uint64_t val = (permutation * Rook_Magic[pos]);
		uint32_t shift = (int)Rook_Shift[pos];
		int index = (int)(val >> shift);
		return index;
	}

	/// Create all lookup tables and initialize them to zeros
	inline void Rook_SetupTables()
	{
		Rook_Tables = new uint64_t*[64];

		for(int i=0; i<64; i++)
		{
			uint64_t bits = Rook_Bits[i];
			Rook_Shift[i] = 64 - Rook_Bits[i];
			int len = 1 << bits;
			Rook_Tables[i] = new uint64_t[len];
			for(int k=0; k<len; k++)
				Rook_Tables[i][k] = (uint64_t)0;
		}
	}

	/// Loads a new entry into the LUT. If if the table contains an unexpected value (collision)
	/// the function returns 1. Returns 0 on success.
	/// Note: There should never, ever be a collision if the magic constant is correct. 
	/// This is only for testing and safety reasons.
	inline int Rook_Load(int pos, uint64_t permutation, uint64_t moveBoard)
	{
		uint64_t* table = Rook_Tables[pos];
		int index = Rook_Index(pos, permutation);

		if(table[index] == 0 || table[index] == moveBoard)
		{
			table[index] = moveBoard;
			return 0;
		}
		else
			return 1;

	}

	inline void Rook_LoadVector(int pos, uint64_t moveBoard)
	{
		Rook_Vectors[pos] = moveBoard;
	}

	__inline_always uint64_t Rook_Read(int pos, uint64_t occupancy)
	{
		uint64_t permutation = occupancy & Rook_Vectors[pos];

		uint64_t* table = Rook_Tables[pos];
		int index = Rook_Index(pos, permutation);

		uint64_t moveBoard = table[index];
		return moveBoard;
	}

}

#endif
