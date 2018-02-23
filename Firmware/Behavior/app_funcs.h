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
void app_read_REG_POKE_IN(void);
void app_read_REG_RESERVED0(void);
void app_read_REG_OUTPUTS_SET(void);
void app_read_REG_OUTPUTS_CLEAR(void);
void app_read_REG_OUTPUTS_TOGGLE(void);
void app_read_REG_OUTPUTS_OUT(void);
void app_read_REG_POKE_DIOS_SET(void);
void app_read_REG_POKE_DIOS_CLEAR(void);
void app_read_REG_POKE_DIOS_TOGGLE(void);
void app_read_REG_POKE_DIOS_OUT(void);
void app_read_REG_POKE_DIOS_CONF(void);
void app_read_REG_POKE_DIOS_IN(void);
void app_read_REG_ADC_AND_DECODER(void);
void app_read_REG_MODE_POKE0_LED(void);
void app_read_REG_MODE_POKE1_LED(void);
void app_read_REG_MODE_POKE2_LED(void);
void app_read_REG_MODE_POKE0_VALVE(void);
void app_read_REG_MODE_POKE1_VALVE(void);
void app_read_REG_MODE_POKE2_VALVE(void);
void app_read_REG_MODE_LED0(void);
void app_read_REG_MODE_LED1(void);
void app_read_REG_MODE_RGB0(void);
void app_read_REG_MODE_RGB1(void);
void app_read_REG_MODE_DO0(void);
void app_read_REG_MODE_DO1(void);
void app_read_REG_MODE_DO2(void);
void app_read_REG_MODE_DO3(void);
void app_read_REG_PULSE_POKE0_LED(void);
void app_read_REG_PULSE_POKE1_LED(void);
void app_read_REG_PULSE_POKE2_LED(void);
void app_read_REG_PULSE_POKE0_VALVE(void);
void app_read_REG_PULSE_POKE1_VALVE(void);
void app_read_REG_PULSE_POKE2_VALVE(void);
void app_read_REG_PULSE_LED0(void);
void app_read_REG_PULSE_LED1(void);
void app_read_REG_PULSE_RGB0(void);
void app_read_REG_PULSE_RGB1(void);
void app_read_REG_PULSE_DO0(void);
void app_read_REG_PULSE_DO1(void);
void app_read_REG_PULSE_DO2(void);
void app_read_REG_PULSE_DO3(void);
void app_read_REG_FREQ_DO0(void);
void app_read_REG_FREQ_DO1(void);
void app_read_REG_FREQ_DO2(void);
void app_read_REG_FREQ_DO3(void);
void app_read_REG_DCYCLE_DO0(void);
void app_read_REG_DCYCLE_DO1(void);
void app_read_REG_DCYCLE_DO2(void);
void app_read_REG_DCYCLE_DO3(void);
void app_read_REG_PWM_START(void);
void app_read_REG_PWM_STOP(void);
void app_read_REG_RGBS(void);
void app_read_REG_RGB0(void);
void app_read_REG_RGB1(void);
void app_read_REG_LED0_CURRENT(void);
void app_read_REG_LED1_CURRENT(void);
void app_read_REG_LED0_MAX_CURRENT(void);
void app_read_REG_LED1_MAX_CURRENT(void);
void app_read_REG_EVNT_ENABLE(void);
void app_read_REG_EN_CAMERAS(void);
void app_read_REG_DIS_CAMERAS(void);
void app_read_REG_EN_SERVOS(void);
void app_read_REG_DIS_SERVOS(void);
void app_read_REG_EN_ENCODERS(void);
void app_read_REG_DIS_ENCODERS(void);
void app_read_REG_RESERVED2(void);
void app_read_REG_RESERVED3(void);
void app_read_REG_RESERVED4(void);
void app_read_REG_RESERVED5(void);
void app_read_REG_RESERVED6(void);
void app_read_REG_RESERVED7(void);
void app_read_REG_RESERVED8(void);
void app_read_REG_RESERVED9(void);
void app_read_REG_CAM_OUT0_FRAME_ACQUIRED(void);
void app_read_REG_CAM_OUT0_FREQ(void);
void app_read_REG_CAM_OUT1_FRAME_ACQUIRED(void);
void app_read_REG_CAM_OUT2_FREQ(void);
void app_read_REG_RESERVED10(void);
void app_read_REG_RESERVED11(void);
void app_read_REG_RESERVED12(void);
void app_read_REG_RESERVED13(void);
void app_read_REG_MOTOR_OUT2_PERIOD(void);
void app_read_REG_MOTOR_OUT2_PULSE(void);
void app_read_REG_MOTOR_OUT3_PERIOD(void);
void app_read_REG_MOTOR_OUT3_PULSE(void);
void app_read_REG_RESERVED14(void);
void app_read_REG_RESERVED15(void);
void app_read_REG_RESERVED16(void);
void app_read_REG_RESERVED17(void);
void app_read_REG_ENCODERS_RESET(void);
void app_read_REG_RESERVED18(void);
void app_read_REG_RESERVED19(void);

