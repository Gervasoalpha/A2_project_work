#ifndef _constants
#define _constants

// CRYSTAL FREQUENCY
#define _XTAL_FREQ 16*1000000

// DEVICE ID
#define DEVICE_ID 5

// UART BAUD RATE
#define BAUD_RATE 9600

// BUFFER SIZE
#define BUFFER_SIZE 50

#define WELCOME_STRING "Premi un tasto.."
#define LOADING_STRING "Loading..."

typedef union {
    unsigned char BY;

    struct {
        unsigned B0 : 1;
        unsigned B1 : 1;
        unsigned B2 : 1;
        unsigned B3 : 1;
        unsigned B4 : 1;
        unsigned B5 : 1;
        unsigned B6 : 1;
        unsigned B7 : 1;
    };
} BYTE_t;

typedef union {
    unsigned int W;

    struct {
        unsigned char BY0 : 8;
        unsigned char BY1 : 8;
    };

    struct {
        unsigned B0 : 1;
        unsigned B1 : 1;
        unsigned B2 : 1;
        unsigned B3 : 1;
        unsigned B4 : 1;
        unsigned B5 : 1;
        unsigned B6 : 1;
        unsigned B7 : 1;
        unsigned B8 : 1;
        unsigned B9 : 1;
        unsigned B10 : 1;
        unsigned B11 : 1;
        unsigned B12 : 1;
        unsigned B13 : 1;
        unsigned B14 : 1;
        unsigned B15 : 1;
    };
} WORD_t;

#endif