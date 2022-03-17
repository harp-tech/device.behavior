#include "app_funcs.h"
#include "app_ios_and_regs.h"
#include "hwbp_core.h"

#define F_CPU 32000000
#include <util/delay.h>

#include "WS2812S.h"
#include "structs.h"

extern countdown_t pulse_countdown;

extern timer_conf_t timer_conf;
extern is_new_timer_conf_t is_new_timer_conf;

ports_state_t _states_;

/************************************************************************/
/* Create pointers to functions                                         */
/************************************************************************/
extern AppRegs app_regs;

void (*app_func_rd_pointer[])(void) = {
	&app_read_REG_PORT_DIS,
	&app_read_REG_RESERVED0,
	&app_read_REG_OUTPUTS_SET,
	&app_read_REG_OUTPUTS_CLEAR,
	&app_read_REG_OUTPUTS_TOGGLE,
	&app_read_REG_OUTPUTS_OUT,
	&app_read_REG_PORT_DIOS_SET,
	&app_read_REG_PORT_DIOS_CLEAR,
	&app_read_REG_PORT_DIOS_TOGGLE,
	&app_read_REG_PORT_DIOS_OUT,
	&app_read_REG_PORT_DIOS_CONF,
	&app_read_REG_PORT_DIOS_IN,
	&app_read_REG_DATA,
	&app_read_REG_OUTPUT_PULSE_EN,
	&app_read_REG_PULSE_PORT0_DO,
	&app_read_REG_PULSE_PORT1_DO,
	&app_read_REG_PULSE_PORT2_DO,
	&app_read_REG_PULSE_PORT0_12V,
	&app_read_REG_PULSE_PORT1_12V,
	&app_read_REG_PULSE_PORT2_12V,
	&app_read_REG_PULSE_LED0,
	&app_read_REG_PULSE_LED1,
	&app_read_REG_PULSE_RGB0,
	&app_read_REG_PULSE_RGB1,
	&app_read_REG_PULSE_DO0,
	&app_read_REG_PULSE_DO1,
	&app_read_REG_PULSE_DO2,
	&app_read_REG_PULSE_DO3,
	&app_read_REG_FREQ_DO0,
	&app_read_REG_FREQ_DO1,
	&app_read_REG_FREQ_DO2,
	&app_read_REG_FREQ_DO3,
	&app_read_REG_DCYCLE_DO0,
	&app_read_REG_DCYCLE_DO1,
	&app_read_REG_DCYCLE_DO2,
	&app_read_REG_DCYCLE_DO3,
	&app_read_REG_PWM_START,
	&app_read_REG_PWM_STOP,
	&app_read_REG_RGBS,
	&app_read_REG_RGB0,
	&app_read_REG_RGB1,
	&app_read_REG_LED0_CURRENT,
	&app_read_REG_LED1_CURRENT,
	&app_read_REG_LED0_MAX_CURRENT,
	&app_read_REG_LED1_MAX_CURRENT,
	&app_read_REG_EVNT_ENABLE,
	&app_read_REG_START_CAMERAS,
	&app_read_REG_STOP_CAMERAS,
	&app_read_REG_EN_SERVOS,
	&app_read_REG_DIS_SERVOS,
	&app_read_REG_EN_ENCODERS,
	&app_read_REG_RESERVED1,
	&app_read_REG_RESERVED2,
	&app_read_REG_RESERVED3,
	&app_read_REG_RESERVED4,
	&app_read_REG_RESERVED5,
	&app_read_REG_RESERVED6,
	&app_read_REG_RESERVED7,
	&app_read_REG_RESERVED8,
	&app_read_REG_RESERVED9,
	&app_read_REG_CAM_OUT0_FRAME_ACQUIRED,
	&app_read_REG_CAM_OUT0_FREQ,
	&app_read_REG_CAM_OUT1_FRAME_ACQUIRED,
	&app_read_REG_CAM_OUT1_FREQ,
	&app_read_REG_RESERVED10,
	&app_read_REG_RESERVED11,
	&app_read_REG_RESERVED12,
	&app_read_REG_RESERVED13,
	&app_read_REG_MOTOR_OUT2_PERIOD,
	&app_read_REG_MOTOR_OUT2_PULSE,
	&app_read_REG_MOTOR_OUT3_PERIOD,
	&app_read_REG_MOTOR_OUT3_PULSE,
	&app_read_REG_RESERVED14,
	&app_read_REG_RESERVED15,
	&app_read_REG_RESERVED16,
	&app_read_REG_RESERVED17,
	&app_read_REG_ENCODERS_RESET,
	&app_read_REG_RESERVED18,
	&app_read_REG_RESERVED19,   
  	&app_read_REG_MIMIC_PORT0_IR,
  	&app_read_REG_MIMIC_PORT1_IR,
  	&app_read_REG_MIMIC_PORT2_IR,
  	&app_read_REG_RESERVED20,
  	&app_read_REG_RESERVED21,
  	&app_read_REG_RESERVED22,
  	&app_read_REG_MIMIC_PORT0_VALVE,
  	&app_read_REG_MIMIC_PORT1_VALVE,
  	&app_read_REG_MIMIC_PORT2_VALVE,
  	&app_read_REG_RESERVED23,
  	&app_read_REG_RESERVED24,
  	&app_read_REG_POKE_INPUT_FILTER_MS
};

bool (*app_func_wr_pointer[])(void*) = {
	&app_write_REG_PORT_DIS,
	&app_write_REG_RESERVED0,
	&app_write_REG_OUTPUTS_SET,
	&app_write_REG_OUTPUTS_CLEAR,
	&app_write_REG_OUTPUTS_TOGGLE,
	&app_write_REG_OUTPUTS_OUT,
	&app_write_REG_PORT_DIOS_SET,
	&app_write_REG_PORT_DIOS_CLEAR,
	&app_write_REG_PORT_DIOS_TOGGLE,
	&app_write_REG_PORT_DIOS_OUT,
	&app_write_REG_PORT_DIOS_CONF,
	&app_write_REG_PORT_DIOS_IN,
	&app_write_REG_DATA,
	&app_write_REG_OUTPUT_PULSE_EN,
	&app_write_REG_PULSE_PORT0_DO,
	&app_write_REG_PULSE_PORT1_DO,
	&app_write_REG_PULSE_PORT2_DO,
	&app_write_REG_PULSE_PORT0_12V,
	&app_write_REG_PULSE_PORT1_12V,
	&app_write_REG_PULSE_PORT2_12V,
	&app_write_REG_PULSE_LED0,
	&app_write_REG_PULSE_LED1,
	&app_write_REG_PULSE_RGB0,
	&app_write_REG_PULSE_RGB1,
	&app_write_REG_PULSE_DO0,
	&app_write_REG_PULSE_DO1,
	&app_write_REG_PULSE_DO2,
	&app_write_REG_PULSE_DO3,
	&app_write_REG_FREQ_DO0,
	&app_write_REG_FREQ_DO1,
	&app_write_REG_FREQ_DO2,
	&app_write_REG_FREQ_DO3,
	&app_write_REG_DCYCLE_DO0,
	&app_write_REG_DCYCLE_DO1,
	&app_write_REG_DCYCLE_DO2,
	&app_write_REG_DCYCLE_DO3,
	&app_write_REG_PWM_START,
	&app_write_REG_PWM_STOP,
	&app_write_REG_RGBS,
	&app_write_REG_RGB0,
	&app_write_REG_RGB1,
	&app_write_REG_LED0_CURRENT,
	&app_write_REG_LED1_CURRENT,
	&app_write_REG_LED0_MAX_CURRENT,
	&app_write_REG_LED1_MAX_CURRENT,
	&app_write_REG_EVNT_ENABLE,
	&app_write_REG_START_CAMERAS,
	&app_write_REG_STOP_CAMERAS,
	&app_write_REG_EN_SERVOS,
	&app_write_REG_DIS_SERVOS,
	&app_write_REG_EN_ENCODERS,
	&app_write_REG_RESERVED1,
	&app_write_REG_RESERVED2,
	&app_write_REG_RESERVED3,
	&app_write_REG_RESERVED4,
	&app_write_REG_RESERVED5,
	&app_write_REG_RESERVED6,
	&app_write_REG_RESERVED7,
	&app_write_REG_RESERVED8,
	&app_write_REG_RESERVED9,
	&app_write_REG_CAM_OUT0_FRAME_ACQUIRED,
	&app_write_REG_CAM_OUT0_FREQ,
	&app_write_REG_CAM_OUT1_FRAME_ACQUIRED,
	&app_write_REG_CAM_OUT1_FREQ,
	&app_write_REG_RESERVED10,
	&app_write_REG_RESERVED11,
	&app_write_REG_RESERVED12,
	&app_write_REG_RESERVED13,
	&app_write_REG_MOTOR_OUT2_PERIOD,
	&app_write_REG_MOTOR_OUT2_PULSE,
	&app_write_REG_MOTOR_OUT3_PERIOD,
	&app_write_REG_MOTOR_OUT3_PULSE,
	&app_write_REG_RESERVED14,
	&app_write_REG_RESERVED15,
	&app_write_REG_RESERVED16,
	&app_write_REG_RESERVED17,
	&app_write_REG_ENCODERS_RESET,
	&app_write_REG_RESERVED18,
	&app_write_REG_RESERVED19,
	&app_write_REG_MIMIC_PORT0_IR,
	&app_write_REG_MIMIC_PORT1_IR,
	&app_write_REG_MIMIC_PORT2_IR,
	&app_write_REG_RESERVED20,
	&app_write_REG_RESERVED21,
	&app_write_REG_RESERVED22,
	&app_write_REG_MIMIC_PORT0_VALVE,
	&app_write_REG_MIMIC_PORT1_VALVE,
	&app_write_REG_MIMIC_PORT2_VALVE,
	&app_write_REG_RESERVED23,
	&app_write_REG_RESERVED24,
	&app_write_REG_POKE_INPUT_FILTER_MS
};


