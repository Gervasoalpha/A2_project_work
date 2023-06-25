
#include <xc.h>
#include "uart.h"
#include "../interrupt/interrupt.h"
#include "../constants.h"

void UARTInit();
void UARTTxChar(char c);
void UARTTxString(char* string);
char UARTInterrupt(unsigned char *ByteReceived);

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
	// enable continuous RX - also enables interrupt
	RCSTAbits.CREN = 1;
}

/**
 * Send a char to the serial port
 * @param c byte to send
 * @return true if data was sent
 */
void UARTTxChar(char c)
{
	TXREG = c;
	while (!TXSTAbits.TRMT);
}

/**
 * Send string to serial port
 * @param string
 */
void UARTTxString(char* string)
{
	unsigned int i;
	for (i = 0; string[i] != '\0'; i++)
	{
		UARTTxChar(string[i]);
	}
}

/**
 * Checks if any data has arrived on serial port, if so puts the received byte in byteReceived
 * @param byteReceived output parameter if data has been received
 * MUST BE PLACED IN THE INTERRUPT FUNCTION
 * @return true if data has been received
 */
char UARTInterrupt(unsigned char *bytesReceived)
{
	if (PIR1bits.RCIF)
	{
		// RX error
		if (RCSTAbits.FERR || RCSTAbits.OERR)
		{
			RCSTAbits.CREN = 0;
			RCSTAbits.CREN = 1;
		}
		//
		bytesReceived[0] = RCREG;
//		char secondByte = RCREG;
//		if (secondByte)
//		{
//			bytesReceived[1] = secondByte;
//		}
		return 1;
	} else
	{
		return 0;
	}
}