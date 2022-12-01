#include <avr/io.h>
#include "hwbp_core_types.h"
#include "app_ios_and_regs.h"

extern AppRegs app_regs;

void mimic_ir_or_valve (uint8_t reg, uint8_t what_to_do)
{
   if (reg & MSK_MIMIC)
   {
      switch (reg)
      {
         case GM_MIMIC_DO0:  if (what_to_do == _SET_IO_) set_DO0;          if (what_to_do == _CLR_IO_) clr_DO0;            if (what_to_do == _TGL_IO_) tgl_DO0; break;
         case GM_MIMIC_DO1:  if (what_to_do == _SET_IO_) set_DO1;          if (what_to_do == _CLR_IO_) clr_DO1;            if (what_to_do == _TGL_IO_) tgl_DO1; break;
         case GM_MIMIC_DO2:  if (what_to_do == _SET_IO_) set_DO2;          if (what_to_do == _CLR_IO_) clr_DO2;            if (what_to_do == _TGL_IO_) tgl_DO2; break;
         case GM_MIMIC_DO3:  if (what_to_do == _SET_IO_) set_DO3;          if (what_to_do == _CLR_IO_) clr_DO3;            if (what_to_do == _TGL_IO_) tgl_DO3; break;
         case GM_MIMIC_DIO0: if (what_to_do == _SET_IO_) set_io(PORTD, 5); if (what_to_do == _CLR_IO_) clear_io(PORTD, 5); if (what_to_do == _TGL_IO_) toggle_io(PORTD, 5); break;
         case GM_MIMIC_DIO1: if (what_to_do == _SET_IO_) set_io(PORTE, 5); if (what_to_do == _CLR_IO_) clear_io(PORTE, 5); if (what_to_do == _TGL_IO_) toggle_io(PORTE, 5); break;
         case GM_MIMIC_DIO2: if (what_to_do == _SET_IO_) set_io(PORTF, 5); if (what_to_do == _CLR_IO_) clear_io(PORTF, 5); if (what_to_do == _TGL_IO_) toggle_io(PORTF, 5); break;
      }
   }      
}


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
	io_pin2in(&PORTJ, 0, PULL_IO_DOWN, SENSE_IO_EDGES_BOTH);             // ADC1_AVAILABLE
	io_pin2in(&PORTH, 0, PULL_IO_TRISTATE, SENSE_IO_EDGES_BOTH);         // DI3

	/* Configure input interrupts */
	io_set_int(&PORTD, INT_LEVEL_LOW, 0, (3<<4), false);                 // POKE0_IR
	io_set_int(&PORTE, INT_LEVEL_LOW, 0, (3<<4), false);                 // POKE1_IR
	io_set_int(&PORTF, INT_LEVEL_LOW, 0, (3<<4), false);                 // POKE2_IR
	
	if(read_ADC1_AVAILABLE)
		io_set_int(&PORTH, INT_LEVEL_LOW, 0, (1<<0), false);              // DI3

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
	TYPE_I16,
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
	TYPE_U8,
	TYPE_U16,
	TYPE_U8,
	TYPE_U16,
	TYPE_U8,
	TYPE_U8,
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
	3,
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
	1
};

