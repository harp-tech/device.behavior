#include "hwbp_core.h"
#include "hwbp_core_regs.h"
#include "hwbp_core_types.h"

#include "app.h"
#include "app_funcs.h"
#include "app_ios_and_regs.h"

#include "WS2812S.h"
#include "structs.h"


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
    uint8_t led[3] = {0, 0, 0};
        
	 timer_type0_stop(&TCF0); clr_DO0;
    timer_type0_stop(&TCE0); clr_DO1;
    timer_type0_stop(&TCD0); clr_DO2;
    timer_type0_stop(&TCC0); clr_DO3;
    
    clr_LED0;
    clr_LED1;
    
    PMIC_CTRL = PMIC_RREN_bm | PMIC_LOLVLEN_bm;
    update_2rgbs(led, led);
    //update_3rgbs(led, led, led);
    PMIC_CTRL = PMIC_RREN_bm | PMIC_LOLVLEN_bm | PMIC_MEDLVLEN_bm | PMIC_HILVLEN_bm;
    
    clr_POKE0_LED;
    clr_POKE1_LED;
    clr_POKE2_LED;    
    
    clr_POKE0_VALVE;
    clr_POKE1_VALVE;
    clr_POKE2_VALVE;
    
    // To do: Clear Pokes DIOs
}

/************************************************************************/
/* General definitions                                                  */
/************************************************************************/
countdown_t pulse_countdown;

/************************************************************************/
/* General used functions                                               */
/************************************************************************/

/************************************************************************/
/* Initialization Callbacks                                             */
/************************************************************************/
uint16_t AdcOffset;

#define DAC_GAINCAL 0x83
#define DAC_OFFSETCAL 0x85

