#ifndef _APP_FUNCTIONS_H_
#define _APP_FUNCTIONS_H_
#include <avr/io.h>


/************************************************************************/
/* Define if not defined                                                */
/************************************************************************/
#ifndef bool
	#define bool uint8_t
#endif
#ifndef true
	#define true 1
#endif
#ifndef false
	#define false 0
#endif


/************************************************************************/
/* Prototypes                                                           */
/************************************************************************/
void app_read_REG_DIGITAL_INPUT_STATE(void);
void app_read_REG_RESERVED0(void);
void app_read_REG_OUTPUT_SET(void);
void app_read_REG_OUTPUT_CLEAR(void);
void app_read_REG_OUTPUT_TOGGLE(void);
void app_read_REG_OUTPUT_STATE(void);
void app_read_REG_PORT_DIO_SET(void);
void app_read_REG_PORT_DIO_CLEAR(void);
void app_read_REG_PORT_DIO_TOGGLE(void);
void app_read_REG_PORT_DIO_STATE(void);
void app_read_REG_PORT_DIO_DIRECTION(void);
void app_read_REG_PORT_DIO_STATE_EVENT(void);
void app_read_REG_ANALOG_DATA(void);
void app_read_REG_OUTPUT_PULSE_ENABLE(void);
void app_read_REG_PULSE_DO_PORT0(void);
void app_read_REG_PULSE_DO_PORT1(void);
void app_read_REG_PULSE_DO_PORT2(void);
void app_read_REG_PULSE_SUPPLY_PORT0(void);
void app_read_REG_PULSE_SUPPLY_PORT1(void);
void app_read_REG_PULSE_SUPPLY_PORT2(void);
void app_read_REG_PULSE_LED0(void);
void app_read_REG_PULSE_LED1(void);
void app_read_REG_PULSE_RGB0(void);
void app_read_REG_PULSE_RGB1(void);
void app_read_REG_PULSE_DO0(void);
void app_read_REG_PULSE_DO1(void);
void app_read_REG_PULSE_DO2(void);
void app_read_REG_PULSE_DO3(void);
void app_read_REG_PWM_FREQUENCY_DO0(void);
void app_read_REG_PWM_FREQUENCY_DO1(void);
void app_read_REG_PWM_FREQUENCY_DO2(void);
void app_read_REG_PWM_FREQUENCY_DO3(void);
void app_read_REG_PWM_DUTY_CYCLE_DO0(void);
void app_read_REG_PWM_DUTY_CYCLE_DO1(void);
void app_read_REG_PWM_DUTY_CYCLE_DO2(void);
void app_read_REG_PWM_DUTY_CYCLE_DO3(void);
void app_read_REG_PWM_START(void);
void app_read_REG_PWM_STOP(void);
void app_read_REG_RGB_ALL(void);
void app_read_REG_RGB0(void);
void app_read_REG_RGB1(void);
void app_read_REG_LED0_CURRENT(void);
void app_read_REG_LED1_CURRENT(void);
void app_read_REG_LED0_MAX_CURRENT(void);
void app_read_REG_LED1_MAX_CURRENT(void);
void app_read_REG_EVENT_ENABLE(void);
void app_read_REG_START_CAMERAS(void);
void app_read_REG_STOP_CAMERAS(void);
void app_read_REG_ENABLE_SERVOS(void);
void app_read_REG_DISABLE_SERVOS(void);
void app_read_REG_ENABLE_ENCODERS(void);
void app_read_REG_ENCODER_MODE(void);
void app_read_REG_RESERVED2(void);
void app_read_REG_RESERVED3(void);
void app_read_REG_RESERVED4(void);
void app_read_REG_RESERVED5(void);
void app_read_REG_RESERVED6(void);
void app_read_REG_RESERVED7(void);
void app_read_REG_RESERVED8(void);
void app_read_REG_RESERVED9(void);
void app_read_REG_CAMERA0_FRAME(void);
void app_read_REG_CAMERA0_FREQUENCY(void);
void app_read_REG_CAMERA1_FRAME(void);
void app_read_REG_CAMERA1_FREQUENCY(void);
void app_read_REG_RESERVED10(void);
void app_read_REG_RESERVED11(void);
void app_read_REG_RESERVED12(void);
void app_read_REG_RESERVED13(void);
void app_read_REG_SERVO_MOTOR2_PERIOD(void);
void app_read_REG_SERVO_MOTOR2_PULSE(void);
void app_read_REG_SERVO_MOTOR3_PERIOD(void);
void app_read_REG_SERVO_MOTOR3_PULSE(void);
void app_read_REG_RESERVED14(void);
void app_read_REG_RESERVED15(void);
void app_read_REG_RESERVED16(void);
void app_read_REG_RESERVED17(void);
void app_read_REG_ENCODER_RESET(void);
void app_read_REG_RESERVED18(void);
void app_read_REG_ENABLE_SERIAL_TIMESTAMP(void);
void app_read_REG_MIMIC_PORT0_IR(void);
void app_read_REG_MIMIC_PORT1_IR(void);
void app_read_REG_MIMIC_PORT2_IR(void);
void app_read_REG_RESERVED20(void);
void app_read_REG_RESERVED21(void);
void app_read_REG_RESERVED22(void);
void app_read_REG_MIMIC_PORT0_VALVE(void);
void app_read_REG_MIMIC_PORT1_VALVE(void);
void app_read_REG_MIMIC_PORT2_VALVE(void);
void app_read_REG_RESERVED23(void);
void app_read_REG_RESERVED24(void);
void app_read_REG_POKE_INPUT_FILTER(void);

