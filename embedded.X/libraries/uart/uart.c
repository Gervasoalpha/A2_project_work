
#include <xc.h>
#include "uart.h"
#include "../constants.h"

void UARTInit();
char UARTTxChar(char c);
char UARTHasReceived();
char UARTGetReceived();

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
 * @return true if some data has arrived
 */
char UARTHasReceived()
{
	if (RCIF)
	{
		RCIF = 0;
		return 1;
	} else
	{
		return 0;
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
	return rxData;
}