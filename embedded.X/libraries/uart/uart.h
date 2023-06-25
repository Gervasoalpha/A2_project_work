#ifndef _uart
#define _uart

void UARTInit();
char UARTTxChar(char c);
void UARTInterrupt();
char UARTGetReceived();

char UARTHasReceived;

#endif