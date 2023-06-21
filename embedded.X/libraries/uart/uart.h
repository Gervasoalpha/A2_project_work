#ifndef _uart
#define _uart

void UARTInit();
char UARTTxChar(char c);
char UARTHasReceived();
char UARTGetReceived();

#endif