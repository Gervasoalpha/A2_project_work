#ifndef _buffer
#define _buffer

char BufferAppend(unsigned char item);
char BufferAppendArray(unsigned char *items, unsigned int size);
unsigned char* BufferGet();
unsigned int BufferGetSize();
void BufferClear();

#endif