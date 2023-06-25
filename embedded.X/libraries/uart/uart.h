#ifndef _uart
#define _uart

void UARTInit();
void UARTTxChar(char c);
void UARTTxString(char* string);
char UARTInterrupt(unsigned char *ByteReceived);

#endif