bool app_write_REG_DIGITAL_INPUT_STATE(void *a);
bool app_write_REG_RESERVED0(void *a);
bool app_write_REG_OUTPUT_SET(void *a);
bool app_write_REG_OUTPUT_CLEAR(void *a);
bool app_write_REG_OUTPUT_TOGGLE(void *a);
bool app_write_REG_OUTPUT_STATE(void *a);
bool app_write_REG_PORT_DIO_SET(void *a);
bool app_write_REG_PORT_DIO_CLEAR(void *a);
bool app_write_REG_PORT_DIO_TOGGLE(void *a);
bool app_write_REG_PORT_DIO_STATE(void *a);
bool app_write_REG_PORT_DIO_DIRECTION(void *a);
bool app_write_REG_PORT_DIO_STATE_EVENT(void *a);
bool app_write_REG_ANALOG_DATA(void *a);
bool app_write_REG_OUTPUT_PULSE_ENABLE(void *a);
bool app_write_REG_PULSE_DO_PORT0(void *a);
bool app_write_REG_PULSE_DO_PORT1(void *a);
bool app_write_REG_PULSE_DO_PORT2(void *a);
bool app_write_REG_PULSE_SUPPLY_PORT0(void *a);
bool app_write_REG_PULSE_SUPPLY_PORT1(void *a);
bool app_write_REG_PULSE_SUPPLY_PORT2(void *a);
bool app_write_REG_PULSE_LED0(void *a);
bool app_write_REG_PULSE_LED1(void *a);
bool app_write_REG_PULSE_RGB0(void *a);
bool app_write_REG_PULSE_RGB1(void *a);
bool app_write_REG_PULSE_DO0(void *a);
bool app_write_REG_PULSE_DO1(void *a);
bool app_write_REG_PULSE_DO2(void *a);
bool app_write_REG_PULSE_DO3(void *a);
bool app_write_REG_PWM_FREQUENCY_DO0(void *a);
bool app_write_REG_PWM_FREQUENCY_DO1(void *a);
bool app_write_REG_PWM_FREQUENCY_DO2(void *a);
bool app_write_REG_PWM_FREQUENCY_DO3(void *a);
bool app_write_REG_PWM_DUTY_CYCLE_DO0(void *a);
bool app_write_REG_PWM_DUTY_CYCLE_DO1(void *a);
bool app_write_REG_PWM_DUTY_CYCLE_DO2(void *a);
bool app_write_REG_PWM_DUTY_CYCLE_DO3(void *a);
bool app_write_REG_PWM_START(void *a);
bool app_write_REG_PWM_STOP(void *a);
bool app_write_REG_RGB_ALL(void *a);
bool app_write_REG_RGB0(void *a);
bool app_write_REG_RGB1(void *a);
bool app_write_REG_LED0_CURRENT(void *a);
bool app_write_REG_LED1_CURRENT(void *a);
bool app_write_REG_LED0_MAX_CURRENT(void *a);
bool app_write_REG_LED1_MAX_CURRENT(void *a);
bool app_write_REG_EVENT_ENABLE(void *a);
bool app_write_REG_START_CAMERAS(void *a);
bool app_write_REG_STOP_CAMERAS(void *a);
bool app_write_REG_ENABLE_SERVOS(void *a);
bool app_write_REG_DISABLE_SERVOS(void *a);
bool app_write_REG_ENABLE_ENCODERS(void *a);
bool app_write_REG_ENCODER_MODE(void *a);
bool app_write_REG_RESERVED2(void *a);
bool app_write_REG_RESERVED3(void *a);
bool app_write_REG_RESERVED4(void *a);
bool app_write_REG_RESERVED5(void *a);
bool app_write_REG_RESERVED6(void *a);
bool app_write_REG_RESERVED7(void *a);
bool app_write_REG_RESERVED8(void *a);
bool app_write_REG_RESERVED9(void *a);
bool app_write_REG_CAMERA0_FRAME(void *a);
bool app_write_REG_CAMERA0_FREQUENCY(void *a);
bool app_write_REG_CAMERA1_FRAME(void *a);
bool app_write_REG_CAMERA1_FREQUENCY(void *a);
bool app_write_REG_RESERVED10(void *a);
bool app_write_REG_RESERVED11(void *a);
bool app_write_REG_RESERVED12(void *a);
bool app_write_REG_RESERVED13(void *a);
bool app_write_REG_SERVO_MOTOR2_PERIOD(void *a);
bool app_write_REG_SERVO_MOTOR2_PULSE(void *a);
bool app_write_REG_SERVO_MOTOR3_PERIOD(void *a);
bool app_write_REG_SERVO_MOTOR3_PULSE(void *a);
bool app_write_REG_RESERVED14(void *a);
bool app_write_REG_RESERVED15(void *a);
bool app_write_REG_RESERVED16(void *a);
bool app_write_REG_RESERVED17(void *a);
bool app_write_REG_ENCODER_RESET(void *a);
bool app_write_REG_RESERVED18(void *a);
bool app_write_REG_ENABLE_SERIAL_TIMESTAMP(void *a);
bool app_write_REG_MIMIC_PORT0_IR(void *a);
bool app_write_REG_MIMIC_PORT1_IR(void *a);
bool app_write_REG_MIMIC_PORT2_IR(void *a);
bool app_write_REG_RESERVED20(void *a);
bool app_write_REG_RESERVED21(void *a);
bool app_write_REG_RESERVED22(void *a);
bool app_write_REG_MIMIC_PORT0_VALVE(void *a);
bool app_write_REG_MIMIC_PORT1_VALVE(void *a);
bool app_write_REG_MIMIC_PORT2_VALVE(void *a);
bool app_write_REG_RESERVED23(void *a);
bool app_write_REG_RESERVED24(void *a);
bool app_write_REG_POKE_INPUT_FILTER(void *a);


#endif /* _APP_FUNCTIONS_H_ */