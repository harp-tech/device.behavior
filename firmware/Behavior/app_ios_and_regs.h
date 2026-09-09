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
// POKE0_IR                Description: Poke 0 infrared
// POKE0_IO                Description: Poke 0 DIO
// POKE1_IR                Description: Poke 1 infrared
// POKE1_IO                Description: Poke 1 DIO
// POKE2_IR                Description: Poke 2 infrared
// POKE2_IO                Description: Poke 2 DIO
// ADC1_AVAILABLE          Description: ADC1 is available on hardware
// DI3                     Description: Input DI3

#define read_POKE0_IR read_io(PORTD, 4)          // POKE0_IR
#define read_POKE0_IO read_io(PORTD, 5)          // POKE0_IO
#define read_POKE1_IR read_io(PORTE, 4)          // POKE1_IR
#define read_POKE1_IO read_io(PORTE, 5)          // POKE1_IO
#define read_POKE2_IR read_io(PORTF, 4)          // POKE2_IR
#define read_POKE2_IO read_io(PORTF, 5)          // POKE2_IO
#define read_ADC1_AVAILABLE read_io(PORTJ, 0)    // ADC1_AVAILABLE
#define read_DI3 read_io(PORTH, 0)               // DI3

/************************************************************************/
/* Definition of output pins                                            */
/************************************************************************/
// DO3                     Description: Output DO0
// DO2                     Description: Output DO1
// DO1                     Description: Output DO2
// DO0                     Description: Output DO3
// LED0                    Description: Output LED0
// LED1                    Description: Output LED1
// RGBS                    Description: One wire LEDs
// POKE0_LED               Description: Poke 0 digital output
// POKE0_VALVE             Description: Poke 0 Valve
// POKE1_LED               Description: Poke 1 digital output
// POKE1_VALVE             Description: Poke 1 Valve
// POKE2_LED               Description: Poke 2 digital output
// POKE2_VALVE             Description: Poke 2 Valve

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
    uint8_t REG_DIGITAL_INPUT_STATE;
	uint8_t REG_RESERVED0;
    uint16_t REG_OUTPUT_SET;
    uint16_t REG_OUTPUT_CLEAR;
    uint16_t REG_OUTPUT_TOGGLE;
    uint16_t REG_OUTPUT_STATE;
    uint8_t REG_PORT_DIO_SET;
    uint8_t REG_PORT_DIO_CLEAR;
    uint8_t REG_PORT_DIO_TOGGLE;
    uint8_t REG_PORT_DIO_STATE;
    uint8_t REG_PORT_DIO_DIRECTION;
    uint8_t REG_PORT_DIO_STATE_EVENT;
    int16_t REG_ANALOG_DATA[3];
    uint16_t REG_OUTPUT_PULSE_ENABLE;
    uint16_t REG_PULSE_DO_PORT0;
    uint16_t REG_PULSE_DO_PORT1;
    uint16_t REG_PULSE_DO_PORT2;
    uint16_t REG_PULSE_SUPPLY_PORT0;
    uint16_t REG_PULSE_SUPPLY_PORT1;
    uint16_t REG_PULSE_SUPPLY_PORT2;
	uint16_t REG_PULSE_LED0;
	uint16_t REG_PULSE_LED1;
	uint16_t REG_PULSE_RGB0;
	uint16_t REG_PULSE_RGB1;
	uint16_t REG_PULSE_DO0;
	uint16_t REG_PULSE_DO1;
	uint16_t REG_PULSE_DO2;
	uint16_t REG_PULSE_DO3;
    uint16_t REG_PWM_FREQUENCY_DO0;
    uint16_t REG_PWM_FREQUENCY_DO1;
    uint16_t REG_PWM_FREQUENCY_DO2;
    uint16_t REG_PWM_FREQUENCY_DO3;
    uint8_t REG_PWM_DUTY_CYCLE_DO0;
    uint8_t REG_PWM_DUTY_CYCLE_DO1;
    uint8_t REG_PWM_DUTY_CYCLE_DO2;
    uint8_t REG_PWM_DUTY_CYCLE_DO3;
	uint8_t REG_PWM_START;
	uint8_t REG_PWM_STOP;
    uint8_t REG_RGB_ALL[6];
	uint8_t REG_RGB0[3];
	uint8_t REG_RGB1[3];
	uint8_t REG_LED0_CURRENT;
	uint8_t REG_LED1_CURRENT;
	uint8_t REG_LED0_MAX_CURRENT;
	uint8_t REG_LED1_MAX_CURRENT;
    uint8_t REG_EVENT_ENABLE;
	uint8_t REG_START_CAMERAS;
	uint8_t REG_STOP_CAMERAS;
    uint8_t REG_ENABLE_SERVOS;
    uint8_t REG_DISABLE_SERVOS;
    uint8_t REG_ENABLE_ENCODERS;
    uint8_t REG_ENCODER_MODE;
	uint8_t REG_RESERVED2;
	uint8_t REG_RESERVED3;
	uint8_t REG_RESERVED4;
	uint8_t REG_RESERVED5;
	uint8_t REG_RESERVED6;
	uint8_t REG_RESERVED7;
	uint8_t REG_RESERVED8;
	uint8_t REG_RESERVED9;
    uint8_t REG_CAMERA0_FRAME;
    uint16_t REG_CAMERA0_FREQUENCY;
    uint8_t REG_CAMERA1_FRAME;
    uint16_t REG_CAMERA1_FREQUENCY;
	uint8_t REG_RESERVED10;
	uint8_t REG_RESERVED11;
	uint8_t REG_RESERVED12;
	uint8_t REG_RESERVED13;
    uint16_t REG_SERVO_MOTOR2_PERIOD;
    uint16_t REG_SERVO_MOTOR2_PULSE;
    uint16_t REG_SERVO_MOTOR3_PERIOD;
    uint16_t REG_SERVO_MOTOR3_PULSE;
	uint8_t REG_RESERVED14;
	uint8_t REG_RESERVED15;
	uint8_t REG_RESERVED16;
	uint8_t REG_RESERVED17;
    uint8_t REG_ENCODER_RESET;
	uint8_t REG_RESERVED18;
	uint8_t REG_ENABLE_SERIAL_TIMESTAMP;
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
    uint8_t REG_POKE_INPUT_FILTER;
} AppRegs;

