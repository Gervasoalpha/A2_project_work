
#include <xc.h>
#include "uart.h"
#include "../constants.h"

void UARTInit();
char UARTTxChar(char c);
void UARTInterrupt();
char UARTGetReceived();

char UARTHasReceived = 0;

/**
 * Initialize UART Serial communication
 */
void UARTInit()
{
	TXSTA |= 0x24;
	RCSTA = 0x90;
	SPBRG = (char) (_XTAL_FREQ / (long) (64UL * BAUD_RATE)) - 1;
	INTCON |= 0x80;
	INTCON |= 0x40;
	PIE1 |= 0x20;
}

/**
 * Send a char to the serial port
 * @param c byte to send
 * @return true if data was sent
 */
char UARTTxChar(char c)
{
	TRISC &= ~0x40;
	TRISC |= 0x80;
	while (!(PIR1 & 0x10));
	PIR1 &= ~0x10;
	TXREG = c;
	return 1;
}

/**
 * Checks if any data has arrived on serial port
 * MUST BE PLACED IN THE INTERRUPT FUNCTION
 */
void UARTInterrupt()
{
	if (RCIF)
	{
		RCIF = 0;
		UARTHasReceived = 1;
	}
}

/**
 * Get the last arrived byte from serial communication
 * @return data in RCREG
 */
char UARTGetReceived()
{
	char rxData = RCREG;
	RCREG = 0;
	UARTHasReceived = 0;
	return rxData;
}