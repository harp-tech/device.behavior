#ifndef _APP_IOS_AND_REGS_H_
#define _APP_IOS_AND_REGS_H_
#include "cpu.h"

void init_ios(void);

void mimic_ir_or_valve (uint8_t reg, uint8_t what_t_do);
#define _SET_IO_ 0
#define _CLR_IO_ 1
#define _TGL_IO_ 2

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
#define read_ADC1_AVAILABLE read_io(PORTJ, 0)   // ADC1_AVAILABLE
#define read_DI3 read_io(PORTH, 0)              // DI3

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
#define set_LED0 clear_io(PORTB, 6)
#define clr_LED0 set_io(PORTB, 6)
#define tgl_LED0 toggle_io(PORTB, 6)
#define read_LED0 read_io(PORTB, 6)

/* LED1 */
#define set_LED1 clear_io(PORTB, 5)
#define clr_LED1 set_io(PORTB, 5)
#define tgl_LED1 toggle_io(PORTB, 5)
#define read_LED1 read_io(PORTB, 5)

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
#define set_POKE0_VALVE do { set_io(PORTD, 7); mimic_ir_or_valve(app_regs.REG_MIMIC_PORT0_VALVE, _SET_IO_); } while(0)
#define clr_POKE0_VALVE do { clear_io(PORTD, 7); mimic_ir_or_valve(app_regs.REG_MIMIC_PORT0_VALVE, _CLR_IO_); } while(0)
#define tgl_POKE0_VALVE do { toggle_io(PORTD, 7); mimic_ir_or_valve(app_regs.REG_MIMIC_PORT0_VALVE, _TGL_IO_);  } while(0)
#define read_POKE0_VALVE read_io(PORTD, 7)

/* POKE1_LED */
#define set_POKE1_LED set_io(PORTE, 6)
#define clr_POKE1_LED clear_io(PORTE, 6)
#define tgl_POKE1_LED toggle_io(PORTE, 6)
#define read_POKE1_LED read_io(PORTE, 6)

/* POKE1_VALVE */
#define set_POKE1_VALVE do { set_io(PORTE, 7); mimic_ir_or_valve(app_regs.REG_MIMIC_PORT1_VALVE, _SET_IO_); } while(0)
#define clr_POKE1_VALVE do { clear_io(PORTE, 7); mimic_ir_or_valve(app_regs.REG_MIMIC_PORT1_VALVE, _CLR_IO_); } while(0)
#define tgl_POKE1_VALVE do { toggle_io(PORTE, 7); mimic_ir_or_valve(app_regs.REG_MIMIC_PORT1_VALVE, _TGL_IO_); } while(0)
#define read_POKE1_VALVE read_io(PORTE, 7)

/* POKE2_LED */
#define set_POKE2_LED set_io(PORTF, 6)
#define clr_POKE2_LED clear_io(PORTF, 6)
#define tgl_POKE2_LED toggle_io(PORTF, 6)
#define read_POKE2_LED read_io(PORTF, 6)

/* POKE2_VALVE */
#define set_POKE2_VALVE do { set_io(PORTF, 7); mimic_ir_or_valve(app_regs.REG_MIMIC_PORT2_VALVE, _SET_IO_); } while(0)
#define clr_POKE2_VALVE do { clear_io(PORTF, 7); mimic_ir_or_valve(app_regs.REG_MIMIC_PORT2_VALVE, _CLR_IO_); } while(0)
#define tgl_POKE2_VALVE do { toggle_io(PORTF, 7); mimic_ir_or_valve(app_regs.REG_MIMIC_PORT2_VALVE, _TGL_IO_); } while(0)
#define read_POKE2_VALVE read_io(PORTF, 7)


