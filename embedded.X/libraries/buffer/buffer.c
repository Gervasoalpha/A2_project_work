//
#include <xc.h>
#include <string.h>
#include "buffer.h"
#include "../constants.h"

char BufferAppend(char item);
char* BufferGet();
int BufferGetSize();
void BufferClear();

char buffer[BUFFER_SIZE];
unsigned int i = 0;

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
 * Get the buffer
 * @return buffer
 */
char* BufferGet()
{
	return buffer;
}

/**
 * Get item length of the buffer
 * @return 
 */
int BufferGetSize()
{
	return(int) i;
}

/**
 * Clears the contents of the buffer
 */
void BufferClear()
{
	memset(buffer, 0, sizeof buffer);
	i = 0;
}