#ifndef _APP_IOS_AND_REGS_H_
#define _APP_IOS_AND_REGS_H_
#include "cpu.h"

void init_ios(void);
/************************************************************************/
/* Definition of input pins                                             */
/************************************************************************/
// POKE0_IR               Description: Poke 0 infrared
// POKE0_IO               Description: Poke 0 DIO
// POKE1_IR               Description: Poke 1 infrared
// POKE1_IO               Description: Poke 1 DIO
// POKE2_IR               Description: Poke 2 infrared
// POKE2_IO               Description: Poke 2 DIO

#define read_POKE0_IR read_io(PORTD, 4)         // POKE0_IR
#define read_POKE0_IO read_io(PORTD, 5)         // POKE0_IO
#define read_POKE1_IR read_io(PORTE, 4)         // POKE1_IR
#define read_POKE1_IO read_io(PORTE, 5)         // POKE1_IO
#define read_POKE2_IR read_io(PORTF, 4)         // POKE2_IR
#define read_POKE2_IO read_io(PORTF, 5)         // POKE2_IO

/************************************************************************/
/* Definition of output pins                                            */
/************************************************************************/
// DO3                    Description: Output DO0
// DO2                    Description: Output DO1
// DO1                    Description: Output DO2
// DO0                    Description: Output DO3
// LED0                   Description: Output LED0
// LED1                   Description: Output LED1
// RGBS                   Description: One wire LEDs
// POKE0_LED              Description: Poke 0 digital output
// POKE0_VALVE            Description: Poke 0 Valve
// POKE1_LED              Description: Poke 1 digital output
// POKE1_VALVE            Description: Poke 1 Valve
// POKE2_LED              Description: Poke 2 digital output
// POKE2_VALVE            Description: Poke 2 Valve

/* DO3 */
#define set_DO3 set_io(PORTC, 0)
#define clr_DO3 clear_io(PORTC, 0)
#define tgl_DO3 toggle_io(PORTC, 0)
#define read_DO3 read_io(PORTC, 0)

/* DO2 */
#define set_DO2 set_io(PORTD, 0)
#define clr_DO2 clear_io(PORTD, 0)
#define tgl_DO2 toggle_io(PORTD, 0)
#define read_DO2 read_io(PORTD, 0)

/* DO1 */
#define set_DO1 set_io(PORTE, 0)
#define clr_DO1 clear_io(PORTE, 0)
#define tgl_DO1 toggle_io(PORTE, 0)
#define read_DO1 read_io(PORTE, 0)

/* DO0 */
#define set_DO0 set_io(PORTF, 0)
#define clr_DO0 clear_io(PORTF, 0)
#define tgl_DO0 toggle_io(PORTF, 0)
#define read_DO0 read_io(PORTF, 0)

/* LED0 */
#define set_LED0 set_io(PORTB, 5)
#define clr_LED0 clear_io(PORTB, 5)
#define tgl_LED0 toggle_io(PORTB, 5)
#define read_LED0 read_io(PORTB, 5)

/* LED1 */
#define set_LED1 set_io(PORTB, 6)
#define clr_LED1 clear_io(PORTB, 6)
#define tgl_LED1 toggle_io(PORTB, 6)
#define read_LED1 read_io(PORTB, 6)

/* RGBS */
#define set_RGBS set_io(PORTC, 5)
#define clr_RGBS clear_io(PORTC, 5)
#define tgl_RGBS toggle_io(PORTC, 5)
#define read_RGBS read_io(PORTC, 5)

/* POKE0_LED */
#define set_POKE0_LED set_io(PORTD, 6)
#define clr_POKE0_LED clear_io(PORTD, 6)
#define tgl_POKE0_LED toggle_io(PORTD, 6)
#define read_POKE0_LED read_io(PORTD, 6)

/* POKE0_VALVE */
#define set_POKE0_VALVE set_io(PORTD, 7)
#define clr_POKE0_VALVE clear_io(PORTD, 7)
#define tgl_POKE0_VALVE toggle_io(PORTD, 7)
#define read_POKE0_VALVE read_io(PORTD, 7)

