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
extern uint8_t int0_enable_counter;

ISR(PORTD_INT0_vect, ISR_NAKED)
{	
   uint8_t reg_port_dis = app_regs.REG_PORT_DIS;
   uint8_t reg_port_dios_in = app_regs.REG_PORT_DIOS_IN; 
   
   app_regs.REG_PORT_DIS &= ~B_DI0;
   app_regs.REG_PORT_DIS |= (read_POKE0_IR) ? B_DI0 : 0;
		
   app_regs.REG_PORT_DIOS_IN &= ~B_DIO0;                      
   app_regs.REG_PORT_DIOS_IN |= (read_POKE0_IO) ? B_DIO0 : 0; 
	
	   
   if(read_POKE0_IR)
      mimic_ir_or_valve(app_regs.REG_MIMIC_PORT0_IR, _SET_IO_);
   else
      mimic_ir_or_valve(app_regs.REG_MIMIC_PORT0_IR, _CLR_IO_);

	if (app_regs.REG_EVNT_ENABLE & B_EVT_PORT_DIS)
	{
		if (reg_port_dis != app_regs.REG_PORT_DIS)
		{
			core_func_send_event(ADD_REG_PORT_DIS, true);
		 
			if (app_regs.REG_POKE_INPUT_FILTER_MS)
			{
				PORTD_INTCTRL &= 0xFC;	// Disable interrupt
				int0_enable_counter = app_regs.REG_POKE_INPUT_FILTER_MS;
			}
		}
		
		if (reg_port_dios_in != app_regs.REG_PORT_DIOS_IN) 
		{
			core_func_send_event(ADD_REG_PORT_DIOS_IN, true); 
		}
	}

	reti();
}

/************************************************************************/ 
/* POKE1_IR                                                             */
/************************************************************************/
extern uint8_t int1_enable_counter;

ISR(PORTE_INT0_vect, ISR_NAKED)
{
   uint8_t reg_port_dis = app_regs.REG_PORT_DIS;
   uint8_t reg_port_dios_in = app_regs.REG_PORT_DIOS_IN; 
   
   app_regs.REG_PORT_DIS &= ~B_DI1;
   app_regs.REG_PORT_DIS |= (read_POKE1_IR) ? B_DI1 : 0;
	
   app_regs.REG_PORT_DIOS_IN &= ~B_DIO1;                       
   app_regs.REG_PORT_DIOS_IN |= (read_POKE1_IO) ? B_DIO1 : 0; 
   
   if(read_POKE1_IR)
      mimic_ir_or_valve(app_regs.REG_MIMIC_PORT1_IR, _SET_IO_);
   else
      mimic_ir_or_valve(app_regs.REG_MIMIC_PORT1_IR, _CLR_IO_);

	if (app_regs.REG_EVNT_ENABLE & B_EVT_PORT_DIS)
	{
   		if (reg_port_dis != app_regs.REG_PORT_DIS)
   		{
      		core_func_send_event(ADD_REG_PORT_DIS, true);
			  
      		if (app_regs.REG_POKE_INPUT_FILTER_MS)
      		{
	      		PORTE_INTCTRL &= 0xFC;	// Disable interrupt
      			int1_enable_counter = app_regs.REG_POKE_INPUT_FILTER_MS;
			}
   		}
	
		if (reg_port_dios_in != app_regs.REG_PORT_DIOS_IN) 
		{
			core_func_send_event(ADD_REG_PORT_DIOS_IN, true); 
		}
	}

	reti();
}

/************************************************************************/ 
/* POKE2_IR                                                             */
/************************************************************************/
extern uint8_t int2_enable_counter;

ISR(PORTF_INT0_vect, ISR_NAKED)
{
   uint8_t reg_port_dis = app_regs.REG_PORT_DIS;
   uint8_t reg_port_dios_in = app_regs.REG_PORT_DIOS_IN; 
   
   app_regs.REG_PORT_DIS &= ~B_DI2;
   app_regs.REG_PORT_DIS |= (read_POKE2_IR) ? B_DI2 : 0;
	
   app_regs.REG_PORT_DIOS_IN &= ~B_DIO2;                       
   app_regs.REG_PORT_DIOS_IN |= (read_POKE2_IO) ? B_DIO2 : 0;
      
   if(read_POKE2_IR)
      mimic_ir_or_valve(app_regs.REG_MIMIC_PORT2_IR, _SET_IO_);
   else
      mimic_ir_or_valve(app_regs.REG_MIMIC_PORT2_IR, _CLR_IO_);
   
	if (app_regs.REG_EVNT_ENABLE & B_EVT_PORT_DIS)
	{
   		if (reg_port_dis != app_regs.REG_PORT_DIS)
   		{
      		core_func_send_event(ADD_REG_PORT_DIS, true);
		  
      		if (app_regs.REG_POKE_INPUT_FILTER_MS)
      		{
	      		PORTF_INTCTRL &= 0xFC;	// Disable interrupt
      			int2_enable_counter = app_regs.REG_POKE_INPUT_FILTER_MS;
			  }
   		}
		if (reg_port_dios_in != app_regs.REG_PORT_DIOS_IN)
		{
			core_func_send_event(ADD_REG_PORT_DIOS_IN, true);
		}
	}

	reti();
}

/************************************************************************/
/* DI3                                                                  */
/************************************************************************/
ISR(PORTH_INT0_vect, ISR_NAKED)
{
	uint8_t reg_port_dis = app_regs.REG_PORT_DIS;
	
	app_regs.REG_PORT_DIS &= ~B_DI3;
	app_regs.REG_PORT_DIS |= (read_DI3) ? B_DI3 : 0;
	
	if (app_regs.REG_EVNT_ENABLE & B_EVT_PORT_DIS)
	{
		if (reg_port_dis != app_regs.REG_PORT_DIS)
		{
			core_func_send_event(ADD_REG_PORT_DIS, true);
		}
	}

	reti();
}

