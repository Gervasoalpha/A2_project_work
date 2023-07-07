#ifndef _buffer
#define _buffer

char BufferAppend(unsigned char item);
char BufferAppendArray(unsigned char *items, unsigned int size);
unsigned char* BufferGet();
unsigned char BufferAt(unsigned int index);
unsigned char* BufferCopy();
unsigned int BufferGetSize();
void BufferClear();

#endif