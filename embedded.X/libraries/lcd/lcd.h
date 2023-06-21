#ifndef _lcd
#define _lcd

void LcdInit();
void LcdSendString(char *string);
void LcdSetCursor(char row, char column);
void LcdClear();

#endif