/************************************************************************/
/* REG_PORTS_IN                                                          */
/************************************************************************/
void app_read_REG_PORT_DIS(void)
{
	app_regs.REG_PORT_DIS = (read_POKE0_IR) ? B_DI0 : 0;
	app_regs.REG_PORT_DIS |= (read_POKE1_IR) ? B_DI1 : 0;
	app_regs.REG_PORT_DIS |= (read_POKE2_IR) ? B_DI2 : 0;
}
bool app_write_REG_PORT_DIS(void *a) { return false; }


/************************************************************************/
/* REG_RESERVED0                                                        */
/************************************************************************/
void app_read_REG_RESERVED0(void) {}
bool app_write_REG_RESERVED0(void *a)
{
	uint8_t reg = *((uint8_t*)a);
	app_regs.REG_RESERVED0 = reg;
	return true;
}


/************************************************************************/
/* REG_OUTPUTS_SET                                                      */
/************************************************************************/
bool rgb0_on = false;
bool rgb1_on = false;

#define start_POKE0_LED do {set_POKE0_LED; if (app_regs.REG_OUTPUT_PULSE_EN & B_PORT0_DO) pulse_countdown.poke0_led = app_regs.REG_PULSE_PORT0_DO + 1; } while(0)
#define start_POKE1_LED do {set_POKE1_LED; if (app_regs.REG_OUTPUT_PULSE_EN & B_PORT1_DO) pulse_countdown.poke1_led = app_regs.REG_PULSE_PORT1_DO + 1; } while(0)
#define start_POKE2_LED do {set_POKE2_LED; if (app_regs.REG_OUTPUT_PULSE_EN & B_PORT2_DO) pulse_countdown.poke2_led = app_regs.REG_PULSE_PORT2_DO + 1; } while(0)

#define start_POKE0_VALVE do {set_POKE0_VALVE; if (app_regs.REG_OUTPUT_PULSE_EN & B_PORT0_12V) pulse_countdown.poke0_valve = app_regs.REG_PULSE_PORT0_12V + 1; } while(0)
#define start_POKE1_VALVE do {set_POKE1_VALVE; if (app_regs.REG_OUTPUT_PULSE_EN & B_PORT1_12V) pulse_countdown.poke1_valve = app_regs.REG_PULSE_PORT1_12V + 1; } while(0)
#define start_POKE2_VALVE do {set_POKE2_VALVE; if (app_regs.REG_OUTPUT_PULSE_EN & B_PORT2_12V) pulse_countdown.poke2_valve = app_regs.REG_PULSE_PORT2_12V + 1; } while(0)

#define start_LED0 do {set_LED0; if (app_regs.REG_OUTPUT_PULSE_EN & B_LED0) pulse_countdown.led0 = app_regs.REG_PULSE_LED0 + 1; } while(0)
#define start_LED1 do {set_LED1; if (app_regs.REG_OUTPUT_PULSE_EN & B_LED1) pulse_countdown.led1 = app_regs.REG_PULSE_LED1 + 1; } while(0)

#define start_RGB0 do {rgb0_on = true; if (app_regs.REG_OUTPUT_PULSE_EN & B_RGB0) pulse_countdown.rgb0 = app_regs.REG_PULSE_RGB0 + 1; } while(0)
#define start_RGB1 do {rgb1_on = true; if (app_regs.REG_OUTPUT_PULSE_EN & B_RGB1) pulse_countdown.rgb1 = app_regs.REG_PULSE_RGB1 + 1; } while(0)

#define start_DO0 do {set_DO0; if (app_regs.REG_OUTPUT_PULSE_EN & B_DO0) pulse_countdown.do0 = app_regs.REG_PULSE_DO0 + 1; } while(0)
#define start_DO1 do {set_DO1; if (app_regs.REG_OUTPUT_PULSE_EN & B_DO1) pulse_countdown.do1 = app_regs.REG_PULSE_DO1 + 1; } while(0)
#define start_DO2 do {set_DO2; if (app_regs.REG_OUTPUT_PULSE_EN & B_DO2) pulse_countdown.do2 = app_regs.REG_PULSE_DO2 + 1; } while(0)
#define start_DO3 do {set_DO3; if (app_regs.REG_OUTPUT_PULSE_EN & B_DO3) pulse_countdown.do3 = app_regs.REG_PULSE_DO3 + 1; } while(0)

void handle_Rgbs(bool use_rgb0, bool use_rgb1)
{
	uint8_t led0[3] = {0, 0, 0};
	uint8_t led1[3] = {0, 0, 0};
	
	if (use_rgb0 == true)
	{
		led0[0] = app_regs.REG_RGB0[0];
		led0[1] = app_regs.REG_RGB0[1];
		led0[2] = app_regs.REG_RGB0[2];
	}
	
	if (use_rgb1 == true)
	{
		led1[0] = app_regs.REG_RGB1[0];
		led1[1] = app_regs.REG_RGB1[1];
		led1[2] = app_regs.REG_RGB1[2];
	}
    
    /* Disable TX interrupts before update disable USART TX and RX */
    PMIC_CTRL = PMIC_RREN_bm | PMIC_LOLVLEN_bm | PMIC_MEDLVLEN_bm;
    		
    /* Disable USART TX */
    uint8_t usartf0_ctrla = USARTF0_CTRLA;
    USARTF0_CTRLA = 0;

    /* Write to RGBs */
    update_2rgbs(led0, led1);

    /* Recover USART TX Interrupt state */
    USARTF0_CTRLA = usartf0_ctrla;

    /* Re-enable high level interrupts */
    PMIC_CTRL = PMIC_RREN_bm | PMIC_LOLVLEN_bm | PMIC_MEDLVLEN_bm | PMIC_HILVLEN_bm;
}

void app_read_REG_OUTPUTS_SET(void) {}
bool app_write_REG_OUTPUTS_SET(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	if (reg & B_PORT0_DO) start_POKE0_LED;
	if (reg & B_PORT1_DO) start_POKE1_LED;
	if (reg & B_PORT2_DO) start_POKE2_LED;
	
	if (reg & B_PORT0_12V) start_POKE0_VALVE;
	if (reg & B_PORT1_12V) start_POKE1_VALVE;
	if (reg & B_PORT2_12V) start_POKE2_VALVE;
	
	if (reg & B_LED0) start_LED0;
	if (reg & B_LED1) start_LED1;
	
	if (reg & B_RGB0) start_RGB0;
	if (reg & B_RGB1) start_RGB1;
	if ((reg & B_RGB0) || (reg & B_RGB1))
	{
		handle_Rgbs(rgb0_on, rgb1_on);
	}
	
	if (reg & B_DO0) start_DO0;
	if (reg & B_DO1) start_DO1;
	if (reg & B_DO2) start_DO2;
	if (reg & B_DO3) start_DO3;
    
   app_regs.REG_OUTPUTS_OUT |= reg;
	app_regs.REG_OUTPUTS_SET = reg;
	
	return true;
}