/************************************************************************/
/* Registers' address                                                   */
/************************************************************************/
/* Registers */
#define ADD_REG_DIGITAL_INPUT_STATE        32 // U8     Reflects the state of DI digital lines of each Port
#define ADD_REG_RESERVED0                  33 // U8     Reserved for future use
#define ADD_REG_OUTPUT_SET                 34 // U16    Set the specified digital output lines.
#define ADD_REG_OUTPUT_CLEAR               35 // U16    Clear the specified digital output lines
#define ADD_REG_OUTPUT_TOGGLE              36 // U16    Toggle the specified digital output lines
#define ADD_REG_OUTPUT_STATE               37 // U16    Write the state of all digital output lines
#define ADD_REG_PORT_DIO_SET               38 // U8     Set the specified port DIO lines
#define ADD_REG_PORT_DIO_CLEAR             39 // U8     Clear the specified port DIO lines
#define ADD_REG_PORT_DIO_TOGGLE            40 // U8     Toggle the specified port DIO lines
#define ADD_REG_PORT_DIO_STATE             41 // U8     Write the state of all port DIO lines
#define ADD_REG_PORT_DIO_DIRECTION         42 // U8     Specifies which of the port DIO lines are outputs
#define ADD_REG_PORT_DIO_STATE_EVENT       43 // U8     Specifies the state of the port DIO lines on a line change
#define ADD_REG_ANALOG_DATA                44 // I16    Voltage at the ADC input and encoder value on Port 2
#define ADD_REG_OUTPUT_PULSE_ENABLE        45 // U16    Enables the pulse function for the specified output lines
#define ADD_REG_PULSE_DO_PORT0             46 // U16    Specifies the duration of the output pulse in milliseconds.
#define ADD_REG_PULSE_DO_PORT1             47 // U16    Specifies the duration of the output pulse in milliseconds.
#define ADD_REG_PULSE_DO_PORT2             48 // U16    Specifies the duration of the output pulse in milliseconds.
#define ADD_REG_PULSE_SUPPLY_PORT0         49 // U16    Specifies the duration of the output pulse in milliseconds.
#define ADD_REG_PULSE_SUPPLY_PORT1         50 // U16    Specifies the duration of the output pulse in milliseconds.
#define ADD_REG_PULSE_SUPPLY_PORT2         51 // U16    Specifies the duration of the output pulse in milliseconds.
#define ADD_REG_PULSE_LED0                 52 // U16    Specifies the duration of the output pulse in milliseconds.
#define ADD_REG_PULSE_LED1                 53 // U16    Specifies the duration of the output pulse in milliseconds.
#define ADD_REG_PULSE_RGB0                 54 // U16    Specifies the duration of the output pulse in milliseconds.
#define ADD_REG_PULSE_RGB1                 55 // U16    Specifies the duration of the output pulse in milliseconds.
#define ADD_REG_PULSE_DO0                  56 // U16    Specifies the duration of the output pulse in milliseconds.
#define ADD_REG_PULSE_DO1                  57 // U16    Specifies the duration of the output pulse in milliseconds.
#define ADD_REG_PULSE_DO2                  58 // U16    Specifies the duration of the output pulse in milliseconds.
#define ADD_REG_PULSE_DO3                  59 // U16    Specifies the duration of the output pulse in milliseconds.
#define ADD_REG_PWM_FREQUENCY_DO0          60 // U16    Specifies the frequency of the PWM at DO0.
#define ADD_REG_PWM_FREQUENCY_DO1          61 // U16    Specifies the frequency of the PWM at DO1.
#define ADD_REG_PWM_FREQUENCY_DO2          62 // U16    Specifies the frequency of the PWM at DO2.
#define ADD_REG_PWM_FREQUENCY_DO3          63 // U16    Specifies the frequency of the PWM at DO3.
#define ADD_REG_PWM_DUTY_CYCLE_DO0         64 // U8     Specifies the duty cycle of the PWM at DO0.
#define ADD_REG_PWM_DUTY_CYCLE_DO1         65 // U8     Specifies the duty cycle of the PWM at DO1.
#define ADD_REG_PWM_DUTY_CYCLE_DO2         66 // U8     Specifies the duty cycle of the PWM at DO2.
#define ADD_REG_PWM_DUTY_CYCLE_DO3         67 // U8     Specifies the duty cycle of the PWM at DO3.
#define ADD_REG_PWM_START                  68 // U8     Starts the PWM on the selected output lines.
#define ADD_REG_PWM_STOP                   69 // U8     Stops the PWM on the selected output lines.
#define ADD_REG_RGB_ALL                    70 // U8     Specifies the state of all RGB LED channels.
#define ADD_REG_RGB0                       71 // U8     Specifies the state of the RGB0 LED channels.
#define ADD_REG_RGB1                       72 // U8     Specifies the state of the RGB1 LED channels.
#define ADD_REG_LED0_CURRENT               73 // U8     Specifies the configuration of current to drive LED 0.
#define ADD_REG_LED1_CURRENT               74 // U8     Specifies the configuration of current to drive LED 1.
#define ADD_REG_LED0_MAX_CURRENT           75 // U8     Specifies the configuration of current to drive LED 0.
#define ADD_REG_LED1_MAX_CURRENT           76 // U8     Specifies the configuration of current to drive LED 1.
#define ADD_REG_EVENT_ENABLE               77 // U8     Specifies the active events in the device.
#define ADD_REG_START_CAMERAS              78 // U8     Specifies the camera outputs to enable in the device.
#define ADD_REG_STOP_CAMERAS               79 // U8     Specifies the camera outputs to disable in the device. An event will be issued when the trigger signal is actually stopped being generated.
#define ADD_REG_ENABLE_SERVOS              80 // U8     Specifies the servo outputs to enable in the device.
#define ADD_REG_DISABLE_SERVOS             81 // U8     Specifies the servo outputs to disable in the device.
#define ADD_REG_ENABLE_ENCODERS            82 // U8     Specifies the port quadrature counters to enable in the device.
#define ADD_REG_ENCODER_MODE               83 // U8     Configures the operation mode of the quadrature encoders.
#define ADD_REG_RESERVED2                  84 // U8     Reserved for future use
#define ADD_REG_RESERVED3                  85 // U8     Reserved for future use
#define ADD_REG_RESERVED4                  86 // U8     Reserved for future use
#define ADD_REG_RESERVED5                  87 // U8     Reserved for future use
#define ADD_REG_RESERVED6                  88 // U8     Reserved for future use
#define ADD_REG_RESERVED7                  89 // U8     Reserved for future use
#define ADD_REG_RESERVED8                  90 // U8     Reserved for future use
#define ADD_REG_RESERVED9                  91 // U8     Reserved for future use
#define ADD_REG_CAMERA0_FRAME              92 // U8     Specifies that a frame was acquired on camera 0.
#define ADD_REG_CAMERA0_FREQUENCY          93 // U16    Specifies the trigger frequency for camera 0.
#define ADD_REG_CAMERA1_FRAME              94 // U8     Specifies that a frame was acquired on camera 1.
#define ADD_REG_CAMERA1_FREQUENCY          95 // U16    Specifies the trigger frequency for camera 1.
#define ADD_REG_RESERVED10                 96 // U8     Reserved for future use
#define ADD_REG_RESERVED11                 97 // U8     Reserved for future use
#define ADD_REG_RESERVED12                 98 // U8     Reserved for future use
#define ADD_REG_RESERVED13                 99 // U8     Reserved for future use
#define ADD_REG_SERVO_MOTOR2_PERIOD       100 // U16    Specifies the period of the servo motor in DO2, in microseconds.
#define ADD_REG_SERVO_MOTOR2_PULSE        101 // U16    Specifies the pulse of the servo motor in DO2, in microseconds.
#define ADD_REG_SERVO_MOTOR3_PERIOD       102 // U16    Specifies the period of the servo motor in DO3, in microseconds.
#define ADD_REG_SERVO_MOTOR3_PULSE        103 // U16    Specifies the pulse of the servo motor in DO3, in microseconds.
#define ADD_REG_RESERVED14                104 // U8     Reserved for future use
#define ADD_REG_RESERVED15                105 // U8     Reserved for future use
#define ADD_REG_RESERVED16                106 // U8     Reserved for future use
#define ADD_REG_RESERVED17                107 // U8     Reserved for future use
#define ADD_REG_ENCODER_RESET             108 // U8     Reset the counter of the specified encoders to zero.
#define ADD_REG_RESERVED18                109 // U8     Reserved for future use
#define ADD_REG_ENABLE_SERIAL_TIMESTAMP   110 // U8     Enables the timestamp for serial TX.
#define ADD_REG_MIMIC_PORT0_IR            111 // U8     Specifies the digital output to mimic the Port 0 IR state.
#define ADD_REG_MIMIC_PORT1_IR            112 // U8     Specifies the digital output to mimic the Port 1 IR state.
#define ADD_REG_MIMIC_PORT2_IR            113 // U8     Specifies the digital output to mimic the Port 2 IR state.
#define ADD_REG_RESERVED20                114 // U8     Reserved for future use
#define ADD_REG_RESERVED21                115 // U8     Reserved for future use
#define ADD_REG_RESERVED22                116 // U8     Reserved for future use
#define ADD_REG_MIMIC_PORT0_VALVE         117 // U8     Specifies the digital output to mimic the Port 0 valve state.
#define ADD_REG_MIMIC_PORT1_VALVE         118 // U8     Specifies the digital output to mimic the Port 1 valve state.
#define ADD_REG_MIMIC_PORT2_VALVE         119 // U8     Specifies the digital output to mimic the Port 2 valve state.
#define ADD_REG_RESERVED23                120 // U8     Reserved for future use
#define ADD_REG_RESERVED24                121 // U8     Reserved for future use
#define ADD_REG_POKE_INPUT_FILTER         122 // U8     Specifies the low pass filter time value for poke inputs, in ms.

