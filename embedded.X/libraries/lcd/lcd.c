//
#define LCD_COLS 16
#define LCD_ROWS 4
#define LCD_EN PORTEbits.RE1
#define LCD_RS PORTEbits.RE2
#define LCD_PORT PORTD
#define LCD_CFG 0x38
#define LCD_ON 0x0F
#define LCD_OFF 0x08
#define LCD_CLEAR 0x01
#define L_L1 0x80
#define L_L2 0xC0
#define L_CR 0x0F
#define L_NCR 0x0C
#define LCD_DISPLAY_SETUP 0x0C
#define LCD_SHIFT_RIGHT 0x1C
#define LCD_HOME 0x02

#include <xc.h>
#include "lcd.h"
#include "../constants.h"

void LcdInit();
void LcdSend(char data, char type);
void LcdSendChar(char c);
void LcdSendString(char *string);
void LcdSetCursor(unsigned char row, unsigned char column);
void LcdClear();
void IntToString(int n, char *string);
int LcdPow(char b, char e);

/**
 * Initialize LCD display
 */
void LcdInit()
{
	LCD_RS = 0;
	LCD_EN = 0;
	__delay_ms(20);
	LCD_EN = 1;
	LcdSend(LCD_CFG, 0);
	__delay_ms(5);
	LcdSend(LCD_CFG, 0);
	__delay_ms(1);
	LcdSend(LCD_CFG, 0);
	LcdSend(LCD_OFF, 0);
	LcdSend(LCD_ON, 0);
	LcdSend(LCD_CFG, 0);
	LcdSend(LCD_DISPLAY_SETUP, 0);
	LcdSend(L_L1, 0);
}

/**
 * 
 * @param data Byte data to send to LCD display
 * @param type 0 => Command, 1 => Display text
 */
void LcdSend(char data, char type)
{
	// Set tris for port D and E
	TRISD = 0;
	TRISE &= ~0x06;
	LCD_EN = 1;
	LCD_PORT = data;
	LCD_RS = (__bit) type;
	__delay_ms(3);
	LCD_EN = 0;
	__delay_ms(3);
	LCD_EN = 1;
}

/**
 * Write a single char to display
 * The char will be written at the current cursor position
 * @param c char
 */
void LcdSendChar(char c)
{
	LcdSend(c, 1);
}

/**
 * Write a string to display
 * The string will be written at the current cursor position
 * @param string
 */
void LcdSendString(char *string)
{
	char c = 0;
	for (c = 0; string[c] != '\0'; c++)
	{
		LcdSend(string[c], 1);
	}
}

/**
 * Shifts the display cursor to the given position
 * @param row 0 based index of rows
 * @param column 0 based index of columns
 */
void LcdSetCursor(unsigned char row, unsigned char column)
{
	if (row >= LCD_ROWS || column >= LCD_COLS)
	{
		return;
	}

	LcdSend(LCD_HOME, 0);

	unsigned int i, rowBytes = 40;

	for (i = 0; i <= ((rowBytes * row) + (column - 1)); i++)
	{
		LcdSend(LCD_SHIFT_RIGHT, 1);
	}
}

/**
 * Empty the display
 */
void LcdClear()
{
	LcdSend(LCD_CLEAR, 0);
}

void IntToString(int n, char *string)
{
	char digits = 1;
	//
	while (n / LcdPow(10, digits))
	{
		digits++;
	}
	//
	for (int i = 0; i < digits; i++)
	{
		string[i] = (char) (n / (10 * i)) + 48;
	}
}

int LcdPow(char b, char e)
{
	int n = 1;
	//
	for (int i = 0; i < e; i++)
	{
		n *= b;
	}
	return n;
}