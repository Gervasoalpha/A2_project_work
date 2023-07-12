//
#ifndef _serialProtocol
#define	_serialProtocol

#include <xc.h> 

typedef enum {
    IDLE,
    RECEIVE_AUTH_CODE,
    WAIT_AUTH_CODE,
    WAIT_USER_UNLOCK_CODE,
    SEND_USER_UNLOCK_CODE,
    WAIT_END,
} device_state;

device_state deviceState;

void EDMLoop(void);

#endif

