//
#include <xc.h>
#include <string.h>
#include <stdio.h>
#include "serialProtocol.h"
#include "../../libraries/constants.h"
#include "../../libraries/buffer/buffer.h"
#include "../../libraries/uart/uart.h"
#include "../../libraries/utils/data_conversion.h"
#include "../../libraries/numpad/numpad.h"
#include "../../libraries/lcd/lcd.h"

#define DELIMITER_TOKEN ";"
#define TERMINATING_TOKEN "\n"
#define TERMINATING_CHAR '\n'

#define PROTO_PING_CODE 1
#define PROTO_AUTH_CODE 4
#define PROTO_SUCCESS_CODE 6
#define PROTO_FAIL_CODE 7

#define PROTO_KEEPALIVE_CODE 2
#define PROTO_REQ_AUTH_CODE 3
#define PROTO_SEND_UNLOCK_CODE 5

void EDMLoop(void);
void ReadMessage();
void ReadUserInput();
char sendMessage(int functionCode, char *payload);
void CopyArray(char *source, char *dest, unsigned char length);

unsigned char *bufferCopy;
unsigned char message[BUFFER_SIZE];
unsigned char lastCheckedItemIndex = 0;

unsigned char userUnlockCode[20];

void EDMLoop(void)
{
	ReadUserInput();

	ReadMessage();
}

void ReadMessage()
{
	unsigned char bufferSize = BufferGetSize();
	// Check if a complete message has arrived
	if (bufferSize == 0 || lastCheckedItemIndex == bufferSize)
	{
		return;
	}

	unsigned char index;
	//	unsigned char delimiterFound = BufferFindFirstInterval(TERMINATING_TOKEN, &index, lastCheckedItemIndex, bufferSize - lastCheckedItemIndex);
	unsigned char delimiterFound = BufferFindFirst(TERMINATING_CHAR, &index);

	if (!delimiterFound)
	{
		lastCheckedItemIndex = index;
		return;
	}

	// just for safety clear out the message buffer
	memset(message, 0, sizeof(message));
	// Get the message and clear the buffer
	bufferCopy = BufferCopy(index + 1);
	BufferClear();
	// Reset for next iteration
	lastCheckedItemIndex = 0;
	// need to copy the buffer AGAIN because strtok only accepts editable strings and pointers are read-only
	CopyArray(bufferCopy, message, index + 1);

	unsigned char *deviceAddressString, *functionCodeString, *payloadString, *checksumString;
	int deviceAddressInt, checksumInt, functionCodeInt;

	// extract data from the message
	// TODO: CONTROLLO STRINGHE
	deviceAddressString = strtok(message, DELIMITER_TOKEN);
	functionCodeString = strtok(NULL, DELIMITER_TOKEN);

	deviceAddressInt = stringToInt(deviceAddressString, 10); // CHECK
	functionCodeInt = stringToInt(functionCodeString, 10); // CHECK

	payloadString = strtok(NULL, DELIMITER_TOKEN);

	checksumString = strtok(NULL, TERMINATING_TOKEN);

	// TODO: CONTROLLO CHECKSUM
	checksumInt = stringToInt(checksumString, 16); // CHECK
	// SEND MESSAGE
	// ERROR

	// check if the message is directed to this device
	if (deviceAddressInt != DEVICE_ID)
	{
		return;
	}

	switch (functionCodeInt)
	{
		case PROTO_PING_CODE:
		{
			//
			switch (deviceState)
			{
				case RECEIVE_AUTH_CODE:
				{
					// SEND MESSAGE
					// CODE 3 => RICHIESTA CODICE AUTENTICAZIONE
					// GOTO WAIT_AUTH_CODE
					sendMessage(PROTO_REQ_AUTH_CODE, NULL);
					deviceState = WAIT_AUTH_CODE; // !!!START TIMEOUT!!!

					LcdClear();
					LcdSendString(LOADING_STRING);
					break;
				}

				case SEND_USER_UNLOCK_CODE:
				{
					// SEND MESSAGE
					// CODE 5 => INVIO CODICE SBLOCCO INSERITO
					// GOTO WAIT_END
					sendMessage(PROTO_SEND_UNLOCK_CODE, userUnlockCode);

					// remember to reset string for next input -- MAYBE NOT BEST IN CASE THE GATEWAY DOESNT RECEIVE IT RIGHT AWAY
					memset(userUnlockCode, 0, sizeof(userUnlockCode));

					deviceState = WAIT_END;
					break;
				}

				default:
				{
					// SEND KEEPALIVE
					sendMessage(PROTO_KEEPALIVE_CODE, NULL);
				}
			}
			break;
		}

		case PROTO_AUTH_CODE:
		{
			// DISPLAY AUTH CODE
			// GOTO WAIT_USER_UNLOCK_CODE
			LcdClear();
			LcdSendString(payloadString);
			LcdSendString("  ");
			__delay_ms(2000);
			deviceState = WAIT_USER_UNLOCK_CODE; // !!!START TIMEOUT!!!
			break;
		}

		case PROTO_SUCCESS_CODE:
		{
			if (deviceState == WAIT_END)
			{
				// APRI PORTA
				// GOTO IDLE
				LcdClear();
				LcdSendString("PORTA APERTA");
				deviceState = IDLE;
			} else
			{
				// SEND MESSAGE
				// ERROR
			}
			break;
		}

		case PROTO_FAIL_CODE:
		{
			if (deviceState == WAIT_END)
			{
				// GOTO IDLE? 
				LcdClear();
				LcdSendString("CASIN");
				LcdClear();
				LcdSendString(WELCOME_STRING);
				deviceState = IDLE;
			} else
			{
				// SEND MESSAGE
				// ERROR
			}
			break;
		}

		default:
		{
		}
	}
}

