#ifndef _timer
#define _timer

enum CLOCK
{
    NS125 = 0x00,
    NS250 = 0x01,
    NS500 = 0x02,
    MS1 = 0x03,
    MS2 = 0x04,
    MS4 = 0x05,
    MS8 = 0x06
};

void TimerZeroInit(char PS);
char TimerZero();

#endif