/* POKE1_LED */
#define set_POKE1_LED set_io(PORTE, 6)
#define clr_POKE1_LED clear_io(PORTE, 6)
#define tgl_POKE1_LED toggle_io(PORTE, 6)
#define read_POKE1_LED read_io(PORTE, 6)

/* POKE1_VALVE */
#define set_POKE1_VALVE set_io(PORTE, 7)
#define clr_POKE1_VALVE clear_io(PORTE, 7)
#define tgl_POKE1_VALVE toggle_io(PORTE, 7)
#define read_POKE1_VALVE read_io(PORTE, 7)

/* POKE2_LED */
#define set_POKE2_LED set_io(PORTF, 6)
#define clr_POKE2_LED clear_io(PORTF, 6)
#define tgl_POKE2_LED toggle_io(PORTF, 6)
#define read_POKE2_LED read_io(PORTF, 6)

/* POKE2_VALVE */
#define set_POKE2_VALVE set_io(PORTF, 7)
#define clr_POKE2_VALVE clear_io(PORTF, 7)
#define tgl_POKE2_VALVE toggle_io(PORTF, 7)
#define read_POKE2_VALVE read_io(PORTF, 7)


/************************************************************************/
/* Registers' structure                                                 */
/************************************************************************/
typedef struct
{
	uint8_t REG_POKE_IN;
	uint8_t REG_POKE_DIG_IN;
	uint16_t REG_OUTPUTS_SET;
	uint16_t REG_OUTPUTS_CLEAR;
	uint16_t REG_OUTPUTS_TOGGLE;
	uint16_t REG_OUTPUTS_OUT;
	uint8_t REG_POKE_DIOS_SET;
	uint8_t REG_POKE_DIOS_CLEAR;
	uint8_t REG_POKE_DIOS_TOGGLE;
	uint8_t REG_POKE_DIOS_OUT;
	uint8_t REG_POKE_DIOS_CONF;
	uint8_t REG_POKE_DIOS_IN;
	uint16_t REG_ADC;
	uint8_t REG_MODE_POKE0_LED;
	uint8_t REG_MODE_POKE1_LED;
	uint8_t REG_MODE_POKE2_LED;
	uint8_t REG_MODE_POKE0_VALVE;
	uint8_t REG_MODE_POKE1_VALVE;
	uint8_t REG_MODE_POKE2_VALVE;
	uint8_t REG_MODE_LED0;
	uint8_t REG_MODE_LED1;
	uint8_t REG_MODE_RGB0;
	uint8_t REG_MODE_RGB1;
	uint8_t REG_MODE_DO0;
	uint8_t REG_MODE_DO1;
	uint8_t REG_MODE_DO2;
	uint8_t REG_MODE_DO3;
	uint16_t REG_PULSE_POKE0_LED;
	uint16_t REG_PULSE_POKE1_LED;
	uint16_t REG_PULSE_POKE2_LED;
	uint16_t REG_PULSE_POKE0_VALVE;
	uint16_t REG_PULSE_POKE1_VALVE;
	uint16_t REG_PULSE_POKE2_VALVE;
	uint16_t REG_PULSE_LED0;
	uint16_t REG_PULSE_LED1;
	uint16_t REG_PULSE_RGB0;
	uint16_t REG_PULSE_RGB1;
	uint16_t REG_PULSE_DO0;
	uint16_t REG_PULSE_DO1;
	uint16_t REG_PULSE_DO2;
	uint16_t REG_PULSE_DO3;
	uint16_t REG_FREQ_DO0;
	uint16_t REG_FREQ_DO1;
	uint16_t REG_FREQ_DO2;
	uint16_t REG_FREQ_DO3;
	uint8_t REG_DCYCLE_DO0;
	uint8_t REG_DCYCLE_DO1;
	uint8_t REG_DCYCLE_DO2;
	uint8_t REG_DCYCLE_DO3;
	uint8_t REG_PWM_START;
	uint8_t REG_PWM_STOP;
	uint8_t REG_RGBS[6];
	uint8_t REG_RGB0[3];
	uint8_t REG_RGB1[3];
	uint8_t REG_LED0_CURRENT;
	uint8_t REG_LED1_CURRENT;
	uint8_t REG_LED0_MAX_CURRENT;
	uint8_t REG_LED1_MAX_CURRENT;
	uint8_t REG_EVNT_ENABLE;
} AppRegs;