void ReadUserInput()
{
	if (deviceState == IDLE)
	{
		char digit = NumpadRead();
		// CLICK FIRST BUTTON => START SEQUENCE
		if (digit != 0xFF)
		{
			deviceState = RECEIVE_AUTH_CODE;
			LcdClear();
			LcdSendString(LOADING_STRING);
			return;
		}
	}

	if (deviceState == WAIT_USER_UNLOCK_CODE)
	{
		char digit = NumpadRead();
		// USER INPUT UNLOCK CODE
		if (digit == '#') // end of input => goto SEND_USER_UNLOCK_CODE
		{
			LcdClear();
			deviceState = SEND_USER_UNLOCK_CODE;
		} else if (digit >= '0' && digit <= '9')
		{
			char str[2];
			str[0] = digit;
			str[1] = '\0';
			strcat(userUnlockCode, str);
			LcdSendChar(digit);
		}
	}
}

/**
 * 
 * @param functionCode
 * @param payload
 * @return 
 */
char sendMessage(int function, char *payload) // CHECK FUNCTION CODE
{
	unsigned char newMessage[BUFFER_SIZE], deviceIdString[10], functionCodeString[10], checksumString[10] = "AA";
	// clear array because it isnt initialized :(
	memset(newMessage, 0, sizeof(newMessage));

	sprintf(deviceIdString, "%d", DEVICE_ID); // CHECK RETURN
	sprintf(functionCodeString, "%d", function); // CHECK RETURN
	//
	strcat(newMessage, deviceIdString);
	strcat(newMessage, DELIMITER_TOKEN);
	//
	strcat(newMessage, functionCodeString);
	strcat(newMessage, DELIMITER_TOKEN);

	strcat(newMessage, payload);
	strcat(newMessage, DELIMITER_TOKEN);

	// 
	strcat(newMessage, checksumString);
	strcat(newMessage, TERMINATING_TOKEN);

	UARTTxString(newMessage);
	return 1;
}

/**
 * 
 * @param source
 * @param dest
 * @param length
 */
void CopyArray(char *source, char *dest, unsigned char length)
{
	for (unsigned char i = 0; i < length; i++)
	{
		dest[i] = source[i];
	}
}