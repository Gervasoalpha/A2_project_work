//
#include <xc.h>
#include <stdlib.h>
#include "data_conversion.h"

int stringToInt(char *string, int b);

/**
 * TODO => MAKE SOME CHECKS
 * @param string
 * @param b
 * @return 
 */
int stringToInt(char *string, int b)
{
	char *end;
	long l = strtol(string, &end, b);

	return(int) l;
}
