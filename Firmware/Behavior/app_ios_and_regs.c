#include <avr/io.h>
#include "hwbp_core_types.h"
#include "app_ios_and_regs.h"

/************************************************************************/
/* Configure and initialize IOs                                         */
/************************************************************************/
void init_ios(void)
{	/* Configure input pins */
	io_pin2in(&PORTD, 4, PULL_IO_UP, SENSE_IO_EDGES_BOTH);               // POKE0_IR
	io_pin2in(&PORTD, 5, PULL_IO_UP, SENSE_IO_EDGES_BOTH);               // POKE0_IO
	io_pin2in(&PORTE, 4, PULL_IO_UP, SENSE_IO_EDGES_BOTH);               // POKE1_IR
	io_pin2in(&PORTE, 5, PULL_IO_UP, SENSE_IO_EDGES_BOTH);               // POKE1_IO
	io_pin2in(&PORTF, 4, PULL_IO_UP, SENSE_IO_EDGES_BOTH);               // POKE2_IR
	io_pin2in(&PORTF, 5, PULL_IO_UP, SENSE_IO_EDGES_BOTH);               // POKE2_IO

	/* Configure input interrupts */
	io_set_int(&PORTD, INT_LEVEL_LOW, 0, (1<<4), false);                 // POKE0_IR
	io_set_int(&PORTE, INT_LEVEL_LOW, 0, (1<<4), false);                 // POKE1_IR
	io_set_int(&PORTF, INT_LEVEL_LOW, 0, (1<<4), false);                 // POKE2_IR

	/* Configure output pins */
	io_pin2out(&PORTC, 0, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DO3
	io_pin2out(&PORTD, 0, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DO2
	io_pin2out(&PORTE, 0, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DO1
	io_pin2out(&PORTF, 0, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DO0
	io_pin2out(&PORTB, 6, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // LED0
	io_pin2out(&PORTB, 5, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // LED1
	io_pin2out(&PORTC, 5, OUT_IO_DIGITAL, IN_EN_IO_DIS);                 // RGBS
	io_pin2out(&PORTD, 6, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // POKE0_LED
	io_pin2out(&PORTD, 7, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // POKE0_VALVE
	io_pin2out(&PORTE, 6, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // POKE1_LED
	io_pin2out(&PORTE, 7, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // POKE1_VALVE
	io_pin2out(&PORTF, 6, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // POKE2_LED
	io_pin2out(&PORTF, 7, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // POKE2_VALVE

	/* Initialize output pins */
	clr_DO3;
	clr_DO2;
	clr_DO1;
	clr_DO0;
	clr_LED0;
	clr_LED1;
	clr_RGBS;
	clr_POKE0_LED;
	clr_POKE0_VALVE;
	clr_POKE1_LED;
	clr_POKE1_VALVE;
	clr_POKE2_LED;
	clr_POKE2_VALVE;
}

/************************************************************************/
/* Registers' stuff                                                     */
/************************************************************************/
AppRegs app_regs;

uint8_t app_regs_type[] = {
	TYPE_U8,
	TYPE_U8,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U16,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8
};

uint16_t app_regs_n_elements[] = {
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	6,
	3,
	3,
	1,
	1,
	1,
	1,
	1
};

uint8_t *app_regs_pointer[] = {
	(uint8_t*)(&app_regs.REG_POKE_IN),
	(uint8_t*)(&app_regs.REG_RESERVED0),
	(uint8_t*)(&app_regs.REG_OUTPUTS_SET),
	(uint8_t*)(&app_regs.REG_OUTPUTS_CLEAR),
	(uint8_t*)(&app_regs.REG_OUTPUTS_TOGGLE),
	(uint8_t*)(&app_regs.REG_OUTPUTS_OUT),
	(uint8_t*)(&app_regs.REG_POKE_DIOS_SET),
	(uint8_t*)(&app_regs.REG_POKE_DIOS_CLEAR),
	(uint8_t*)(&app_regs.REG_POKE_DIOS_TOGGLE),
	(uint8_t*)(&app_regs.REG_POKE_DIOS_OUT),
	(uint8_t*)(&app_regs.REG_POKE_DIOS_CONF),
	(uint8_t*)(&app_regs.REG_POKE_DIOS_IN),
	(uint8_t*)(&app_regs.REG_ADC),
	(uint8_t*)(&app_regs.REG_MODE_POKE0_LED),
	(uint8_t*)(&app_regs.REG_MODE_POKE1_LED),
	(uint8_t*)(&app_regs.REG_MODE_POKE2_LED),
	(uint8_t*)(&app_regs.REG_MODE_POKE0_VALVE),
	(uint8_t*)(&app_regs.REG_MODE_POKE1_VALVE),
	(uint8_t*)(&app_regs.REG_MODE_POKE2_VALVE),
	(uint8_t*)(&app_regs.REG_MODE_LED0),
	(uint8_t*)(&app_regs.REG_MODE_LED1),
	(uint8_t*)(&app_regs.REG_MODE_RGB0),
	(uint8_t*)(&app_regs.REG_MODE_RGB1),
	(uint8_t*)(&app_regs.REG_MODE_DO0),
	(uint8_t*)(&app_regs.REG_MODE_DO1),
	(uint8_t*)(&app_regs.REG_MODE_DO2),
	(uint8_t*)(&app_regs.REG_MODE_DO3),
	(uint8_t*)(&app_regs.REG_PULSE_POKE0_LED),
	(uint8_t*)(&app_regs.REG_PULSE_POKE1_LED),
	(uint8_t*)(&app_regs.REG_PULSE_POKE2_LED),
	(uint8_t*)(&app_regs.REG_PULSE_POKE0_VALVE),
	(uint8_t*)(&app_regs.REG_PULSE_POKE1_VALVE),
	(uint8_t*)(&app_regs.REG_PULSE_POKE2_VALVE),
	(uint8_t*)(&app_regs.REG_PULSE_LED0),
	(uint8_t*)(&app_regs.REG_PULSE_LED1),
	(uint8_t*)(&app_regs.REG_PULSE_RGB0),
	(uint8_t*)(&app_regs.REG_PULSE_RGB1),
	(uint8_t*)(&app_regs.REG_PULSE_DO0),
	(uint8_t*)(&app_regs.REG_PULSE_DO1),
	(uint8_t*)(&app_regs.REG_PULSE_DO2),
	(uint8_t*)(&app_regs.REG_PULSE_DO3),
	(uint8_t*)(&app_regs.REG_FREQ_DO0),
	(uint8_t*)(&app_regs.REG_FREQ_DO1),
	(uint8_t*)(&app_regs.REG_FREQ_DO2),
	(uint8_t*)(&app_regs.REG_FREQ_DO3),
	(uint8_t*)(&app_regs.REG_DCYCLE_DO0),
	(uint8_t*)(&app_regs.REG_DCYCLE_DO1),
	(uint8_t*)(&app_regs.REG_DCYCLE_DO2),
	(uint8_t*)(&app_regs.REG_DCYCLE_DO3),
	(uint8_t*)(&app_regs.REG_PWM_START),
	(uint8_t*)(&app_regs.REG_PWM_STOP),
	(uint8_t*)(app_regs.REG_RGBS),
	(uint8_t*)(app_regs.REG_RGB0),
	(uint8_t*)(app_regs.REG_RGB1),
	(uint8_t*)(&app_regs.REG_LED0_CURRENT),
	(uint8_t*)(&app_regs.REG_LED1_CURRENT),
	(uint8_t*)(&app_regs.REG_LED0_MAX_CURRENT),
	(uint8_t*)(&app_regs.REG_LED1_MAX_CURRENT),
	(uint8_t*)(&app_regs.REG_EVNT_ENABLE)
};