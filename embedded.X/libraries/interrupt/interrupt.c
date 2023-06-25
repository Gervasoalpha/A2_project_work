//
#include <xc.h>
#include "interrupt.h"

/**
 * Enables the use of interrupts globally
 */
void InterruptInit()
{
	INTCONbits.GIE = 1;
	INTCONbits.PEIE = 1;
}

