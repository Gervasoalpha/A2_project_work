#ifndef _lcd
#define _lcd

void LcdInit();
void LcdSendChar(char c);
void LcdSendString(char *string);
void LcdSetCursor(unsigned char row, unsigned char column);
void LcdSendInt(int n);
void LcdClear();

#endif