void core_callback_1st_config_hw_after_boot(void)
{
	/* Initialize IOs */
	/* Don't delete this function!!! */
	init_ios();
	
	/* Initialize LEDs digital line */
	initialize_rgb();

	/* Configure DAC calibration using default values */
	DACA.CH0GAINCAL = DAC_GAINCAL;
	DACA.CH0OFFSETCAL = DAC_OFFSETCAL;
	DACB.CH0GAINCAL = DAC_GAINCAL;
	DACB.CH0OFFSETCAL = DAC_OFFSETCAL;

    /* Initialize DACB dual channel using internal 1V as reference */
	DACB.CTRLB = DAC_CHSEL_DUAL_gc;
	DACB.CTRLC = DAC_REFSEL_INT1V_gc;
	DACB.CTRLA = DAC_CH0EN_bm | DAC_CH1EN_bm | DAC_ENABLE_bm;

	/* Initialize ADC */
	PR_PRPA &= ~(PR_ADC_bm);									// Remove power reduction
	ADCA_CTRLA = ADC_ENABLE_bm;								// Enable ADCA
	ADCA_CTRLB = ADC_CURRLIMIT_HIGH_gc;						// High current limit, max. sampling rate 0.5MSPS
	ADCA_CTRLB  |= ADC_RESOLUTION_12BIT_gc;				// 12-bit result, right adjusted
	ADCA_REFCTRL = ADC_REFSEL_INTVCC_gc;					// VCC/1.6 = 3.3/1.6 = 2.0625 V
	ADCA_PRESCALER = ADC_PRESCALER_DIV128_gc;				// 250 ksps
	// Propagation Delay = (1 + 12[bits]/2 + 1[gain]) / fADC[125k] = 32 us
	// Note: For single measurements, Propagation Delay is equal to Conversion Time
		
	ADCA_CH0_CTRL = ADC_CH_INPUTMODE_SINGLEENDED_gc;	// Single-ended positive input signal
	ADCA_CH0_MUXCTRL = 1 << 3;									// Pin 1 for zero calibration
	ADCA_CH0_INTCTRL = ADC_CH_INTMODE_COMPLETE_gc;		// Rise interrupt flag when conversion is complete
	ADCA_CH0_INTCTRL |= ADC_CH_INTLVL_OFF_gc;				// Interrupt is not used
		
	/* Wait 100 us to stabilization before measure the zero/GND value */
	timer_type0_enable(&TCD0, TIMER_PRESCALER_DIV2, 1600, INT_LEVEL_OFF);
	while(!timer_type0_get_flag(&TCD0));
	timer_type0_stop(&TCD0);
		
	/* Measure and save adc offset */
	ADCA_CH0_CTRL |= ADC_CH_START_bm;						// Start conversion
	while(!(ADCA_CH0_INTFLAGS & ADC_CH_CHIF_bm));		// Wait for conversion to finish
	ADCA_CH0_INTFLAGS = ADC_CH_CHIF_bm;						// Clear interrupt bit
	AdcOffset = ADCA_CH0_RES;									// Read offset

	/* Point ADC to the right channel */
	ADCA_CH0_MUXCTRL = 0 << 3;									// Select pin 0 for further conversions
	ADCA_CH0_CTRL |= ADC_CH_START_bm;						// Force the first conversion
	while(!(ADCA_CH0_INTFLAGS & ADC_CH_CHIF_bm));		// Wait for conversion to finish
	ADCA_CH0_INTFLAGS = ADC_CH_CHIF_bm;						// Clear interrupt bit
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
    app_regs.REG_MODE_POKE0_VALVE = GM_PULSE;
    app_regs.REG_MODE_POKE1_VALVE = GM_PULSE;
    app_regs.REG_MODE_POKE2_VALVE = GM_PULSE;
    app_regs.REG_MODE_LED0 = GM_SOFTWARE;
    app_regs.REG_MODE_LED1 = GM_SOFTWARE;
    app_regs.REG_MODE_RGB0 = GM_SOFTWARE;
    app_regs.REG_MODE_RGB1 = GM_SOFTWARE;
    app_regs.REG_MODE_DO0 = GM_SOFTWARE;
    app_regs.REG_MODE_DO1 = GM_SOFTWARE;
    app_regs.REG_MODE_DO2 = GM_SOFTWARE;
    app_regs.REG_MODE_DO3 = GM_SOFTWARE;

    app_regs.REG_PULSE_POKE0_LED = 500;
    app_regs.REG_PULSE_POKE1_LED = 500;
    app_regs.REG_PULSE_POKE2_LED = 500;
    app_regs.REG_PULSE_POKE0_VALVE = 15;
    app_regs.REG_PULSE_POKE1_VALVE = 15;
    app_regs.REG_PULSE_POKE2_VALVE = 15;
    app_regs.REG_PULSE_LED0 = 500;
    app_regs.REG_PULSE_LED1 = 500;
    app_regs.REG_PULSE_RGB0 = 500;
    app_regs.REG_PULSE_RGB1 = 500;
    app_regs.REG_PULSE_DO0 = 250;
    app_regs.REG_PULSE_DO1 = 250;
    app_regs.REG_PULSE_DO2 = 250;
    app_regs.REG_PULSE_DO3 = 250;
    
    app_regs.REG_FREQ_DO0 = 1000;
    app_regs.REG_FREQ_DO1 = 2000;
    app_regs.REG_FREQ_DO2 = 3000;
    app_regs.REG_FREQ_DO3 = 4000;
    
    app_regs.REG_DCYCLE_DO0 = 50;
    app_regs.REG_DCYCLE_DO1 = 50;
    app_regs.REG_DCYCLE_DO2 = 50;
    app_regs.REG_DCYCLE_DO3 = 50;
    
    app_regs.REG_PWM_START = 0;
    app_regs.REG_PWM_STOP = 0;
    
    app_regs.REG_RGBS[0] = 255;  // Green
    app_regs.REG_RGBS[1] = 0;		// Red
    app_regs.REG_RGBS[2] = 0;		// Blue
    app_regs.REG_RGBS[3] = 0;		// Green
    app_regs.REG_RGBS[4] = 0;		// Red
    app_regs.REG_RGBS[5] = 255;  // Blue
    
    app_regs.REG_RGB0[0] = 255;  // Green
    app_regs.REG_RGB0[1] = 0;		// Red
    app_regs.REG_RGB0[2] = 0;		// Blue
    
    app_regs.REG_RGB1[0] = 0;		// Green
    app_regs.REG_RGB1[1] = 0;		// Red
    app_regs.REG_RGB1[2] = 255;  // Blue
    
    app_regs.REG_LED0_CURRENT = 10;
    app_regs.REG_LED1_CURRENT = 10;
    app_regs.REG_LED0_MAX_CURRENT = 30;
    app_regs.REG_LED1_MAX_CURRENT = 30;
    
    app_regs.REG_EVNT_ENABLE = B_EVT_POKE_IN | B_EVT_POKE_DIOS_IN | B_EVT_ADC;
}

