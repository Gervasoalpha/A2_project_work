//
#include <xc.h>
#include <string.h>
#include "buffer.h"
#include "../constants.h"

char BufferAppend(unsigned char item);
char BufferAppendArray(unsigned char *items, unsigned int size);
unsigned char* BufferGet();
unsigned char* BufferCopy();
unsigned char BufferAt(unsigned int i);
unsigned char BufferFindFirst(unsigned char item, unsigned int *i);
unsigned int BufferGetSize();
void BufferClear();

unsigned char buffer[BUFFER_SIZE];
unsigned int index = 0;

/**
 * Appends an item to the end of the buffer
 * @param item to be appended
 * @return true if the item was successfully appended
 */
char BufferAppend(unsigned char item)
{
	if (index >= BUFFER_SIZE)
	{
		return 0;
	}
	//
	buffer[index] = item;
	index++;
	return 1;
}

/**
 * Appends an array of items to the buffer
 * @param items	array of items to be appended
 * @param size number of items of the array to append to the buffer
 * @return true if all the specified items were successfully appended
 */
char BufferAppendArray(unsigned char *items, unsigned int size)
{
	if (size == 0 || size > (BUFFER_SIZE - index))
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
	return index;
}

/**
 * Get the item at the specified index
 * @param i index
 * @return the item if it exists
 */
unsigned char BufferAt(unsigned int i)
{
	if (i >= index)
	{
		return 0;
	}
	//
	return buffer[i];
}

/**
 * Find the first item occurrence in the buffer and returns it
 * @param item to be found
 * @param i return the index of the found item
 * @return the item if found, 0 otherwise
 */
unsigned char BufferFindFirst(unsigned char item, unsigned int *i)
{
	for (unsigned int k = 0; k < index; k++)
	{
		if (buffer[k] == item)
		{
			*i = k;
			return buffer[k];
		}
	}
	return 0;
}

/**
 * Clears the contents of the buffer
 */
void BufferClear()
{
	memset(buffer, 0, sizeof buffer);
	index = 0;
}

unsigned char* BufferCopy()
{
	unsigned char newBuffer[BUFFER_SIZE];
	memcpy(newBuffer, buffer, index);
	return newBuffer;
}