uint8_t *app_regs_pointer[] = {
	(uint8_t*)(&app_regs.REG_PORT_DIS),
	(uint8_t*)(&app_regs.REG_RESERVED0),
	(uint8_t*)(&app_regs.REG_OUTPUTS_SET),
	(uint8_t*)(&app_regs.REG_OUTPUTS_CLEAR),
	(uint8_t*)(&app_regs.REG_OUTPUTS_TOGGLE),
	(uint8_t*)(&app_regs.REG_OUTPUTS_OUT),
	(uint8_t*)(&app_regs.REG_PORT_DIOS_SET),
	(uint8_t*)(&app_regs.REG_PORT_DIOS_CLEAR),
	(uint8_t*)(&app_regs.REG_PORT_DIOS_TOGGLE),
	(uint8_t*)(&app_regs.REG_PORT_DIOS_OUT),
	(uint8_t*)(&app_regs.REG_PORT_DIOS_CONF),
	(uint8_t*)(&app_regs.REG_PORT_DIOS_IN),
	(uint8_t*)(app_regs.REG_DATA),
	(uint8_t*)(&app_regs.REG_OUTPUT_PULSE_EN),
	(uint8_t*)(&app_regs.REG_PULSE_PORT0_DO),
	(uint8_t*)(&app_regs.REG_PULSE_PORT1_DO),
	(uint8_t*)(&app_regs.REG_PULSE_PORT2_DO),
	(uint8_t*)(&app_regs.REG_PULSE_PORT0_12V),
	(uint8_t*)(&app_regs.REG_PULSE_PORT1_12V),
	(uint8_t*)(&app_regs.REG_PULSE_PORT2_12V),
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
	(uint8_t*)(&app_regs.REG_EVNT_ENABLE),
	(uint8_t*)(&app_regs.REG_START_CAMERAS),
	(uint8_t*)(&app_regs.REG_STOP_CAMERAS),
	(uint8_t*)(&app_regs.REG_EN_SERVOS),
	(uint8_t*)(&app_regs.REG_DIS_SERVOS),
	(uint8_t*)(&app_regs.REG_EN_ENCODERS),
	(uint8_t*)(&app_regs.REG_RESERVED1),
	(uint8_t*)(&app_regs.REG_RESERVED2),
	(uint8_t*)(&app_regs.REG_RESERVED3),
	(uint8_t*)(&app_regs.REG_RESERVED4),
	(uint8_t*)(&app_regs.REG_RESERVED5),
	(uint8_t*)(&app_regs.REG_RESERVED6),
	(uint8_t*)(&app_regs.REG_RESERVED7),
	(uint8_t*)(&app_regs.REG_RESERVED8),
	(uint8_t*)(&app_regs.REG_RESERVED9),
	(uint8_t*)(&app_regs.REG_CAM_OUT0_FRAME_ACQUIRED),
	(uint8_t*)(&app_regs.REG_CAM_OUT0_FREQ),
	(uint8_t*)(&app_regs.REG_CAM_OUT1_FRAME_ACQUIRED),
	(uint8_t*)(&app_regs.REG_CAM_OUT1_FREQ),
	(uint8_t*)(&app_regs.REG_RESERVED10),
	(uint8_t*)(&app_regs.REG_RESERVED11),
	(uint8_t*)(&app_regs.REG_RESERVED12),
	(uint8_t*)(&app_regs.REG_RESERVED13),
	(uint8_t*)(&app_regs.REG_MOTOR_OUT2_PERIOD),
	(uint8_t*)(&app_regs.REG_MOTOR_OUT2_PULSE),
	(uint8_t*)(&app_regs.REG_MOTOR_OUT3_PERIOD),
	(uint8_t*)(&app_regs.REG_MOTOR_OUT3_PULSE),
	(uint8_t*)(&app_regs.REG_RESERVED14),
	(uint8_t*)(&app_regs.REG_RESERVED15),
	(uint8_t*)(&app_regs.REG_RESERVED16),
	(uint8_t*)(&app_regs.REG_RESERVED17),
	(uint8_t*)(&app_regs.REG_ENCODERS_RESET),
	(uint8_t*)(&app_regs.REG_RESERVED18),
	(uint8_t*)(&app_regs.REG_RESERVED19),
	(uint8_t*)(&app_regs.REG_MIMIC_PORT0_IR),
	(uint8_t*)(&app_regs.REG_MIMIC_PORT1_IR),
	(uint8_t*)(&app_regs.REG_MIMIC_PORT2_IR),
	(uint8_t*)(&app_regs.REG_RESERVED20),
	(uint8_t*)(&app_regs.REG_RESERVED21),
	(uint8_t*)(&app_regs.REG_RESERVED22),
	(uint8_t*)(&app_regs.REG_MIMIC_PORT0_VALVE),
	(uint8_t*)(&app_regs.REG_MIMIC_PORT1_VALVE),
	(uint8_t*)(&app_regs.REG_MIMIC_PORT2_VALVE),
	(uint8_t*)(&app_regs.REG_RESERVED23),
	(uint8_t*)(&app_regs.REG_RESERVED24),
	(uint8_t*)(&app_regs.REG_POKE_INPUT_FILTER_MS)
};