//
#include <xc.h>
#include <string.h>
#include "buffer.h"
#include "../constants.h"

char BufferAppend(char item);
char BufferGetSize();
void BufferClear();

char buffer[BUFFER_SIZE];
uint16_t i = 0;

/**
 * Appends an item to the end of the buffer
 * @param item to be appended
 * @return true if the item was succesfully appended
 */
char BufferAppend(char item)
{
	if (i >= BUFFER_SIZE)
	{
		return 0;
	} else
	{
		buffer[i] = item;
		i++;
		return 1;
	}
}

/**
 * Get item length of the buffer
 * @return 
 */
char BufferGetSize()
{
	return i;
}

/**
 * Clears the contents of the buffer
 */
void BufferClear()
{
	memset(buffer, 0, sizeof buffer);
	i = 0;
}