bool app_write_REG_POKE_IN(void *a);
bool app_write_REG_RESERVED0(void *a);
bool app_write_REG_OUTPUTS_SET(void *a);
bool app_write_REG_OUTPUTS_CLEAR(void *a);
bool app_write_REG_OUTPUTS_TOGGLE(void *a);
bool app_write_REG_OUTPUTS_OUT(void *a);
bool app_write_REG_POKE_DIOS_SET(void *a);
bool app_write_REG_POKE_DIOS_CLEAR(void *a);
bool app_write_REG_POKE_DIOS_TOGGLE(void *a);
bool app_write_REG_POKE_DIOS_OUT(void *a);
bool app_write_REG_POKE_DIOS_CONF(void *a);
bool app_write_REG_POKE_DIOS_IN(void *a);
bool app_write_REG_ADC_AND_DECODER(void *a);
bool app_write_REG_MODE_POKE0_LED(void *a);
bool app_write_REG_MODE_POKE1_LED(void *a);
bool app_write_REG_MODE_POKE2_LED(void *a);
bool app_write_REG_MODE_POKE0_VALVE(void *a);
bool app_write_REG_MODE_POKE1_VALVE(void *a);
bool app_write_REG_MODE_POKE2_VALVE(void *a);
bool app_write_REG_MODE_LED0(void *a);
bool app_write_REG_MODE_LED1(void *a);
bool app_write_REG_MODE_RGB0(void *a);
bool app_write_REG_MODE_RGB1(void *a);
bool app_write_REG_MODE_DO0(void *a);
bool app_write_REG_MODE_DO1(void *a);
bool app_write_REG_MODE_DO2(void *a);
bool app_write_REG_MODE_DO3(void *a);
bool app_write_REG_PULSE_POKE0_LED(void *a);
bool app_write_REG_PULSE_POKE1_LED(void *a);
bool app_write_REG_PULSE_POKE2_LED(void *a);
bool app_write_REG_PULSE_POKE0_VALVE(void *a);
bool app_write_REG_PULSE_POKE1_VALVE(void *a);
bool app_write_REG_PULSE_POKE2_VALVE(void *a);
bool app_write_REG_PULSE_LED0(void *a);
bool app_write_REG_PULSE_LED1(void *a);
bool app_write_REG_PULSE_RGB0(void *a);
bool app_write_REG_PULSE_RGB1(void *a);
bool app_write_REG_PULSE_DO0(void *a);
bool app_write_REG_PULSE_DO1(void *a);
bool app_write_REG_PULSE_DO2(void *a);
bool app_write_REG_PULSE_DO3(void *a);
bool app_write_REG_FREQ_DO0(void *a);
bool app_write_REG_FREQ_DO1(void *a);
bool app_write_REG_FREQ_DO2(void *a);
bool app_write_REG_FREQ_DO3(void *a);
bool app_write_REG_DCYCLE_DO0(void *a);
bool app_write_REG_DCYCLE_DO1(void *a);
bool app_write_REG_DCYCLE_DO2(void *a);
bool app_write_REG_DCYCLE_DO3(void *a);
bool app_write_REG_PWM_START(void *a);
bool app_write_REG_PWM_STOP(void *a);
bool app_write_REG_RGBS(void *a);
bool app_write_REG_RGB0(void *a);
bool app_write_REG_RGB1(void *a);
bool app_write_REG_LED0_CURRENT(void *a);
bool app_write_REG_LED1_CURRENT(void *a);
bool app_write_REG_LED0_MAX_CURRENT(void *a);
bool app_write_REG_LED1_MAX_CURRENT(void *a);
bool app_write_REG_EVNT_ENABLE(void *a);
bool app_write_REG_EN_CAMERAS(void *a);
bool app_write_REG_DIS_CAMERAS(void *a);
bool app_write_REG_EN_SERVOS(void *a);
bool app_write_REG_DIS_SERVOS(void *a);
bool app_write_REG_EN_ENCODERS(void *a);
bool app_write_REG_DIS_ENCODERS(void *a);
bool app_write_REG_RESERVED2(void *a);
bool app_write_REG_RESERVED3(void *a);
bool app_write_REG_RESERVED4(void *a);
bool app_write_REG_RESERVED5(void *a);
bool app_write_REG_RESERVED6(void *a);
bool app_write_REG_RESERVED7(void *a);
bool app_write_REG_RESERVED8(void *a);
bool app_write_REG_RESERVED9(void *a);
bool app_write_REG_CAM_OUT0_FRAME_ACQUIRED(void *a);
bool app_write_REG_CAM_OUT0_FREQ(void *a);
bool app_write_REG_CAM_OUT1_FRAME_ACQUIRED(void *a);
bool app_write_REG_CAM_OUT2_FREQ(void *a);
bool app_write_REG_RESERVED10(void *a);
bool app_write_REG_RESERVED11(void *a);
bool app_write_REG_RESERVED12(void *a);
bool app_write_REG_RESERVED13(void *a);
bool app_write_REG_MOTOR_OUT2_PERIOD(void *a);
bool app_write_REG_MOTOR_OUT2_PULSE(void *a);
bool app_write_REG_MOTOR_OUT3_PERIOD(void *a);
bool app_write_REG_MOTOR_OUT3_PULSE(void *a);
bool app_write_REG_RESERVED14(void *a);
bool app_write_REG_RESERVED15(void *a);
bool app_write_REG_RESERVED16(void *a);
bool app_write_REG_RESERVED17(void *a);
bool app_write_REG_ENCODERS_RESET(void *a);
bool app_write_REG_RESERVED18(void *a);
bool app_write_REG_RESERVED19(void *a);


#endif /* _APP_FUNCTIONS_H_ */