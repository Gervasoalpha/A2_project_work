//
#include <xc.h>
#include <string.h>
#include "buffer.h"
#include "../constants.h"

char BufferAppend(unsigned char item);
char BufferAppendArray(unsigned char *items, unsigned int size);
unsigned char* BufferGet();
unsigned char BufferAt(unsigned int index);
unsigned int BufferGetSize();
void BufferClear();

unsigned char buffer[BUFFER_SIZE];
unsigned int i = 0;

/**
 * Appends an item to the end of the buffer
 * @param item to be appended
 * @return true if the item was succesfully appended
 */
char BufferAppend(unsigned char item)
{
	if (i >= BUFFER_SIZE)
	{
		return 0;
	}
	//
	buffer[i] = item;
	i++;
	return 1;
}

/**
 * Appends an array of items to the buffer
 * @param items	array of items to be appended
 * @param size number of items of the array to append to the buffer
 * @return true if all the spcified items were succesfully appended
 */
char BufferAppendArray(unsigned char *items, unsigned int size)
{
	if (size == 0 || size > (BUFFER_SIZE - i))
	{
		return 0;
	}
	//
	unsigned int i;
	for (i = 0; i < size; i++)
	{
		if (!BufferAppend(items[i]))
		{
			return 0;
		}
	}
	return 1;
}

/**
 * Get the buffer
 * @return buffer
 */
unsigned char* BufferGet()
{
	return buffer;
}

/**
 * Get item length of the buffer
 * @return 
 */
unsigned int BufferGetSize()
{
	return i;
}

/**
 * Get the item at the specified index
 * @param index
 * @return the item if it exists
 */
unsigned char BufferAt(unsigned int index)
{
	if (index >= i)
	{
		return 0;
	}
	//
	return buffer[index];
}

/**
 * Clears the contents of the buffer
 */
void BufferClear()
{
	memset(buffer, 0, sizeof buffer);
	i = 0;
}