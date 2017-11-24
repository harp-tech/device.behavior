#ifndef _WS2812_H_
#define _WS2812_H_
#include <avr/io.h>

/************************************************************************/
/* User defines here the digital output                                 */
/************************************************************************/
#define RGB_PORT        PORTC
#define RGB_PORT_SET    PORTC_OUTSET
#define RGB_PORT_CLR    PORTC_OUTCLR
#define RGB_PIN         5


/************************************************************************/
/* Defines and Macros                                                   */
/************************************************************************/
//#define _1CLK_CYCLE  asm("nop")
#define _1CLK_CYCLE asm volatile ("nop" :: )
#define _2CLK_CYCLE  _1CLK_CYCLE; _1CLK_CYCLE
#define _5CLK_CYCLE  _2CLK_CYCLE; _2CLK_CYCLE; _1CLK_CYCLE
#define _10CLK_CYCLE _5CLK_CYCLE; _5CLK_CYCLE

// T0H [200:500] ns = [6,4:16] clock cycles (nops)
// T1H [550:850] ns = [17,6:27,2] clock cycles (nops)

// T0L [650:950] ns = [20,8:30,4] clock cycles (nops)
// T1L [450:750] ns = [14,4:24] clock cycles (nops)

#define T0H_TIME _5CLK_CYCLE;  _2CLK_CYCLE                  // 7
#define T1H_TIME _10CLK_CYCLE; _10CLK_CYCLE; _1CLK_CYCLE    // 21

#define T0L_TIME _10CLK_CYCLE; _2CLK_CYCLE                  // 12
#define T1L_TIME _5CLK_CYCLE;  _1CLK_CYCLE                  // 6

#define XMIT_ZERO                       \
do                                      \
{                                       \
    RGB_PORT_CLR = (1 << RGB_PIN);      \
    T0H_TIME;                           \
    RGB_PORT_SET = (1 << RGB_PIN);      \
    T0L_TIME;                           \
} while (0)

#define XMIT_ONE                        \
do                                      \
{                                       \
    RGB_PORT_CLR = (1 << RGB_PIN);      \
    T1H_TIME;                           \
    RGB_PORT_SET = (1 << RGB_PIN);      \
    T1L_TIME;                           \
} while (0)

#define XMIT_COLOR(color)                           \
do                                                  \
{                                                   \
    if (color & 0x80) XMIT_ONE; else XMIT_ZERO;     \
    if (color & 0x40) XMIT_ONE; else XMIT_ZERO;     \
    if (color & 0x20) XMIT_ONE; else XMIT_ZERO;     \
    if (color & 0x10) XMIT_ONE; else XMIT_ZERO;     \
    if (color & 0x08) XMIT_ONE; else XMIT_ZERO;     \
    if (color & 0x04) XMIT_ONE; else XMIT_ZERO;     \
    if (color & 0x02) XMIT_ONE; else XMIT_ZERO;     \
    if (color & 0x01) XMIT_ONE; else XMIT_ZERO;     \
} while (0)

/************************************************************************/
/* Prototypes                                                           */
/************************************************************************/
void initialize_rgb (void);
void update_2rgbs (uint8_t * rgb_led0, uint8_t * rgb_led1);
void update_3rgbs (uint8_t * rgb_led0, uint8_t * rgb_led1, uint8_t * rgb_led2);

#endif /* _WS2812_H_ */