/************************************************************************/
/* REG_OUTPUTS_CLEAR                                                    */
/************************************************************************/
void app_read_REG_OUTPUTS_CLEAR(void) {}
bool app_write_REG_OUTPUTS_CLEAR(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	if (reg & B_PORT0_DO) clr_POKE0_LED;
	if (reg & B_PORT1_DO) clr_POKE1_LED;
	if (reg & B_PORT2_DO) clr_POKE2_LED;
	
	if (reg & B_PORT0_12V) clr_POKE0_VALVE;
	if (reg & B_PORT1_12V) clr_POKE1_VALVE;
	if (reg & B_PORT2_12V) clr_POKE2_VALVE;
	
	if (reg & B_LED0) clr_LED0;
	if (reg & B_LED1) clr_LED1;
	
	if (reg & B_RGB0) rgb0_on = false;
	if (reg & B_RGB1) rgb1_on = false;
	if ((reg & B_RGB0) || (reg & B_RGB1))
	{
		handle_Rgbs(rgb0_on, rgb1_on);
	}

	if (reg & B_DO0) clr_DO0;
	if (reg & B_DO1) clr_DO1;
	if (reg & B_DO2) clr_DO2;
	if (reg & B_DO3) clr_DO3;
    
   app_regs.REG_OUTPUTS_OUT &= ~reg;	
	app_regs.REG_OUTPUTS_CLEAR = reg;
	
	return true;
}


/************************************************************************/
/* REG_OUTPUTS_TOGGLE                                                   */
/************************************************************************/
void app_read_REG_OUTPUTS_TOGGLE(void) {}
bool app_write_REG_OUTPUTS_TOGGLE(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	if (reg & B_PORT0_DO) { if (read_POKE0_LED) tgl_POKE0_LED; else start_POKE0_LED;}
	if (reg & B_PORT1_DO) { if (read_POKE1_LED) tgl_POKE1_LED; else start_POKE1_LED;}
	if (reg & B_PORT2_DO) { if (read_POKE2_LED) tgl_POKE2_LED; else start_POKE2_LED;}
	
	if (reg & B_PORT0_12V) { if (read_POKE0_VALVE) tgl_POKE0_VALVE; else start_POKE0_VALVE;}
	if (reg & B_PORT1_12V) { if (read_POKE1_VALVE) tgl_POKE1_VALVE; else start_POKE1_VALVE;}
	if (reg & B_PORT2_12V) { if (read_POKE2_VALVE) tgl_POKE2_VALVE; else start_POKE2_VALVE;}
	

	if (reg & B_LED0) { if (!read_LED0) tgl_LED0; else start_LED0;}
	if (reg & B_LED1) { if (!read_LED1) tgl_LED1; else start_LED1;}
	
	if (reg & B_RGB0)
	{
		if (rgb0_on)
			rgb0_on = false;
		else
			start_RGB0;
	}
	if (reg & B_RGB1)
	{
		if (rgb1_on)
			rgb1_on = false;
		else
			start_RGB1;
	}
	if ((reg & B_RGB0) || (reg & B_RGB1))
	{
		handle_Rgbs(rgb0_on, rgb1_on);
	}
	
	if (reg & B_DO0) { if (read_DO0) tgl_DO0; else start_DO0;}
	if (reg & B_DO1) { if (read_DO1) tgl_DO1; else start_DO1;}
	if (reg & B_DO2) { if (read_DO2) tgl_DO2; else start_DO2;}
	if (reg & B_DO3) { if (read_DO3) tgl_DO3; else start_DO3;}
        
	app_regs.REG_OUTPUTS_OUT ^= reg;
	app_regs.REG_OUTPUTS_TOGGLE = reg;

	return true;
}


/************************************************************************/
/* REG_OUTPUTS_OUT                                                      */
/************************************************************************/
void app_read_REG_OUTPUTS_OUT(void)
{
	app_regs.REG_OUTPUTS_OUT = (read_POKE0_LED) ? B_PORT0_DO : 0;
	app_regs.REG_OUTPUTS_OUT |= (read_POKE1_LED) ? B_PORT1_DO : 0;
	app_regs.REG_OUTPUTS_OUT |= (read_POKE2_LED) ? B_PORT2_DO : 0;
	
	app_regs.REG_OUTPUTS_OUT |= (read_POKE0_VALVE) ? B_PORT0_12V: 0;
	app_regs.REG_OUTPUTS_OUT |= (read_POKE1_VALVE) ? B_PORT1_12V: 0;
	app_regs.REG_OUTPUTS_OUT |= (read_POKE2_VALVE) ? B_PORT2_12V: 0;
	
	app_regs.REG_OUTPUTS_OUT |= (read_LED0) ? 0 : B_LED0;
	app_regs.REG_OUTPUTS_OUT |= (read_LED1) ? 0 : B_LED1;
	
	app_regs.REG_OUTPUTS_OUT |= (rgb0_on) ? B_RGB0 : 0;
	app_regs.REG_OUTPUTS_OUT |= (rgb1_on) ? B_RGB1 : 0;
	
	app_regs.REG_OUTPUTS_OUT |= (read_DO0) ? B_DO0 : 0;
	app_regs.REG_OUTPUTS_OUT |= (read_DO1) ? B_DO1 : 0;
	app_regs.REG_OUTPUTS_OUT |= (read_DO2) ? B_DO2 : 0;
	app_regs.REG_OUTPUTS_OUT |= (read_DO3) ? B_DO3 : 0;
}

bool app_write_REG_OUTPUTS_OUT(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	bool prev_rgb0_on, prev_rgb1_on;
	prev_rgb0_on = rgb0_on;
	prev_rgb1_on = rgb1_on;
	
	if (reg & B_PORT0_DO) start_POKE0_LED; else clr_POKE0_LED;
	if (reg & B_PORT1_DO) start_POKE1_LED; else clr_POKE1_LED;
	if (reg & B_PORT2_DO) start_POKE2_LED; else clr_POKE2_LED;
	
	if (reg & B_PORT0_12V) start_POKE0_VALVE; else clr_POKE0_VALVE;
	if (reg & B_PORT1_12V) start_POKE1_VALVE; else clr_POKE1_VALVE;
	if (reg & B_PORT2_12V) start_POKE2_VALVE; else clr_POKE2_VALVE;
	
	if (reg & B_LED0) start_LED0; else clr_LED0;
	if (reg & B_LED1) start_LED1; else clr_LED1;
	
	if (reg & B_RGB0) start_RGB0; else rgb0_on = false;
	if (reg & B_RGB1) start_RGB1; else rgb1_on = false;
	
	if ((prev_rgb0_on != rgb0_on) || (prev_rgb1_on != rgb1_on))
	{
		handle_Rgbs(rgb0_on, rgb1_on);
	}
	
	if (reg & B_DO0) start_DO0; else clr_DO0;
	if (reg & B_DO1) start_DO1; else clr_DO1;
	if (reg & B_DO2) start_DO2; else clr_DO2;
	if (reg & B_DO3) start_DO3; else clr_DO3;
    
    app_regs.REG_OUTPUTS_OUT = reg;

	return true;
}


/************************************************************************/
/* REG_PORT_DIOS_SET                                                    */
/************************************************************************/
void app_read_REG_PORT_DIOS_SET(void)
{
	//app_regs.REG_PORT_DIOS_SET = 0;

}

bool app_write_REG_PORT_DIOS_SET(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_PORT_DIOS_SET = reg;
	return true;
}


/************************************************************************/
/* REG_PORT_DIOS_CLEAR                                                  */
/************************************************************************/
void app_read_REG_PORT_DIOS_CLEAR(void)
{
	//app_regs.REG_PORT_DIOS_CLEAR = 0;

}

bool app_write_REG_PORT_DIOS_CLEAR(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_PORT_DIOS_CLEAR = reg;
	return true;
}


/************************************************************************/
/* REG_PORT_DIOS_TOGGLE                                                 */
/************************************************************************/
void app_read_REG_PORT_DIOS_TOGGLE(void)
{
	//app_regs.REG_PORT_DIOS_TOGGLE = 0;

}

bool app_write_REG_PORT_DIOS_TOGGLE(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_PORT_DIOS_TOGGLE = reg;
	return true;
}


/************************************************************************/
/* REG_PORT_DIOS_OUT                                                    */
/************************************************************************/
void app_read_REG_PORT_DIOS_OUT(void)
{
	//app_regs.REG_PORT_DIOS_OUT = 0;

}

bool app_write_REG_PORT_DIOS_OUT(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_PORT_DIOS_OUT = reg;
	return true;
}


/************************************************************************/
/* REG_PORT_DIOS_CONF                                                   */
/************************************************************************/
void app_read_REG_PORT_DIOS_CONF(void)
{
	//app_regs.REG_PORT_DIOS_CONF = 0;

}

bool app_write_REG_PORT_DIOS_CONF(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_PORT_DIOS_CONF = reg;
	return true;
}


