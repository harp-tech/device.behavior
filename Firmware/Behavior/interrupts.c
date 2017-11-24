#include "cpu.h"
#include "hwbp_core_types.h"
#include "app_ios_and_regs.h"
#include "app_funcs.h"
#include "hwbp_core.h"

#include "structs.h"

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

/************************************************************************/
/* PWM DOx                                                              */
/************************************************************************/
timer_conf_t timer_conf;
is_new_timer_conf_t is_new_timer_conf;

ISR(TCF0_OVF_vect, ISR_NAKED)
{
    if (is_new_timer_conf.pwm_do0)
    {
        TCF0_PER = timer_conf.target_do0;
        TCF0_CCA = timer_conf.dcycle_do0;
        TCF0_CTRLA = timer_conf.prescaler_do0;
        is_new_timer_conf.pwm_do0 = false;
    }
    
    reti();
}

ISR(TCE0_OVF_vect, ISR_NAKED)
{
    if (is_new_timer_conf.pwm_do1)
    {
        TCE0_PER = timer_conf.target_do1;
        TCE0_CCA = timer_conf.dcycle_do1;
        TCE0_CTRLA = timer_conf.prescaler_do1;
        is_new_timer_conf.pwm_do1 = false;
    }
    
    reti();
}

ISR(TCD0_OVF_vect, ISR_NAKED)
{
    if (is_new_timer_conf.pwm_do2)
    {
        TCD0_PER = timer_conf.target_do2;
        TCD0_CCA = timer_conf.dcycle_do2;
        TCD0_CTRLA = timer_conf.prescaler_do2;
        is_new_timer_conf.pwm_do2 = false;
    }
    
    reti();
}

ISR(TCC0_OVF_vect, ISR_NAKED)
{
    if (is_new_timer_conf.pwm_do3)
    {
        TCC0_PER = timer_conf.target_do3;
        TCC0_CCA = timer_conf.dcycle_do3;
        TCC0_CTRLA = timer_conf.prescaler_do3;
        is_new_timer_conf.pwm_do3 = false;
    }
    
    reti();
}