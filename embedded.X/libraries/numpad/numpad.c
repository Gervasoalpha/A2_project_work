#define COLPORT PORTB
#define ROWPORT PORTD
#define TRIS_COLPORT TRISB
#define TRIS_ROWPORT TRISD
#define COL_NUM 3
#define ROW_NUM 4

#include <xc.h>
#include "numpad.h"
#include "../constants.h"


const unsigned char keypad [] = {'*', 7, 4, 1, 0, 8, 5, 2, '#', 9, 6, 3};

char NumpadRead();

/**
 * 
 * @return 
 */
char NumpadRead()
{
    TRIS_COLPORT &= ~0x07;
    TRIS_ROWPORT |= 0x0f;

    char colScan, rowScan, currentKeyVal, currentKey;
    static char oldKeyVal;

    for (colScan = 0; colScan < COL_NUM; colScan++)
    {
        COLPORT |= 0x07;
        COLPORT &= ~(0x01 << colScan);
        __delay_ms(15);
        for (rowScan = 0; rowScan < ROW_NUM; rowScan++)
        {
            currentKeyVal = (ROWPORT & (0x01 << rowScan));
            if (!currentKeyVal && oldKeyVal)
            {
                currentKey = keypad[rowScan + (4 * colScan)];
                oldKeyVal = 0;
                while (!currentKeyVal)
                {
                    currentKeyVal = (ROWPORT & (1 << rowScan));
                    __delay_ms(20);
                }
                return currentKey;
            }
            oldKeyVal = 1;
        }
    }
    return 0xff;
}