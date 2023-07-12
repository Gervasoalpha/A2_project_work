#ifndef _buffer
#define _buffer

char BufferAppend(unsigned char item);
char BufferAppendArray(unsigned char *items, unsigned char size);
unsigned char* BufferGet();
unsigned char BufferAt(unsigned char i);
unsigned char BufferFindFirst(unsigned char item, unsigned char *i);
unsigned char BufferFindFirstInterval(unsigned char item, unsigned char *i, unsigned char start, unsigned char length);
unsigned char* BufferCopy(unsigned char length);
unsigned char BufferGetSize();
void BufferClear();

#endif