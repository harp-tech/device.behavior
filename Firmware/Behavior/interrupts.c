#include "cpu.h"
#include "hwbp_core_types.h"
#include "app_ios_and_regs.h"
#include "app_funcs.h"
#include "hwbp_core.h"

/************************************************************************/
/* Declare application registers                                        */
/************************************************************************/
extern AppRegs app_regs;

/************************************************************************/
/* Interrupts from Timers                                               */
/************************************************************************/
// ISR(TCC0_OVF_vect, ISR_NAKED)
// ISR(TCD0_OVF_vect, ISR_NAKED)
// ISR(TCE0_OVF_vect, ISR_NAKED)
// ISR(TCF0_OVF_vect, ISR_NAKED)
// 
// ISR(TCC0_CCA_vect, ISR_NAKED)
// ISR(TCD0_CCA_vect, ISR_NAKED)
// ISR(TCE0_CCA_vect, ISR_NAKED)
// ISR(TCF0_CCA_vect, ISR_NAKED)
// 
// ISR(TCD1_OVF_vect, ISR_NAKED)
// 
// ISR(TCD1_CCA_vect, ISR_NAKED)

/************************************************************************/ 
/* POKE0_IR                                                             */
/************************************************************************/
ISR(PORTD_INT0_vect, ISR_NAKED)
{	
	app_regs.REG_POKE_IN &= ~B_IR0;
	app_regs.REG_POKE_IN |= (read_POKE0_IR) ? B_IR0 : 0;

	if (app_regs.REG_EVNT_ENABLE & B_EVT_POKE_IN)
	{
		core_func_send_event(ADD_REG_POKE_IN, true);
	}

	reti();
}

/************************************************************************/ 
/* POKE1_IR                                                             */
/************************************************************************/
ISR(PORTE_INT0_vect, ISR_NAKED)
{
	app_regs.REG_POKE_IN &= ~B_IR1;
	app_regs.REG_POKE_IN |= (read_POKE1_IR) ? B_IR1 : 0;

	if (app_regs.REG_EVNT_ENABLE & B_EVT_POKE_IN)
	{
		core_func_send_event(ADD_REG_POKE_IN, true);
	}

	reti();
}

/************************************************************************/ 
/* POKE2_IR                                                             */
/************************************************************************/
ISR(PORTF_INT0_vect, ISR_NAKED)
{
	app_regs.REG_POKE_IN &= ~B_IR2;
	app_regs.REG_POKE_IN |= (read_POKE2_IR) ? B_IR2 : 0;

	if (app_regs.REG_EVNT_ENABLE & B_EVT_POKE_IN)
	{
		core_func_send_event(ADD_REG_POKE_IN, true);
	}

	reti();
}