/************************************************************************/
/* Registers' address                                                   */
/************************************************************************/
/* Registers */
#define ADD_REG_POKE_IN                     32 // U8     Reflects the state of each Poke's infreared beam
#define ADD_REG_POKE_DIG_IN                 33 // U8     Reflects the state of each Poke's digital input
#define ADD_REG_OUTPUTS_SET                 34 // U16    Set the correspondent output
#define ADD_REG_OUTPUTS_CLEAR               35 // U16    Clear the correspondent output
#define ADD_REG_OUTPUTS_TOGGLE              36 // U16    Toggle the correspondent output
#define ADD_REG_OUTPUTS_OUT                 37 // U16    Control the correspondent output
#define ADD_REG_POKE_DIOS_SET               38 // U8     Set the correspondent DIO
#define ADD_REG_POKE_DIOS_CLEAR             39 // U8     Clear the correspondent DIO
#define ADD_REG_POKE_DIOS_TOGGLE            40 // U8     Toggle the correspondent DIO
#define ADD_REG_POKE_DIOS_OUT               41 // U8     Control the correspondent DIO
#define ADD_REG_POKE_DIOS_CONF              42 // U8     Set the DIOs direction (1 is output)
#define ADD_REG_POKE_DIOS_IN                43 // U8     State of the DIOs
#define ADD_REG_ADC                         44 // U16    Voltage at ADC input
#define ADD_REG_MODE_POKE0_LED              45 // U8     Mode of the output
#define ADD_REG_MODE_POKE1_LED              46 // U8     
#define ADD_REG_MODE_POKE2_LED              47 // U8     
#define ADD_REG_MODE_POKE0_VALVE            48 // U8     
#define ADD_REG_MODE_POKE1_VALVE            49 // U8     
#define ADD_REG_MODE_POKE2_VALVE            50 // U8     
#define ADD_REG_MODE_LED0                   51 // U8     
#define ADD_REG_MODE_LED1                   52 // U8     
#define ADD_REG_MODE_RGB0                   53 // U8     
#define ADD_REG_MODE_RGB1                   54 // U8     
#define ADD_REG_MODE_DO0                    55 // U8     
#define ADD_REG_MODE_DO1                    56 // U8     
#define ADD_REG_MODE_DO2                    57 // U8     
#define ADD_REG_MODE_DO3                    58 // U8     
#define ADD_REG_PULSE_POKE0_LED             59 // U16    Configuration of the output pulse [1 : 65535]
#define ADD_REG_PULSE_POKE1_LED             60 // U16    
#define ADD_REG_PULSE_POKE2_LED             61 // U16    
#define ADD_REG_PULSE_POKE0_VALVE           62 // U16    
#define ADD_REG_PULSE_POKE1_VALVE           63 // U16    
#define ADD_REG_PULSE_POKE2_VALVE           64 // U16    
#define ADD_REG_PULSE_LED0                  65 // U16    
#define ADD_REG_PULSE_LED1                  66 // U16    
#define ADD_REG_PULSE_RGB0                  67 // U16    
#define ADD_REG_PULSE_RGB1                  68 // U16    
#define ADD_REG_PULSE_DO0                   69 // U16    
#define ADD_REG_PULSE_DO1                   70 // U16    
#define ADD_REG_PULSE_DO2                   71 // U16    
#define ADD_REG_PULSE_DO3                   72 // U16    
#define ADD_REG_FREQ_DO0                    73 // U16    Frequency of the output [1 : TBD]
#define ADD_REG_FREQ_DO1                    74 // U16    
#define ADD_REG_FREQ_DO2                    75 // U16    
#define ADD_REG_FREQ_DO3                    76 // U16    
#define ADD_REG_DCYCLE_DO0                  77 // U8     Dutycycle of the output [1 : 99]
#define ADD_REG_DCYCLE_DO1                  78 // U8     
#define ADD_REG_DCYCLE_DO2                  79 // U8     
#define ADD_REG_DCYCLE_DO3                  80 // U8     
#define ADD_REG_PWM_START                   81 // U8     Start the PWM output on the select output
#define ADD_REG_PWM_STOP                    82 // U8     Stop the PWM output on the select output
#define ADD_REG_RGBS                        83 // U8     [RGB0 Green] [RGB0  Red] [RGB0 Blue] [RGB1 Green] [RGB1  Red] [RGB1 Blue]
#define ADD_REG_RGB0                        84 // U8     [RGB0 Green] [RGB0  Red] [RGB0 Blue]
#define ADD_REG_RGB1                        85 // U8     [RGB1 Green] [RGB1  Red] [RGB1 Blue]
#define ADD_REG_LED0_CURRENT                86 // U8     Configuration of current to drive LED 0 [2:100]
#define ADD_REG_LED1_CURRENT                87 // U8     Configuration of current to drive LED 1 [2:100]
#define ADD_REG_LED0_MAX_CURRENT            88 // U8     Configuration of current to drive LED 0 [5:100]
#define ADD_REG_LED1_MAX_CURRENT            89 // U8     Configuration of current to drive LED 1 [5:100]
#define ADD_REG_EVNT_ENABLE                 90 // U8     Enable the Events