/************************************************************************/
/* PWM DOx                                                              */
/************************************************************************/
timer_conf_t timer_conf;
is_new_timer_conf_t is_new_timer_conf;

extern ports_state_t _states_;

extern bool stop_camera_do0;
extern bool stop_camera_do1;

ISR(TCF0_OVF_vect, ISR_NAKED)
{
    if (_states_.pwm.do0)
    {
        if (is_new_timer_conf.pwm_do0)
        {
            TCF0_PER = timer_conf.target_do0 - 1;
            TCF0_CCA = timer_conf.dcycle_do0;
            TCF0_CTRLA = timer_conf.prescaler_do0;
            is_new_timer_conf.pwm_do0 = false;
        }
    }        
    
    if (_states_.camera.do0)
    {
        if (app_regs.REG_EVNT_ENABLE & B_EVT_CAM0)
        {
            app_regs.REG_CAM_OUT0_FRAME_ACQUIRED = 1;
            core_func_send_event(ADD_REG_CAM_OUT0_FRAME_ACQUIRED, true);
        }
    }
    
    reti();
}

ISR(TCF0_CCA_vect, ISR_NAKED)
{
    if (_states_.camera.do0)
    {
        if (stop_camera_do0)
        {
            stop_camera_do0 = false;
        
            clr_DO0;
            timer_type0_stop(&TCF0);
            _states_.camera.do0 = false;
				
				app_regs.REG_STOP_CAMERAS = B_EN_CAM_OUT0;
				core_func_send_event(ADD_REG_STOP_CAMERAS, true);
        }
    }        
    
    reti();
}

ISR(TCE0_OVF_vect, ISR_NAKED)
{
    if (_states_.pwm.do1)
    {
        if (is_new_timer_conf.pwm_do1)
        {
            TCE0_PER = timer_conf.target_do1 - 1;
            TCE0_CCA = timer_conf.dcycle_do1;
            TCE0_CTRLA = timer_conf.prescaler_do1;
            is_new_timer_conf.pwm_do1 = false;
        }
    }     
          
    if (_states_.camera.do1)
    {
        if (app_regs.REG_EVNT_ENABLE & B_EVT_CAM1)
        {
            app_regs.REG_CAM_OUT1_FRAME_ACQUIRED = 1;
            core_func_send_event(ADD_REG_CAM_OUT1_FRAME_ACQUIRED, true);
        }
    }       
    
    reti();
}

ISR(TCE0_CCA_vect, ISR_NAKED)
{
    if (_states_.camera.do1)
    {
        if (stop_camera_do1)
        {
            stop_camera_do1 = false;
        
            clr_DO1;
            timer_type0_stop(&TCE0);
            _states_.camera.do1 = false;
            
            app_regs.REG_STOP_CAMERAS = B_EN_CAM_OUT1;
            core_func_send_event(ADD_REG_STOP_CAMERAS, true);
        }
    }        
    
    reti();
}

ISR(TCD0_OVF_vect, ISR_NAKED)
{
    if (_states_.pwm.do2)
    {
        if (is_new_timer_conf.pwm_do2)
        {
            TCD0_PER = timer_conf.target_do2 - 1;
            TCD0_CCA = timer_conf.dcycle_do2;
            TCD0_CTRLA = timer_conf.prescaler_do2;
            is_new_timer_conf.pwm_do2 = false;
        }
    }        
    
    reti();
}

ISR(TCC0_OVF_vect, ISR_NAKED)
{
    if (_states_.pwm.do3)
    {
        if (is_new_timer_conf.pwm_do3)
        {
            TCC0_PER = timer_conf.target_do3 - 1;
            TCC0_CCA = timer_conf.dcycle_do3;
            TCC0_CTRLA = timer_conf.prescaler_do3;
            is_new_timer_conf.pwm_do3 = false;
        }
    }
        
    reti();
}

/************************************************************************/
/* ADC                                                                  */
/************************************************************************/
extern int16_t AdcOffset;

extern bool first_adc_channel;

ISR(ADCA_CH0_vect, ISR_NAKED)
{
	bool send_event = false;
	
	if (first_adc_channel)
	{
		first_adc_channel = false;
		
		/* Read ADC0 Channel 0 */
		app_regs.REG_DATA[0] = ((int16_t)(ADCA_CH0_RES & 0x0FFF)) - AdcOffset;
		
		if (read_ADC1_AVAILABLE)
		{
			/* Start conversation on ADCA Channel 2*/
			ADCA_CH0_MUXCTRL = 2 << 3;
			ADCA_CH0_CTRL |= ADC_CH_START_bm;
		}
		else
		{
			send_event = true;
		}
	}
	else
	{		
		/* Read ADC0 Channel 2 */
		app_regs.REG_DATA[2] = ((int16_t)(ADCA_CH0_RES & 0x0FFF)) - AdcOffset;
		
		/* Validate readings */
		if (app_regs.REG_DATA[0] < 0)
			app_regs.REG_DATA[0] = 0;			
		if (app_regs.REG_DATA[2] < 0)
			app_regs.REG_DATA[2] = 0;
			
		send_event = true;
	}
	
	if (send_event)
	{
		if (app_regs.REG_EVNT_ENABLE & B_EVT_DATA)
		{
			core_func_send_event(ADD_REG_DATA, false);
		}
	}	
		
	reti();
}