/************************************************************************/
/* Registers' structure                                                 */
/************************************************************************/
typedef struct
{
	uint8_t REG_PORT_DIS;
	uint8_t REG_RESERVED0;
	uint16_t REG_OUTPUTS_SET;
	uint16_t REG_OUTPUTS_CLEAR;
	uint16_t REG_OUTPUTS_TOGGLE;
	uint16_t REG_OUTPUTS_OUT;
	uint8_t REG_PORT_DIOS_SET;
	uint8_t REG_PORT_DIOS_CLEAR;
	uint8_t REG_PORT_DIOS_TOGGLE;
	uint8_t REG_PORT_DIOS_OUT;
	uint8_t REG_PORT_DIOS_CONF;
	uint8_t REG_PORT_DIOS_IN;
	int16_t REG_DATA[3];
	uint16_t REG_OUTPUT_PULSE_EN;
	uint16_t REG_PULSE_PORT0_DO;
	uint16_t REG_PULSE_PORT1_DO;
	uint16_t REG_PULSE_PORT2_DO;
	uint16_t REG_PULSE_PORT0_12V;
	uint16_t REG_PULSE_PORT1_12V;
	uint16_t REG_PULSE_PORT2_12V;
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
	uint8_t REG_START_CAMERAS;
	uint8_t REG_STOP_CAMERAS;
	uint8_t REG_EN_SERVOS;
	uint8_t REG_DIS_SERVOS;
	uint8_t REG_EN_ENCODERS;
	uint8_t REG_RESERVED1;
	uint8_t REG_RESERVED2;
	uint8_t REG_RESERVED3;
	uint8_t REG_RESERVED4;
	uint8_t REG_RESERVED5;
	uint8_t REG_RESERVED6;
	uint8_t REG_RESERVED7;
	uint8_t REG_RESERVED8;
	uint8_t REG_RESERVED9;
	uint8_t REG_CAM_OUT0_FRAME_ACQUIRED;
	uint16_t REG_CAM_OUT0_FREQ;
	uint8_t REG_CAM_OUT1_FRAME_ACQUIRED;
	uint16_t REG_CAM_OUT1_FREQ;
	uint8_t REG_RESERVED10;
	uint8_t REG_RESERVED11;
	uint8_t REG_RESERVED12;
	uint8_t REG_RESERVED13;
	uint16_t REG_MOTOR_OUT2_PERIOD;
	uint16_t REG_MOTOR_OUT2_PULSE;
	uint16_t REG_MOTOR_OUT3_PERIOD;
	uint16_t REG_MOTOR_OUT3_PULSE;
	uint8_t REG_RESERVED14;
	uint8_t REG_RESERVED15;
	uint8_t REG_RESERVED16;
	uint8_t REG_RESERVED17;
	uint8_t REG_ENCODERS_RESET;
	uint8_t REG_RESERVED18;
	uint8_t REG_RESERVED19;
	uint8_t REG_MIMIC_PORT0_IR;
	uint8_t REG_MIMIC_PORT1_IR;
	uint8_t REG_MIMIC_PORT2_IR;
	uint8_t REG_RESERVED20;
	uint8_t REG_RESERVED21;
	uint8_t REG_RESERVED22;
	uint8_t REG_MIMIC_PORT0_VALVE;
	uint8_t REG_MIMIC_PORT1_VALVE;
	uint8_t REG_MIMIC_PORT2_VALVE;
	uint8_t REG_RESERVED23;
	uint8_t REG_RESERVED24;
	uint8_t REG_POKE_INPUT_FILTER_MS;
} AppRegs;

