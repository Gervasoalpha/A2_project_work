//
#define PRELOAD_VALUE 131

#include <xc.h>
#include "timer.h"
#include "../interrupt/interrupt.h"
#include "../constants.h"

void TimerZeroInit(char PS);
char TimerZero();

/**
 * Enables Timer 0
 * @param PS prescaler value, please use CLOCK enum
 * @return 
 */
void TimerZeroInit(char PS)
{
	//
	InterruptInit();
	// enable timer zero interrupt
	INTCONbits.T0IE = 1;
	// set prescaler
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
	if (INTCONbits.T0IF)
	{
		//
		INTCONbits.T0IF = 0;
		//
		TMR0 = PRELOAD_VALUE;
		return 1;
	} else
	{
		return 0;
	}
}
