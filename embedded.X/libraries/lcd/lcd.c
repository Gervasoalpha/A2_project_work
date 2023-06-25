//
#define LCD_EN RE1
#define LCD_RS RE2
#define LCD_PORT PORTD
#define LCD_FUNCTION_SET 0x38
#define LCD_ALL_ON 0x0F
#define LCD_ALL_OFF 0x08
#define LCD_CLEAR 0x01
#define L_L1 0x80
#define L_L2 0xC0
#define L_CR 0x0F
#define L_NCR 0x0C
#define LCD_DISPLAY_SETUP 0x0C

#include <xc.h>
#include "lcd.h"
#include "../constants.h"

void LcdInit();
void LcdSend(char data, char type);
void LcdSendString(char *string);
void LcdSetCursor(char row, char column);
void LcdClear();
void IntToString(int n, char *string);
int LcdPow(char b, char e);

void LcdInit()
{
	LCD_RS = 0;
	LCD_EN = 0;

	__delay_ms(20);

	LCD_EN = 1;

	LcdSend(LCD_FUNCTION_SET, 0);

	__delay_ms(5);

	LcdSend(LCD_FUNCTION_SET, 0);

	__delay_ms(1);

	LcdSend(LCD_FUNCTION_SET, 0);
	LcdSend(LCD_ALL_OFF, 0);
	LcdSend(LCD_ALL_ON, 0);
	LcdSend(LCD_FUNCTION_SET, 0);
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
	// 
	LCD_EN = 1;
	LCD_PORT = data;
	LCD_RS = (__bit) type;

	__delay_ms(3);

	LCD_EN = 0;

	__delay_ms(3);

	LCD_EN = 1;
}

void LcdSendString(char *string)
{
	char c = 0;

	for (c = 0; string[c] != '\0'; c++)
	{
		LcdSend(string[c], 1);
	}
}

void LcdSetCursor(char row, char column)
{

}

void LcdClear()
{
	LcdSend(LCD_CLEAR, 0);
}

void IntToString(int n, char *string)
{
	char digits = 1;

	while (n / LcdPow(10, digits))
	{
		digits++;
	}

	for (int i = 0; i < digits; i++)
	{
		string[i] = (char) (n / (10 * i)) + 48;
	}
}

int LcdPow(char b, char e)
{
	int n = 1;

	for (int i = 0; i < e; i++)
	{
		n *= b;
	}

	return n;
}