/************************************************************************/
/* Registers' address                                                   */
/************************************************************************/
/* Registers */
#define ADD_REG_PORT_DIS                    32 // U8     Reflects the state of DI digital lines of each Port
#define ADD_REG_RESERVED0                   33 // U8     Reserved for future use
#define ADD_REG_OUTPUTS_SET                 34 // U16    Set the correspondent output
#define ADD_REG_OUTPUTS_CLEAR               35 // U16    Clear the correspondent output
#define ADD_REG_OUTPUTS_TOGGLE              36 // U16    Toggle the correspondent output
#define ADD_REG_OUTPUTS_OUT                 37 // U16    Control the correspondent output
#define ADD_REG_PORT_DIOS_SET               38 // U8     Set the correspondent DIO
#define ADD_REG_PORT_DIOS_CLEAR             39 // U8     Clear the correspondent DIO
#define ADD_REG_PORT_DIOS_TOGGLE            40 // U8     Toggle the correspondent DIO
#define ADD_REG_PORT_DIOS_OUT               41 // U8     Control the correspondent DIO
#define ADD_REG_PORT_DIOS_CONF              42 // U8     Set the DIOs direction (1 is output)
#define ADD_REG_PORT_DIOS_IN                43 // U8     State of the DIOs
#define ADD_REG_DATA                        44 // I16    Voltage at ADC input and decoder (poke 2) value
#define ADD_REG_OUTPUT_PULSE_EN             45 // U16    Enable the output pulse for the selected output
#define ADD_REG_PULSE_PORT0_DO              46 // U16    Configuration of the output pulse [1 : 65535]
#define ADD_REG_PULSE_PORT1_DO              47 // U16    
#define ADD_REG_PULSE_PORT2_DO              48 // U16    
#define ADD_REG_PULSE_PORT0_12V             49 // U16    
#define ADD_REG_PULSE_PORT1_12V             50 // U16    
#define ADD_REG_PULSE_PORT2_12V             51 // U16    
#define ADD_REG_PULSE_LED0                  52 // U16    
#define ADD_REG_PULSE_LED1                  53 // U16    
#define ADD_REG_PULSE_RGB0                  54 // U16    
#define ADD_REG_PULSE_RGB1                  55 // U16    
#define ADD_REG_PULSE_DO0                   56 // U16    
#define ADD_REG_PULSE_DO1                   57 // U16    
#define ADD_REG_PULSE_DO2                   58 // U16    
#define ADD_REG_PULSE_DO3                   59 // U16    
#define ADD_REG_FREQ_DO0                    60 // U16    Frequency of the output [1 : TBD]
#define ADD_REG_FREQ_DO1                    61 // U16    
#define ADD_REG_FREQ_DO2                    62 // U16    
#define ADD_REG_FREQ_DO3                    63 // U16    
#define ADD_REG_DCYCLE_DO0                  64 // U8     Dutycycle of the output [1 : 99]
#define ADD_REG_DCYCLE_DO1                  65 // U8     
#define ADD_REG_DCYCLE_DO2                  66 // U8     
#define ADD_REG_DCYCLE_DO3                  67 // U8     
#define ADD_REG_PWM_START                   68 // U8     Start the PWM output on the selected output
#define ADD_REG_PWM_STOP                    69 // U8     Stop the PWM output on the selected output
#define ADD_REG_RGBS                        70 // U8     [RGB0 Green] [RGB0  Red] [RGB0 Blue] [RGB1 Green] [RGB1  Red] [RGB1 Blue]
#define ADD_REG_RGB0                        71 // U8     [RGB0 Green] [RGB0  Red] [RGB0 Blue]
#define ADD_REG_RGB1                        72 // U8     [RGB1 Green] [RGB1  Red] [RGB1 Blue]
#define ADD_REG_LED0_CURRENT                73 // U8     Configuration of current to drive LED 0 [2:100]
#define ADD_REG_LED1_CURRENT                74 // U8     Configuration of current to drive LED 1 [2:100]
#define ADD_REG_LED0_MAX_CURRENT            75 // U8     Configuration of current to drive LED 0 [5:100]
#define ADD_REG_LED1_MAX_CURRENT            76 // U8     Configuration of current to drive LED 1 [5:100]
#define ADD_REG_EVNT_ENABLE                 77 // U8     Enable the Events
#define ADD_REG_START_CAMERAS               78 // U8     Enable cameras
#define ADD_REG_STOP_CAMERAS                79 // U8     Disable cameras
#define ADD_REG_EN_SERVOS                   80 // U8     Enable servo motors control
#define ADD_REG_DIS_SERVOS                  81 // U8     Disable servo motors control
#define ADD_REG_EN_ENCODERS                 82 // U8     Enable encoders
#define ADD_REG_RESERVED1                   83 // U8     Reserved for future use
#define ADD_REG_RESERVED2                   84 // U8     Reserved for future use
#define ADD_REG_RESERVED3                   85 // U8     Reserved for future use
#define ADD_REG_RESERVED4                   86 // U8     Reserved for future use
#define ADD_REG_RESERVED5                   87 // U8     Reserved for future use
#define ADD_REG_RESERVED6                   88 // U8     Reserved for future use
#define ADD_REG_RESERVED7                   89 // U8     Reserved for future use
#define ADD_REG_RESERVED8                   90 // U8     Reserved for future use
#define ADD_REG_RESERVED9                   91 // U8     Reserved for future use
#define ADD_REG_CAM_OUT0_FRAME_ACQUIRED     92 // U8     
#define ADD_REG_CAM_OUT0_FREQ               93 // U16    Configures the camera's sample frequency [1;600]
#define ADD_REG_CAM_OUT1_FRAME_ACQUIRED     94 // U8     
#define ADD_REG_CAM_OUT1_FREQ               95 // U16    Configures the camera's sample frequency [1;600]
#define ADD_REG_RESERVED10                  96 // U8     Reserved for future use
#define ADD_REG_RESERVED11                  97 // U8     Reserved for future use
#define ADD_REG_RESERVED12                  98 // U8     Reserved for future use
#define ADD_REG_RESERVED13                  99 // U8     Reserved for future use
#define ADD_REG_MOTOR_OUT2_PERIOD          100 // U16    Configures the servo motor period (us)  (sensitive to 2 us)
#define ADD_REG_MOTOR_OUT2_PULSE           101 // U16    Configures the servo motor pulse (us)  (sensitive to 2 us)
#define ADD_REG_MOTOR_OUT3_PERIOD          102 // U16    Configures the servo motor period (us)  (sensitive to 2 us)
#define ADD_REG_MOTOR_OUT3_PULSE           103 // U16    Configures the servo motor pulse (us)  (sensitive to 2 us)
#define ADD_REG_RESERVED14                 104 // U8     Reserved for future use
#define ADD_REG_RESERVED15                 105 // U8     Reserved for future use
#define ADD_REG_RESERVED16                 106 // U8     Reserved for future use
#define ADD_REG_RESERVED17                 107 // U8     Reserved for future use
#define ADD_REG_ENCODERS_RESET             108 // U8     Resets the encoders counter to ZERO
#define ADD_REG_RESERVED18                 109 // U8     Reserved for future use
#define ADD_REG_RESERVED19                 110 // U8     Reserved for future use
#define ADD_REG_MIMIC_PORT0_IR             111 // U8     
#define ADD_REG_MIMIC_PORT1_IR             112 // U8     
#define ADD_REG_MIMIC_PORT2_IR             113 // U8     
#define ADD_REG_RESERVED20                 114 // U8     
#define ADD_REG_RESERVED21                 115 // U8     
#define ADD_REG_RESERVED22                 116 // U8     
#define ADD_REG_MIMIC_PORT0_VALVE          117 // U8     
#define ADD_REG_MIMIC_PORT1_VALVE          118 // U8     
#define ADD_REG_MIMIC_PORT2_VALVE          119 // U8     
#define ADD_REG_RESERVED23                 120 // U8     
#define ADD_REG_RESERVED24                 121 // U8     
#define ADD_REG_POKE_INPUT_FILTER_MS       122 // U8     

