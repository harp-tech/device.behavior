#include "WS2812S.h"

void initialize_rgb (void)
{
    RGB_PORT.DIR |= (1 << RGB_PIN);
    RGB_PORT_CLR = (1 << RGB_PIN);
}

#define XMIT_BYTE asm (             \
    "ld     __tmp_reg__, Z+     \n" \
    "sbrs   __tmp_reg__, 7      \n" \
    "call   zero                \n" \
    "sbrc   __tmp_reg__, 7      \n" \
    "call   one                 \n" \
    "sbrs   __tmp_reg__, 6      \n" \
    "call   zero                \n" \
    "sbrc   __tmp_reg__, 6      \n" \
    "call   one                 \n" \
    "sbrs   __tmp_reg__, 5      \n" \
    "call   zero                \n" \
    "sbrc   __tmp_reg__, 5      \n" \
    "call   one                 \n" \
    "sbrs   __tmp_reg__, 4      \n" \
    "call   zero                \n" \
    "sbrc   __tmp_reg__, 4      \n" \
    "call   one                 \n" \
    "sbrs   __tmp_reg__, 3      \n" \
    "call   zero                \n" \
    "sbrc   __tmp_reg__, 3      \n" \
    "call   one                 \n" \
    "sbrs   __tmp_reg__, 2      \n" \
    "call   zero                \n" \
    "sbrc   __tmp_reg__, 2      \n" \
    "call   one                 \n" \
    "sbrs   __tmp_reg__, 1      \n" \
    "call   zero                \n" \
    "sbrc   __tmp_reg__, 1      \n" \
    "call   one                 \n" \
    "sbrs   __tmp_reg__, 0      \n" \
    "call   zero_less           \n" \
    "sbrc   __tmp_reg__, 0      \n" \
    "call   one_less            \n" \
)

uint8_t rgbs_array[9];

void update_2rgbs (uint8_t * rgb_led0, uint8_t * rgb_led1)
{
    uint8_t * address;
    
    rgbs_array[0] = rgb_led0[0];
    rgbs_array[1] = rgb_led0[1];
    rgbs_array[2] = rgb_led0[2];
    rgbs_array[3] = rgb_led1[0];
    rgbs_array[4] = rgb_led1[1];
    rgbs_array[5] = rgb_led1[2];
    
    asm volatile (
    "rjmp start2leds            \n"
    
    "zero:                      \n"
    "sts    0x0645, r16         \n"     // Clear output pin
    "nop                        \n"     // 8x nops (250 ns)
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "sts    0x0646, r16         \n"     // Set output pin
    "nop                        \n"     // 22x nops (687,50 ns)
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "ret                        \n"
    
    "one:                       \n"
    "sts    0x0645, r16         \n"     // Clear output pin
    "nop                        \n"     // 22x nops (687,50 ns)
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "sts    0x0646, r16         \n"     // Set output pin
    "nop                        \n"     // 14x nops (437,50 ns)
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "ret                        \n"

    "zero_less:                 \n"
    "sts    0x0645, r16         \n"     // Clear output pin
    "nop                        \n"     // 8x nops (250 ns)
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "sts    0x0646, r16         \n"     // Set output pin
    "nop                        \n"     // 21 nops (687,50 ns)
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "ret                        \n"
    
    "one_less:                  \n"
    "sts    0x0645, r16         \n"     // Clear output pin
    "nop                        \n"     // 22x nops (687,50 ns)
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "sts    0x0646, r16         \n"     // Set output pin
    "nop                        \n"     // 13 nops (437,50 ns)
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "nop                        \n"
    "ret                        \n"
    
    
    "start2leds:                \n"
    "push	r16                 \n"
    "push	__tmp_reg__         \n"
    "ldi    r16, 0x20           \n"     // Load 0x20 to R16 -- means output port bit 5
    );
    
    /* Send address to Z pointer */
    address = (uint16_t)(&rgbs_array[0]);
    asm volatile (
    "ld __tmp_reg__, Z        \n\t"
    :   /* No outputs. */
    :"z"(address)
    );
    
    XMIT_BYTE;
    XMIT_BYTE;
    XMIT_BYTE;
    
    XMIT_BYTE;
    XMIT_BYTE;
    XMIT_BYTE;
    
    asm volatile (
    "pop    __tmp_reg__         \n"
    "pop    r16                 \n"
    );
}

    
void update_3rgbs (uint8_t * rgb_led0, uint8_t * rgb_led1, uint8_t * rgb_led2)
{  
    uint8_t * address;
    
    rgbs_array[0] = rgb_led0[0];
    rgbs_array[1] = rgb_led0[1];
    rgbs_array[2] = rgb_led0[2];    
    rgbs_array[3] = rgb_led1[0];
    rgbs_array[4] = rgb_led1[1];
    rgbs_array[5] = rgb_led1[2];    
    rgbs_array[6] = rgb_led2[0];
    rgbs_array[7] = rgb_led2[1];
    rgbs_array[8] = rgb_led2[2];    
    
    asm volatile (
        "rjmp start3leds            \n"
        
        
                
        "start3leds:                \n"
        "push	r16                 \n"
        "push	__tmp_reg__         \n"        
        "ldi    r16, 0x20           \n"     // Load 0x20 to R16 -- means output port bit 5
        );
    
    /* Send address to Z pointer */
    address = (uint16_t)(&rgbs_array[0]);
    asm volatile (
        "ld __tmp_reg__, Z        \n\t"
        :   /* No outputs. */
        :"z"(address)
    );
    
    XMIT_BYTE;
    XMIT_BYTE;
    XMIT_BYTE;
    
    XMIT_BYTE;
    XMIT_BYTE;
    XMIT_BYTE;
              
    XMIT_BYTE;
    XMIT_BYTE;
    XMIT_BYTE;
        
    asm volatile (        
        "pop    __tmp_reg__         \n"
        "pop    r16                 \n"
    );
}