/************************************************************************/
/* REG_PORT_DIOS_IN                                                     */
/************************************************************************/
void app_read_REG_PORT_DIOS_IN(void)
{
	//app_regs.REG_PORT_DIOS_IN = 0;
	
	app_regs.REG_PORT_DIOS_IN = (read_POKE0_IO) ? B_DIO0 : 0; 
	app_regs.REG_PORT_DIOS_IN |= (read_POKE1_IO) ? B_DIO1 : 0; 
	app_regs.REG_PORT_DIOS_IN |= (read_POKE2_IO) ? B_DIO2 : 0; 

}

bool app_write_REG_PORT_DIOS_IN(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_PORT_DIOS_IN = reg;
	return true;
}


/************************************************************************/
/* REG_DATA                                                             */
/************************************************************************/
// This register is an array with 2 positions
void app_read_REG_DATA(void) {}      // The register is always updated
bool app_write_REG_DATA(void *a)     
{
	uint16_t *reg = ((uint16_t*)a);

	app_regs.REG_DATA[1] = reg[1];   // Write only to encoder counter

	if (_states_.quad_counter.port2)
	{
		TCD1_CNT = 0x8000 + reg[1];	// Write only to encoder counter
	}
    
	return true;
}


/************************************************************************/
/* REG_MODE_POKE0_LED                                                   */
/************************************************************************/
void app_read_REG_OUTPUT_PULSE_EN(void) {}
bool app_write_REG_OUTPUT_PULSE_EN(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_OUTPUT_PULSE_EN = reg;
	return true;
}


/************************************************************************/
/* REG_PULSE_PORT0_DO                                                  */
/************************************************************************/
void app_read_REG_PULSE_PORT0_DO(void) {}
bool app_write_REG_PULSE_PORT0_DO(void *a)
{
	if (*((uint16_t*)a) < 1)
        return false;
        
	app_regs.REG_PULSE_PORT0_DO = *((uint16_t*)a);
	return true;
}


/************************************************************************/
/* REG_PULSE_PORT1_DO                                                  */
/************************************************************************/
void app_read_REG_PULSE_PORT1_DO(void) {}
bool app_write_REG_PULSE_PORT1_DO(void *a)
{
    if (*((uint16_t*)a) < 1)
        return false;
    
	app_regs.REG_PULSE_PORT1_DO = *((uint16_t*)a);
	return true;
}


/************************************************************************/
/* REG_PULSE_PORT2_DO                                                  */
/************************************************************************/
void app_read_REG_PULSE_PORT2_DO(void) {}
bool app_write_REG_PULSE_PORT2_DO(void *a)
{
    if (*((uint16_t*)a) < 1)
        return false;

	app_regs.REG_PULSE_PORT2_DO = *((uint16_t*)a);
	return true;
}


/************************************************************************/
/* REG_PULSE_PORT0_12V                                                */
/************************************************************************/
void app_read_REG_PULSE_PORT0_12V(void) {}
bool app_write_REG_PULSE_PORT0_12V(void *a)
{
    if (*((uint16_t*)a) < 1)
        return false;

	app_regs.REG_PULSE_PORT0_12V = *((uint16_t*)a);
	return true;
}


/************************************************************************/
/* REG_PULSE_PORT1_12V                                                */
/************************************************************************/
void app_read_REG_PULSE_PORT1_12V(void) {}
bool app_write_REG_PULSE_PORT1_12V(void *a)
{
    if (*((uint16_t*)a) < 1)
        return false;

	app_regs.REG_PULSE_PORT1_12V = *((uint16_t*)a);
	return true;
}


/************************************************************************/
/* REG_PULSE_PORT2_12V                                                */
/************************************************************************/
void app_read_REG_PULSE_PORT2_12V(void) {}
bool app_write_REG_PULSE_PORT2_12V(void *a)
{
    if (*((uint16_t*)a) < 1)
        return false;

	app_regs.REG_PULSE_PORT2_12V = *((uint16_t*)a);
	return true;
}


/************************************************************************/
/* REG_PULSE_LED0                                                       */
/************************************************************************/
void app_read_REG_PULSE_LED0(void) {}
bool app_write_REG_PULSE_LED0(void *a)
{
    if (*((uint16_t*)a) < 1)
        return false;

	app_regs.REG_PULSE_LED0 = *((uint16_t*)a);
	return true;
}


/************************************************************************/
/* REG_PULSE_LED1                                                       */
/************************************************************************/
void app_read_REG_PULSE_LED1(void) {}
bool app_write_REG_PULSE_LED1(void *a)
{
    if (*((uint16_t*)a) < 1)
        return false;

	app_regs.REG_PULSE_LED1 = *((uint16_t*)a);
	return true;
}


/************************************************************************/
/* REG_PULSE_RGB0                                                       */
/************************************************************************/
void app_read_REG_PULSE_RGB0(void) {}
bool app_write_REG_PULSE_RGB0(void *a)
{
    if (*((uint16_t*)a) < 1)
        return false;

	app_regs.REG_PULSE_RGB0 = *((uint16_t*)a);
	return true;
}


/************************************************************************/
/* REG_PULSE_RGB1                                                       */
/************************************************************************/
void app_read_REG_PULSE_RGB1(void) {}
bool app_write_REG_PULSE_RGB1(void *a)
{
    if (*((uint16_t*)a) < 1)
        return false;

	app_regs.REG_PULSE_RGB1 = *((uint16_t*)a);
	return true;
}


/************************************************************************/
/* REG_PULSE_DO0                                                        */
/************************************************************************/
void app_read_REG_PULSE_DO0(void) {}
bool app_write_REG_PULSE_DO0(void *a)
{
    if (*((uint16_t*)a) < 1)
        return false;

	app_regs.REG_PULSE_DO0 = *((uint16_t*)a);
	return true;
}


/************************************************************************/
/* REG_PULSE_DO1                                                        */
/************************************************************************/
void app_read_REG_PULSE_DO1(void) {}
bool app_write_REG_PULSE_DO1(void *a)
{
    if (*((uint16_t*)a) < 1)
        return false;

	app_regs.REG_PULSE_DO1 = *((uint16_t*)a);
	return true;
}


/************************************************************************/
/* REG_PULSE_DO2                                                        */
/************************************************************************/
void app_read_REG_PULSE_DO2(void) {}
bool app_write_REG_PULSE_DO2(void *a)
{
    if (*((uint16_t*)a) < 1)
        return false;

	app_regs.REG_PULSE_DO2 = *((uint16_t*)a);
	return true;
}


/************************************************************************/
/* REG_PULSE_DO3                                                        */
/************************************************************************/
void app_read_REG_PULSE_DO3(void) {}
bool app_write_REG_PULSE_DO3(void *a)
{
    if (*((uint16_t*)a) < 1)
        return false;

	app_regs.REG_PULSE_DO3 = *((uint16_t*)a);
	return true;
}


/************************************************************************/
/* REG_FREQ_DO0                                                         */
/************************************************************************/
void app_read_REG_FREQ_DO0(void) {}
bool app_write_REG_FREQ_DO0(void *a)
{
	uint16_t reg = *((uint16_t*)a);
    
    if (reg < 1 || reg > 10000)
        return false;
    
    calculate_timer_16bits(32000000, reg, &timer_conf.prescaler_do0, &timer_conf.target_do0);
    timer_conf.dcycle_do0 = app_regs.REG_DCYCLE_DO0/100.0 * timer_conf.target_do0 + 0.5;    
    is_new_timer_conf.pwm_do0 = true;

	app_regs.REG_FREQ_DO0 = reg;
	return true;
}


/************************************************************************/
/* REG_FREQ_DO1                                                         */
/************************************************************************/
void app_read_REG_FREQ_DO1(void) {}
bool app_write_REG_FREQ_DO1(void *a)
{
	uint16_t reg = *((uint16_t*)a);
    
    if (reg < 1 || reg > 10000)
        return false;
    
    calculate_timer_16bits(32000000, reg, &timer_conf.prescaler_do1, &timer_conf.target_do1);
    timer_conf.dcycle_do1 = app_regs.REG_DCYCLE_DO1/100.0 * timer_conf.target_do1 + 0.5;    
    is_new_timer_conf.pwm_do1 = true;

	app_regs.REG_FREQ_DO1 = reg;
	return true;
}


/************************************************************************/
/* REG_FREQ_DO2                                                         */
/************************************************************************/
void app_read_REG_FREQ_DO2(void)
{
	//app_regs.REG_FREQ_DO2 = 0;

}