/************************************************************************/
/* PWM Generator registers' memory limits                               */
/*                                                                      */
/* DON'T change the APP_REGS_ADD_MIN value !!!                          */
/* DON'T change these names !!!                                         */
/************************************************************************/
/* Memory limits */
#define APP_REGS_ADD_MIN                    0x20
#define APP_REGS_ADD_MAX                    0x7A
#define APP_NBYTES_OF_REG_BANK              134

/************************************************************************/
/* Registers' bits                                                      */
/************************************************************************/
#define B_DI0                              (1<<0)       // Port 0 digital input
#define B_DI1                              (1<<1)       // Port 1 digital input
#define B_DI2                              (1<<2)       // Port 2 digital input
#define B_DI3                              (1<<3)       // Digital input DI3
#define B_PORT0_DO                         (1<<0)       // 
#define B_PORT1_DO                         (1<<1)       // 
#define B_PORT2_DO                         (1<<2)       // 
#define B_PORT0_12V                        (1<<3)       // 
#define B_PORT1_12V                        (1<<4)       // 
#define B_PORT2_12V                        (1<<5)       // 
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
#define B_PWM_DO0                          (1<<0)       // 
#define B_PWM_DO1                          (1<<1)       // 
#define B_PWM_DO2                          (1<<2)       // 
#define B_PWM_DO3                          (1<<3)       // 
#define B_EVT_PORT_DIS                     (1<<0)       // Event of register POKE_IN
#define B_EVT_PORT_DIOS_IN                 (1<<1)       // Event of register POKE_DIOS_IN
#define B_EVT_DATA                         (1<<2)       // Event of register ADC
#define B_EVT_CAM0                         (1<<3)       // Event of CAM_OUT0_FRAME_ACQUIRED
#define B_EVT_CAM1                         (1<<4)       // Event of CAM_OUT1_FRAME_ACQUIRED
#define B_EN_CAM_OUT0                      (1<<0)       // Camera on digital output 0
#define B_EN_CAM_OUT1                      (1<<1)       // Camera on digital output 1
#define B_EN_SERVO_OUT2                    (1<<2)       // Servo on digital output 2
#define B_EN_SERVO_OUT3                    (1<<3)       // Servo on digital output 3
#define B_EN_ENCODER_PORT2                 (1<<2)       // Encoder on port 2
#define B_CAM_ACQ                          (1<<0)       // Camera frame was triggered
#define B_RST_ENCODER_PORT2                (1<<2)       // Reset the encoder counter on Port 2
#define MSK_MIMIC                          0x0F         // 
#define GM_MIMIC_NONE                      0x00         // 
#define GM_MIMIC_DIO0                      0x01         // Is reflected on DIO0
#define GM_MIMIC_DIO1                      0x02         // Is reflected on DIO1
#define GM_MIMIC_DIO2                      0x03         // Is reflected on DIO2
#define GM_MIMIC_DO0                       0x04         // Is reflected on DO0
#define GM_MIMIC_DO1                       0x05         // Is reflected on DO1
#define GM_MIMIC_DO2                       0x06         // Is reflected on DO2
#define GM_MIMIC_DO3                       0x07         // Is reflected on DO3

#endif /* _APP_REGS_H_ */