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
void app_read_REG_POKE_DIG_IN(void);
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
void app_read_REG_ADC(void);
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

bool app_write_REG_POKE_IN(void *a);
bool app_write_REG_POKE_DIG_IN(void *a);
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
bool app_write_REG_ADC(void *a);
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


#endif /* _APP_FUNCTIONS_H_ */