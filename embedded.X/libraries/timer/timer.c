//
#define PRELOAD_VALUE 131
#define _GIE 0x80
#define _PEIE 0x40
#define _T0IE 0x20
#define _PSA 0x08
#define _T0IF 0x04

#include <xc.h>
#include "timer.h"
#include "../constants.h"

void InterruptInit();
void TimerZeroInit(char PS);
char TimerZero();

/**
 * Enables the use of interrupts
 */
void InterruptInit()
{
	INTCON |= _GIE | _PEIE;
}

/**
 * Enables Timer 0
 * @param PS prescaler value, please use CLOCK enum
 * @return 
 */
void TimerZeroInit(char PS)
{
	//
	InterruptInit();
	//
	INTCON |= _T0IE;
	//
	OPTION_REG |= PS;
	//
	TMR0 = PRELOAD_VALUE;
}

/**
 * Interrupt function for Timer 0
 * @param preloadValue
 * @return true if an interrupt happened
 */
char TimerZero()
{
	//
	if (INTCON & _T0IF)
	{
		//
		INTCON &= ~_T0IF;
		//
		TMR0 = PRELOAD_VALUE;
		return 1;
	} else
	{
		return 0;
	}
}