bool app_write_REG_FREQ_DO2(void *a)
{
	uint16_t reg = *((uint16_t*)a);
    
    if (reg < 1 || reg > 10000)
        return false;
    
    calculate_timer_16bits(32000000, reg, &timer_conf.prescaler_do2, &timer_conf.target_do2);
    timer_conf.dcycle_do2 = app_regs.REG_DCYCLE_DO2/100.0 * timer_conf.target_do2 + 0.5;    
    is_new_timer_conf.pwm_do2 = true;

	app_regs.REG_FREQ_DO2 = reg;
	return true;
}


/************************************************************************/
/* REG_FREQ_DO3                                                         */
/************************************************************************/
void app_read_REG_FREQ_DO3(void)
{
	//app_regs.REG_FREQ_DO3 = 0;

}

bool app_write_REG_FREQ_DO3(void *a)
{
	uint16_t reg = *((uint16_t*)a);
    
    if (reg < 1 || reg > 10000)
        return false;
    
    calculate_timer_16bits(32000000, reg, &timer_conf.prescaler_do3, &timer_conf.target_do3);
    timer_conf.dcycle_do3 = app_regs.REG_DCYCLE_DO3/100.0 * timer_conf.target_do3 + 0.5;
    is_new_timer_conf.pwm_do3 = true;

	app_regs.REG_FREQ_DO3 = reg;
	return true;
}


/************************************************************************/
/* REG_DCYCLE_DO0                                                       */
/************************************************************************/
void app_read_REG_DCYCLE_DO0(void) {}
bool app_write_REG_DCYCLE_DO0(void *a)
{
	uint8_t reg = *((uint8_t*)a);
	
	if (reg < 1 || reg > 99)
	    return false;
    
    //calculate_timer_16bits(32000000, app_regs.REG_FREQ_DO0, &timer_conf.prescaler_do0, &timer_conf.target_do0);
    timer_conf.dcycle_do0 = reg/100.0 * timer_conf.target_do0 + 0.5;
    is_new_timer_conf.pwm_do0 = true;
    
	app_regs.REG_DCYCLE_DO0 = reg;
	return true;
}


/************************************************************************/
/* REG_DCYCLE_DO1                                                       */
/************************************************************************/
void app_read_REG_DCYCLE_DO1(void)
{
	//app_regs.REG_DCYCLE_DO1 = 0;

}

bool app_write_REG_DCYCLE_DO1(void *a)
{
	uint8_t reg = *((uint8_t*)a);
	
	if (reg < 1 || reg > 99)
	    return false;
    
    //calculate_timer_16bits(32000000, app_regs.REG_FREQ_DO0, &timer_conf.prescaler_do0, &timer_conf.target_do0);
    timer_conf.dcycle_do1 = reg/100.0 * timer_conf.target_do1 + 0.5;
    is_new_timer_conf.pwm_do1 = true;

	app_regs.REG_DCYCLE_DO1 = reg;
	return true;
}


/************************************************************************/
/* REG_DCYCLE_DO2                                                       */
/************************************************************************/
void app_read_REG_DCYCLE_DO2(void)
{
	//app_regs.REG_DCYCLE_DO2 = 0;

}

bool app_write_REG_DCYCLE_DO2(void *a)
{
	uint8_t reg = *((uint8_t*)a);
	
	if (reg < 1 || reg > 99)
	    return false;
    
    //calculate_timer_16bits(32000000, app_regs.REG_FREQ_DO0, &timer_conf.prescaler_do0, &timer_conf.target_do0);
    timer_conf.dcycle_do2 = reg/100.0 * timer_conf.target_do2 + 0.5;
    is_new_timer_conf.pwm_do2 = true;

	app_regs.REG_DCYCLE_DO2 = reg;
	return true;
}


/************************************************************************/
/* REG_DCYCLE_DO3                                                       */
/************************************************************************/
void app_read_REG_DCYCLE_DO3(void)
{
	//app_regs.REG_DCYCLE_DO3 = 0;

}

bool app_write_REG_DCYCLE_DO3(void *a)
{
	uint8_t reg = *((uint8_t*)a);
	
	if (reg < 1 || reg > 99)
	    return false;
    
    //calculate_timer_16bits(32000000, app_regs.REG_FREQ_DO0, &timer_conf.prescaler_do0, &timer_conf.target_do0);
    timer_conf.dcycle_do3 = reg/100.0 * timer_conf.target_do3 + 0.5;
    is_new_timer_conf.pwm_do3 = true;

	app_regs.REG_DCYCLE_DO3 = reg;
	return true;
}


/************************************************************************/
/* REG_PWM_START                                                        */
/************************************************************************/
void app_read_REG_PWM_START(void)
{
    app_regs.REG_PWM_START = 0;
    
    if(_states_.pwm.do0)
    {
        app_regs.REG_PWM_START |= B_PWM_DO0;
    }
    
    if(_states_.pwm.do1)
    {
        app_regs.REG_PWM_START |= B_PWM_DO1;
    }
    
    if(_states_.pwm.do2)
    {
        app_regs.REG_PWM_START |= B_PWM_DO2;
    }
    
    if(_states_.pwm.do3)
    {
        app_regs.REG_PWM_START |= B_PWM_DO3;
    }
}

bool app_write_REG_PWM_START(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	if ((reg & B_PWM_DO0) && !_states_.pwm.do0)
    {
        timer_type0_pwm(&TCF0, timer_conf.prescaler_do0, timer_conf.target_do0, timer_conf.dcycle_do0, INT_LEVEL_LOW, INT_LEVEL_OFF);
        is_new_timer_conf.pwm_do0 = false;
        _states_.pwm.do0 = true;
        start_DO0;
    }    
    
	if ((reg & B_PWM_DO1) && !_states_.pwm.do1)
	{
        timer_type0_pwm(&TCE0, timer_conf.prescaler_do1, timer_conf.target_do1, timer_conf.dcycle_do1, INT_LEVEL_LOW, INT_LEVEL_OFF);
        is_new_timer_conf.pwm_do1 = false;
        _states_.pwm.do1 = true;
        start_DO1;
	}
    
	if ((reg & B_PWM_DO2) && !_states_.pwm.do2)
	{
        timer_type0_pwm(&TCD0, timer_conf.prescaler_do2, timer_conf.target_do2, timer_conf.dcycle_do2, INT_LEVEL_LOW, INT_LEVEL_OFF);
        is_new_timer_conf.pwm_do2 = false;
        _states_.pwm.do2 = true;
        start_DO2;
	}
    
	if ((reg & B_PWM_DO3) && !_states_.pwm.do3)
	{   	
        timer_type0_pwm(&TCC0, timer_conf.prescaler_do3, timer_conf.target_do3, timer_conf.dcycle_do3, INT_LEVEL_LOW, INT_LEVEL_OFF);
        is_new_timer_conf.pwm_do3 = false;
        _states_.pwm.do3 = true;
        start_DO3;
	}
    
    app_regs.REG_PWM_START = reg;
	return true;
}


/************************************************************************/
/* REG_PWM_STOP                                                         */
/************************************************************************/
void app_read_REG_PWM_STOP(void) {}
bool app_write_REG_PWM_STOP(void *a)
{
	uint8_t reg = *((uint8_t*)a);
	
	if ((reg & B_PWM_DO0) && _states_.pwm.do0)
	{
		clr_DO0;
		timer_type0_stop(&TCF0);
		_states_.pwm.do0 = false;
	}

	if ((reg & B_PWM_DO1) && _states_.pwm.do1)
	{
		clr_DO1;
		timer_type0_stop(&TCE0);
		_states_.pwm.do1 = false;
	}
	
	if ((reg & B_PWM_DO2) && _states_.pwm.do2)
	{
		clr_DO2;
		timer_type0_stop(&TCD0);
		_states_.pwm.do2 = false;
	}
	
	if ((reg & B_PWM_DO3) && _states_.pwm.do3)
	{
		clr_DO3;
		timer_type0_stop(&TCC0);
		_states_.pwm.do3 = false;
	}
	
	app_regs.REG_PWM_START = ~reg & 0x0F;
	app_regs.REG_PWM_STOP = reg;
	
	return true;
}