/************************************************************************/
/* Behavior registers' memory limits                                    */
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
#define B_DI_PORT0                      (1<<0)       // Port 0 digital input
#define B_DI_PORT1                      (1<<1)       // Port 1 digital input
#define B_DI_PORT2                      (1<<2)       // Port 2 digital input
#define B_DI3                           (1<<3)       // Digital input DI3
#define B_DO_PORT0                      (1<<0)       // 
#define B_DO_PORT1                      (1<<1)       // 
#define B_DO_PORT2                      (1<<2)       // 
#define B_SUPPLY_PORT0                  (1<<3)       // 
#define B_SUPPLY_PORT1                  (1<<4)       // 
#define B_SUPPLY_PORT2                  (1<<5)       // 
#define B_LED0                          (1<<6)       // 
#define B_LED1                          (1<<7)       // 
#define B_RGB0                          (1<<8)       // 
#define B_RGB1                          (1<<9)       // 
#define B_DO0                           (1<<10)      // 
#define B_DO1                           (1<<11)      // 
#define B_DO2                           (1<<12)      // 
#define B_DO3                           (1<<13)      // 
#define B_DIO0                          (1<<0)       // 
#define B_DIO1                          (1<<1)       // 
#define B_DIO2                          (1<<2)       // 
#define B_PWM_DO0                       (1<<0)       // 
#define B_PWM_DO1                       (1<<1)       // 
#define B_PWM_DO2                       (1<<2)       // 
#define B_PWM_DO3                       (1<<3)       // 
#define B_PORT_DI                       (1<<0)       // Event from register DigitalInputState
#define B_PORT_DIO                      (1<<1)       // Event from register PortDIOStateEvent
#define B_ANALOG_DATA                   (1<<2)       // Event from register AnalogData
#define B_CAMERA0                       (1<<3)       // Event from register Camera0Frame
#define B_CAMERA1                       (1<<4)       // Event from register Camera1Frame
#define B_CAMERA_OUTPUT0                (1<<0)       // Camera on digital output 0
#define B_CAMERA_OUTPUT1                (1<<1)       // Camera on digital output 1
#define B_SERVO_OUTPUT2                 (1<<2)       // Servo on digital output 2
#define B_SERVO_OUTPUT3                 (1<<3)       // Servo on digital output 3
#define B_ENCODER_PORT2                 (1<<2)       // Encoder on port 2
#define B_FRAME_ACQUIRED                (1<<0)       // Camera frame was triggered
#define B_TIMESTAMP_PORT2               (1<<2)       // Enable the serial timestamp TX on Port 2
#define MSK_MIMIC_OUTPUT                0x07         // 
#define GM_MIMIC_OUTPUT_NONE            0x00         // 
#define GM_MIMIC_OUTPUT_DIO0            0x01         // Is reflected on DIO0
#define GM_MIMIC_OUTPUT_DIO1            0x02         // Is reflected on DIO1
#define GM_MIMIC_OUTPUT_DIO2            0x03         // Is reflected on DIO2
#define GM_MIMIC_OUTPUT_DO0             0x04         // Is reflected on DO0
#define GM_MIMIC_OUTPUT_DO1             0x05         // Is reflected on DO1
#define GM_MIMIC_OUTPUT_DO2             0x06         // Is reflected on DO2
#define GM_MIMIC_OUTPUT_DO3             0x07         // Is reflected on DO3
#define MSK_ENCODER_MODE                0x01         // 
#define GM_ENCODER_MODE_POSITION        0x00         // 
#define GM_ENCODER_MODE_DISPLACEMENT    0x01         // 

#endif /* _APP_REGS_H_ */