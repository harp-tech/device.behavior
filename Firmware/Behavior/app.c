#include "hwbp_core.h"
#include "hwbp_core_regs.h"
#include "hwbp_core_types.h"

#include "app.h"
#include "app_funcs.h"
#include "app_ios_and_regs.h"


/************************************************************************/
/* Declare application registers                                        */
/************************************************************************/
extern AppRegs app_regs;
extern uint8_t app_regs_type[];
extern uint16_t app_regs_n_elements[];
extern uint8_t *app_regs_pointer[];
extern void (*app_func_rd_pointer[])(void);
extern bool (*app_func_wr_pointer[])(void*);


/************************************************************************/
/* Initialize app                                                       */
/************************************************************************/
void hwbp_app_initialize(void)
{
	/* Start core */
	core_func_start_core(1216, 1, 0, 1, 0, 0, (uint8_t*)(&app_regs), APP_NBYTES_OF_REG_BANK, APP_REGS_ADD_MAX - APP_REGS_ADD_MIN + 1);
}

/************************************************************************/
/* Handle if a catastrophic error occur                                 */
/************************************************************************/
void core_callback_catastrophic_error_detected(void)
{
	
}

/************************************************************************/
/* General definitions                                                  */
/************************************************************************/
// #define NBYTES 23

/************************************************************************/
/* General used functions                                               */
/************************************************************************/
/* Load external functions if needed */
//#include "hwbp_app_pwm_gen_funcs.c"

/*
void update_enabled_pwmx(void)
{
	if (!core_bool_is_visual_enabled())
	return;
	
	if (!(app_regs.REG_CH_CONFEN & B_USEEN0) || ((app_regs.REG_CH_CONFEN & B_USEEN0) && (app_regs.REG_CH_ENABLE & B_EN0)))
		set_ENABLED_PWM0;
	else
		clr_ENABLED_PWM0;
}

ISR(PORTB_INT0_vect, ISR_NAKED)
{
	reti();
}
*/


/************************************************************************/
/* Initialization Callbacks                                             */
/************************************************************************/
void core_callback_1st_config_hw_after_boot(void)
{
	/* Initialize IOs */
	/* Don't delete this function!!! */
	init_ios();
	
	/* SYNC OUTx */
	//io_pin2out(&PORTC, 1, OUT_IO_DIGITAL, IN_EN_IO_DIS);

	/* TRIG_IN0 */
	//io_pin2in(&PORTF, 5, PULL_IO_TRISTATE, SENSE_IO_EDGES_BOTH);
	//io_set_int(&PORTF, INT_LEVEL_LOW, 0, (1<<5), true);
}

void core_callback_reset_registers(void)
{
	//app_regs.REG_POKE_IN = 0;
    //app_regs.REG_POKE_DIG_IN = 0;
    
    app_regs.REG_OUTPUTS_SET = 0;
    app_regs.REG_OUTPUTS_CLEAR = 0;
    app_regs.REG_OUTPUTS_TOGGLE = 0;
    app_regs.REG_OUTPUTS_OUT = 0;
    
    app_regs.REG_POKE_DIOS_SET = 0;
    app_regs.REG_POKE_DIOS_CLEAR = 0;
    app_regs.REG_POKE_DIOS_TOGGLE = 0;
    app_regs.REG_POKE_DIOS_OUT = 0;
    app_regs.REG_POKE_DIOS_CONF = 0; // All as inputs
    //app_regs.REG_POKE_DIOS_IN = 0;
    
    //app_regs.REG_ADC = 0;
    
    app_regs.REG_MODE_POKE0_LED = GM_SOFTWARE;
    app_regs.REG_MODE_POKE1_LED = GM_SOFTWARE;
    app_regs.REG_MODE_POKE2_LED = GM_SOFTWARE;
    app_regs.REG_MODE_POKE0_VALVE = GM_SOFTWARE;
    app_regs.REG_MODE_POKE1_VALVE = GM_SOFTWARE;
    app_regs.REG_MODE_POKE2_VALVE = GM_SOFTWARE;
    app_regs.REG_MODE_LED0 = GM_SOFTWARE;
    app_regs.REG_MODE_LED1 = GM_SOFTWARE;
    app_regs.REG_MODE_RGB0 = GM_SOFTWARE;
    app_regs.REG_MODE_RGB1 = GM_SOFTWARE;
    app_regs.REG_MODE_DO0 = GM_SOFTWARE;
    app_regs.REG_MODE_DO1 = GM_SOFTWARE;
    app_regs.REG_MODE_DO2 = GM_SOFTWARE;
    app_regs.REG_MODE_DO3 = GM_SOFTWARE;

    app_regs.REG_PULSE_POKE0_LED = 200;
    app_regs.REG_PULSE_POKE1_LED = 200;
    app_regs.REG_PULSE_POKE2_LED = 200;
    app_regs.REG_PULSE_POKE0_VALVE = 15;
    app_regs.REG_PULSE_POKE1_VALVE = 15;
    app_regs.REG_PULSE_POKE2_VALVE = 15;
    app_regs.REG_PULSE_LED0 = 500;
    app_regs.REG_PULSE_LED1 = 500;
    app_regs.REG_PULSE_RGB0 = 500;
    app_regs.REG_PULSE_RGB1 = 500;
    app_regs.REG_PULSE_DO0 = 1000;
    app_regs.REG_PULSE_DO1 = 1000;
    app_regs.REG_PULSE_DO2 = 1000;
    app_regs.REG_PULSE_DO3 = 1000;
    
    app_regs.REG_FREQ_DO0 = 1000;
    app_regs.REG_FREQ_DO1 = 1000;
    app_regs.REG_FREQ_DO2 = 1000;
    app_regs.REG_FREQ_DO3 = 1000;
    
    app_regs.REG_DCYCLE_DO0 = 50;
    app_regs.REG_DCYCLE_DO1 = 50;
    app_regs.REG_DCYCLE_DO2 = 50;
    app_regs.REG_DCYCLE_DO3 = 50;
    
    app_regs.REG_PWM_START = 0;
    app_regs.REG_PWM_STOP = 0;
    
    app_regs.REG_RGBS[0] = 64;  // Green
    app_regs.REG_RGBS[1] = 16;  // Red
    app_regs.REG_RGBS[2] = 16;  // Blue
    app_regs.REG_RGBS[3] = 16;  // Green
    app_regs.REG_RGBS[4] = 16;  // Red
    app_regs.REG_RGBS[5] = 64;  // Blue
    
    app_regs.REG_RGB0[0] = 64;  // Green
    app_regs.REG_RGB0[1] = 16;  // Red
    app_regs.REG_RGB0[2] = 16;  // Blue
    
    app_regs.REG_RGB1[0] = 16;  // Green
    app_regs.REG_RGB1[1] = 16;  // Red
    app_regs.REG_RGB1[2] = 64;  // Blue
    
    app_regs.REG_LED0_CURRENT = 10;
    app_regs.REG_LED1_CURRENT = 10;
    app_regs.REG_LED0_MAX_CURRENT = 30;
    app_regs.REG_LED1_MAX_CURRENT = 30;
    
    app_regs.REG_EVNT_ENABLE = B_EVT_POKE_IN | B_EVT_POKE_DIG_IN | B_EVT_POKE_DIOS_IN | B_EVT_ADC;
}

