#ifndef KNIGHT
#define KNIGHT

#include "../Default.h"

extern "C"
{
	__dllexport void Knight_Load(int pos, uint64_t moveBoard);
	__dllexport uint64_t Knight_Read(int pos);

	extern uint64_t Knight_Table[64];

	/// Loads a new move board into the KnightTable array
	inline void Knight_Load(int pos, uint64_t moveBoard)
	{
		Knight_Table[pos] = moveBoard;
	}

	__inline_always uint64_t Knight_Read(int pos)
	{
		uint64_t val = Knight_Table[pos];
		return val;
	}

}

#endif