/************************************************************************/
/* REG_RGBS                                                             */
/************************************************************************/
// This register is an array with 6 positions
void app_read_REG_RGBS(void) {}
bool app_write_REG_RGBS(void *a)
{
	uint8_t *reg = ((uint8_t*)a);
	
	app_regs.REG_RGBS[0] = reg[1];
	app_regs.REG_RGBS[1] = reg[0];
	app_regs.REG_RGBS[2] = reg[2];
	app_regs.REG_RGBS[3] = reg[4];
	app_regs.REG_RGBS[4] = reg[3];
	app_regs.REG_RGBS[5] = reg[5];

	app_regs.REG_RGB0[0] = reg[1];
	app_regs.REG_RGB0[1] = reg[0];
	app_regs.REG_RGB0[2] = reg[2];
	
	app_regs.REG_RGB1[0] = reg[4];
	app_regs.REG_RGB1[1] = reg[3];
	app_regs.REG_RGB1[2] = reg[5];
   
	handle_Rgbs(rgb0_on, rgb1_on);

	return true;
}


/************************************************************************/
/* REG_RGB0                                                             */
/************************************************************************/
// This register is an array with 3 positions
void app_read_REG_RGB0(void) {}
bool app_write_REG_RGB0(void *a)
{
	uint8_t *reg = ((uint8_t*)a);
    
	app_regs.REG_RGBS[0] = reg[1];
	app_regs.REG_RGBS[1] = reg[0];
	app_regs.REG_RGBS[2] = reg[2];

	app_regs.REG_RGB0[0] = reg[1];
	app_regs.REG_RGB0[1] = reg[0];
	app_regs.REG_RGB0[2] = reg[2];

	handle_Rgbs(rgb0_on, rgb1_on);
    
	return true;
}


/************************************************************************/
/* REG_RGB1                                                             */
/************************************************************************/
// This register is an array with 3 positions
void app_read_REG_RGB1(void) {}
bool app_write_REG_RGB1(void *a)
{
	uint8_t *reg = ((uint8_t*)a);
	
	app_regs.REG_RGBS[3] = reg[1];
	app_regs.REG_RGBS[4] = reg[0];
	app_regs.REG_RGBS[5] = reg[2];

	app_regs.REG_RGB1[0] = reg[1];
	app_regs.REG_RGB1[1] = reg[0];
	app_regs.REG_RGB1[2] = reg[2];

	handle_Rgbs(rgb0_on, rgb1_on);
    
	return true;
}


/************************************************************************/
/* REG_LED0_CURRENT                                                     */
/************************************************************************/
void app_read_REG_LED0_CURRENT(void) {}
bool app_write_REG_LED0_CURRENT(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	if (reg < 1 || reg > 100)
	{
		return false;
	}

	if (reg > app_regs.REG_LED0_MAX_CURRENT)
	{
		return false;
	}

	/* Set DAC value */
    uint16_t dac = (uint16_t) ( (float)(reg) / 100.0 * 4096.0 );
	if (dac > 4095)
        DACB_CH1DATA = 4095;
    else
        DACB_CH1DATA = dac;

	app_regs.REG_LED0_CURRENT = reg;
	return true;
}


/************************************************************************/
/* REG_LED1_CURRENT                                                     */
/************************************************************************/
void app_read_REG_LED1_CURRENT(void) {}
bool app_write_REG_LED1_CURRENT(void *a)
{
	uint8_t reg = *((uint8_t*)a);
	
	if (reg < 1 || reg > 100)
	{
		return false;
	}

	if (reg > app_regs.REG_LED1_MAX_CURRENT)
	{
		return false;
	}

	/* Set DAC value */
    uint16_t dac = (uint16_t) ( (float)(reg) / 100.0 * 4096.0 );	
    if (dac > 4095)
	    DACB_CH0DATA = 4095;
    else
        DACB_CH0DATA = dac;

	app_regs.REG_LED1_CURRENT = reg;
	return true;
}


/************************************************************************/
/* REG_LED0_MAX_CURRENT                                                 */
/************************************************************************/
void app_read_REG_LED0_MAX_CURRENT(void) {}
bool app_write_REG_LED0_MAX_CURRENT(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	if (reg < 2 || reg > 100)
	{
		return false;
	}

	app_regs.REG_LED0_MAX_CURRENT = reg;
	return true;
}


/************************************************************************/
/* REG_LED1_MAX_CURRENT                                                 */
/************************************************************************/
void app_read_REG_LED1_MAX_CURRENT(void) {}
bool app_write_REG_LED1_MAX_CURRENT(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	if (reg < 2 || reg > 100)
	{
		return false;
	}

	app_regs.REG_LED1_MAX_CURRENT = reg;
	return true;
}


/************************************************************************/
/* REG_EVNT_ENABLE                                                      */
/************************************************************************/
void app_read_REG_EVNT_ENABLE(void) {}
bool app_write_REG_EVNT_ENABLE(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_EVNT_ENABLE = reg;
	return true;
}


/************************************************************************/
/* REG_START_CAMERAS                                                    */
/************************************************************************/
void start_cameras(TC0_t* timer, uint16_t freq)
{
    if (freq < 8)
    {
        timer_type0_pwm(timer, TIMER_PRESCALER_DIV256, (32000000/256)/freq, (16000000/256)/freq, INT_LEVEL_LOW, INT_LEVEL_LOW);
    }
    else if (freq < 64)
    {
        timer_type0_pwm(timer, TIMER_PRESCALER_DIV64, (32000000/64)/freq, (16000000/64)/freq, INT_LEVEL_LOW, INT_LEVEL_LOW);
    }
    else if (freq < 128)
    {
        timer_type0_pwm(timer, TIMER_PRESCALER_DIV8, (32000000/8)/freq, (16000000/8)/freq, INT_LEVEL_LOW, INT_LEVEL_LOW);
    }
    else if (freq < 256)
    {
        timer_type0_pwm(timer, TIMER_PRESCALER_DIV4, (32000000/4)/freq, (16000000/4)/freq, INT_LEVEL_LOW, INT_LEVEL_LOW);
    }
    else if (freq <= 600)
    {
        timer_type0_pwm(timer, TIMER_PRESCALER_DIV2, (32000000/2)/freq, (16000000/2)/freq, INT_LEVEL_LOW, INT_LEVEL_LOW);
    }
}    
        
void app_read_REG_START_CAMERAS(void)
{
	app_regs.REG_START_CAMERAS = 0;
	
	if(_states_.camera.do0)
    {
    	app_regs.REG_START_CAMERAS |= B_EN_CAM_OUT0;
    }        
    
    if(_states_.camera.do1)
    {
    	app_regs.REG_START_CAMERAS |= B_EN_CAM_OUT1;
    }        
}
bool app_write_REG_START_CAMERAS(void *a)
{
	uint8_t reg = *((uint8_t*)a);
    
    if ((reg & B_EN_CAM_OUT0) && !_states_.camera.do0)
    {
        /* Make sure the output pin is equal to 0 for a while before start triggering the camera */
        clr_DO0;
        _delay_us(16);      // Measured, gives around 55us before the the first trigger pulse
        
        /* Start the camera */
        start_cameras(&TCF0, app_regs.REG_CAM_OUT0_FREQ);        
        _states_.camera.do0 = true;
    }
    
    if ((reg & B_EN_CAM_OUT1) && !_states_.camera.do1)
    {
        /* Make sure the output pin is equal to 0 for a while before start triggering the camera */
        clr_DO1;
        _delay_us(16);      // Measured, gives around 55us before the the first trigger pulse
            
        /* Start the camera */
        start_cameras(&TCE0, app_regs.REG_CAM_OUT1_FREQ);
        _states_.camera.do1 = true;
    }

	app_regs.REG_START_CAMERAS = reg;
	return true;
}


/************************************************************************/
/* REG_STOP_CAMERAS                                                     */
/************************************************************************/
bool stop_camera_do0 = false;
bool stop_camera_do1 = false;

void app_read_REG_STOP_CAMERAS(void) {}
bool app_write_REG_STOP_CAMERAS(void *a)
{
	uint8_t reg = *((uint8_t*)a);
    
    if ((reg & B_EN_CAM_OUT0) && _states_.camera.do0)
    {
        stop_camera_do0 = true;
    }
        
    if ((reg & B_EN_CAM_OUT1) && _states_.camera.do1)
    {
        stop_camera_do1 = true;
    }

	app_regs.REG_STOP_CAMERAS = reg;
	return true;
}


/************************************************************************/
/* REG_EN_SERVOS                                                        */
/************************************************************************/
void app_read_REG_EN_SERVOS(void)
{
	app_regs.REG_EN_SERVOS = 0;
    
    if (_states_.servo.do2)
    {
        app_regs.REG_EN_SERVOS |= B_EN_SERVO_OUT2;        
    }
    
    if (_states_.servo.do3)
    {
        app_regs.REG_EN_SERVOS |= B_EN_SERVO_OUT3;
    }
}