void core_callback_registers_were_reinitialized(void)
{
	uint16_t aux16b = app_regs.REG_OUTPUTS_OUT;
    app_write_REG_OUTPUTS_OUT(&aux16b);
    
    /* Check if the user indication is valid */
	//update_enabled_pwmx();
	
	/* Update state register */
	//app_regs.REG_TRIG_STATE = (read_TRIG_IN0) ? B_LTRG0 : 0;
	//app_regs.REG_TRIG_STATE |= (read_TRIG_IN1) ? B_LTRG1 : 0;

	/* Reset start bits */
	//app_regs.REG_TRG0_START = 0;
	//app_regs.REG_TRG1_START = 0;

	/*
	if ((app_regs.REG_MODE0 & B_M0) == GM_BNC_MODE)
	{
		app_regs.REG_OUT0 = app_regs.REG_CTRL0;
		set_OUT0(app_regs.REG_OUT0);
	}
	else
	{
		set_OUT0(app_regs.REG_OUT0);
	}
	*/
}

/************************************************************************/
/* Callbacks: Visualization                                             */
/************************************************************************/
void core_callback_visualen_to_on(void)
{
	/* Update channels enable indicators */
	//update_enabled_pwmx();
}

void core_callback_visualen_to_off(void)
{
	/* Clear all the enabled indicators */
}

/************************************************************************/
/* Callbacks: Change on the operation mode                              */
/************************************************************************/
void core_callback_device_to_standby(void) {}
void core_callback_device_to_active(void) {}
void core_callback_device_to_enchanced_active(void) {}
void core_callback_device_to_speed(void) {}

/************************************************************************/
/* Callbacks: 1 ms timer                                                */
/************************************************************************/
void core_callback_t_before_exec(void) {}
void core_callback_t_after_exec(void) {}
void core_callback_t_new_second(void) {}
void core_callback_t_500us(void) {}
void core_callback_t_1ms(void) {}

/************************************************************************/
/* Callbacks: uart control                                              */
/************************************************************************/
void core_callback_uart_rx_before_exec(void) {}
void core_callback_uart_rx_after_exec(void) {}
void core_callback_uart_tx_before_exec(void) {}
void core_callback_uart_tx_after_exec(void) {}
void core_callback_uart_cts_before_exec(void) {}
void core_callback_uart_cts_after_exec(void) {}

/************************************************************************/
/* Callbacks: Read app register                                         */
/************************************************************************/
bool core_read_app_register(uint8_t add, uint8_t type)
{
	/* Check if it will not access forbidden memory */
	if (add < APP_REGS_ADD_MIN || add > APP_REGS_ADD_MAX)
		return false;
	
	/* Check if type matches */
	if (app_regs_type[add-APP_REGS_ADD_MIN] != type)
		return false;
	
	/* Receive data */
	(*app_func_rd_pointer[add-APP_REGS_ADD_MIN])();	

	/* Return success */
	return true;
}

/************************************************************************/
/* Callbacks: Write app register                                        */
/************************************************************************/
bool core_write_app_register(uint8_t add, uint8_t type, uint8_t * content, uint16_t n_elements)
{
	/* Check if it will not access forbidden memory */
	if (add < APP_REGS_ADD_MIN || add > APP_REGS_ADD_MAX)
		return false;
	
	/* Check if type matches */
	if (app_regs_type[add-APP_REGS_ADD_MIN] != type)
		return false;

	/* Check if the number of elements matches */
	if (app_regs_n_elements[add-APP_REGS_ADD_MIN] != n_elements)
		return false;

	/* Process data and return false if write is not allowed or contains errors */
	return (*app_func_wr_pointer[add-APP_REGS_ADD_MIN])(content);
}