/************************************************************************/
/* PWM Generator registers' memory limits                               */
/*                                                                      */
/* DON'T change the APP_REGS_ADD_MIN value !!!                          */
/* DON'T change these names !!!                                         */
/************************************************************************/
/* Memory limits */
#define APP_REGS_ADD_MIN                    0x20
#define APP_REGS_ADD_MAX                    0x5A
#define APP_NBYTES_OF_REG_BANK              91

/************************************************************************/
/* Registers' bits                                                      */
/************************************************************************/
#define B_IR0                              (1<<0)       // Infrared 0
#define B_IR1                              (1<<1)       // Infrared 1
#define B_IR2                              (1<<2)       // Infrared 2
#define B_DIG_IN0                          (1<<0)       // Digital input of Poke 0
#define B_DIG_IN1                          (1<<1)       // Digital input of Poke 1
#define B_DIG_IN2                          (1<<2)       // Digital input of Poke 2
#define B_POKE0_LED                        (1<<0)       // 
#define B_POKE1_LED                        (1<<1)       // 
#define B_POKE2_LED                        (1<<2)       // 
#define B_POKE0_VALVE                      (1<<3)       // 
#define B_POKE1_VALVE                      (1<<4)       // 
#define B_POKE2_VALVE                      (1<<5)       // 
#define B_LED0                             (1<<6)       // 
#define B_LED1                             (1<<7)       // 
#define B_RGB0                             (1<<8)       // 
#define B_RGB1                             (1<<9)       // 
#define B_DO0                              (1<<10)      // 
#define B_DO1                              (1<<11)      // 
#define B_DO2                              (1<<12)      // 
#define B_DO3                              (1<<13)      // 
#define B_DIO0                             (1<<0)       // 
#define B_DIO1                             (1<<1)       // 
#define B_DIO2                             (1<<2)       // 
#define MSK_BEH_MODE                       (3<<0)       // 
#define GM_SOFTWARE                        (0<<0)       // 
#define GM_PULSE                           (1<<0)       // 
#define B_EVT_POKE_IN                      (1<<0)       // Event of register POKE_IN
#define B_EVT_POKE_DIG_IN                  (1<<1)       // Event of register POKE_DIG_IN
#define B_EVT_POKE_DIOS_IN                 (1<<2)       // Event of register POKE_DIOS_IN
#define B_EVT_ADC                          (1<<3)       // Event of register ADC

#endif /* _APP_REGS_H_ */