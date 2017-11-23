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
/* POKE0_IN                                                             */
/************************************************************************/
ISR(PORTD_INT0_vect, ISR_NAKED)
{
	reti();
}

/************************************************************************/ 
/* POKE1_IN                                                             */
/************************************************************************/
ISR(PORTE_INT0_vect, ISR_NAKED)
{
	reti();
}

/************************************************************************/ 
/* POKE2_IN                                                             */
/************************************************************************/
ISR(PORTF_INT0_vect, ISR_NAKED)
{
	reti();
}