bool app_write_REG_EN_SERVOS(void *a)
{
	uint8_t reg = *((uint8_t*)a);
    
    if ((reg & B_EN_SERVO_OUT2) && !_states_.servo.do2)
    {
        _states_.servo.do2 = true;
        timer_type0_pwm(&TCD0, TIMER_PRESCALER_DIV64, (app_regs.REG_MOTOR_OUT2_PERIOD >> 1), (app_regs.REG_MOTOR_OUT2_PULSE >> 1), INT_LEVEL_OFF, INT_LEVEL_OFF);
    }
    
    if ((reg & B_EN_SERVO_OUT3) && !_states_.servo.do3)
    {
        _states_.servo.do3 = true;
        timer_type0_pwm(&TCC0, TIMER_PRESCALER_DIV64, (app_regs.REG_MOTOR_OUT3_PERIOD >> 1), (app_regs.REG_MOTOR_OUT3_PULSE >> 1), INT_LEVEL_OFF, INT_LEVEL_OFF);
    }

	app_regs.REG_EN_SERVOS = reg;
	return true;
}


/************************************************************************/
/* REG_DIS_SERVOS                                                       */
/************************************************************************/
void app_read_REG_DIS_SERVOS(void) {}
bool app_write_REG_DIS_SERVOS(void *a)
{
	uint8_t reg = *((uint8_t*)a);

    if ((reg & B_EN_SERVO_OUT2) && _states_.servo.do2)
    {
        _states_.servo.do2 = false;
        timer_type0_stop(&TCD0);
    }
    
    if ((reg & B_EN_SERVO_OUT3) && _states_.servo.do3)
    {
        _states_.servo.do3 = false;
        timer_type0_stop(&TCC0);
    }
    
	app_regs.REG_DIS_SERVOS = reg;
	return true;
}


/************************************************************************/
/* REG_EN_ENCODERS                                                      */
/************************************************************************/
void app_read_REG_EN_ENCODERS(void)
{
	app_regs.REG_EN_ENCODERS = 0;
    
    if(_states_.quad_counter.port2)
    {
        app_regs.REG_EN_ENCODERS |= B_EN_ENCODER_PORT2;
    }
}

