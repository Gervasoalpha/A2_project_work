#ifndef Constant_
#define Constant_

#include <xc.h>

// CRYSTAL FREQUENCY
#define _XTAL_FREQ 8*1000000

// BS
#define TRUE 1
#define FALSE 0

// TIMERS
#define PRELOAD_VALUE 131
#define GIE 0x80
#define PEIE 0x40
#define T0IE 0x20
#define PSA 0x08
#define T0IF 0x04

// INTERRUPT
#define ONE_MILLIS_SIZE 50

// LCD
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

// UART
#define BAUD_RATE 115200

#endif