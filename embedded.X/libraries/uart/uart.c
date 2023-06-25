
#include <xc.h>
#include "uart.h"
#include "../interrupt/interrupt.h"
#include "../constants.h"

void UARTInit();
char UARTTxChar(char c);
void UARTInterrupt();
char UARTGetReceived();

/**
 * Initialize UART Serial communication
 */
void UARTInit()
{
	// enable global interrupt
	InterruptInit();
	// set baud rate
	SPBRG = (char) (_XTAL_FREQ / (long) (64UL * BAUD_RATE)) - 1;
	// 8 bits TX
	TXSTAbits.TX9 = 0;
	// enable TX - also enables interrupt
	TXSTAbits.TXEN = 1;
	// async TX
	TXSTAbits.SYNC = 0;
	// high baud rate TX
	TXSTAbits.BRGH = 1;
	// enable RX
	RCSTAbits.SPEN = 1;
	// 8 bits RX
	RCSTAbits.RX9 = 0;
	// enable continuous RX
	RCSTAbits.CREN = 1;
}

/**
 * Send a char to the serial port
 * @param c byte to send
 * @return true if data was sent
 */
char UARTTxChar(char c)
{
	TXREG = c;
	while (!TXSTAbits.TRMT);
	return 1;
}

/**
 * Checks if any data has arrived on serial port
 * MUST BE PLACED IN THE INTERRUPT FUNCTION
 */
void UARTInterrupt()
{
	if (PIR1bits.RCIF)
	{
		PIR1bits.RCIF = 0;
		UARTHasReceived = 1;
		// RX error
		if (RCSTAbits.FERR || RCSTAbits.OERR)
		{
			RCSTAbits.CREN = 0;
			RCSTAbits.CREN = 1;
		}
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