void core_callback_registers_were_reinitialized(void)
{
	uint16_t aux16b = app_regs.REG_OUTPUTS_OUT;
    app_write_REG_OUTPUTS_OUT(&aux16b);
    
    uint8_t aux8b = app_regs.REG_LED0_CURRENT;
    app_write_REG_LED0_CURRENT(&aux8b);
    aux8b = app_regs.REG_LED1_CURRENT;
    app_write_REG_LED1_CURRENT(&aux8b);
    
    aux16b = app_regs.REG_FREQ_DO0;
    app_write_REG_FREQ_DO0(&aux16b);
    aux16b = app_regs.REG_FREQ_DO1;
    app_write_REG_FREQ_DO1(&aux16b);
    aux16b = app_regs.REG_FREQ_DO2;
    app_write_REG_FREQ_DO2(&aux16b);
    aux16b = app_regs.REG_FREQ_DO3;
    app_write_REG_FREQ_DO3(&aux16b);
    
    aux8b = app_regs.REG_PWM_START;
    app_write_REG_PWM_START(&aux8b);
}

/************************************************************************/
/* Callbacks: Visualization                                             */
/************************************************************************/
void core_callback_visualen_to_on(void) {}
void core_callback_visualen_to_off(void) {}

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
extern void handle_Rgbs(bool use_rgb0, bool use_rgb1);

extern bool rgb0_on;
extern bool rgb1_on;

void core_callback_t_before_exec(void)
{
	ADCA_CH0_CTRL |= ADC_CH_START_bm;						// Force the first conversion
	while(!(ADCA_CH0_INTFLAGS & ADC_CH_CHIF_bm));		// Wait for conversion to finish
	ADCA_CH0_INTFLAGS = ADC_CH_CHIF_bm;						// Clear interrupt bit

	if (ADCA_CH0_RES > AdcOffset)
		app_regs.REG_ADC = (ADCA_CH0_RES & 0x0FFF) - AdcOffset;
	else
		app_regs.REG_ADC = 0;

	if (app_regs.REG_EVNT_ENABLE & B_EVT_ADC)
	{
		core_func_send_event(ADD_REG_ADC, true);
	}
}
void core_callback_t_after_exec(void) {}
void core_callback_t_new_second(void) {}

void core_callback_t_500us(void)
{
	bool prev_rgb0_on, prev_rgb1_on;
	prev_rgb0_on = rgb0_on;
	prev_rgb1_on = rgb1_on;

	if (pulse_countdown.poke0_led > 0)
		if (--pulse_countdown.poke0_led == 0)
			clr_POKE0_LED;
	if (pulse_countdown.poke1_led > 0)
		if (--pulse_countdown.poke1_led == 0)
			clr_POKE1_LED;
	if (pulse_countdown.poke2_led > 0)
		if (--pulse_countdown.poke2_led == 0)
			clr_POKE2_LED;

	if (pulse_countdown.poke0_valve > 0)
		if (--pulse_countdown.poke0_valve == 0)
			clr_POKE0_VALVE;
	if (pulse_countdown.poke1_valve > 0)
		if (--pulse_countdown.poke1_valve == 0)
			clr_POKE1_VALVE;
	if (pulse_countdown.poke2_valve > 0)
		if (--pulse_countdown.poke2_valve == 0)
			clr_POKE2_VALVE;

	if (pulse_countdown.led0 > 0)
		if (--pulse_countdown.led0 == 0)
			clr_LED0;
	if (pulse_countdown.led1 > 0)
		if (--pulse_countdown.led1 == 0)
			clr_LED1;

	if (pulse_countdown.rgb0 > 0)
		if (--pulse_countdown.rgb0 == 0)
			rgb0_on = false;
	if (pulse_countdown.rgb1 > 0)
		if (--pulse_countdown.rgb1 == 0)
			rgb1_on = false;

	if ((prev_rgb0_on != rgb0_on) || (prev_rgb1_on != rgb1_on))
	{
		handle_Rgbs(rgb0_on, rgb1_on);
	}
	
	if (pulse_countdown.do0 > 0)
	if (--pulse_countdown.do0 == 0)
	{
		clr_DO0;
		timer_type0_stop(&TCF0);
	}
	if (pulse_countdown.do1 > 0)
	if (--pulse_countdown.do1 == 0)
	{
		clr_DO1;
		timer_type0_stop(&TCE0);
	}
	if (pulse_countdown.do2 > 0)
	if (--pulse_countdown.do2 == 0)
	{
		clr_DO2;
		timer_type0_stop(&TCD0);
	}
	if (pulse_countdown.do3 > 0)
	if (--pulse_countdown.do3 == 0)
	{
		clr_DO3;
		timer_type0_stop(&TCC0);
	}
}

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