bool app_write_REG_EN_ENCODERS(void *a)
{
	uint8_t reg = *((uint8_t*)a);
    
    if ((reg & B_EN_ENCODER_PORT2) && !_states_.quad_counter.port2)
    {        
        _states_.quad_counter.port2 = true;
        
        /* Turn off interrupts on the Encoder pins and redefine pins to input */
        PORTF_INTCTRL &= ~(PORT_INT0LVL_gm);                                // Shut down interrupts on PORTF
        PORTF_INTCTRL &= ~(PORT_INT0LVL_gm);                                // Shut down interrupts on PORTF
        io_pin2in(&PORTF, 4, PULL_IO_TRISTATE, SENSE_IO_LOW_LEVEL);         // POKE2_IR
        io_pin2in(&PORTF, 5, PULL_IO_TRISTATE, SENSE_IO_LOW_LEVEL);         // POKE2_IO
        
        /* Set up quadrature decoding event */
        EVSYS_CH0MUX = EVSYS_CHMUX_PORTF_PIN4_gc;                           // P. 77
        EVSYS_CH0CTRL = EVSYS_QDEN_bm | EVSYS_DIGFILT_2SAMPLES_gc;          // P. 78
        
        /* Stop and reset timer */
        TCD1_CTRLA = TC_CLKSEL_OFF_gc;
        TCD1_CTRLFSET = TC_CMD_RESET_gc;
        
        /* Configure timer */
        TCD1_CTRLD = TC_EVACT_QDEC_gc | TC_EVSEL_CH0_gc;	                // P. 180-1
        TCD1_PER = 0xFFFF;
        TCD1_CNT = 0x8000;
        
        /* Start timer */
        TCD1_CTRLA=TC_CLKSEL_DIV1_gc;
    }
    
    if (!(reg & B_EN_ENCODER_PORT2) && _states_.quad_counter.port2)
    {
        if (_states_.quad_counter.port2)
        {
            _states_.quad_counter.port2 = false;
            
            /* Stop and reset timer */
            TCD1_CTRLA = TC_CLKSEL_OFF_gc;
            TCD1_CTRLFSET = TC_CMD_RESET_gc;
        
            /* Turn inputs to default configuration (same as *init_ios()* func)  */
            io_pin2in(&PORTF, 4, PULL_IO_UP, SENSE_IO_EDGES_BOTH);  // POKE2_IR
            io_set_int(&PORTF, INT_LEVEL_LOW, 0, (3<<4), false);    // POKE2_IR  & IO IN
		
            /* Reset register */
            app_regs.REG_DATA[1] = 0;
        }
    }
    
    app_regs.REG_EN_ENCODERS = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED1                                                        */
/************************************************************************/
void app_read_REG_RESERVED1(void) {}
bool app_write_REG_RESERVED1(void *a) {return true;}
/************************************************************************/
/* REG_RESERVED2                                                        */
/************************************************************************/
void app_read_REG_RESERVED2(void) {}
bool app_write_REG_RESERVED2(void *a) {return true;}
/************************************************************************/
/* REG_RESERVED3                                                        */
/************************************************************************/
void app_read_REG_RESERVED3(void) {}
bool app_write_REG_RESERVED3(void *a) {return true;}
/************************************************************************/
/* REG_RESERVED4                                                        */
/************************************************************************/
void app_read_REG_RESERVED4(void) {}
bool app_write_REG_RESERVED4(void *a) {return true;}
/************************************************************************/
/* REG_RESERVED5                                                        */
/************************************************************************/
void app_read_REG_RESERVED5(void) {}
bool app_write_REG_RESERVED5(void *a) {return true;}
/************************************************************************/
/* REG_RESERVED6                                                        */
/************************************************************************/
void app_read_REG_RESERVED6(void) {}
bool app_write_REG_RESERVED6(void *a) {return true;}
/************************************************************************/
/* REG_RESERVED7                                                        */
/************************************************************************/
void app_read_REG_RESERVED7(void) {}
bool app_write_REG_RESERVED7(void *a) {return true;}
/************************************************************************/
/* REG_RESERVED8                                                        */
/************************************************************************/
void app_read_REG_RESERVED8(void) {}
bool app_write_REG_RESERVED8(void *a) {return true;}

/************************************************************************/
/* REG_RESERVED9                                                        */
/************************************************************************/
void app_read_REG_RESERVED9(void) {}
bool app_write_REG_RESERVED9(void *a) {return true;}

/************************************************************************/
/* REG_CAM_OUT0_FRAME_ACQUIRED                                          */
/************************************************************************/
void app_read_REG_CAM_OUT0_FRAME_ACQUIRED(void) {}
bool app_write_REG_CAM_OUT0_FRAME_ACQUIRED(void *a) {return false;}
/************************************************************************/
/* REG_CAM_OUT0_FREQ                                                    */
/************************************************************************/
void app_read_REG_CAM_OUT0_FREQ(void) {}
bool app_write_REG_CAM_OUT0_FREQ(void *a)
{
	uint16_t reg = *((uint16_t*)a);
	
    if (reg < 2 || reg > 600)
		return false;    

	app_regs.REG_CAM_OUT0_FREQ = reg;
	return true;
}


/************************************************************************/
/* REG_CAM_OUT1_FRAME_ACQUIRED                                          */
/************************************************************************/
void app_read_REG_CAM_OUT1_FRAME_ACQUIRED(void) {}
bool app_write_REG_CAM_OUT1_FRAME_ACQUIRED(void *a) {return false;}
/************************************************************************/
/* REG_CAM_OUT2_FREQ                                                    */
/************************************************************************/
void app_read_REG_CAM_OUT1_FREQ(void) {}
bool app_write_REG_CAM_OUT1_FREQ(void *a)
{
	uint16_t reg = *((uint16_t*)a);
    
    if (reg < 2 || reg > 600)
        return false;

	app_regs.REG_CAM_OUT1_FREQ = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED10                                                       */
/************************************************************************/
void app_read_REG_RESERVED10(void) {}
bool app_write_REG_RESERVED10(void *a) {return true;}
/************************************************************************/
/* REG_RESERVED11                                                       */
/************************************************************************/
void app_read_REG_RESERVED11(void) {}
bool app_write_REG_RESERVED11(void *a) {return true;}
/************************************************************************/
/* REG_RESERVED12                                                       */
/************************************************************************/
void app_read_REG_RESERVED12(void) {}
bool app_write_REG_RESERVED12(void *a) {return true;}
/************************************************************************/
/* REG_RESERVED13                                                       */
/************************************************************************/
void app_read_REG_RESERVED13(void) {}
bool app_write_REG_RESERVED13(void *a) {return true;}


/************************************************************************/
/* REG_MOTOR_OUT2_PERIOD                                                */
/************************************************************************/
void app_read_REG_MOTOR_OUT2_PERIOD(void) {}
bool app_write_REG_MOTOR_OUT2_PERIOD(void *a)
{
	uint16_t reg = *((uint16_t*)a);
    
    if (reg < 2 || reg > 65534)
        return false;
    
    if (_states_.servo.do2)
    {
        TCD0_PER = (reg >> 1) - 1;
    }

	app_regs.REG_MOTOR_OUT2_PERIOD = reg;
	return true;
}
/************************************************************************/
/* REG_MOTOR_OUT2_PULSE                                                 */
/************************************************************************/
void app_read_REG_MOTOR_OUT2_PULSE(void) {}
bool app_write_REG_MOTOR_OUT2_PULSE(void *a)
{
	uint16_t reg = *((uint16_t*)a);
    
   if (reg < 6 || reg > 65530)
      return false;
        
   if (_states_.servo.do2)
   {
      TCD0_CCA = (reg >> 1) - 1;
   }

	app_regs.REG_MOTOR_OUT2_PULSE = reg;
	return true;
}


/************************************************************************/
/* REG_MOTOR_OUT3_PERIOD                                                */
/************************************************************************/
void app_read_REG_MOTOR_OUT3_PERIOD(void) {}
bool app_write_REG_MOTOR_OUT3_PERIOD(void *a)
{
	uint16_t reg = *((uint16_t*)a);
    
    if (reg < 2 || reg > 65534)
        return false;
    
    if (_states_.servo.do3)
    {
        TCC0_PER = (reg >> 1) - 1;
    }

	app_regs.REG_MOTOR_OUT3_PERIOD = reg;
	return true;
}
/************************************************************************/
/* REG_MOTOR_OUT3_PULSE                                                 */
/************************************************************************/
void app_read_REG_MOTOR_OUT3_PULSE(void) {}
bool app_write_REG_MOTOR_OUT3_PULSE(void *a)
{
	uint16_t reg = *((uint16_t*)a);
   
   if (reg < 6 || reg > 65530)
      return false;
        
   if (_states_.servo.do3)
   {
      TCC0_CCA = (reg >> 1) - 1;
   }

	app_regs.REG_MOTOR_OUT3_PULSE = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED14                                                       */
/************************************************************************/
void app_read_REG_RESERVED14(void) {}
bool app_write_REG_RESERVED14(void *a) {return true;}
/************************************************************************/
/* REG_RESERVED15                                                       */
/************************************************************************/
void app_read_REG_RESERVED15(void) {}
bool app_write_REG_RESERVED15(void *a) {return true;}
/************************************************************************/
/* REG_RESERVED16                                                       */
/************************************************************************/
void app_read_REG_RESERVED16(void) {}
bool app_write_REG_RESERVED16(void *a) {return true;}
/************************************************************************/
/* REG_RESERVED17                                                       */
/************************************************************************/
void app_read_REG_RESERVED17(void) {}
bool app_write_REG_RESERVED17(void *a) {return true;}


/************************************************************************/
/* REG_ENCODER_PORT2_RESET                                              */
/************************************************************************/
void app_read_REG_ENCODERS_RESET(void) {}
bool app_write_REG_ENCODERS_RESET(void *a)
{
	uint8_t reg = *((uint8_t*)a);
    
    if (reg & B_RST_ENCODER_PORT2)
    {
        if (_states_.quad_counter.port2)
        {
            TCD1_CNT = 0x8000;            
        }
    }

	app_regs.REG_ENCODERS_RESET = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED18                                                       */
/************************************************************************/
void app_read_REG_RESERVED18(void)  {}
bool app_write_REG_RESERVED18(void *a) {return true;}
/************************************************************************/
/* REG_RESERVED19                                                       */
/************************************************************************/
void app_read_REG_RESERVED19(void) {}
bool app_write_REG_RESERVED19(void *a) {return true;}

/************************************************************************/
/* REG_MIMIC_PORT0_IR                                                   */
/************************************************************************/
void update_DIO_to_mimic (unsigned char reg)
{
   if (reg & GM_MIMIC_DIO0)
      io_pin2out(&PORTD, 4, OUT_IO_DIGITAL, IN_EN_IO_EN);   // DIO0 to output
   if (reg & GM_MIMIC_DIO0)
      io_pin2out(&PORTE, 4, OUT_IO_DIGITAL, IN_EN_IO_EN);   // DIO1 to output
   if (reg & GM_MIMIC_DIO0)
      io_pin2out(&PORTF, 4, OUT_IO_DIGITAL, IN_EN_IO_EN);   // DIO2 to output
}

void app_read_REG_MIMIC_PORT0_IR(void) {}
bool app_write_REG_MIMIC_PORT0_IR(void *a)
{
   uint8_t reg = *((uint8_t*)a);
   update_DIO_to_mimic(reg);
   app_regs.REG_MIMIC_PORT0_IR = reg;
   return true;
}

/************************************************************************/
/* REG_MIMIC_PORT1_IR                                                   */
/************************************************************************/
void app_read_REG_MIMIC_PORT1_IR(void) {}
bool app_write_REG_MIMIC_PORT1_IR(void *a)
{
   uint8_t reg = *((uint8_t*)a);
   update_DIO_to_mimic(reg);
   app_regs.REG_MIMIC_PORT1_IR = reg;
   return true;
}

/************************************************************************/
/* REG_MIMIC_PORT2_IR                                                   */
/************************************************************************/
void app_read_REG_MIMIC_PORT2_IR(void) {}
bool app_write_REG_MIMIC_PORT2_IR(void *a)
{
   uint8_t reg = *((uint8_t*)a);
   update_DIO_to_mimic(reg);
   app_regs.REG_MIMIC_PORT2_IR = reg;
   return true;
}
   
/************************************************************************/
/* REG_RESERVED20                                                       */
/************************************************************************/
void app_read_REG_RESERVED20(void)  {}
bool app_write_REG_RESERVED20(void *a) {return true;}
/************************************************************************/
/* REG_RESERVED21                                                       */
/************************************************************************/
void app_read_REG_RESERVED21(void) {}
bool app_write_REG_RESERVED21(void *a) {return true;}   
/************************************************************************/
/* REG_RESERVED22                                                       */
/************************************************************************/
void app_read_REG_RESERVED22(void) {}
bool app_write_REG_RESERVED22(void *a) {return true;} 

/************************************************************************/
/* REG_MIMIC_PORT0_VALVE                                                */
/************************************************************************/
void app_read_REG_MIMIC_PORT0_VALVE(void) {}
bool app_write_REG_MIMIC_PORT0_VALVE(void *a)
{
   uint8_t reg = *((uint8_t*)a);
   update_DIO_to_mimic(reg);
   app_regs.REG_MIMIC_PORT0_VALVE = reg;
   return true;
}

/************************************************************************/
/* REG_MIMIC_PORT1_VALVE                                                */
/************************************************************************/
void app_read_REG_MIMIC_PORT1_VALVE(void) {}
bool app_write_REG_MIMIC_PORT1_VALVE(void *a)
{
   uint8_t reg = *((uint8_t*)a);
   update_DIO_to_mimic(reg);
   app_regs.REG_MIMIC_PORT1_VALVE = reg;
   return true;
}

/************************************************************************/
/* REG_MIMIC_PORT2_VALVE                                                */
/************************************************************************/
void app_read_REG_MIMIC_PORT2_VALVE(void) {}
bool app_write_REG_MIMIC_PORT2_VALVE(void *a)
{
   uint8_t reg = *((uint8_t*)a);
   update_DIO_to_mimic(reg);
   app_regs.REG_MIMIC_PORT2_VALVE = reg;
   return true;
}
   
/************************************************************************/
/* REG_RESERVED23                                                       */
/************************************************************************/
void app_read_REG_RESERVED23(void)  {}
bool app_write_REG_RESERVED23(void *a) {return true;}
/************************************************************************/
/* REG_RESERVED24                                                       */
/************************************************************************/
void app_read_REG_RESERVED24(void) {}
bool app_write_REG_RESERVED24(void *a) {return true;}
/************************************************************************/
/* REG_RESERVED25                                                       */
/************************************************************************/
void app_read_REG_POKE_INPUT_FILTER_MS(void) {}
bool app_write_REG_POKE_INPUT_FILTER_MS(void *a)
{
	uint8_t reg = *((uint8_t*)a);
	app_regs.REG_POKE_INPUT_FILTER_MS = reg;	
	return true;
}